using Application.CommandHandlers.Vacancy;
using Application.CommandHandlers.VacancyGroup;
using Application.DTO.Command.VacancyGroup;
using Application.DTO.Query.VacancyGroup;
using Application.QueryHandlers.Vacancy;
using Application.QueryHandlers.VacancyGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CommandHandlers.VacancyGroup.CreateVacancyGroupCommandHandler;
using static Application.QueryHandlers.VacancyGroup.GetAllVacancyGroupQueryHandler;

namespace VacancyManagementSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VacancyGroupController : ControllerBase
    {
        private readonly ISender _sender;
        public VacancyGroupController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<GetVacancyGroupDto>> Get(int id)
        {
            var response = await _sender.Send(new GetVacancyGroupQuery(id));
            return Ok(response.VacancyGroup);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetVacancyGroupDto>>> GetAll()
        {
            var response = await _sender.Send(new GetVacancyGroupsQuery());
            return Ok(response.VacancyGroups);
        }



        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateVacancyGroupDto vacancyGroupDto)
        {
            var response = await _sender.Send(new CreateVacancyGroupCommand(vacancyGroupDto));
            return Ok(response.Id);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update(UpdateVacancyGroupDto vacancyGroupDto)
        {
            var response = await _sender.Send(new UpdateVacancyGroupCommand(vacancyGroupDto));
            return Ok(response.Id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _sender.Send(new DeleteVacancyGroupCommand(id));
            return Ok(response.Id);
        }
    }
}
