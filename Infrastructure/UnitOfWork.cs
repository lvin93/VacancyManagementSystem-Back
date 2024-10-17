using Application;
using Application.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Concrete;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VacancyManagementDbContext _context;
        private IVacancyRepository _vacancy;
        private IVacancyGroupRepository _vacancyGroup;
        private ICandidateRepository _candidate;
        private IQuestionRepository _question;
        private IAnswerRepository _answer;
        private ICandidateAnswerRepository _candidateAnswer;
        private ICandidateVacancyRepository _candidateVacancy;
        private IFileRepository _file;
        private IReadonlyRepository _readonlyView;


        public UnitOfWork(VacancyManagementDbContext context)
        {
            _context = context;
        }

        public IVacancyRepository Vacancy => _vacancy ?? new VacancyRepository(_context);
        public IVacancyGroupRepository VacancyGroup => _vacancyGroup?? new VacancyGroupRepository(_context);
        public ICandidateRepository Candidate => _candidate?? new CandidateRepository(_context);
        public IQuestionRepository Question => _question?? new QuestionRepository(_context);
        public IAnswerRepository Answer => _answer?? new AnswerRepository(_context);
        public ICandidateAnswerRepository CandidateAnswer => _candidateAnswer?? new CandidateAnswerRepository(_context);    
        public ICandidateVacancyRepository CandidateVacancy => _candidateVacancy?? new CandidateVacancyRepository(_context);
        public IFileRepository File => _file?? new FileRepository(_context);

        public IReadonlyRepository ReadonlyView => _readonlyView?? new ReadonlyRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
