using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ThreeLayer.MVC.Controllers
{
    public class BrazilianPersonController : Controller
    {
        private readonly IMapper mapper;

        public BrazilianPersonController(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public async Task<IActionResult> GetAllAsync()
        {


            return View();
        }
    }
}
