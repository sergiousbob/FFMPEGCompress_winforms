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
            comboBox1.SelectedItem = "worst";
            comboBox2.SelectedItem = "avi";
            comboBox3.SelectedItem = "avi";
            comboBox4.SelectedItem = "worst";
            this.BackColor = Color.Aquamarine;
        }










        void asyncBntObrFile()   ///обработка файла 
        {
            int a = 1;
            button1.Enabled = false;

            if (comboBox4.SelectedItem == "lossless")
            {
                crf_video = 17;


            }
            if (comboBox4.SelectedItem == "default")
            {
                crf_video = 23;


            }
            if (comboBox4.SelectedItem == "worst")
            {
                crf_video = 28;


            }



            string FilePath = $"{textBox4.Text}";
            string directoryOutFile = $@"{textBox3.Text}";
      


          
            
                Process process = new Process();
                process.StartInfo.FileName = "ffmpeg";





                string naimen = $@"{textBox3.Text}\VideoConvert{a}";


                process.StartInfo.Arguments = "chcp 65001";
                process.StartInfo.Arguments = $"-i {FilePath} -vcodec libx265 -crf {crf_video} -preset ultrafast {naimen}.mp4";
                a++;



                process.StartInfo.UseShellExecute = false;


                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();



                string output = process.StandardOutput.ReadToEnd();
                MessageBox.Show("All videos have been processed!!!");
                button1.Enabled = true;
        }



















        void asyncBtn()    ////обработка папки c видео
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



                process.StartInfo.Arguments = $"-i {aviFile} -vcodec libx265 -crf {crf_video} -preset faster {naimen}.mp4";
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



        private async void button1_Click_1(object sender, EventArgs e)   ///кнопка сжатия папки
        {
            await Task.Run(() => asyncBtn());
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите путь к папке" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {

                    textBox1.Text = fbd.SelectedPath;
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите путь к папке" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {

                    textBox2.Text = fbd.SelectedPath;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)   ///путь до одного файла
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = ofd.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e) ////папка назначения для файла
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите путь к папке" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {

                    textBox3.Text = fbd.SelectedPath;
                }
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await Task.Run(() => asyncBntObrFile());
        }
    }
}
