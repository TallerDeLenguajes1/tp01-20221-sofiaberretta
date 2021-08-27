using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrabajoPractico1.Models;

namespace TrabajoPractico1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public string Problema1(string num)
        {
            try
            {
                int cuadrado;
                int numero = int.Parse(num);

                cuadrado = numero * numero;
                return $"El cuadrado de {num} es {cuadrado}";
            }
            catch (FormatException)
            {
                return "#ERROR No se ha ingresado un numero.";
            }
            catch (OverflowException)
            {
                return "#ERROR El numero ingresado es demasiado grande.";
            }
            catch (Exception ex)
            {
                return $"#ERROR {ex.Message}";
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
