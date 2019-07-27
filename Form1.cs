using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Runtime.InteropServices;
using System.Threading;

namespace ozelkalem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            Thread th=new Thread(new ThreadStart(formrun));
            th.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            th.Abort();

        }

        private void formrun()
        {
            Application.Run(new splash_screen());
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        void listele()
        {

            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("randevular");
            var collection = database.GetCollection<appointment>("appointment");

            BindingList<appointment> doclist = new BindingList<appointment>();

            foreach (var appointment in collection.FindAll())
            {

                doclist.Add(appointment);


            }

            bindingSource1.DataSource = doclist;

            schedulerControl1.Start = DateTime.Today;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
          login lg = new login();
          lg.ShowDialog();

           listele();

            timer1.Start();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Programı Kapatmak İstediğinize Emin Misiniz?", "Program Kapanıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //tus codigos
                Application.Exit();
            }
            else
            {
                //tus codigos
            }
        }

        private void schedulerControl1_Click(object sender, EventArgs e)
        {
            textBox3.Text = schedulerControl1.SelectedInterval.Start.ToString();
            textBox4.Text = schedulerControl1.SelectedInterval.End.ToString();

           
        }

        private void button5_Click(object sender, EventArgs e)
        {

            textBox1.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;

            if (textBox1.Text == "")
            {
                MessageBox.Show("Başlık Alanı Boş Geçilmez!");
                textBox1.BackColor = Color.Red; return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("Başlangıç Alanı Boş Geçilmez!");
                textBox3.BackColor = Color.Red; return;
            }
            if (textBox4.Text == "")
            {
                MessageBox.Show("Bitiş Alanı Boş Geçilmez");
                textBox4.BackColor = Color.Red; return;
            }

            var randevu = new appointment();
            randevu.baslik = textBox1.Text;
            randevu.aciklama = textBox2.Text;
            randevu.baslangic =Convert.ToDateTime(textBox3.Text).ToLocalTime();
            randevu.bitis = Convert.ToDateTime(textBox4.Text).ToLocalTime();
            randevu.renk = Convert.ToInt16(comboBox1.Text);
            randevu.mekan = textBox5.Text;
            if (checkBox1.Checked == true)
                randevu.tumgun = true;
            else
                randevu.tumgun = false;




            try
            {
                var connectionString = "mongodb://localhost:27017";
                var client = new MongoClient(connectionString);
                var server = client.GetServer();
                var database = server.GetDatabase("randevular");
                var collection = database.GetCollection<appointment>("appointment");


                collection.Save(randevu);
                
                MessageBox.Show("kaydedildi");
                listele();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            schedulerStorage1.RefreshData();

            schedulerControl1.Refresh();

            if (schedulerControl1.Services.SchedulerState.IsDataRefreshAllowed)
            {
                schedulerControl1.RefreshData();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            randevular1.BringToFront();
            randevular1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            randevular1.Visible = false;
        }
    }

       
}

