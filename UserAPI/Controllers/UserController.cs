using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using UserAPI.Models.Users;
using UserAPI.Models.Users.Exceptions;
using UserAPI.Services.Users;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService) =>
            this.userService = userService;

        [HttpPost]
        public async ValueTask<ActionResult<User>> PostUserAsync(User user)
        {
            try
            {
                User addedUser =
                    await this.userService.AddUserAsync(user);

                return Created(addedUser);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when (userDependencyValidationException.InnerException is InvalidUserReferenceException)
            {
                return FailedDependency(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
               when (userDependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<User>> GetAllUsers()
        {
            try
            {
                IQueryable<User> users = this.userService.RetrieveAllUsers();

                return Ok(users);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }


        [HttpPut]
        public async ValueTask<ActionResult<User>> PutUserAsync(User user)
        {
            try
            {
                User modifiedUser =
                    await this.userService.ModifyUserAsync(user);

                return Ok(modifiedUser);
            }
            catch (UserValidationException userValidationException)
                when (userValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userValidationException.InnerException);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
               when (userDependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }

        [HttpDelete("{userId}")]
        public async ValueTask<ActionResult<User>> DeleteUserByIdAsync(Guid userId)
        {
            try
            {
                User deletedUser =
                    await this.userService.RemoveUserByIdAsync(userId);

                return Ok(deletedUser);
            }
            catch (UserValidationException userValidationException)
                when (userValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userValidationException.InnerException);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when (userDependencyValidationException.InnerException is LockedUserException)
            {
                return Locked(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                return BadRequest(userDependencyValidationException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }
    }
}