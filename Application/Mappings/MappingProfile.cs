using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.DTO.Command.Answer;
using Application.DTO.Command.Candidate;
using Application.DTO.Command.Question;
using Application.DTO.Command.Vacancy;
using Application.DTO.Command.VacancyGroup;
using Application.DTO.Query.Answer;
using Application.DTO.Query.Candidate;
using Application.DTO.Query.Question;
using Application.DTO.Query.Vacancy;
using Application.DTO.Query.VacancyGroup;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Vacancy

            CreateMap<Vacancy, GetVacancyDto>().ReverseMap();
            CreateMap<CreateVacancyDto, Vacancy>().ReverseMap();
            CreateMap<UpdateVacancyDto, Vacancy>().ReverseMap();

            #endregion

            #region VacancyGroup

            CreateMap<VacancyGroup, GetVacancyGroupDto>().ReverseMap();
            CreateMap<CreateVacancyGroupDto, VacancyGroup>().ReverseMap();
            CreateMap<UpdateVacancyGroupDto, VacancyGroup>().ReverseMap();

            #endregion

            #region Candidate

            CreateMap<Candidate, GetCandidateDto>().ReverseMap();
            CreateMap<CreateCandidateDto, Candidate>().ReverseMap();
            CreateMap<UpdateCandidateDto, Candidate>().ReverseMap();

            #endregion

            #region Question

            CreateMap<Question, GetQuestionDto>().ReverseMap();
            CreateMap<CreateQuestionDto, Question>().ReverseMap();
            CreateMap<UpdateQuestionDto, Question>().ReverseMap();

            #endregion

            #region Answer

            CreateMap<AnswerOption, GetAnswersForUserDto>().ReverseMap();
            CreateMap<AnswerOption, GetAnswersForAdminDto>().ReverseMap();
            CreateMap<CandidateAnswer, AcceptAnswerDto>().ReverseMap();
            CreateMap<CreateAnswerDto, AnswerOption>().ReverseMap();
            CreateMap<UpdateAnswerDto, AnswerOption>().ReverseMap();

            #endregion

        }
    }
}
