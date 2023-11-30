using MasterMealMind.API.Services;
using MasterMealMind.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterMealMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly GroceryService _groceryService;

        public GroceryController(GroceryService grocerieService)
        {
            _groceryService = grocerieService;
        }


        [HttpGet]
        public async Task<IEnumerable<Grocery>> GetGroceriesAsync() => await _groceryService.GetAllGroceries();

        [HttpGet("{id}")]
        public async Task<Grocery> GetOneGroceryByIdAsync(int id) => await _groceryService.GetOneGrocery(id);

        [HttpPost]
        public async Task CreateGroceryAsync([FromBody] Grocery grocery) => await _groceryService.CreateGrocery(grocery);


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrocery(int id, [FromBody] Grocery updatedGrocery)
        {

            if (id != updatedGrocery.Id)
            {
                return BadRequest();
            }
            if (!await _groceryService.GroceryExists(id))
            {
                return NotFound();
            }
            _groceryService.UpdateGrocery(updatedGrocery);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrocery(int id)
        {
            if (!await _groceryService.GroceryExists(id))
            {
                return NotFound();
            }

            _groceryService.DeleteGrocery(id);

            return Ok();
        }
    }
}
