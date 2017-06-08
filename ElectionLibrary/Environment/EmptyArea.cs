using System;
namespace ElectionLibrary.Environment
{
    [Serializable]
    public class EmptyArea : AbstractArea
    {
        public EmptyArea(string name, Position position) : base(name, position)
        {
        }
    }
}
