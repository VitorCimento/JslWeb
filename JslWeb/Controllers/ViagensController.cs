using JslWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace JslWeb.Controllers
{
    public class ViagensController : Controller
    {
        public static readonly string API = "https://localhost:7105/api/";

        public async Task<ActionResult> Index()
        {
            List<Viagem> viagemList = await GetAll();

            return View(viagemList);
        }

        public async Task<ActionResult> FormView(long id = 0)
        {
            ViewBag.MotoristaId = new SelectList(await GetAllMotoristas(), "Id", "Nome");

            if (id == 0)
            {
                ViewBag.Header = "";
                ViewBag.Titulo = "Viagem - Novo";
                return View(new Viagem());
            }
            else
            {
                ViewBag.Header = $"Código: {id.ToString()}";
                ViewBag.Titulo = "Viagem - Alteração";
                return await Edit(id);
            }
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var viagem = await GetViagem(id);

            return View(viagem);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var viagem = await GetViagem(id);

            if (viagem == null) return NotFound();

            return View(viagem);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> FormView(long id,
            [Bind("Id, PesoCarga, DtHrViagem, LocalEntrega, LocalSaida, TotalKm, Motorista, MotoristaId")] Viagem viagem)
        {
            if (id == 0)
                return await Create(viagem);
            else
                return await Edit(id, viagem);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("PesoCarga, DtHrViagem, LocalEntrega, LocalSaida, TotalKm, Motorista, MotoristaId")] Viagem viagem)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(viagem), Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(API);
                    HttpResponseMessage responseMessage = await client.PostAsync("viagens/", content);

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        return Problem(statusCode: (int)responseMessage.StatusCode);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.MotoristaId = new SelectList(await GetAllMotoristas(), "Id", "Nome", viagem.MotoristaId);
            return View(viagem);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long id, 
            [Bind("Id, PesoCarga, DtHrViagem, LocalEnrega, LocalSaida, TotalKm, Motorista, MotoristaId")] Viagem viagem)
        {
            if (id != viagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(viagem), Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(API);
                        HttpResponseMessage responseMessage = await client.PutAsync($"viagens/{id.ToString()}", content);

                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            return Problem(statusCode: (int)responseMessage.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!await ViagemExists(viagem.Id))
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
            ViewBag.MotoristaId = new SelectList(await GetAllMotoristas(), "Id", "Nome", viagem.MotoristaId);
            return View(viagem);
        }


        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var viagem = await GetViagem(id);

            if (id != viagem.Id) return NotFound();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responeMessage = await client.DeleteAsync($"viagens/{id.ToString()}");

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

        private async Task<bool> ViagemExists(long? id)
        {
            var viagem = await GetViagem(id);
            if (viagem != null)
            {
                return id == viagem.Id;
            }
            return false;
        }


        private async Task<List<Viagem>> GetAll()
        {
            List<Viagem> viagemList = new List<Viagem>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responeMessage = await client.GetAsync("viagens");

                if (responeMessage.IsSuccessStatusCode)
                {
                    var response = responeMessage.Content.ReadAsStringAsync().Result;
                    viagemList = JsonConvert.DeserializeObject<List<Viagem>>(response);
                }
            }

            return viagemList;
        }

        private async Task<Viagem> GetViagem(long? id)
        {
            Viagem viagem = new Viagem();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage responseMessage = await client.GetAsync($"viagens/{id.ToString()}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    viagem = JsonConvert.DeserializeObject<Viagem>(response);
                }
            }

            return viagem;
        }

        public async Task<List<Motorista>> GetAllMotoristas()
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
    }
}
