using System;
namespace ElectionLibrary.Environment
{
    [Serializable]
    public class Street : AbstractArea
    {
        public Street(string name, Position position) : base(name, position)
        {
        }
    }
}
