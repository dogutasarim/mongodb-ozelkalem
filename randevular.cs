using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ozelkalem
{
    public partial class randevular : UserControl
    {
        public randevular()
        {
            InitializeComponent();
        }

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

            dataGridView1.DataSource = doclist;

        }

        private void randevular_Load(object sender, EventArgs e)
        {
            listele();
            
        }

            public object _id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                checkBox1.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[7].Value);
                _id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Hata Oluştu: " + exception);
                throw;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string baslik = textBox1.Text;
            string aciklama = textBox2.Text;
            DateTime baslangic = Convert.ToDateTime(textBox3.Text).ToLocalTime();
            DateTime bitis = Convert.ToDateTime(textBox4.Text).ToLocalTime();
            int renk = Convert.ToInt16(comboBox1.Text);
            string mekan = textBox5.Text;
            bool tumgun;
            if (checkBox1.Checked == true)
                tumgun = true;
            else
                tumgun = false;


            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase("randevular");
            IMongoCollection<appointment> collection = database.GetCollection<appointment>("appointment");

            
           
            try
            {
                var filter = Builders<appointment>.Filter.Eq(p =>p._id,_id);
                var builder = Builders<appointment>.Update.Set(p => p.baslik, baslik).Set(p => p.aciklama, aciklama).Set(p => p.baslangic, baslangic).Set(p => p.bitis, bitis).Set(p => p.renk, renk).Set(p => p.mekan, mekan).Set(p => p.tumgun, tumgun);
                collection.UpdateOne(filter, builder, new UpdateOptions {IsUpsert = true});
                MessageBox.Show("Güncelleme başarılı");
                listele();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Hata Oluştu: " + exception);
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase("randevular");
            IMongoCollection<appointment> collection = database.GetCollection<appointment>("appointment");

            DialogResult cevap = MessageBox.Show("Kaydı Silmek İstediğinize Emin Misiniz?", "Silme işlemi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cevap==DialogResult.Yes)
            {
                try
                {
                    var filter = Builders<appointment>.Filter.Eq(p => p._id, _id);
                    collection.DeleteOne(filter);
                    MessageBox.Show("Kayıt Silindi!");
                    listele();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Hata Oluştu: " + exception);
                    throw;
                }
            }

            
        }
    }
}
