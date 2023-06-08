using System.ComponentModel.DataAnnotations.Schema;

namespace RelationshipApi.Data.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RpgClass { get; set; }
        public User User { get; set; } // 1 character có 1 user

        [ForeignKey("UserId")] // Khóa phụ là userId
        public int UserId { get; set; }

        public Weapon Weapon { get; set; }
        public List<Skills> Skills { get; set; }
    }
}