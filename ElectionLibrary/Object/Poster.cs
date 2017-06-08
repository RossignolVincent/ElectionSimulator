using AbstractLibrary.Object;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Object
{
    public class Poster : AbstractObject
    {
        public Position Position { get; set; }

        public Poster(string name, Position position) : base(name)
        {
        }
    }
}
