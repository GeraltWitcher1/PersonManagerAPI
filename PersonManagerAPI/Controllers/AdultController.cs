using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonManagerAPI.Data;
using PersonManagerAPI.Models;

namespace PersonManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdultController : ControllerBase
    {
        private IAdultService adultManager;

        public AdultController(IAdultService adultManager)
        {
            this.adultManager = adultManager;
        }

        
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>>
            GetTodos([FromQuery] int? id)
        {
            try
            {
                IList<Adult> todos = await adultManager.GetAll();
                
                if (id != null)
                {
                    todos = todos.Where(ad => ad.Id == id).ToList();
                }

                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id)
        {
            try
            {
                await adultManager.RemoveAdult(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Adult added = await adultManager.AddAdult(adult);
                return Created($"/{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        public async Task<ActionResult<Adult>> UpdateTodo([FromBody] Adult adult)
        {
            try
            {
                Adult updatedAdult = await adultManager.UpdateAdult(adult);
                return Ok(updatedAdult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    }
}