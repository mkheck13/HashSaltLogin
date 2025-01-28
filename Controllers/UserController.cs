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

    }
}