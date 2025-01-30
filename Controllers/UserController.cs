using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashSaltLogin.Models;
using HashSaltLogin.Services;
using Microsoft.AspNetCore.Mvc;

namespace HashSaltLogin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("CreateUser")]

        public bool CreateUser([FromBody]UserDTO newUser)
        {
            return _userServices.CreateUser(newUser);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]UserDTO user)
        {
            //we are storing our login return inside of our token variable to eventually check if the login was successful
            string stringToken = _userServices.Login(user);

            if(stringToken != null)
            {
                return Ok(new {Token = stringToken});
            }
            else
            {
                return Unauthorized(new {Message = "Login was unsuccessful Invalid Email or Password"});
            }

        }

    }
}