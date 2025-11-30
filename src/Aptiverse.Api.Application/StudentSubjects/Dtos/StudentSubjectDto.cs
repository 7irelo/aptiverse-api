namespace Aptiverse.Api.Application.StudentSubjects.Dtos
{
    public record StudentSubjectDto
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public string SubjectId { get; set; }
        public int Progress { get; set; }
        public int Target { get; set; }
        public double AverageScore { get; set; }
        public int StudyHours { get; set; }
        public int AssignmentsCompleted { get; set; }
        public int UpcomingDeadlines { get; set; }
        public string Strength { get; set; }
        public string Weakness { get; set; }
        public DateTime LastActivity { get; set; }
        public string PerformanceTrend { get; set; }
        public double StudyEfficiency { get; set; }
        public double PredictedScore { get; set; }
        public double DifficultyLevel { get; set; }
        public double ConfidenceLevel { get; set; }
        public double LearningVelocity { get; set; }
        public double RetentionRate { get; set; }
    }
}
