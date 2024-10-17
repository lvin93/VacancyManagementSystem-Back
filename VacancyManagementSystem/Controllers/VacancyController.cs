using Application.CommandHandlers.Vacancy;
using Application.DTO.Command.Vacancy;
using Application.DTO.Query.Vacancy;
using Application.QueryHandlers.Vacancy;
using Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacancyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly ISender _sender;

        public VacancyController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("Get")]
        public async Task<ActionResult<GetVacancyDto>> Get(int id)
        {
            var response = await _sender.Send(new GetVacancyQuery(id));
            return Ok(response.Vacancy);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<VwVacancy>>> GetAll()
        {
            var response = await _sender.Send(new GetAllVacanciesQuery());
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpGet("GetAllForUser")]
        public async Task<ActionResult<List<VwVacancy>>> GetAllForUser()
        {
            var response = await _sender.Send(new GetAllVacanciesForUserQuery());
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateVacancyDto vacancyDto)
        {
            var response = await _sender.Send(new CreateVacancyCommand(vacancyDto));
            return Ok(response.Id);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update(UpdateVacancyDto vacancyDto)
        {
            var response = await _sender.Send(new UpdateVacancyCommand(vacancyDto));
            return Ok(response.Id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _sender.Send(new DeleteVacancyCommand(id));
            return Ok(response.Id);
        }
    }
}
