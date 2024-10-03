using ThreeLayer.Business.Interfaces.Repository;
using ThreeLayer.Business.Models;
using ThreeLayer.Data.Context;

namespace ThreeLayer.Data.Repository
{
    public class BrazilianPersonRepository : Repository<BrazilianPerson>, IBrazilianPersonRepository
    {
        public BrazilianPersonRepository(AppDbContext context) : base(context)
        {
        }


    }
}
