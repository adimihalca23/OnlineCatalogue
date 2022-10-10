using Data;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalogueWEB.DTOs;

namespace OnlineCatalogueWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : Controller
    {
        private readonly DataLayer dataLayer;

        public SubjectsController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        /// <summary>
        /// Add a subject by a teacher
        /// </summary>
        /// <param name="subjectName">student id</param>
        /// <param name="teacherId">new address</param>
        /// <returns>Created subject data</returns>
        [HttpPost("add-subject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectToCreate))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult CreateSubject([FromBody] string subjectName, [FromQuery] int teacherId)
        {
            return Ok(dataLayer.AddSubject(subjectName, teacherId));
        }


    }
}
