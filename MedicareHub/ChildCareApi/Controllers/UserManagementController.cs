using AutoMapper;
using ChildCare.DTOs;
using ChildCareApi.Configs;
using ChildCareApi.DTOs;
using ChildCareApi.Models;
using ChildCareCore.Interfaces;
using ChildCareCore.Resources;
using ChildCareCore.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ChildCareApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : BaseController
    {
        #region Service 

        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        #endregion

        #region Constructor

        public UserManagementController(ILogger<AccountController> logger, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtConfig> jwtConfigOption)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtConfig = jwtConfigOption.Value;


        }
        #endregion


        #region Get All User List 

        [HttpGet]
        [Route("GetAllUserList")]
        public IActionResult GetAllUser()
        {
            try
            {
                var usersdata = _unitOfWork.User.GetAllUsers();


                var usersDto = _mapper.Map<List<RegistrationViewModel>>(usersdata);
                _logger.LogInformation("Return Data Successfully");
                return SuccessResult(usersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured :{ex.GetBaseException().Message}");
                return StatusCode(500, BaseResponseMessages.INTERNAL_SERVER_ERROR);
                throw;
            }
        }

        #endregion

        #region Get All UserList By Specification
        [HttpPost]
        [Route("GetAllUserBySpec")]
        public IActionResult GetAllUserListData([FromBody] UserSpecification specs)
        {
            try
            {
                var authUser = new AuthUser(User);
                var user = _unitOfWork.User.GetUserByUser(authUser.Id, specs);
                var pageMetaData = new
                {
                    user.CurrentPage,
                    user.HasNext,
                    user.HasPrevious,
                    user.TotalCount,
                    user.TotalPages
                };
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pageMetaData));
                var userdto = _mapper.Map<List<UserDto>>(user);

                _logger.LogInformation("Return Data successfully");
                return SuccessResult(userdto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured :{ex.GetBaseException().Message}");
                return StatusCode(500, "Internal Server Error");
                throw;
            }

        }

        #endregion

        #region Get User By UserId



     //[HttpGet("{id}", Name = "GetUserById")]

        [HttpGet]
        [Route("GetUserById")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {

                var user = _unitOfWork.User.GetUserById(id);

                var authUser = new AuthUser(User);
                if (user.UserId != authUser.Id)
                {
                    _logger.LogError("Todo does not belongs to this user");
                    return Unauthorized();
                }


                if (user != null)
                {
                    var userData = _mapper.Map<UserDto>(user);

                    _logger.LogInformation("Return Data successfully");
                    return SuccessResult(userData);
                }
                else
                {
                    _logger.LogInformation("No Data Found");
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured :{ex.GetBaseException().Message}");
                return StatusCode(500, "Internal Server Error");
                throw;
            }

        }

        #endregion

        #region  Update Records By UserId




        //[HttpPut("{id}")]
        [HttpPut]

        [Route("UpdateUserById")]

        public IActionResult UpdateUser(Guid id, UerUpdateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return BadRequest();
                }
                var userObj = _unitOfWork.User.GetUserById(id);
                if (userObj == null)
                {
                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return NotFound();
                }

                var authUser = new AuthUser(User);
                if (userObj.UserId != authUser.Id)
                {
                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return Unauthorized();
                }



                _mapper.Map(model, userObj);
                _unitOfWork.User.UpdateUser(userObj);
                _unitOfWork.Save();

                var userEntity = _mapper.Map<UserDto>(userObj);

                return SuccessResult(userEntity);
              //  return CreatedAtRoute("GetUserById", new { id = userEntity.UserId }, userEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occur when updating user:" + ex.GetBaseException().Message.ToString());
                return StatusCode(500, BaseResponseMessages.INTERNAL_SERVER_ERROR);
            }


        }
        #endregion

        #region Delete Records 

        //[HttpDelete("{id}")]
        [HttpPost]
        [Route("DeleteUserById")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return BadRequest();
                }
                var user = _unitOfWork.User.GetUserById(id);

          

                if (user == null)
                {
                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return NotFound();
                }
                var authUser = new AuthUser(User);


                if (user.UserId != authUser.Id)
                {
                    _logger.LogError(BaseResponseMessages.INVALID_USER_CREDENTIAL);
                    return Unauthorized();
                }


                _unitOfWork.User.DeleteUser(user);
                _unitOfWork.Save();
                var userEntity = _mapper.Map<RegistrationViewModel>(user);

                return SuccessResult(userEntity);
                //return Ok("Record has been deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occur when updating todo:" + ex.GetBaseException().Message.ToString());
                return StatusCode(500, BaseResponseMessages.INTERNAL_SERVER_ERROR);
            }


        }

        #endregion
    }
}
