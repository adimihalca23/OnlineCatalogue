using Data;
using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalogue.DTOs;
using OnlineCatalogueWEB.DTOs;
using TemaLab19.DTOs;
using TemaLab19.Extensions;

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

        /// <summary>
        /// Deletes a subject based on an id
        /// </summary>
        /// <param name="subjectId">subject id</param>
        [HttpDelete("remove-{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult DeleteSubject([FromRoute] int subjectId)
        {
            dataLayer.DeleteSubject(subjectId);
            return Ok();
        }

        /// <summary>
        /// Creates a teacher based on the provided data
        /// </summary>
        /// <param name="teacherToCreate">Teacher data</param>
        /// <returns>Created student data</returns>
        [HttpPost("add-teacher")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherToCreate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult CreateTeacher([FromBody] TeacherToCreate teacherToCreate)
        {
            return Ok(dataLayer.CreateTeacher(teacherToCreate.ToEntity()).ToDto());
        }

        /// <summary>
        /// Deletes a teacher based on an id
        /// </summary>
        /// <param name="teacherId">teacher id</param>
        [HttpDelete("remove-{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult DeleteTeacher([FromRoute] int teacherId)
        {
            dataLayer.DeleteTeacher(teacherId);
            return Ok();
        }

        /// <summary>
        /// Updates or creates a teache's address information
        /// </summary>
        /// <param name="teacherId">teacher id</param>
        /// <param name="newAddress">new address</param>
        [HttpPut("update-{teacherId}/address")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult ChangeTeacherAdress([FromRoute] int teacherId, [FromBody] AddressToUpdate newAddress)
        {
            try
            {
                dataLayer.ChangeTeacherAddress(teacherId, newAddress.ToEntity());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Add a subject to a specific teacher
        /// </summary>
        /// <param name="teacherId">teacher id</param>
        /// <param name="subjectId">subject id</param>
        [HttpPost("assign-teacher")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult AssignTeacherToSubject([FromQuery] int teacherId, [FromQuery] int subjectId)
        {
            try
            {
                dataLayer.AssignTeacherToSubject(teacherId, subjectId);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Promote a teacher
        /// </summary>
        /// <param name="teacherId">teacher id</param>
        [HttpPost("promote-teacher")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult PromoteTeacher([FromQuery] int teacherId)
        {
            try
            {
                dataLayer.PromoteTeacher(teacherId);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
    }
}
