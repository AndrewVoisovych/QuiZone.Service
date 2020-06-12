using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace QuiZone.API.Controllers
{
    [Route("api/quiz")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class QuizController : BaseController<Quiz, QuizDTO>
    {
        private readonly IQuizService quizService;
        public QuizController(IMapper mapper, IQuizService quizService)
            : base(mapper, quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await quizService.GetAllAsync();

            return result.Any()
                ? Ok(result)
                : (IActionResult)NotFound();
        }

        
        [HttpGet]
        [Route("start")]
       
        public  IActionResult StartQuiz(int id)
        {
            return Ok("DASUKA");

        }

    }
}