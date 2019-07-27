using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ozelkalem
{
    class appointment
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("baslik")]
        public string baslik { get; set; }

        [BsonElement("aciklama")]
        public string aciklama { get; set; }

        [BsonElement("baslangic")]
        public object baslangic { get; set; }

        [BsonElement("bitis")]
        public object bitis { get; set; }

        [BsonElement("renk")]
        public int renk { get; set; }

        [BsonElement("mekan")]
        public string mekan { get; set; }

        [BsonElement("tumgun")]
        public bool tumgun { get; set; }

        public appointment()
        {
            this._id = ObjectId.GenerateNewId();
        }
    }
}
