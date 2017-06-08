using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Repository.Reader
{
    public class FileReader : IReader
    {
        public string Path { get; set; }

        public FileReader(string Path)
        {
            this.Path = Path;
        }

        public object Read()
        {
            return this.ReadAll();
        }

        public byte[] ReadAll()
        {
            return File.ReadAllBytes(Path);
        }

        public IEnumerable<object> ReadLineByLine()
        {
            return File.ReadLines(Path);
        }

        public byte[] ReadAllAsBytes()
        {
            return File.ReadAllBytes(Path);
        }
    }
}
