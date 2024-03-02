using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            string[] studentName = new string[] { "Smruti","mansi","saumya","abhisheka" };
            return Ok(studentName);
        }
    }
}
