using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace UserGroup.Web.Controllers
{
    public class UsersController : Controller
    {
        static List<UserViewModel> Users = new List<UserViewModel>{
            new UserViewModel {FirstName = "Inigo", LastName = "Montoya"},
            new UserViewModel {FirstName = "Princess", LastName = "Buttercup"},
        };
        public IActionResult Index()
        {
            return View(Users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Users.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            Users[id].Id = id;
            return View(Users[id]);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Users[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}