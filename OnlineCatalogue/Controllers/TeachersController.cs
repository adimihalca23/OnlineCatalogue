using Data.Exceptions;
using Data;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalogue.DTOs;
using OnlineCatalogue.Extensions;

namespace OnlineCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : Controller
    {
        private readonly DataLayer dataLayer;
        public TeachersController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        /// <summary>
        /// Creates a teacher based on the provided data
        /// </summary>
        /// <param name="teacherToCreate">Teacher data</param>
        /// <returns>Created teacher data</returns>
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Get all marks of a student given by a teacher
        /// </summary>
        /// <param name="teacherId">teacher Id</param>
        /// <returns>List of marks data</returns>
        [HttpGet("get-marks-by-teacher")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarkToGet>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarks([FromQuery] int teacherId)
        {
            try
            {
                dataLayer.GetMarksByTeacher(teacherId).Select(m => m.ToDto());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(dataLayer.GetMarksByTeacher(teacherId).Select(m => m.ToDto()));
        }
    }
}
