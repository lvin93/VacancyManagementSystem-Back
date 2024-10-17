using Common.Entities;

namespace Domain.Entities
{
    public class File: AuditableEntity
    {
        public string Path { get; set; } 
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FileType { get; set; }
        public string MimeType { get; set; }
        public decimal? Size { get; set; }
        public string OriginalFileName { get; set; }
        public ICollection<CandidateVacancy> CandidateVacancy { get; set; }
    }
}
