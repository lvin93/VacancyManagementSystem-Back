using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Queries;
using Domain.Views;

namespace Application.QueryHandlers.Candidate
{
    public record GetAllCandidatesResponse(IEnumerable<VwCandidate> Candidates);
    public record GetAllCandidatesQuery() : IQuery<IResult<GetAllCandidatesResponse, DomainError>>;

    public class GetCandidatesQueryHandler : IQueryHandler<GetAllCandidatesQuery, IResult<GetAllCandidatesResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCandidatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetAllCandidatesResponse, DomainError>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _unitOfWork.ReadonlyView.GetCandidates();
            return Result.Success<GetAllCandidatesResponse, DomainError>
               (new GetAllCandidatesResponse(
                   candidates.ToList()
               ));
        }
    }
}
