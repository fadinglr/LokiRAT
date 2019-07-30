using common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LokiRAT
{
    public class screenshot : Form
    {
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private Image img;
        private ToolStripMenuItem newScreenshotToolStripMenuItem;
        private PictureBox pictureBox1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(screenshot));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScreenshotToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 48);
            // 
            // newScreenshotToolStripMenuItem
            // 
            this.newScreenshotToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newScreenshotToolStripMenuItem.Image")));
            this.newScreenshotToolStripMenuItem.Name = "newScreenshotToolStripMenuItem";
            this.newScreenshotToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.newScreenshotToolStripMenuItem.Text = "New Screenshot";
            this.newScreenshotToolStripMenuItem.Click += new System.EventHandler(this.newScreenshotToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(741, 476);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // screenshot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 476);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "screenshot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screenshot";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        string id;
        string myLastCommand = "-1";

        public screenshot(string id)
        {
            this.InitializeComponent();
            this.id = id;
        }

        private void newScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.AddCommandToList(id, "screen");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.img.Save(sfd.FileName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Global.lastCommand[id] != this.myLastCommand)
                {
                    this.myLastCommand = Global.lastCommand[id];
                    string[] rets = Regex.Split(Global.GetLastCommandText(id), "{-}");
                    if (rets[0] == "LSscreen")
                    {
                        MemoryStream stream = new MemoryStream(gzip.Decompress(utils.GetStringToBytes(rets[1])));
                        this.img = Image.FromStream(stream);
                        Bitmap image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
                        Graphics graphics = Graphics.FromImage(image);
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(this.img, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
                        graphics.Dispose();
                        this.pictureBox1.Image = image;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}