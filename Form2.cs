using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace ozelkalem
{
    public partial class Form2 : Form
    { 
        
            MongoServer server;
            MongoDatabase database;
            MongoCollection collection;

        public Form2()
        {
            InitializeComponent();
        }

           
        
        private void Form2_Load(object sender, EventArgs e)
        {
            string connection = "mongodb://localhost:21017";
            server = IMongoCreateViewOptions(connection);
            database=server.GetDatabase("randevu");

            listView1.View=View.Details;
            listView1.FullRowSelect=true;
            collection=database.GetCollection<randevu>("Randevular");

           var goster=collection.AsQueryable<randevu>().ToList();
           foreach(var item in goster)
           {
           string[] array={item._id.ToString(),item.aciklama.ToString()};
               listView1.Items.Add(new ListViewItem(array));
           
           }

        }
    }
}
