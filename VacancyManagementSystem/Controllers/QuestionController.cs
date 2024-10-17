using Application.CommandHandlers.Answer;
using Application.CommandHandlers.Candidate;
using Application.CommandHandlers.Question;
using Application.DTO.Command.Candidate;
using Application.DTO.Command.Question;
using Application.DTO.Query.Candidate;
using Application.DTO.Query.Question;
using Application.QueryHandlers.Candidate;
using Application.QueryHandlers.Question;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VacancyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ISender _sender;

        public QuestionController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("GetByVacancyIdForUser")]
        public async Task<ActionResult<List<GetQuestionDto>>> GetQuestionsByVacancyIdForUser(int vacancyId)
        {
            var response = await _sender.Send(new GetQuestionsByVacancyQuery(vacancyId));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpGet("GetByVacancyIdForAdmin")]
        public async Task<ActionResult<List<GetQuestionDto>>> GetByVacancyIdForAdmin(int vacancyId)
        {
            var response = await _sender.Send(new GetQuestionsByVacancyForAdminQuery(vacancyId));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateQuestionDto question)
        {
            var response = await _sender.Send(new CreateQuestionCommand(question));

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }


        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update(UpdateQuestionDto question)
        {
            var response = await _sender.Send(new UpdateQuestionCommand(question));

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _sender.Send(new DeleteQuestionCommand(id));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }
    }
}
