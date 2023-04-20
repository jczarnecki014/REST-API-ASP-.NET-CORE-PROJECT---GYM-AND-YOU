namespace GymAndYou.Entities.EntitiesInterface
{
    /// <summary>
    /// <typeparamref name="IDbEntity"/> is a marker that defines the class as Database entities set
    /// </summary>
    public interface IDbEntity
    {
        public int Id { get; set; }
    }
}
