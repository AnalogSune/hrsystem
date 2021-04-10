namespace API.Entities
{
    public class Recruitment
    {
        public int Id { get; set; }

        public string CV { get; set; }

        public string CoverLetter { get; set; }

        public string CandidateNote { get; set; }

        public int Status { get; set; }

        public string AdminNote { get; set; }
    }
}