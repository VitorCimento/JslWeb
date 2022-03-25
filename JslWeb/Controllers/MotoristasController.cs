using JslWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace JslWeb.Controllers
{
    public class MotoristasController : Controller
    {
        public static readonly string API = "https://localhost:7105/api/";

        public async Task<ActionResult> Index()
        {
            List<Motorista> motoristaList = await GetAll();

            return View(motoristaList);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var motorista = await GetMotorista(id);

            return View(motorista);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var motorista = await GetMotorista(id);

            if (motorista == null) return NotFound();

            return View(motorista);
        }

        [HttpPost,
            ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long id, 
            [Bind("Id, Nome, Sobrenome, CaminhaoMarca, CaminhaoModelo, CaminhaoPlaca, CaminhaoEixos, EndLogradouro, EndNumero, EndBairro, EndCidade, EndCep, EndUf, Viagens")] Motorista motorista)
        {
            if (id != motorista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(motorista), Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(API);
                        HttpResponseMessage responseMessage = await client.PutAsync($"motoristas/{id.ToString()}", content);

                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            return Problem(statusCode: (int)responseMessage.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (! await MotoristaExists(motorista.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(motorista);
        }


        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var motorista = await GetMotorista(id);

            if (id != motorista.Id) return NotFound();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responeMessage = await client.DeleteAsync($"motoristas/{id.ToString()}");

                if (responeMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Problem(statusCode: (int)responeMessage.StatusCode);
                }
            }
        }

        private async Task<bool> MotoristaExists(long? id)
        {
            var motorista = await GetMotorista(id);
            if (motorista != null)
            {
                return id == motorista.Id;
            }
            return false;
        }


        private async Task<List<Motorista>> GetAll()
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

            return motoristaList;
        }

        private async Task<Motorista> GetMotorista(long? id)
        {
            Motorista motorista = new Motorista();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage responseMessage = await client.GetAsync($"motoristas/{id.ToString()}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    motorista = JsonConvert.DeserializeObject<Motorista>(response);
                }
            }

            return motorista;
        }
    }
}
