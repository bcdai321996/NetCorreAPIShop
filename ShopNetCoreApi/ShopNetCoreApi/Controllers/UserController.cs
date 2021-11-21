using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ShopNetCoreApi.Data.Infrastructure;
using ShopNetCoreApi.Extentions;
using ShopNetCoreApi.Models.Entities;
using ShopNetCoreApi.Models.ViewModel;

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
        [Extentions.Authorize]
        [HttpPost("GetUser")]
        
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUseResult()
        {
            var user =  await _unitOfWork.UserRepositories.GetAll();
            return Ok(user);

        }
        
        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public ActionResult CreateUser([FromBody] AppUser user)
        {
            byte[] passWordHash, PasswordSalt;
            _unitOfWork.UserRepositories.CreatePasswordHash(user.PassWord, out passWordHash, out PasswordSalt);
            user.PassWord = Convert.ToBase64String(passWordHash);
            user.PasswordSalt = Convert.ToBase64String(PasswordSalt);
             _unitOfWork.UserRepositories.Add(user);
            _unitOfWork.CompleteAsync();
            return Ok(new Responses("Success", "User create successfully","200"));

        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public ActionResult AuthenticateResult([FromBody] AuthenticateRequest request)
        {
            var user = _unitOfWork.UserRepositories.Authenticate(request.UserName, request.PassWord);
            if (user == null) return BadRequest(new Responses("Not Success", "User or PassWord is incorrect", "301"));
            return Ok(user);

        }
    }
}
