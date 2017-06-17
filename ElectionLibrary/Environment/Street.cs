using AbstractLibrary.Object;
using ElectionLibrary.Object;
using ElectionLibrary.Parties;
using System;
namespace ElectionLibrary.Environment
{
    [Serializable]
    public class Street : AbstractArea
    {
        public Street(string name, Position position) : base(name, position)
        {
        }

        public bool IsThereAlreadyPosterOfParty(PoliticalParty party)
        {
            foreach(AbstractObject obj in Objects)
            {
                if(obj is Poster poster && poster.Party == party)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
