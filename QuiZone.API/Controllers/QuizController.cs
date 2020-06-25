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

    public class QuizController : BaseController<Quiz, QuizDTO>
    {
        private readonly IQuizService quizService;
        private readonly IQuestionService questionService;
        public QuizController(IMapper mapper, IQuizService quizService, IQuestionService questionService)
            : base(mapper, quizService)
        {
            this.quizService = quizService;
            this.questionService = questionService;
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
        [Route("start/{quizId}")]

        public async Task<IActionResult> StartQuizAsync([FromRoute] int quizId)
        {
            var result = await questionService.GetAllAsync(quizId);

            return result != null
                ? Ok(result)
                : (IActionResult)NotFound();
        }


        [HttpGet]
        [Route("end/link/{quizId}")]
        [Authorize]
        public IActionResult EndQuizAsync([FromRoute] int quizId)
        {
            var userId = base.GetAuthUserId();
            if (userId < 0)
            {
                return BadRequest("Користувача не розпізнано");
            }

            string hash = quizService.GetEndLinkHash(userId, quizId);

            return Ok(hash);
        }


        [HttpGet]
        [Route("question/count/{quizId}")]
        public async Task<IActionResult> GetCountQuestionFromQuizAsync([FromRoute] int quizId)
        {
            var quiz = await quizService.GetAsync(quizId);

            return quiz != null
                ? Ok(await questionService.GetCountQuestionFromQuiz(quizId))
                : (IActionResult)NotFound();
        }

    }
}