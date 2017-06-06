using AbstractLibrary.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Event
{
    [Serializable]
    public abstract class ElectionEvent : AbstractEvent
    {

        public ElectionEvent()
        {

        }
    }
}
