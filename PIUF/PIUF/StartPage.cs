using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZKFPEngXControl;
using System.Threading;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Media;
namespace SwiftEntry2
{
    public partial class StartPage : Form
    {
      //  public int sample_num;
//        public int sample_num;
        private GifImage gifImage = null;
        private string filePath = @"D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Resources\\giphy.gif";
        public static int i = 1;

        public StartPage()
        {
            
            InitializeComponent();
            //b) We control the animation
            gifImage = new GifImage(filePath);
            gifImage.ReverseAtEnd = false; //dont reverse at end
            picGif.ImageLocation = "D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Resources\\Finger.gif";
            picGif.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.DayOfWeek;
            //picGif.Image = gifImage.GetNextFrame();
            //picGif.SizeMode = PictureBoxSizeMode.StretchImage;
            //        refreshScanner();
        }


        /// <summary>
        /// //////////////////////////btn to login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginPage l = new LoginPage();
            l.Show();
            this.Hide();
        }
        /// <summary>
        /// //////////Fingerprint scan process 
        /// scanner connected
        /// fingerprint scanned 
        /// saved in the folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void StartPage_Load(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;     
           // fingerpriCapture();
            
            
            fingerpriCapture();

        }


        /// <summary>
        /// //////////function to capture fingerprint
        /// </summary>
        public void fingerpriCapture() {
            ZKFPEngX device = new ZKFPEngX();
            //device.InitEngine();
            if (device.InitEngine() == 0)
                // MessageBox.Show("Connected");
                // Debug.WriteLine("Connected");

                device.BeginCapture();

            device.VerTplFileName = "image.jpg";

           
            device.OnCapture += delegate(bool ActionResult, object ATemplate)
            {
                Console.Beep(5000, 100);////play beep sound when fingerprint is scanned
                i = i + 1;
                device.SaveJPG("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Scanimage\\user_" + i + ".jpg");
                device.SaveJPG("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\WorkingAttendanePicture\\temp.jpg");
                pictureBox2.Image = Image.FromFile("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Scanimage\\user_" + i + ".jpg");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                //GetSampleIDFromSRN smp = new GetSampleIDFromSRN();
                //smp.Show();
                MessageBox.Show("Add Your Code","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtSrn.Focus();
                
                
                //    if (!backgroundWorker1.IsBusy)
                //{

                //    backgroundWorker1.RunWorkerAsync();

                //}
                

            };

        
        }

        public void fingerCaptureVitra() {
            
                ZKFPEngX device = new ZKFPEngX();
                //device.InitEngine();
                if (device.InitEngine() == 0)
                    // MessageBox.Show("Connected");
                    // Debug.WriteLine("Connected");

                    device.BeginCapture();

                device.VerTplFileName = "image.jpg";


                device.OnCapture += delegate(bool ActionResult, object ATemplate)
                {
                    Console.Beep(5000, 100);////play beep sound when fingerprint is scanned
                    i = i + 1;
                    device.SaveJPG("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Scanimage\\user_" + i + ".jpg");
                    device.SaveJPG("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\WorkingAttendanePicture\\temp.jpg");
                    pictureBox2.Image = Image.FromFile("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Scanimage\\user_" + i + ".jpg");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (!backgroundWorker1.IsBusy)
                    {
                        
                        
                            backgroundWorker1.RunWorkerAsync();
                        
                    }
                };

            
        }
        /// <summary>
        /// ///////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lnlEmpDepartment_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// //////////To delete all the image from buffer while page closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var folderPath = Server.MapPath("~/Images/");
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            ScanDelete sdd = new ScanDelete();
            sdd.deleteAll("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Scanimage");
        }
        /// <summary>
        /// ////////background ma attendance mark garchha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (txtSrn.Text == "")
            //{
            //    //MessageBox.Show("Enter  your code then Place your finger!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //pictureBox2.Image = Image.FromFile("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Resources\\images.jpg");

            //}
            //else
            //{
            FristPage fp = new FristPage();
            int empid = fp.empidFromSrn(txtSrn.Text.ToString());
            IdForFingerprintUpdate idd = new IdForFingerprintUpdate();
            int sample_num = idd.sampleIDofuser(empid);
            // MessageBox.Show(sample_num.ToString());

                MLApp.MLApp matlab = new MLApp.MLApp();
                try
                {

                    int id1;
                    //, id2;
                    // matlab.Execute(@"cd D:\VBlab\MatlabIntegration");
                    matlab.Execute(@"cd D:\VBlab\SwiftEntry2\ImageProcessing");

                    string img = "D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\WorkingAttendanePicture\\temp.jpg";
                    // string img = "D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Scanimage\\user_" + j + ".jpg";
                    // Define the output 
                    object result = null;
                    
                    // Call the MATLAB function myfunc
                    matlab.Feval("matchfinger", 1, out result, img, sample_num);
                                  
                    object[] res = result as object[];
                    id1 = Convert.ToInt16(res[0]);
                    int id2 = calculateEmployeeId(id1);
                    //label1.Visible = true;
                    markAttendance(id2);
                    //addSample(id1, id2);
                    
                    //lblProgress.Text = res[0].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No match found", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            //}
        }



        /// <summary>
        /// //////////////function to view employees name
        /// </summary>
        /// <param name="employeeid">id of employee to be marked attendance</param>
        private void viewEmployeeName(int employeeid)
        {

            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "swiftentry_db";
            if (dbCon.IsConnect())
            {
                try
                {

                    string sqlQuery = "SELECT * FROM `employee` WHERE employee_id='" + employeeid + "'";
                    //MessageBox.Show(query);

                    MySqlCommand cmd = new MySqlCommand(sqlQuery, dbCon.Connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        //return dr["fname"].ToString();
                        //lblDate.Text=dr["fname"].ToString();
                       MessageBox.Show(dr["fname"].ToString()+" "+dr["mname"]+" "+dr["lname"],"Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                       // MessageBox.Show("Manita Bhadra Dahal");
                        // ViewIdentifiedEmployee bbb = new ViewIdentifiedEmployee(dr["fname"].ToString());
                        //bbb.Show();

                    }

                    dr.Close();




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ///return ("No match found");
                }
            }

        }

        ///////////////////marking attendance//////////////////////////
        private void markAttendance(int emp_id)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "swiftentry_db";
            if (dbCon.IsConnect())
            {
                 try
                {
                    String q1 = "SELECT * FROM `attendence` WHERE employe_id='"+emp_id+"' and attendance_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                    MySqlCommand cmd = new MySqlCommand(q1, dbCon.Connection);
                    MySqlDataReader dr2 = cmd.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        dr2.Close();
                        MarkCheckOut(emp_id);
                        
                    }
                    else {
                        dr2.Close();
                        finalAttendance(emp_id);
                    
                    }
                    
                 }catch(Exception ec){
                 
                 MessageBox.Show(ec.Message);
                 }
            }
        }////ends

        /// <summary>
        /// //////////add attendance
        /// </summary>
        /// <param name="e_id"></param>
            private void finalAttendance(int e_id){
            
             var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "swiftentry_db";
                if(dbCon.IsConnect()){
                try{
                    //suppose col0 and col1 are defined as VARCHAR in the DB
                    string query = "INSERT into `attendence`(`employe_id`,`attendance_date`,`check_in`) VALUES('" + e_id + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "')";
                    //MessageBox.Show(query);
                    MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);
                    int a = cmd.ExecuteNonQuery();
                    if (a != 0)
                    {
                        MessageBox.Show("Attendance Marked","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                }//if ends
            }///ends
             ///
            private void MarkCheckOut(int e_id1)
            {

                var dbCon = DBConnection.Instance();
                dbCon.DatabaseName = "swiftentry_db";
                if (dbCon.IsConnect())
                {
                    try
                    {
                        //suppose col0 and col1 are defined as VARCHAR in the DB
                        String query = "UPDATE `attendence` SET check_out='"+DateTime.Now.ToString("HH:mm:ss")+"' WHERE employe_id='"+e_id1+"'";
                        //string query = "INSERT into `attendence`(`employe_id`,`attendance_date`,`check_in`) VALUES('" + e_id + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "')";
                        //MessageBox.Show(query);
                        MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);
                        int a = cmd.ExecuteNonQuery();
                        if (a != 0)
                        {
                            MessageBox.Show("Checked Out", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }//if ends
            }///ends

        /// <summary>
        /// ///////function that calculate employeeid 
        /// </summary>
        private int calculateEmployeeId(int id1)
        {
                var dbCon = DBConnection.Instance();
                dbCon.DatabaseName = "swiftentry_db";
                int ed=0;    
                if (dbCon.IsConnect())
                {
                    try
                    {
                        

                        String Query = "SELECT * FROM `sample` as s inner join employee as e on s.emp_id=e.employee_id WHERE s.sample_number='" + id1 + "'";
                        MySqlCommand cmd = new MySqlCommand(Query, dbCon.Connection);
                        MySqlDataReader dr23 = cmd.ExecuteReader();
                        while (dr23.Read())
                        {

                            ed = Convert.ToInt16(dr23["emp_id"]);
                            MessageBox.Show(dr23["fname"] + " " + dr23["mname"] + " " + dr23["lname"],"Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            dr23.Close();
                            return ed;
                          

                        }

                        dr23.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Your fingerprint donot present in database " + ex.Message);

                    } 
                    return ed;
                }//if ends
                else { return 0; }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = "Complete";
            txtSrn.Text = "";
            
            pictureBox2.Image = Image.FromFile("D:\\VBlab\\SwiftEntry2\\SwiftEntry2\\Resources\\images.jpg");
        }

        private void sampleNumber(object sender, EventArgs e)
        {
         // fingerpriCapture();
        }

        private void txtSrn_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Processing... Please Wait!!";
            if (!backgroundWorker1.IsBusy)
            {


                backgroundWorker1.RunWorkerAsync();

            }
        }//ends







    }//class

}///namespace
