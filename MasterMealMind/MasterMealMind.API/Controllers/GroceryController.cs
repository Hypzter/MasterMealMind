using MasterMealMind.API.DAL;
using MasterMealMind.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterMealMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly GroceryRepository _groceryRepository;

        public GroceryController(GroceryRepository grocerieRepository)
        {
            _groceryRepository = grocerieRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Grocery>> GetGroceriesAsync() => await _groceryRepository.GetAllGroceries();

        [HttpGet("{id}")]
        public async Task<Grocery> GetOneGroceryByIdAsync(int id) => await _groceryRepository.GetOneGrocery(id);

        [HttpPost]
        public async Task CreateGroceryAsync([FromBody] Grocery grocery) => await _groceryRepository.CreateGrocery(grocery);


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrocery(int id, [FromBody] Grocery updatedGrocery)
        {

            if (id != updatedGrocery.Id)
            {
                return BadRequest();
            }
            if (!await _groceryRepository.GroceryExists(id))
            {
                return NotFound();
            }
            _groceryRepository.UpdateGrocery(updatedGrocery);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrocery(int id)
        {
            if (!await _groceryRepository.GroceryExists(id))
            {
                return NotFound();
            }

            _groceryRepository.DeleteGrocery(id);

            return Ok();
        }
    }
}
