using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketAPI.Models;

namespace MovieTicketAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAPIController : ControllerBase
    {
        private readonly MovieAPIDbContext Movieapicontroller;
        public MovieAPIController(MovieAPIDbContext movieapidbcontext)
        {
            Movieapicontroller = movieapidbcontext;
        }
        [HttpGet]
        public IEnumerable<MovieAPI> GetMovieDetails()
        {
            return Movieapicontroller.movieapi.ToList();
        }
        [HttpGet("GetMovieDetailsById")]
        public MovieAPI GetMovieDetailsById(int Id)
        {
            return Movieapicontroller.movieapi.Find(Id);
        }
        [HttpPost("InsertMovieDetails")]
        public IActionResult InsertMovieDetails([FromBody] MovieAPI movieapi)
        {
            if (movieapi.Id.ToString() != "")
            {

                Movieapicontroller.movieapi.Add(movieapi);
                Movieapicontroller.SaveChanges();
                return Ok("Movie Details Saved successfully");
            }
            else
                return BadRequest(); 
        }

        [HttpPut("UpdateMovieDeatils")]
        public IActionResult UpdateMovieDeatils([FromBody] MovieAPI movieapi)
        {
            if (movieapi.Id.ToString() != "")
            {
                Movieapicontroller.Entry(movieapi).State = EntityState.Modified;
                Movieapicontroller.SaveChanges();
                return Ok("Movie Details Updated successfully");
            }
            else
                return BadRequest();
        }

        [HttpDelete("DeleteMovieDetails")]
        public IActionResult DeleteMovieDetails(int Id)
        {
            
            var result = Movieapicontroller.movieapi.Find(Id);
            Movieapicontroller.movieapi.Remove(result);
            Movieapicontroller.SaveChanges();
            return Ok("Movie Details Deleted successfully");
        }
    }
}
