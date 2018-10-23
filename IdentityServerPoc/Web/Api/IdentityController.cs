using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerPoc.Web.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return new JsonResult("You are authenticated, you fat cow.");
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
