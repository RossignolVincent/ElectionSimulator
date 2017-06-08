using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Repository.Appender
{
    public interface IAppender
    {
        void Write(object data);
        void Append(object data);
        void Clear();
    }
}
