using ThreeLayer.Business.Helpers;

namespace ThreeLayer.Business.Models
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = GuidGenerator.Create();
        }

        public Guid Id { get; set; }

        public string CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public bool Deleted { get; set; }

        public uint RowVersion { get; set; }
    }
}
