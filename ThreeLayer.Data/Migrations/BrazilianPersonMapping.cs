using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreeLayer.Business.Models;

namespace ThreeLayer.Data.Migrations
{
    public class BrazilianPersonMapping : IEntityTypeConfiguration<BrazilianPerson>
    {
        public void Configure(EntityTypeBuilder<BrazilianPerson> builder)
        {
            
        }
    }
}
