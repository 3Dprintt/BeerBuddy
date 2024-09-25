using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BeerBuddy.Controllers
{
    public class T_brasserieController : Controller
    {
        // GET: T_brasserie
        public async Task<ActionResult> Index(string searchTerm)
        {
            var bars = await GetBarsAndPubsAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(bars);
        }

        // Méthode pour obtenir la liste des bars et pubs
        private async Task<List<Place>> GetBarsAndPubsAsync(string searchTerm)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://lz4.overpass-api.de/api/interpreter?data=[out:json];node[\"amenity\"~\"bar|pub\"](around:5000,50.2910,2.7775);out body;";
                System.Diagnostics.Debug.WriteLine("Requête URL: " + url);

                var response = await client.GetStringAsync(url);
                System.Diagnostics.Debug.WriteLine("Réponse JSON: " + response);

                var result = JsonConvert.DeserializeObject<OverpassResponse>(response);

                // Log pour vérifier les résultats
                System.Diagnostics.Debug.WriteLine("Nombre de résultats: " + result.Elements.Count);

                return result.Elements.Select(e => new Place
                {
                    Name = e.Tags.Name,
                    Vicinity = $"{e.Tags.HouseNumber} {e.Tags.Street}, {e.Tags.City}, {e.Tags.Postcode}",
                    Rating = 0 // Overpass API ne fournit pas de note
                }).ToList();
            }
        }

        // Autres actions...

        // GET: T_brasserie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: T_brasserie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_brasserie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: T_brasserie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: T_brasserie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: T_brasserie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: T_brasserie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    public class OverpassResponse
    {
        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }
    }

    public class Element
    {
        [JsonProperty("tags")]
        public Tags Tags { get; set; }
    }

    public class Tags
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("addr:street")]
        public string Street { get; set; }

        [JsonProperty("addr:housenumber")]
        public string HouseNumber { get; set; }

        [JsonProperty("addr:city")]
        public string City { get; set; }

        [JsonProperty("addr:postcode")]
        public string Postcode { get; set; }
    }

    public class Place
    {
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public float Rating { get; set; }
    }
}
