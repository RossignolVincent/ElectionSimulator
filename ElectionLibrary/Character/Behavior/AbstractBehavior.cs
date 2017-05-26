using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.Behavior
{
    public abstract class AbstractBehavior
    {
        public abstract void NextTurn(AbstractArea area);
        public abstract int Influence(int moral, int nbTurn);
    }
}