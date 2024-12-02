using Microsoft.AspNetCore.Mvc;
using MyWebApi1.Models;

namespace MyWebApi1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {

        [HttpGet]
        public IActionResult demo()
        {
            return Ok("Success");
        }


        
        [HttpGet("himanshu")]
        public IActionResult hello()
        {
            return Ok("This is test api");
        }

        [HttpGet("showproduct")]
        public IActionResult showproduct()
        {
            /*
            products pro1 = new products();
            pro1.name = "Mouse";
            pro1.description = "Dell Wireless Mouse";
            pro1.price = 200;
            */

            var pro1 = new products
            {
                name = "Mouse",
                description = "HP Wireless mouse",
                price = 250
            };

            var pro2 = new products
            {
                name = "Keyboard",
                description = "HP Wireless Keyboard",
                price = 500
            };

            List<products> allproduct = new List<products>();
            allproduct.Add(pro1);
            allproduct.Add(pro2);

            return Ok(allproduct);

        }

        [HttpPost("addproduct")]
        public IActionResult addproduct( [FromForm] products pro )
        {

            if (pro == null)
            {
                return BadRequest("Invalid Data");
            }


            return Ok(pro); 

        }
        

    }
}
