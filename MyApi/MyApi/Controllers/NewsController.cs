using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MyApi.Repository;
using MyApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MyApi.MyContext;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class NewsController : ControllerBase
    {
        private IRepository<News> _NewsRepo;

        private readonly AppSettings _appSettings;

        private UserManager<User> _userManager;
        private SignInManager<User> _singInManager;

        
        public NewsController(IRepository<News> newsRepo, UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AppSettings> appSettings)
        {
            _NewsRepo = newsRepo;

            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;

        }


        #region Authentication

        [HttpPost]
        [Route("Register")]
        //POST : /api/News/Register
        public async Task<Object> PostUser(NewUserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/News/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }

        #endregion



        #region CRUD
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<News>> Get()
        {
            return Ok(_NewsRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<News> Get(int id)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            var news = _NewsRepo.GetById(id);

            if (news == null)
            {

                return NotFound();
            }

            return Ok(news);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<News> Post([FromBody] News news)
        {
            try
            {
                _NewsRepo.Create(news);
            }
            catch (Exception e)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    throw;
                }
            }

            return Ok();

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<News> Put(int id, [FromBody] News news)
        {

            try
            {
                _NewsRepo.Edit(news);
            }
            catch (Exception e)
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }

                if (id != news.ID)
                {

                    return BadRequest();
                }



                if (news == null)
                {

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<News> Delete(int id)
        {

            try
            {
                _NewsRepo.Delete(id);
            }


            catch (Exception e)
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        #endregion
        

    }
}