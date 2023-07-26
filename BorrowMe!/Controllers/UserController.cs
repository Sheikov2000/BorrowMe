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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetByFirebaseUserId(string firebaseUserId)
        {
            var user = _userRepository.GetByFirebaseUserId(firebaseUserId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var user = _userRepository.GetByFirebaseUserId(firebaseUserId);

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
                nameof(GetByFirebaseUserId), new { firebaseUserId = user.FirebaseUserId }, user);
        }

        private User GetCurrentUserProfile()
        {
            var firebaseId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepository.GetByFirebaseUserId(firebaseId);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, User user)
        {
            _userRepository.UpdateUser(user);
            return NoContent();

        }

    }
}