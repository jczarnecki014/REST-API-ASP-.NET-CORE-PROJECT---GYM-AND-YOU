using GymAndYou.Entities.EntitiesInterface;

namespace GymAndYou.Entities
{
    public class Role : IDbEntity
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
    }
}
