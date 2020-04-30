using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.DataAccess;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuiZone.API.Controllers
{
    [Route("api/quiz")]
    [ApiController]
    public class QuizController : BaseController<Quiz, QuizDTO>
    {
        private readonly IQuizService quizService;
        public QuizController(IMapper mapper, IQuizService quizService)
            : base(mapper, quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await quizService.GetAllAsync();

            return result.Any()
                ? Ok(result)
                : (IActionResult)NotFound();
        }
        

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions(int id)
        {
            var result = await quizService.GetQuestionByQuizAsync(id);

            return result.Any()
                ? Ok(result)
                : (IActionResult)NotFound();

        }

    }
}