using AbstractLibrary.Repository.Appender;
using AbstractLibrary.Repository.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Repository
{
    public class Repository
    {
        public IAppender Appender { get; set; }

        public IReader Reader { get; set; }

        public Repository(IAppender Appender, IReader Reader)
        {
            this.Appender = Appender;
            this.Reader = Reader;
        }

        public void Write(object data)
        {
            Appender.Write(data);
        }

        public void Append(object data)
        {
            Appender.Append(data);
        }

        public void Clear(object data)
        {
            Appender.Clear();
        }

        public object Read()
        {
            return Reader.Read();
        }
    }
}
