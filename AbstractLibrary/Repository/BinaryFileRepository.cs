using AbstractLibrary.Repository.Appender;
using AbstractLibrary.Repository.Reader;
using AbstractLibrary.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Repository
{
    public class BinaryFileRepository : FileRepository
    {
        public BinarySerializer Serializer { get; }

        public BinaryFileRepository(FileAppender Appender, FileReader Reader) : base(Appender, Reader)
        {
            this.Serializer = new BinarySerializer();
        }

        public override void Write(object data)
        {
            byte[] bytes = this.Serializer.Serialize(data);
            base.Write(bytes);
        }

        public override void Append(object data)
        {
            byte[] bytes = this.Serializer.Serialize(data);
            base.Append(bytes);
        }

        public override object Read()
        {
            byte[] data = (byte[]) base.Read();
            return this.Serializer.Deserialize(data);
        }
    }
}
