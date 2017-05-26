using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.Behavior
{
    public abstract class AbstractBehavior
    {
        public abstract Position Move();
        public abstract int Influence(int moral, int nbTurn);
    }
}