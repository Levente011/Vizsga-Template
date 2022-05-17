using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizsga.Models;

namespace Vizsga.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly AppContext _appContext;

        public ApiController(AppContext appContext) 
        {
            _appContext = appContext;
        }

        [HttpGet("api/orders")]
        public ActionResult Get()
        {
            return Ok
            (
                _appContext.Set<OrderModel>()
                           .Include(o => o.Category)
                           .Select(o => new
                           {
                               o.Id,
                               Date = $"{o.Date:yyyy-MM-dd}"
                           })
            );
        }

        [HttpPost("api/new")]
        public ActionResult Post(dynamic data)
        {
            try
            {
                var order = System.Text.Json.JsonSerializer
                                       .Deserialize<OrderModel>(
                                            data.ToString(),
                                            new System.Text.Json.JsonSerializerOptions()
                                            {
                                                PropertyNameCaseInsensitive = true
                                            }
                                       );
                _appContext.Set<OrderModel>().Add(order);
                _appContext.SaveChanges();
                return StatusCode(201, new
                {
                    id = order.Id
                });
            }
            catch
            {
                return BadRequest("Hiányos adatok");
            }
        }

        [HttpDelete("api/delete/{id}")]
        public ActionResult Delete(int id)
        {
            var order = _appContext.Set<OrderModel>().FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound("Szöveg");
            _appContext.Remove(order);
            _appContext.SaveChanges();
            return NoContent();
        }
    }
}
