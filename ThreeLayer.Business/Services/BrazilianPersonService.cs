using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Interfaces.Repository;
using ThreeLayer.Business.Interfaces.Services;
using ThreeLayer.Business.Models;
using ThreeLayer.Business.Validation;

namespace ThreeLayer.Business.Services
{
    public class BrazilianPersonService : BaseService, IBrazilianPersonService
    {
        private readonly IBrazilianPersonRepository _brazilianPersonRepository;

        public BrazilianPersonService(
            INotifier notifier,
            IBrazilianPersonRepository brazilianPersonRepository) : base(notifier)
        {
            _brazilianPersonRepository = brazilianPersonRepository;
        }

        public async Task<BrazilianPerson> AddAsync(BrazilianPerson brazilianPerson)
        {
            if (!ExecuteValidation(new BrazilianPersonValidation(), brazilianPerson)) return null;

            return await _brazilianPersonRepository.AddAsync(brazilianPerson);
        }

        public async Task<List<BrazilianPerson>> GetAllAsync()
        {
            return await _brazilianPersonRepository.GetAllAsync();
        }
    }
}
