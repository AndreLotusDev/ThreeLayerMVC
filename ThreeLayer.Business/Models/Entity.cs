namespace ThreeLayer.Business.Models
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifiedByUserId { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
