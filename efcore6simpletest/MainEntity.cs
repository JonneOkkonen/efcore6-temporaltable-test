namespace EFCore6SimpleTest
{ 
    public class MainEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public OwnedEntity OwnedEntity { get; set; }
    }
}
