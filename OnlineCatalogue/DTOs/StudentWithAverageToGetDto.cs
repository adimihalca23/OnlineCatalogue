namespace OnlineCatalogueWEB.DTOs
{
    public class StudentWithAverageToGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Average { get; set; }

        public StudentWithAverageToGetDto(int id, string name, int age, double average)
        {
            Id = id;
            Name = name;
            Age = age;
            Average = average;
        }
    }
}
