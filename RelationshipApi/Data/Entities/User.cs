namespace RelationshipApi.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<Character> Character { get; set; } // 1 User có nhiều Character
    }
}