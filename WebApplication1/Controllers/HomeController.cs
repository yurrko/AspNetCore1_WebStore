﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        private readonly IValuesService _valuesService;

        public HomeController( IValuesService valuesService )
        {
            _valuesService = valuesService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _valuesService.GetAsync();
            return View( values );
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }


        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult ErrorStatus( string id )
        {
            if ( id == "404" )
                return RedirectToAction( "PageNotFound" );
            return Content( $"Статуcный код ошибки: {id}" );
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}