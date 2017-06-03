using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Serializer
{
    public interface ISerializer<T>
    {
        T Serialize(object data);
        object Deserialize(T data);
    }
}
