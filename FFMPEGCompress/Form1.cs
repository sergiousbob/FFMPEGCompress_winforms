using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FFMPEGCompress
{
    public partial class Form1 : Form
    {
        int crf_video;
        static Form1 form1;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "lossless";
            comboBox2.SelectedItem = "avi";
            this.BackColor = Color.Aquamarine;
        }




        void asyncBtn()
        {
            int a = 1;
            button1.Enabled = false;

            if (comboBox1.SelectedItem == "lossless")
            {
                crf_video = 17;
        

            }
            if (comboBox1.SelectedItem == "default")
            {
                crf_video = 23;
        

            }
            if (comboBox1.SelectedItem == "worst")
            {
                crf_video = 28;
        

            }



            string directoryPath = $"{textBox1.Text}";
            string directoryOut = $@"{textBox2.Text}";
            string[] aviFiles = Directory.GetFiles(directoryPath, $"*.{comboBox2.Text}");
  

            foreach (string aviFile in aviFiles)
            {
                Process process = new Process();
                process.StartInfo.FileName = "ffmpeg";



               

                string naimen = $@"{textBox2.Text}\VideoConvert{a}";
         


                process.StartInfo.Arguments = $"-i {aviFile} -vcodec libx265 -crf {crf_video} -preset ultrafast {naimen}.mp4";
                a++;



                process.StartInfo.UseShellExecute = false;


                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();



                string output = process.StandardOutput.ReadToEnd();
 
            }
            MessageBox.Show("All videos have been processed!!!");
            button1.Enabled = true;
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() => asyncBtn());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите путь к папке" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {

                    textBox1.Text = fbd.SelectedPath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите путь к папке" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {

                    textBox2.Text = fbd.SelectedPath;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure wanna be exit ?", "Exit Application", MessageBoxButtons.YesNoCancel);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                
                Process[] p = Process.GetProcessesByName("ffmpeg");
                if (p.Length > 0) p[0].Kill();
                Process[] x = Process.GetProcessesByName("FFMPEGCompress");
                if (x.Length > 0) x[0].Kill();
            }
            else
            {

                e.Cancel = true;

            }


     


        }
    }
}
