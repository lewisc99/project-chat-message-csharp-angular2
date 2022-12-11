using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.Helpers.Contants;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Models.dto;
using TalkToApiStudyTest.Helpers.Token;
using TalkToApiStudyTest.V1.Services.Contracts;

#pragma warning disable
namespace TalkToApiStudyTest.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Produces(CustomMediaType.Hateoas, CustomMediaType.returnXML, CustomMediaType.returnJSON)]
    [EnableCors(PolicyName = "anyMethod")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenRepository;
        private readonly IMapper _mapper;
        public UserController(IUserService userRepository, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, ITokenService tokenRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }
        
        [HttpGet("",Name = "GetAll")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> GetAll([FromHeader(Name ="Accept")] string mediaType)
        {
            List<ApplicationUser> usersAppUser = _userManager.Users.ToList();

            if (mediaType == CustomMediaType.Hateoas)
            {
                var listUsersDTO = _mapper.Map<List<ApplicationUser>, List<UserDTO>>(usersAppUser);

                foreach (var list in listUsersDTO)
                {
                    list.links.Add(new LinkDTO("_self", Url.Link("Get", new { id = list.Id }), "GET"));
                }

                var result = new ListDTO<UserDTO> { Result = listUsersDTO };
                result.links.Add(new LinkDTO("_self", Url.Link("GetAll", new { }), "GET"));

                return Ok(result);
            }

            else
            {
                List<UserDTOSemHyperlink> userSemHyperLink = _mapper.Map<List<ApplicationUser>, List<UserDTOSemHyperlink>>(usersAppUser);
                return Ok(userSemHyperLink);
            }
        }

        [HttpGet("{id}", Name = "Get")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Get(string id)
        {
            ApplicationUser userById = await _userRepository.Get(id);

            if (userById == null)
            {
               return NotFound();
            }

            UserDTO userDTO = _mapper.Map<ApplicationUser, UserDTO>(userById);
            userDTO.links.Add(new LinkDTO("_self", Url.Link("Get", new { Id = id }), "GET"));
            userDTO.links.Add(new LinkDTO("_Update", Url.Link("Update", new { Id = id }), "GET"));

            return Ok(userDTO);
        }

        [HttpPost("login")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user != null)
                {
                    var token = CreateToken.BuildToken(user);
                    var tokenModel = new Token()
                    {
                        RefreshToken = token.RefreshToken,
                        ExpirationRefreshToken = token.ExpirationRefreshToken,
                        ExpirationToken = token.Expiration,
                        User = user,
                        Created = DateTime.Now,
                        Utilized = false
                    };

                    _tokenRepository.Register(tokenModel);

                    return Ok(token);
                }
                ModelState.AddModelError(nameof(loginDTO.Email), "Invalid user or password");
            }
            return Unauthorized();
        }

        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        [HttpPost("", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerDTO,
           [FromHeader(Name = "Accept")] string mediaType)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = registerDTO.Name;
                user.Email = registerDTO.Email;
                user.FullName = registerDTO.Name;
                user.Slogan = registerDTO.Slogan;

                IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (!result.Succeeded)
                {
                    List<string> errors = new List<string>();
                    
                    foreach(var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }
                    return UnprocessableEntity(errors);
                }
                else
                {
                    if (mediaType == CustomMediaType.Hateoas)
                    {

                        UserDTO userDTO = transformToCustomMediaTypeHateaos(user);

                        return Ok(userDTO);
                    }

                    else
                    {
                       UserDTOSemHyperlink userSemHyperLink = _mapper.Map<ApplicationUser,UserDTOSemHyperlink>(user);
                       
                       return Ok(userSemHyperLink);
                    }
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }


        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await  _signInManager.SignOutAsync();
            object logout = new { logout = true };
            return Ok(logout);
        }



        [MapToApiVersion("1.0")]
        [HttpPost("renovate")]

        public async Task<ActionResult> Renovate([FromBody] TokenDTO tokenDTO)
        {

            Token oldToken = await _tokenRepository.Get(tokenDTO.RefreshToken);

            if (oldToken == null)
            {
                return NotFound();
            }

            oldToken.Updated = DateTime.Now;
            oldToken.Utilized = true;
            _tokenRepository.Update(oldToken);


            ApplicationUser user = await _userRepository.Get(oldToken.UserId);
            TokenDTO newToken = CreateToken.BuildToken(user);
            Token tokenModel = new Token(newToken.RefreshToken, user, false, newToken.Expiration, newToken.ExpirationRefreshToken, DateTime.Now);

            _tokenRepository.Register(tokenModel);

            return Ok(newToken);

        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}",Name = "Update")]
        public async Task<ActionResult> Update(string id, [FromBody] RegisterUser registerUser,[FromHeader(Name = "Accept")] string mediaType)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if ( user.Id != id)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
             
                convertToApplicationUser(user, registerUser);

                IdentityResult result = await _userManager.UpdateAsync(user);
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, registerUser.Password);

                if (!result.Succeeded)
                {
                    List<string> errors = new List<string>();

                    foreach ( var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }
                    return UnprocessableEntity(errors);
                }
                else
                {
                    if (mediaType == CustomMediaType.Hateoas)
                    {

                        UserDTO userDTO = transformToCustomMediaTypeHateaos(user);

                        return Ok(userDTO);
                    }
                    else
                    {
                        UserDTOSemHyperlink userSemHyperLink = _mapper.Map<ApplicationUser, UserDTOSemHyperlink>(user);

                        return Ok(userSemHyperLink);
                    }
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        private void convertToApplicationUser(ApplicationUser userApplication, RegisterUser userDTO)
        {
            userApplication.UserName = userDTO.Name;
            userApplication.Email = userDTO.Email;
            userApplication.FullName = userDTO.Email;
            userApplication.Slogan = userDTO.Slogan;
        }

        private UserDTO transformToCustomMediaTypeHateaos(ApplicationUser user)
        {
            UserDTO userDTO = _mapper.Map<ApplicationUser, UserDTO>(user);

            userDTO.links.Add(new LinkDTO("_self", Url.Link("Get", new { id = user.Id }), "GET"));
            userDTO.links.Add(new LinkDTO("_Register", Url.Link("Register", new { }), "POST"));
            userDTO.links.Add(new LinkDTO("_Update", Url.Link("Update", new { id = user.Id }), "PUT"));

            return userDTO;
        }

 
    }
}
