using MasterMealMind.Infrastructure.Services;
using MasterMealMind.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MasterMealMind.Core.Services;

namespace MasterMealMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly IGroceryService _groceryService;

        public GroceryController(IGroceryService groceryService)
        {
            _groceryService = groceryService;
        }


        [HttpGet]
        public async Task<IEnumerable<Grocery>> GetGroceriesAsync() => await _groceryService.GetAllGroceries();

        [HttpGet("{id}")]
        public async Task<Grocery> GetOneGroceryByIdAsync(int id) => await _groceryService.GetOneGrocery(id);

        [HttpPost]
        public async Task AddOrUpdateGroceryAsync([FromBody] Grocery grocery) => await _groceryService.AddOrUpdateGrocery(grocery);


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrocery(int id, [FromBody] Grocery grocery)
        {

            if (id != grocery.Id)
            {
                return BadRequest();
            }
            if (!await _groceryService.GroceryExists(grocery.Name))
            {
                return NotFound();
            }
            await _groceryService.UpdateGrocery(grocery);

            return Ok();
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
