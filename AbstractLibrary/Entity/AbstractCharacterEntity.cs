namespace AbstractLibrary.Entity
{
    public class AbstractCharacterEntity : AbstractEntity
    {
        public string Name { get; set; }
        public AbstractAreaEntity Position { get; set; }
    }
}