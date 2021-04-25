using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.Dto;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository UserRepository { get; }

        public UsersController(IUserRepository UserRepository)
        {
            UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
        }

        // /api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UserRepository.List();
        }

        // /api/Users/<index>
        [HttpGet("{id}")]
        public ActionResult<User?> Get(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            User? returnedUser = UserRepository.GetItem(id);
            return returnedUser;
        }

        //DELETE /api/Users/<index>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            if (UserRepository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        // POST /api/Users
        [HttpPost]
        public ActionResult<User?> Post([FromBody] User? myUser)
        {
            if (myUser is null)
            {
                return BadRequest();
            }
            return UserRepository.Create(myUser);
        }

        // PUT /api/Users/<id>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]UpdateUser? updatedUser)
        {
            if (updatedUser is null)
            {
                return BadRequest();
            }
            User? foundUser = UserRepository.GetItem(id);
            if (foundUser is not null)
            {
                if (!string.IsNullOrWhiteSpace(updatedUser.LastName))
                {
                    foundUser.LastName = updatedUser.LastName;
                }
                if (!string.IsNullOrWhiteSpace(updatedUser.FirstName))
                {
                    foundUser.FirstName = updatedUser.FirstName;
                }
                UserRepository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}