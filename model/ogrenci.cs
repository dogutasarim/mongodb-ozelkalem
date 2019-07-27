using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace randevu
{
    
        public class randevu
        {
            public int _id { get; set; }
            public DateTime tarih { get; set; }
            public string aciklama { get; set; }
            public string kimle { get; set; }
            public string nerede { get; set; }
            public string saat { get; set; }
       


    }
}
