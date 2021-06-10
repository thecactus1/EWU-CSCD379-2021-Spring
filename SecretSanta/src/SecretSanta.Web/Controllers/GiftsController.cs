using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Data;
using SecretSanta.Web.ViewModels;
using System.Collections.Generic;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller{
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
/*    public class GiftsController : Controller
    {
        DbContext dbcontext = new DbContext();
        public IActionResult Index()
        {
            var gifts = dbcontext.Gifts.OrderBy(g => g.Priority).ToList();
            List<GiftViewModel> giftlist = new List<GiftViewModel>();
            foreach(Gift i in gifts){
                GiftViewModel g = new GiftViewModel{Id = i.Id, UserId = i.Id, Title = i.Title, Description = i.Description, Url = i.Url, Priority = i.Priority };
                giftlist.Add(g);
            }
            return View(giftlist);
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
                viewModel.Id = dbcontext.Gifts.Max(g => g.Id) + 1;
                Gift g = new Gift{Id = viewModel.Id, UserId = viewModel.Id, Title = viewModel.Title, Description = viewModel.Description, Url = viewModel.Url, Priority = viewModel.Priority };
                dbcontext.Gifts.Add(g);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return View(dbcontext.Gifts.Single(g => g.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(GiftViewModel viewModel)
        {
            Gift g = new Gift{Id = viewModel.Id, UserId = viewModel.Id, Title = viewModel.Title, Description = viewModel.Description, Url = viewModel.Url, Priority = viewModel.Priority };
            if (g is null)
            {
                return null;
            }
            

            //MockData.Groups[item.Id] = item;
            Group temp = dbcontext.Groups.Find(g.Id);
            if (temp is null)
            {
                Create(viewModel);
            }
            else
            {
                dbcontext.Groups.Remove(dbcontext.Groups.Find(g.Id));
                Create(viewModel);
            }
            dbcontext.SaveChanges();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Gift found = dbcontext.Gifts.Find(id);
            dbcontext.Gifts.Remove(found);
            dbcontext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}*/