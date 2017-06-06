using ElectionLibrary.Environment;
using System;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public abstract class AbstractBehavior
    {
        public abstract void NextTurn(AbstractArea area);
        public abstract int Influence(int moral, int nbTurn);
    }
}