using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractLibrary.Repository.Appender;
using AbstractLibrary.Repository.Reader;
using AbstractLibrary.Serializer;

namespace AbstractLibrary.Repository
{
    class JSONFileRepository : FileRepository
    {
        public JSONSerializer Serializer { get; }

        public JSONFileRepository(FileAppender Appender, FileReader Reader) : base(Appender, Reader)
        {
            this.Serializer = new JSONSerializer();
        }

        public new void Write(object data)
        {
            String json = this.Serializer.Serialize(data);
            base.Write(json);
        }

        public new void Append(object data)
        {
            String json = this.Serializer.Serialize(data);
            base.Append(json);
        }

        public new object Read()
        {
            String json = base.Read().ToString();
            return this.Serializer.Deserialize(json);
        }
    }
}
