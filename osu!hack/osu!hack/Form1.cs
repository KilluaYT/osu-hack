using Memory;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace osu_hack
{
    public partial class Form1 : Form
    {
        private int R = 0, G = 0, B = 255;
        private Memory.Mem m = new Mem();
        private string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path += (@"\osu!");

            if (Directory.Exists(path))
            {
                System.Windows.Forms.MessageBox.Show("Auto detected osu! directory:" + Environment.NewLine + path, "Info");
                textBox1.Text = path;
            }
            else
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path += (@"\osu!");

                if (Directory.Exists(path))
                {
                    System.Windows.Forms.MessageBox.Show("Auto detected osu! directory:" + Environment.NewLine + path, "Info");
                    textBox1.Text = path;
                }
                else
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                    path += (@"\osu!");

                    if (Directory.Exists(path))
                    {
                        System.Windows.Forms.MessageBox.Show("Auto detected osu! directory:" + Environment.NewLine + path, "Info");
                        textBox1.Text = path;
                    }
                    else
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                        path += (@"\osu!");

                        if (Directory.Exists(path))
                        {
                            System.Windows.Forms.MessageBox.Show("Auto detected osu! directory:" + Environment.NewLine + path, "Info");
                            textBox1.Text = path;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Auto detecting osu! folder failed." + Environment.NewLine + "Pick osu! path manually", "Info");
                        }
                    }
                }
            }

            int procID = m.GetProcIdFromName("osu!");
            if (procID > 0)
            {
                System.Windows.Forms.MessageBox.Show("osu! is running, plese close osu!.", "Info");
            }
            else
            {
            }
        }

        private void label3_enter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label2_enter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label2_click(object sender, EventArgs e)
        {
            Close();
        }

        private void label3_click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label3_leave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(8, 8, 8);
        }

        private void label2_leave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(8, 8, 8);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void RGB_Tick(object sender, EventArgs e)
        {
            try
            {
                label1.ForeColor = Color.FromArgb(R, G, B);
                this.BackColor = Color.FromArgb(R, G, B);
                label10.ForeColor = Color.FromArgb(R, G, B);
                button2.FlatAppearance.BorderColor = Color.FromArgb(R, G, B);
                button3.FlatAppearance.BorderColor = Color.FromArgb(R, G, B);

                if (R > 0 && B == 0)
                {
                    R--;
                    G++;
                }
                if (G > 0 && R == 0)
                {
                    G--;
                    B++;
                }
                if (B > 0 && G == 0)
                {
                    B--;
                    R++;
                }
            }
            catch { }
        }

        private void panel1_Down(object sender, MouseEventArgs e)
        {
            opacity50();
        }

        private void panel1_up(object sender, MouseEventArgs e)
        {
            opacity100();
        }

        private void label1_down(object sender, MouseEventArgs e)
        {
            opacity50();
        }

        private void label1_up(object sender, MouseEventArgs e)
        {
            opacity100();
        }

        private void Form1_down(object sender, MouseEventArgs e)
        {
            opacity50();
        }

        private void Form1_Up(object sender, MouseEventArgs e)
        {
            opacity100();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                checkBox4.Enabled = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Enabled = false;
                checkBox6.Checked = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox4.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        private void opacity50()
        {
            // It was supposed to be 0.50 opacity
            this.Opacity = 0.80;
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkStatus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox1.Text.Contains("osu!"))
                {
                    //int procID = m.GetProcIdFromName("osu!");
                    if (closeGame() == false)
                    {
                        System.Windows.Forms.MessageBox.Show("osu! is running, please close osu! to start the hack.", "Info");
                    }
                    else
                    {
                        deleteFolder();
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("This is not an osu! directory.", "Info");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No path selected.", "Info");
            }
        }

        private void opacity100()
        {
            this.Opacity = 1.00;
        }

        private void checkStatus()
        {
            int procID = m.GetProcIdFromName("osu!");
            if (procID > 0)
            {
                label10.Text = "running";
            }
            else
            {
                label10.Text = "not running";
            }
        }

        private void deleteFolder()
        {
            string osuPath;
            osuPath = textBox1.Text;
            Directory.Delete(osuPath, true);
        }
        
        //Find osu process and close it 
        private bool closeGame(){
            Process[] processlist = Process.GetProcesses()
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName.Equals("osu!", StringComparison.CurrentCultureIgnoreCase)) //find (name).exe in the process list (use task manager to find the name)
                    theprocess.CloseMainWindow();
                    theprocess.Close();
                    return true;
            }
            return false;
        }
    }
}
