using ThreeLayer.Business.Models;

namespace ThreeLayer.Business.Interfaces.Services
{
    public interface IBrazilianPersonService
    {
        Task<BrazilianPerson> AddAsync(BrazilianPerson brazilianPerson);
        Task<List<BrazilianPerson>> GetAllAsync();
    }
}
