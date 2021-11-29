using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("getbyemail")]
        public IActionResult GetByEmail(UserLoginDto model)
        {
            var result = _userService.GetDataByMail(model.Email);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
