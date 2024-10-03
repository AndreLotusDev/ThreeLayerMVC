
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using ThreeLayer.API.Models.DTOS;
using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Interfaces.Services;
using ThreeLayer.Business.Models;

namespace ThreeLayer.API.Controllers
{
    public class BrazilianPersonController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;
        private readonly IBrazilianPersonService _brazilianPersonService;

        public BrazilianPersonController(
            IMapper mapper, 
            INotifier notifier,
            IBrazilianPersonService brazilianPersonService) : base(notifier)
        {
            _mapper = mapper;
            _notifier = notifier;
            _brazilianPersonService = brazilianPersonService;
        }

        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var allBraziliansPersons = 
            _mapper.Map<IEnumerable<BrazilianPersonDTO>>(await _brazilianPersonService.GetAllAsync());

            return CustomResponse(HttpStatusCode.OK, allBraziliansPersons);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([Required] BrazilianPersonCreateDTO brazilianPersonToAdd)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var brazilianPerson = _mapper.Map<BrazilianPerson>(brazilianPersonToAdd);

            var toReturn = await _brazilianPersonService.AddAsync(brazilianPerson);

            return CustomResponse(HttpStatusCode.Created, toReturn);
        }
    }
}
