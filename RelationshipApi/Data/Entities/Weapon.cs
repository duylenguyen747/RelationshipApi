using System.ComponentModel.DataAnnotations.Schema;

namespace RelationshipApi.Data.Entities
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public Character Character { get; set; }// 1 character có 1 weapon

        [ForeignKey("CharacterId")]
        public int CharacterId { get; set; }
    }
}