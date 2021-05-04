using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IUsersClient Client {get;}

        public UsersController(IUsersClient client){
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }


        public async Task<IActionResult> Index()
        {
            ICollection<User> users = await Client.GetAllAsync();
            List<UserViewModel> viewModelUsers = new();           
            foreach(User e in users)
            {
                viewModelUsers.Add(new UserViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,

                });
            }
            return View(viewModelUsers);
        }


        public IActionResult Create()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await Client.PostAsync(new User {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                });
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
       public async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            UserViewModel model = ConvertFromClient(await Client.GetAsync(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                UpdateUser uu = new UpdateUser(){
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                await Client.PutAsync(model.Id, uu);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        private UserViewModel ConvertFromClient(User model){
            UserViewModel export = new(){
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id
            };
            return export;
        }
    }
}