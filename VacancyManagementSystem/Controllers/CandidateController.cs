using Application.CommandHandlers.Candidate;
using Application.DTO.Command.Candidate;
using Application.DTO.Query.Candidate;
using Application.QueryHandlers.Candidate;
using Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacancyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IWebHostEnvironment _environment;

        public CandidateController(ISender sender, IWebHostEnvironment environment)
        {
            _sender = sender;
            _environment = environment;
        }


        [HttpGet("Get")]
        public async Task<ActionResult<GetCandidateDto>> Get(int id)
        {
            var response = await _sender.Send(new GetCandidateQuery(id));
            return Ok(response.Candidate);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<VwCandidate>>> GetAll()
        {
            var response = await _sender.Send(new GetAllCandidatesQuery());
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }



        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateCandidateDto candidateDto)
        {
            var response = await _sender.Send(new CreateCandidateCommand(candidateDto));

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update(UpdateCandidateDto candidateDto)
        {
            var response = await _sender.Send(new UpdateCandidateCommand(candidateDto));
            return Ok(response.Id);
        }

        [HttpPost("AddResume")]
        public async Task<ActionResult<int>> AddResume([FromForm] IFormFile resume, [FromForm] int candidateId, [FromForm] int vacancyId)
        {
           var candidateDto= new UpdateCandidateResumeDto { CandidateId=candidateId, VacancyId=vacancyId, Resume= resume };
            var response = await _sender.Send(new UpdateCandidateResumeCommand(candidateDto, _environment.ContentRootPath));
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Error);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _sender.Send(new DeleteCandidateCommand(id));
            return Ok(response.Id);
        }

        [HttpGet("DownloadResume")]
        public async Task<ActionResult<FileContentDto>> DownloadResume(int id)
        {
            var response = await _sender.Send(new DownloadResumeQuery(id, _environment.ContentRootPath));
            var downloadedFile = File(response.File.FileContent, response.File.ContentType, response.File.FileName);
            return downloadedFile;
        }
    }
}
