using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuiZone.API.Filters;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.DataAccess.Models.Abstractions;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Int32;

namespace QuiZone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public abstract class BaseController<TEntity, TEntityDTO> : ControllerBase
        where TEntity : class, IEntity, new()
        where TEntityDTO : class
    {

        private readonly ICrudService<TEntityDTO, TEntity> baseService;
        protected readonly IMapper mapper;

        public BaseController(IMapper mapper, ICrudService<TEntityDTO, TEntity> baseService)
        {
            this.mapper = mapper;
            this.baseService = baseService;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await baseService.GetAsync(id);

            return result != null
                ? Ok(result)
                : (IActionResult)NotFound();
        }


        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntityDTO obj)
        {
            var result = await baseService.InsertAsync(obj);
            var dto = mapper.Map<TEntity, TEntityDTO>(result);

            return result == null
               ? (IActionResult)BadRequest()
                : Created($"api/[controller]/{result.Id}", dto);
        }

        [ServiceFilter(typeof(UpdateExceptionFilterAttribute))]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] TEntityDTO obj)
        {
            var result = await baseService.UpdateAsync(id, obj);

            return result == null
                ? (IActionResult)NotFound()
                : Ok(result);
        }


        [ServiceFilter(typeof(DeleteExceptionFilterAttribute))]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var result = await baseService.RemoveAsync(id);

            return result == null
                ? (IActionResult)NotFound()
                : Ok(result);
        }


        protected int GetAuthUserId()
        {
            try
            {
                return Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            catch
            {
                return -1;
            }
        }
    }
}