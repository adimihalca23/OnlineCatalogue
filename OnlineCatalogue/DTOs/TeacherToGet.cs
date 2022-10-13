namespace OnlineCatalogue.DTOs
{
    public class TeacherToGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public AddressToGet Address { get; set; }
        public SubjectToGet Subject { get; set; }
    }
}
