using Application.DTO.Query.Candidate;
using AutoMapper;
using Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.Candidate
{
    public record GetCandidateResponse(GetCandidateDto Candidate);
    public record GetCandidateQuery(int Id) : IQuery<GetCandidateResponse>;

    public class GetCandidateQueryHandler : IQueryHandler<GetCandidateQuery, GetCandidateResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCandidateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCandidateResponse> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            var candidateEntity = await _unitOfWork.Candidate.GetAsync(x => x.Id == request.Id);
            var candidate = new GetCandidateDto();
            _mapper.Map(candidateEntity, candidate);
            return new GetCandidateResponse(candidate);
        }
    }
}
