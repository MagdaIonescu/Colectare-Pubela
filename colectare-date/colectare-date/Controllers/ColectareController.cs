using colectare_date.Data;
using colectare_date.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_date.Controllers
{
    [Route("api/colectari")]
    [ApiController]
    public class ColectareController : ControllerBase
    {
        private readonly AppDbContext context;

        public ColectareController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostColectare([FromBody] Colectare colectare)
        {
            if (colectare == null || string.IsNullOrWhiteSpace(colectare.PubelaId))
                return BadRequest("Date invalide!");

            if (colectare.TimpColectare == default(DateTime))
            {
                colectare.TimpColectare = DateTime.UtcNow;
            }

            context.Colectari.Add(colectare);
            await context.SaveChangesAsync();

            return Ok(new { message = "Colectare înregistrata!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetColectari()
        {
            var colectari = await context.Colectari
                .Select(c => new
                {
                    c.Id,
                    c.PubelaId,
                    TimpColectare = c.TimpColectare.ToString("dd-MM-yyyy HH:mm:ss")
                })
                .ToListAsync();

            return Ok(colectari);
        }
    }
}
