namespace Application.DTO.Command.Question
{
    public class UpdateQuestionDto
    {
        public int Id { get; set; }
        public int? DifficultyLevel { get; set; }
        public string QuestionText { get; set; }
        public int VacancyId { get; set; }
    }
}
