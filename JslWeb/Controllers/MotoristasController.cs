using JslWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace JslWeb.Controllers
{
    public class MotoristasController : Controller
    {
        public static readonly string API = "https://localhost:7105/api/";

        public async Task<ActionResult> Index()
        {
            List<Motorista> motoristaList = new List<Motorista>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responeMessage = await client.GetAsync("motoristas");

                if (responeMessage.IsSuccessStatusCode)
                {
                    var response = responeMessage.Content.ReadAsStringAsync().Result;
                    motoristaList = JsonConvert.DeserializeObject<List<Motorista>>(response);
                }
            }

            return View(motoristaList);
        }
    }
}
