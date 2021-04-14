using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        static List<GiftViewModel> Gifts = new List<GiftViewModel>{
            new GiftViewModel {Title = "The Princess Bride", 
            Description = "A classic children's novel, but really anyone can enjoy it.",
            URL = "https://www.amazon.com/Princess-Bride-Deluxe-Morgensterns-Adventure/dp/1328948854/ref=sr_1_2?dchild=1&keywords=the+princess+bride&qid=1618381069&sr=8-2",
            UserID = 1,
            Priority = 1,
            },
        };
        public IActionResult Index()
        {
            return View(Gifts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Gifts.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            Gifts[id].Id = id;
            return View(Gifts[id]);
        }

        [HttpPost]
        public IActionResult Edit(GiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Gifts[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}