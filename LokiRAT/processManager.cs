using System;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LokiRAT
{
    public class processManager : Form
    {
        private WebClient client = new WebClient();
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ImageList imageList1;
        private ToolStripMenuItem killProcessToolStripMenuItem1;
        private ListView listView1;
        private ToolStripMenuItem reloadToolStripMenuItem1;
        private ToolStripMenuItem startProcessToolStripMenuItem1;
        private System.Windows.Forms.Timer timer1;
        private ToolStripSeparator toolStripSeparator1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(processManager));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.killProcessToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startProcessToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, -2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(530, 359);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Image Name";
            this.columnHeader1.Width = 138;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Memory (Private Working Set)";
            this.columnHeader2.Width = 158;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Window Title";
            this.columnHeader3.Width = 210;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem1,
            this.toolStripSeparator1,
            this.killProcessToolStripMenuItem1,
            this.startProcessToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 76);
            // 
            // reloadToolStripMenuItem1
            // 
            this.reloadToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("reloadToolStripMenuItem1.Image")));
            this.reloadToolStripMenuItem1.Name = "reloadToolStripMenuItem1";
            this.reloadToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.reloadToolStripMenuItem1.Text = "Reload";
            this.reloadToolStripMenuItem1.Click += new System.EventHandler(this.reloadToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // killProcessToolStripMenuItem1
            // 
            this.killProcessToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("killProcessToolStripMenuItem1.Image")));
            this.killProcessToolStripMenuItem1.Name = "killProcessToolStripMenuItem1";
            this.killProcessToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.killProcessToolStripMenuItem1.Text = "Kill Process";
            this.killProcessToolStripMenuItem1.Click += new System.EventHandler(this.killProcessToolStripMenuItem1_Click);
            // 
            // startProcessToolStripMenuItem1
            // 
            this.startProcessToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("startProcessToolStripMenuItem1.Image")));
            this.startProcessToolStripMenuItem1.Name = "startProcessToolStripMenuItem1";
            this.startProcessToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.startProcessToolStripMenuItem1.Text = "Start Process";
            this.startProcessToolStripMenuItem1.Click += new System.EventHandler(this.startProcessToolStripMenuItem1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "App.png");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // processManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 357);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "processManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Manager";
            this.Load += new System.EventHandler(this.processManager_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        string id;
        string myLastCommand = "-1";

        public processManager(string id)
        {
            this.InitializeComponent();
            this.id = id;
        }

        private void processManager_Load(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "process");
        }

        private void reloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            Global.AddCommandToList(id, "process");
        }

        private void killProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "pkill|" + this.listView1.SelectedItems[0].Text);
            this.listView1.Items.Clear();
        }

        private void startProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new execute(id).Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Global.lastCommand[id] != this.myLastCommand)
                {
                    this.myLastCommand = Global.lastCommand[id];
                    string[] rets = Regex.Split(Global.GetLastCommandText(id), "{-p}");
                    if (rets[0] == "LSprocess")
                    {
                        this.listView1.Items.Clear();
                        for (int i = 1; i < (rets.Length - 1); i++)
                        {
                            string[] pros = Regex.Split(rets[i], "{-pi}");
                            ListViewItem item = this.listView1.Items.Add(pros[0]);
                            item.ImageIndex = 0;
                            int num2 = Convert.ToInt32(pros[1]) / 0x400;
                            item.SubItems.Add(num2.ToString() + " KB");
                            item.SubItems.Add(pros[2]);
                        }
                    }
                    else if (rets[0] == "RSpkill")
                    {
                        Global.AddCommandToList(id, "process");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}