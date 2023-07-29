using Microsoft.AspNetCore.Mvc;
using SnapShop.Models;
using SnapShop.Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;

namespace SnapShop.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentsController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment?>> Get(Guid id)
        {
            var entity = await _assignmentRepository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<List<Assignment>> GetAll()
        {
            return Ok(_assignmentRepository.GetAllNew());
        }

        [HttpPost]
        public async Task<ActionResult<Assignment>> Create(Assignment entity)
        {
            await _assignmentRepository.Create(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Assignment entity)
        {
            var existingEntity = await _assignmentRepository.GetById(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            // Do changes

            await _assignmentRepository.Update(existingEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingEntity = await _assignmentRepository.GetById(id);
            if (existingEntity == null)
                return NotFound();

            await _assignmentRepository.Delete(id);
            return NoContent();
        }
    }
}
