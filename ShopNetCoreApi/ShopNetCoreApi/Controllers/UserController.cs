using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopNetCoreApi.Data.Infrastructure;
using ShopNetCoreApi.Models.Entities;

namespace ShopNetCoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetUser")]
        public ActionResult<IEnumerable<AppUser>> GetUseResult()
        {
            var user = _unitOfWork.UserRepositories.GetAll();
            return Ok(user);

        }
    }
}
