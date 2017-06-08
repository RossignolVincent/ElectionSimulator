using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AbstractLibrary.Serializer;

namespace AbstractLibrary.Repository.Appender
{

    public class FileAppender : IAppender
    {
        public String Path { get; set; }

        public FileAppender(string Path)
        {
            this.Path = Path;
        }

        public void Create()
        {
            if(!File.Exists(Path))
            {
                File.Create(Path);
            }
        }

        public void Append(object data)
        {
            String text = data != null ? data.ToString() : "";
            File.AppendAllText(Path, text);
        }

        public void Clear()
        {
            File.WriteAllText(Path, "");
        }

        public void Write(object data)
        {
            var serializer = new BinarySerializer();
            byte[] bytes = serializer.Serialize(data);
            File.WriteAllBytes(Path, bytes);
        }
    }
}
