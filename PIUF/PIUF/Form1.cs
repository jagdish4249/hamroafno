using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZKFPEngXControl;

namespace PIUF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            fingerpriCapture();
        }

        public void fingerpriCapture()
        {
            ZKFPEngX device = new ZKFPEngX();
            //device.InitEngine();
            if (device.InitEngine() == 0)
                // MessageBox.Show("Connected");
                // Debug.WriteLine("Connected");

                device.BeginCapture();

            device.VerTplFileName = "image.jpg";


            device.OnCapture += delegate (bool ActionResult, object ATemplate)
            {
                int i=0;
                Console.Beep(5000, 100);////play beep sound when fingerprint is scanned
                //i = i + 1;
                device.SaveJPG("D:\\8th project reference\\PIUF\\images\\user_" + i + ".jpg");
               
                
                device.SaveJPG("D:\\8th project reference\\PIUF\\images\\WorkingPicture\\temp.jpg");
                pictureBox1.Image = Image.FromFile("D:\\8th project reference\\PIUF\\images\\user_" + i + ".jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //GetSampleIDFromSRN smp = new GetSampleIDFromSRN();
                //smp.Show();
                MessageBox.Show("Add Your Code", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                


                //    if (!backgroundWorker1.IsBusy)
                //{

                //    backgroundWorker1.RunWorkerAsync();

                //}


            };


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
