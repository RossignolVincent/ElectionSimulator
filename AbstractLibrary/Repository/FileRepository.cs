using AbstractLibrary.Repository.Appender;
using AbstractLibrary.Repository.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Repository
{
    class FileRepository : Repository
    {
        public new FileAppender Appender;
        public new FileReader Reader;

        public FileRepository(FileAppender Appender, FileReader Reader) : base(Appender, Reader)
        {
        }
    }
}
