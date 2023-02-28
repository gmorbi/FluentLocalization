using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentLocalization.Domain.Commands;
using FluentLocalization.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using FluentLocalization.Domain.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Resources;
using System.Reflection;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FluentLocalization.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IValidator<User> _validator;

        public UserController(IValidator<User> validator)
        {
            _validator = validator;
        }

        [HttpPut("validauser/{id}")]
        public async Task<IActionResult> PutUser(
            int id,
            [FromBody] UserRequest user,
            [FromServices] IStringLocalizerFactory factory)
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            var userModel = new User()
            {
                Id = id,
                Name = user.Name,
                Gender = user.Gender,
                Age = user.Age
            };

            var result = _validator.Validate(userModel);

            if (!result.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
            
            return StatusCode(StatusCodes.Status200OK, "Valid");
        }
    }
}

