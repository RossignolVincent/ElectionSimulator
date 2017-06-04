namespace AbstractLibrary.Entity
{
    public class AbstractObjectEntity : AbstractEntity
    {
        public string Name { get; set; }
        public AbstractAreaEntity Position { get; set; }
    }
}