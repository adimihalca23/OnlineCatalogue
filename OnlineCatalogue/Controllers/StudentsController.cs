using Data;
using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalogue.DTOs;
using OnlineCatalogue.Extensions;
using System.Runtime.InteropServices;

namespace TemaLab19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataLayer dataLayer;

        public StudentsController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        /// <summary>
        /// Retrieves all students with their addresses
        /// </summary>
        /// <returns>Students list</returns>
        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGet>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllStudents()
        {
            return Ok(dataLayer.GetAllStudents().Select(s => s.ToDto()).ToList());
        }

        /// <summary>
        /// Retrieves a student based on its corresponding id
        /// </summary>
        /// <param name="studentId">student id to get</param>
        /// <returns>studednt data</returns>
        [HttpGet("get-{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetStudent([FromRoute] int studentId)
        {
            try
            {
                dataLayer.GetStudentById(studentId);
            }
            catch (EntityNotFoundException e) 
            {
                return NotFound(e.Message);
            }

            return Ok(dataLayer.GetStudentById(studentId).ToDto());
        }

        /// <summary>
        /// Creates a student based on the provided data
        /// </summary>
        /// <param name="studentToCreate">Student data</param>
        /// <returns>Created student data</returns>
        [HttpPost("add-student")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToCreate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult CreateStudent([FromBody] StudentToCreate studentToCreate)
        {
            return Ok(dataLayer.CreateStudent(studentToCreate.ToEntity()).ToDto()); 
        }

        /// <summary>
        /// Deletes a student based on an id
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <param name="deleteAddress">if true the student address will be deleted as well</param>
        [HttpDelete("remove-{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult DeleteStudent([FromRoute] int studentId, [FromQuery] bool deleteAddress)
        {
            dataLayer.DeleteStudent(studentId, deleteAddress);
            return Ok();
        }

        /// <summary>
        /// Updates a student's data
        /// </summary>
        /// <param name="studentId">the id of the student to modify</param>
        /// <param name="newStudentData">new student information</param>
        [HttpPut("update-{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult UpdateStudent([FromRoute] int studentId, [FromBody] StudentToUpdate newStudentData)
        {
            try
            {
                dataLayer.ChangeStudentData(studentId, newStudentData.ToEntity());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Updates or creates a student's address information
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <param name="newAddress">new address</param>
        [HttpPut("update-{studentId}/address")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult ChangeStudentAddress([FromRoute] int studentId, [FromBody] AddressToUpdate newAddress)
        {
            try
            {
                dataLayer.ChangeStudentAddress(studentId, newAddress.ToEntity());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Add a mark to a specific student
        /// </summary>
        /// <param name="studnetId">student id</param>
        /// <param name="markValue">mark value</param>
        /// <param name="subjectId">subject id</param>
        [HttpPost("add-mark")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult AddMarkToStudent([FromQuery] int studnetId, [FromBody] int markValue, [FromQuery] int subjectId)
        {
            try
            {
                dataLayer.AddMarkToStudent(studnetId, subjectId, markValue);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Get all marks of a student; if subject is specified then get all marks for a subject
        /// </summary>
        /// <param name="studentId">student Id</param>
        /// <param name="subjectId">subject Id</param>
        /// <returns>List of marks of a student</returns>
        [HttpGet("get-all-marks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarkToGet>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarks([FromQuery] int studentId, [FromQuery] int subjectId)
        {
            return Ok(dataLayer.GetAllMarks(studentId, subjectId).Select(m => m.ToDto()));
        }

        /// <summary>
        /// Get the average value of all marks per subject
        /// </summary>
        /// <param name="studentId">student Idd</param>
        /// <returns>List of averages per subject, of a student</returns>
        [HttpGet("get-averages-marks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<double>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAveragesPerSubject([FromQuery] int studentId)
        {
            try
            {
                return Ok(dataLayer.GetAveragesPerSubject(studentId));
            } 
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get all students ordered
        /// </summary>
        /// <param name="orderDescending">Order type</param>
        /// <returns>List of a orderd students</returns>
        [HttpGet("get-studens-ordered")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentWithAverageToGetDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllStudentsOrderd([Optional][FromQuery] bool orderDescending)
        {
            return Ok(dataLayer.GetAllStudentsOrdered(orderDescending).Select(s => s.ToDtoAverage()));
        }


    }
}
