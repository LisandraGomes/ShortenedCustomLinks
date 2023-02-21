using APIShortLink.Entities;
using APIShortLink.Models;
using APIShortLink.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace APIShortLink.Controllers
{

    [ApiController]
    [Route("api/shortenedLinks")]
    public class ShortenedLinksController : ControllerBase
    {
        public readonly DevEncurtaUrlDbContext _context;

        public ShortenedLinksController(DevEncurtaUrlDbContext context)
        {
            _context = context;
        }

        //Pontos de Acessos ou Endpoints
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Links);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var link = _context.Links.SingleOrDefault(a => a.Id == id);
            if (link == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Post
        (AddOrUpdateShortenedLinkModel model)
        {
            var link = new ShortenedCustomLink(model.Title, model.DestinationLink);

            _context.Links.Add(link);
            _context.SaveChanges();
            //retorna com base em outro endpoint
            return CreatedAtAction("GetById", new { id = link.Id }, link);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AddOrUpdateShortenedLinkModel model)
        {
            var link = _context.Links.SingleOrDefault(a => a.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            link.Update(model.Title, model.DestinationLink);
            _context.Links.Update(link);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var link = _context.Links.SingleOrDefault(a => a.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            _context.Links.Remove(link);
            return Ok("Remoção feita!");
        }

        //Redirecionamento para o link
        [HttpGet("/{code}")]
        public IActionResult Redirect(string code)
        {
            var link = _context.Links.SingleOrDefault(a => a.Code == code);
            if (link == null)
                return NotFound();

            return Redirect(link.DestinationLink);
        }

    }
}