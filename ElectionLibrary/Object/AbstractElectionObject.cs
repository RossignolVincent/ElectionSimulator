using AbstractLibrary.Object;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Object
{
    public abstract class AbstractElectionObject : AbstractObject
    {
        public Position Position { get; set; }

        public AbstractElectionObject(string name, Position position) : base(name)
        {
            Position = position;
        }
    }
}
