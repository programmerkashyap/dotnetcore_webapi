using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi1.Models;

namespace MyWebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        public AppDbContext _context;
        public IWebHostEnvironment _environment;
        public UsersController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        [HttpPost("adduser")]
        public async Task<IActionResult> adduser( [FromForm] users data, [FromForm] IFormFile picture )
        {
            if (data == null)
            {
                return BadRequest(new basicresponse { status="error", message ="data can not be null"} );
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (picture != null)
            {
                string a = DateTime.Now.ToString("yyyyMMddHHmmss");
                Random rnd = new Random();
                int num = rnd.Next(1000, 9999);

                string folderpath = Path.Combine(_environment.WebRootPath, "uploads");
                string filename = a+"_"+num+"_"+ picture.FileName;
                string filepath = Path.Combine(folderpath, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                await picture.CopyToAsync(stream);
                data.photo = filename;
            }

            _context.users.Add(data);
            _context.SaveChanges();

            var output = new basicresponse
            {
                status = "Success",
                message = "Data Inserted Successfully",
                data = data
            };
            return Ok(output);

        }

        [HttpGet("showusers")]
        public IActionResult showusers()
        {
            var data = _context.users.ToList();
            return Ok(data);
        }

        [HttpGet("singleuser/{id}")]
        public IActionResult singleuser(int id)
        {
            var data = _context.users.Find(id); 
            return Ok(data);
        }

        [HttpDelete("deleteuser/{id}")]
        public IActionResult deleteuser(int id)
        {
            var data = _context.users.Find(id);
            _context.users.Remove(data);
            _context.SaveChanges();

            var output = new basicresponse
            {
                status = "Success",
                message = "Data Deleted Successfully",
                data=data
            };

            return Ok(output);
        }

        [HttpPut("updateuser")]
        public IActionResult updateuser([FromForm] users newdata)
        {
            var olddata = _context.users.Find(newdata.id);
            olddata.name = newdata.name;
            olddata.email = newdata.email;
            olddata.mobile = newdata.mobile;
            olddata.password = newdata.password;
            olddata.city = newdata.city;

            _context.users.Update(olddata);
            _context.SaveChanges();

            var output = new basicresponse
            {
                status = "success",
                message = "Data Updated Successfully",
                data = olddata
            };

            return Ok(output);
        }


        [HttpPost("login")]
        public IActionResult login([FromForm] string email, [FromForm] string password)
        {
            var data = _context.users.FirstOrDefault(x => x.email == email && x.password == password);
            if(data != null)
            {
                // login success
                var output = new basicresponse
                {
                    status = "success",
                    message = "Login Successful",
                    data = data
                };
                return Ok(output);

            }
            else
            {
                // login failed
                var output = new basicresponse
                {
                    status = "error",
                    message = "Email or Password is incorrect"
                };
                return Ok(output);
            }
        }





    }
}
