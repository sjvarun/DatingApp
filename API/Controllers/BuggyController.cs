using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public BuggyController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> AuthValidation()
        {
            return "Secret Key";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> UserNotFound()
        {
            var user = _dataContext.Users.Find(-1);
            if (user == null) { return NotFound("User not found"); }
            else { return user; }
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var user = _dataContext.Users.Find(-1);
            return user.ToString();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return new BadRequestResult();
        }
    }
}
