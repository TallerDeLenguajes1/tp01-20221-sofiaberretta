using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public IActionResult Problema2()
        {
            return View();
        }

        public IActionResult Problema3()
        {
            return View();
        }

        public IActionResult Problema4()
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

        public string CalculoProblema2(string num1, string num2)
        {
            try
            {
                int division;
                int dividendo = int.Parse(num1);
                int divisor = int.Parse(num2);

                division = dividendo / divisor;
                return $"La division entre {dividendo} / {divisor} es {division}";
            }
            catch (FormatException)
            {
                return "#ERROR No se ha ingresado un numero.";
            }
            catch (OverflowException)
            {
                return "#ERROR El numero ingresado es demasiado grande.";
            }
            catch (DivideByZeroException)
            {
                return $"#ERROR No se puede dividir por cero.";
            }
            catch(Exception ex)
            {
                return $"#ERROR {ex.Message}";
            }

        }

        public string ConsumirAPI()
        {
            var url = $"https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            ProvinciasArgentina ProvinciasArg;
            ProvinciasArg = null;
            string listaProvincias = "";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                ProvinciasArg = JsonSerializer.Deserialize<ProvinciasArgentina>(responseBody);
                            }

                            for(int i = 0; i < ProvinciasArg.Cantidad; i++)
                            {
                                listaProvincias += " ID " + ProvinciasArg.Provincias[i].Id + "| " + ProvinciasArg.Provincias[i].Nombre + "\n";
                            }
                        }
                    }
                }

                return listaProvincias;
            }
            catch (WebException)
            {
                return "#Error No se pudo acceder a la red";
            }
            catch(Exception ex)
            {
                return $"#ERROR {ex.Message}";
            }
        }

        //DATOS PARA CONSUMO DE API
        public class Parametros
        {
            [JsonPropertyName("campos")]
            public List<string> Campos { get; set; }
        }

        public class Provincia
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("nombre")]
            public string Nombre { get; set; }
        }

        public class ProvinciasArgentina
        {
            [JsonPropertyName("cantidad")]
            public int Cantidad { get; set; }

            [JsonPropertyName("inicio")]
            public int Inicio { get; set; }

            [JsonPropertyName("parametros")]
            public Parametros Parametros { get; set; }

            [JsonPropertyName("provincias")]
            public List<Provincia> Provincias { get; set; }

            [JsonPropertyName("total")]
            public int Total { get; set; }
        }

        public string CalculoProblema4(string num1, string num2)
        {
            try
            {
                int division;
                int km = int.Parse(num1);
                int litro = int.Parse(num2);

                division = km / litro;
                return $"La cantidad de km por litro es {division}";
            }
            catch (FormatException)
            {
                return "#ERROR No se ha ingresado un numero.";
            }
            catch (OverflowException)
            {
                return "#ERROR El numero ingresado es demasiado grande.";
            }
            catch (DivideByZeroException)
            {
                return $"#ERROR No se puede dividir por cero.";
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
