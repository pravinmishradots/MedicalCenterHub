using AutoMapper;
using ChildCare.DTOs;
using ChildCareApi.Configs;
using ChildCareApi.DTOs;
using ChildCareCore.Helper;
using ChildCareCore.Interfaces;
using ChildCareCore.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChildCareApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : BaseController

    {
        #region Service 

        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        #endregion

        #region Constructor

        public AccountController(ILogger<AccountController> logger, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtConfig> jwtConfigOption)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtConfig = jwtConfigOption.Value;


        }
        #endregion

        #region Login User


        [HttpPost]
        [Route("Login")]

        public IActionResult UserLogin([FromBody] LoginRequestDto userLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return BadRequest();
                }
                var userData = _unitOfWork.User.GetUserByEmail(userLogin.EmailId!);



                //var usertype = userData.UserRoleId;


                if (userData == null)
                {
                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return BadRequest();
                }

                var isAuthorized = CommonClass.PasswordsMatch(userData.Password!, userLogin.Password!, userData.SaltKey!);

                if (!isAuthorized)
                {
                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return BadRequest();
                }

                var userMappedData = _mapper.Map<UserDto>(userData);
                var token = GenerateToken(userData);
                userMappedData.Token = token;
                return SuccessResult(userMappedData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occur when login user:" + ex.GetBaseException().Message.ToString());
                return StatusCode(500, BaseResponseMessages.INTERNAL_SERVER_ERROR);
                throw;
            }
        }

        #endregion

 

        #region  User Register 
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult Register([FromBody] CreateUserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return BadRequest();
                }

                var userEntity = _mapper.Map<ChildCareCore.Entities.User>(model);
                userEntity.SaltKey = CommonClass.CreateSaltKey(5);
                userEntity.Password = CommonClass.CreatePasswordHash(model.Password!, userEntity.SaltKey);


                _unitOfWork.User.CreateUser(userEntity);
                _unitOfWork.Save();


                var token = GenerateToken(userEntity);
                var user = _mapper.Map<UserDto>(userEntity);
                user.Token = token;
                return SuccessResult(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occur when registering user:" + ex.GetBaseException().Message.ToString());
                return StatusCode(500, BaseResponseMessages.INTERNAL_SERVER_ERROR);
            }

        }


        #endregion

        #region Generate Token 

        private string GenerateToken(ChildCareCore.Entities.User user)
        {
            var authClaim = new List<Claim> {

            new Claim(ClaimTypes.Name,user.DisplayName!.Trim()),
            new Claim(ClaimTypes.Sid,user.UserId.ToString()),
            new Claim(ClaimTypes.Role,user.UserRoleId.ToString()!),
            new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secrete));
            var token = new JwtSecurityToken
            (
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.Now.AddMinutes(10),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

    }
}
