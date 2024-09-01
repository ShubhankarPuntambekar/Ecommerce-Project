using API.DTOs;
using CORE.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController:BaseApiController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthrized()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not a proper response");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is an internal error");
        }

        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDtos product)
        {
            return Ok();
        }

    }
}
