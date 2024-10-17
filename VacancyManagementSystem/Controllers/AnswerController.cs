using Application.CommandHandlers.Answer;
using Application.DTO.Command.Answer;
using Application.DTO.Query.Answer;
using Application.QueryHandlers.Answer;
using Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacancyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly ISender _sender;

        public AnswerController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetAnswersByQuestionIdForUser")]
        public async Task<ActionResult<List<GetAnswersForUserDto>>> GetAnswersByQuestionIdForUser(int questionId)
        {
            var response = await _sender.Send(new GetAnswersByQuestionIdForUserQuery(questionId));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpGet("GetByQuestionIdForAdmin")]
        public async Task<ActionResult<List<GetAnswersForAdminDto>>> GetByQuestionIdForAdmin(int questionId)
        {
            var response = await _sender.Send(new GetAnswersByQuestionIdForAdminQuery(questionId));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpGet("GetCandidateAnswers")]
        public async Task<ActionResult<List<VwCandidateAnswers>>> GetCandidatrAnswers(int questionId, int candidateId)
        {
            var response = await _sender.Send(new GetAnswerByQuestionIdAndCandidateIdQuery(questionId, candidateId));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpPost("AcceptAnswer")]
        public async Task<ActionResult<int>> Create(AcceptAnswerDto acceptAnswer)
        {
            var response = await _sender.Send(new AcceptAnswerCommand(acceptAnswer));

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateAnswerDto answer)
        {
            var response = await _sender.Send(new CreateAnswerCommand(answer));

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update(UpdateAnswerDto answer)
        {
            var response = await _sender.Send(new UpdateAnswerCommand(answer));

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _sender.Send(new DeleteAnswerCommand(id));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }
    }
}
