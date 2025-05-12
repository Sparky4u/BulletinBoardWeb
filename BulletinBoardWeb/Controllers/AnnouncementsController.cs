using BulletinBoardWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoardWeb.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiUrl;

        public AnnouncementsController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _apiUrl = config["ApiUrl"];
        }



        public async Task<IActionResult> Index(string category)
        {
            var client = _clientFactory.CreateClient();
            var data = await client.GetFromJsonAsync<List<AnnouncementViewModel>>(_apiUrl);

            ViewBag.SelectedCategory = category;
            ViewBag.Categories = data.Select(d => d.Category).Distinct().ToList();

            if (!string.IsNullOrEmpty(category))
                data = data.Where(d => d.Category == category).ToList();

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementViewModel model)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_apiUrl, model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _clientFactory.CreateClient();
            var data = await client.GetFromJsonAsync<List<AnnouncementViewModel>>(_apiUrl);
            var item = data.FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnnouncementViewModel model)
        {
            var client = _clientFactory.CreateClient();
            await client.PutAsJsonAsync($"{_apiUrl}/{model.Id}", model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient();
            await client.DeleteAsync($"{_apiUrl}/{id}");
            return RedirectToAction("Index");
        }

        private Dictionary<string, List<string>> GetCategories()
        {
            return new Dictionary<string, List<string>>
            {
                ["Побутова техніка"] = new() { "Холодильники", "Пральні машини", "Бойлери" },
                ["Комп'ютерна техніка"] = new() { "ПК", "Ноутбуки", "Монітори" },
                ["Смартфони"] = new() { "Android смартфони", "iOS/Apple смартфони" },
                ["Інше"] = new() { "Одяг", "Аксесуари", "Спортивне обладнання" }
            };
        }
    }
}
