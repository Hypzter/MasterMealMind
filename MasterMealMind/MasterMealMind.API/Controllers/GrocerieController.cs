using MasterMealMind.API.DAL;
using Microsoft.AspNetCore.Mvc;

namespace MasterMealMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrocerieController : ControllerBase
    {
        private readonly GrocerieRepository _grocerieRepository;

        public GrocerieController(GrocerieRepository grocerieRepository)
        {
            _grocerieRepository = grocerieRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Models.Grocery>> GetGroceriesAsync() => await _grocerieRepository.GetAllGroceries();


        [HttpGet("{id}")]
        public async Task<Models.Grocery> GetOneGrocerieAsync(int id) => await _grocerieRepository.GetOneGrocerie(id);


        [HttpPost]
        public async Task CreateGrocerieAsync([FromBody] Models.Grocery grocerie) => await _grocerieRepository.CreateGrocerie(grocerie);

        // PUT api/<GrocerieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GrocerieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
