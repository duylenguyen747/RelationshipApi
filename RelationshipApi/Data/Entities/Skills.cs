namespace RelationshipApi.Data.Entities
{
    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<Character> Characters { get; set; } // nhiều tướng có nhiều skill
    }
}