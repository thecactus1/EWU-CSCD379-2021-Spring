using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private DbContext dbcontext;
        private IGiftRepository GiftsRepository { get; }
        public IUserRepository UserRepository { get; }

        public GiftsController(IGiftRepository repository, IUserRepository userRepository)
        {
            GiftsRepository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            UserRepository = userRepository ?? throw new System.ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public IEnumerable<Gift> Get()
        {
            return GiftsRepository.List();
        }

        [HttpGet("{id}")]
        public ActionResult<Gift?> Get(int id)
        {
            Gift? Gifts = GiftsRepository.GetItem(id);
            if (Gifts is null) return NotFound();
            return Gifts;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Delete(int id)
        {
            if (GiftsRepository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Gift), (int)HttpStatusCode.OK)]
        public ActionResult<Gift?> Post([FromBody] Gift Gifts)
        {
            return GiftsRepository.Create(Gifts!);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Put(int id, [FromBody] Gift? Gifts)
        {
            Data.Gift? foundGifts = GiftsRepository.GetItem(id);
            if (foundGifts is not null)
            {
                foundGifts.Title = Gifts?.Title ?? "";

                GiftsRepository.Save(foundGifts);
                return Ok();
            }
            return NotFound();
        }
    }
}
