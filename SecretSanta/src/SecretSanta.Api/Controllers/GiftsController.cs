using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private IGiftRepository Repository { get; }

        public GiftsController(IGiftRepository repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IEnumerable<Dto.Gift> Get()
        {
            return Repository.List().Select(x => Dto.Gift.ToDto(x)!);
        }

        [HttpGet("{id}")]
        public ActionResult<Dto.Gift?> Get(int id)
        {
            Dto.Gift? Gift = Dto.Gift.ToDto(Repository.GetItem(id));
            if (Gift is null) return NotFound();
            return Gift;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Delete(int id)
        {
            if (Repository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Dto.Gift), (int)HttpStatusCode.OK)]
        public ActionResult<Dto.Gift?> Post([FromBody] Dto.Gift Gift)
        {
            return Dto.Gift.ToDto(Repository.Create(Dto.Gift.FromDto(Gift)!));
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Put(int id, [FromBody] Dto.UpdateGift? Gift)
        {
            Data.Gift? foundGift = Repository.GetItem(id);
            if (foundGift is not null)
            {
                foundGift.Title = Gift?.Title ?? "";
                foundGift.Priority = Gift!.Priority;
                foundGift.Description = Gift?.Description ?? "";
                foundGift.Url = Gift?.Url ?? "";
                foundGift.UserId = Gift.UserId;
                
                Repository.Save(foundGift);
                return Ok();
            }
            return NotFound();
        }
    }
}
