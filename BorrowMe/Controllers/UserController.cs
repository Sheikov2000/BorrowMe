using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using BorrowMe.Models;
using BorrowMe.Repositories;


namespace BorrowMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{firebaseId}")]
        public IActionResult GetByFirebaseUserId(string firebaseId)
        {
            var user = _userRepository.GetByFirebaseUserId(firebaseId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [HttpGet("DoesUserExist/{firebaseId}")]
        public IActionResult DoesUserExist(string firebaseId)
        {
            var user = _userRepository.GetByFirebaseUserId(firebaseId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpGet("users/{id}")]
        public IActionResult GetUserById(int id)
        {

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [HttpPost]
        public IActionResult Post(User user)
        {
            _userRepository.AddUser(user);
            return CreatedAtAction(
                nameof(GetByFirebaseUserId), new { firebaseId = user.FirebaseId }, user);
        }

   

        [HttpPut("{id}")]
        public IActionResult Edit(int id, User user)
        {
            _userRepository.UpdateUser(user);
            return NoContent();

        
        }

        [HttpGet("Me")]
        public IActionResult Me()
        {
            var user = GetCurrentUserProfile();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        private User GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}