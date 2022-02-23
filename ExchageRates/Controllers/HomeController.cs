using ExchageRates.Models;
using ExchageRates.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExchageRates.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExhangeRepository<Exchange> _exhangeRepository;
        private readonly DataContext _dataContext;
        public HomeController(ILogger<HomeController> logger, DataContext dataContext, IExhangeRepository<Exchange> exhangeRepository)
        {
            _logger = logger;
            _dataContext = dataContext;
            _exhangeRepository = exhangeRepository;
        }

        public IActionResult Index()
        {
            var exhanges = _exhangeRepository.GetAll();
            return View(exhanges);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
