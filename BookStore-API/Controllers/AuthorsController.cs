using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.DTOs;
using BookStore_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Authors in the book store's database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _AuthorRepository;
        private readonly ILoggerService _Logger;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, ILoggerService logger, IMapper mapper)
        {
            this._AuthorRepository = authorRepository;
            this._Logger = logger;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get All Authors
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of Authors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                _Logger.LogInfo("Attempted Get All Authors");
                var authors = await _AuthorRepository.FindAll();
                var response = _mapper.Map<IList<AuthorDTO>>(authors);
                _Logger.LogInfo("Response has all authors");
                return Ok(response);
            }
            catch (Exception ex)
            {
                InternalError(ex.ToString() + " - " + ex.InnerException);
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Get an author by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>An autho's record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthor(int Id)
        {
            try
            {
                _Logger.LogInfo($"Getting author for Id: {Id}");
                var author = await _AuthorRepository.FindById(Id);
                if (author == null)
                {
                    _Logger.LogWarning($"Author with id: {Id} was not found!");
                    return NotFound();
                }
                var response = _mapper.Map<AuthorDTO>(author);
                _Logger.LogInfo($"Response has returned author for Id: {Id}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                InternalError(ex.ToString() + " - " + ex.InnerException);
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Creates an author in the database
        /// </summary>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                
                if (authorDTO == null)
                {
                    _Logger.LogWarning($"Empty request submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarning($"Request was incomplete");
                    return BadRequest(ModelState);
                }

                _Logger.LogInfo($"Creating new author{ authorDTO.Firstname + " " + authorDTO.Lastname}");
                var author =  _mapper.Map<Author>(authorDTO);
                var isSuccess = await _AuthorRepository.Create(author);

                if (!isSuccess)
                {
                    return InternalError($"Author creation failed for author: {author.Firstname + " " + author.Lastname}");
                }
                _Logger.LogInfo($"Author: { author.Firstname + " " + author.Lastname} has been created!");
                return Created("Create", new { author });
            }
            catch(Exception e)
            {
                InternalError(e.ToString() + " - " + e.InnerException);
                return StatusCode(500, e.Message);
            }
        }

        private ObjectResult InternalError(string message)
        {
            _Logger.LogError(message);
            return StatusCode(500, $"There was an internal error.  Contact the administrator with this error: {message}");

        }
    }
}
