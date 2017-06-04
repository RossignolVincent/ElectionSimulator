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
    public class JSONFileRepository : FileRepository
    {
        public JSONSerializer Serializer { get; }

        public JSONFileRepository(FileAppender Appender, FileReader Reader) : base(Appender, Reader)
        {
            this.Serializer = new JSONSerializer();
        }

        public override void Write(object data)
        {
            String json = this.Serializer.Serialize(data);
            Console.WriteLine(json);
            base.Write(json);
        }

        public override void Append(object data)
        {
            String json = this.Serializer.Serialize(data);
            Console.WriteLine(json);
            base.Append(json);
        }

        public override object Read()
        {
            String json = base.Read().ToString();
            Console.WriteLine(json);
            return this.Serializer.Deserialize(json);
        }
    }
}
