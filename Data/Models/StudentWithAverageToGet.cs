namespace Data.Models
{
    public class StudentWithAverageToGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Average { get; set; }

        public StudentWithAverageToGet(int id, string name, int age, double average)
        {
            Id = id;
            Name = name;
            Age = age;
            Average = average;
        }
    }
}
