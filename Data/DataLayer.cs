using Data.Exceptions;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataLayer
    {
        private string connectionString;

        public DataLayer(string connectionString )
        {
            this.connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            using var ctx = new CatalogueDbContext(this.connectionString);
            return ctx.Students.Include(s=>s.Address).ToList();
        }

        public Student GetStudentById(int studentId)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var student = ctx.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if (student == null)
                throw new EntityNotFoundException($"A student with an id of {studentId} was not found.");

            return student;
        }
        public Student CreateStudent(Student studentToCreate)
        {
            var student = new Student { FirstName = studentToCreate.FirstName, LastName = studentToCreate.LastName, Age = studentToCreate.Age };
            using var ctx = new CatalogueDbContext(this.connectionString);

            ctx.Add(student);
            ctx.SaveChanges();
            return student;
        }
        public void DeleteStudent(int studentId, bool deleteAddress)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var student = ctx.Students
                .Include(student => student.Address)
                .Where(s => s.Id == studentId)
                .FirstOrDefault();

            if (student == null)
            {
                return;
            }

            if (!deleteAddress)
            {
                if (student.Address != null)
                {
                    student.Address.StudentId = null;
                    student.Address = null;
                }
            }
            else
            {
                if (student.Address != null && student.Address.TeacherId == null)
                {
                    ctx.Remove(student.Address);
                }
            }

            ctx.Remove(student);
            ctx.SaveChanges();
        }
        public void ChangeStudentData(int studentId, Student newStudentData)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);
            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);

            if (student == null)
                throw new EntityNotFoundException($"A student with an id of {studentId} was not found.");

            student.FirstName = newStudentData.FirstName;
            student.LastName = newStudentData.LastName;
            student.Age = newStudentData.Age;

            ctx.SaveChanges();
        }
        public void ChangeStudentAddress(int studentId, Address newAddress)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);
            
            var student = ctx.Students.Include(s=>s.Address).FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new EntityNotFoundException($"A student with an id of {studentId} was not found.");

            if (student.Address==null)
            {
                student.Address = new Address();
            }

            student.Address.City = newAddress.City;
            student.Address.Street = newAddress.Street;
            student.Address.Number = newAddress.Number;

            ctx.SaveChanges();
        }



        public Subject AddSubject(string subjectName, int teacherId)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var subject = new Subject { Name = subjectName, TeacherId = teacherId };

            ctx.Subjects.Add(subject);
            ctx.SaveChanges();

            return subject;
        }

        public void AddMarkToStudent(int studentId, int subjectId, int markValue)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var student = ctx.Students.FirstOrDefault(x => x.Id == studentId);
            if (student == null)
                throw new EntityNotFoundException($"A student with an id of {studentId} was not found.");

            student.Marks.Add(new Mark { Value = markValue, CreationDate = DateTime.UtcNow, StudentId = studentId, SubjectId = subjectId });

            ctx.SaveChanges();
        }

        public List<Mark> GetAllMarks(int studentId, int? subjectId)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var student = ctx.Students.Include(s => s.Marks).FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new EntityNotFoundException($"A student with an id of {studentId} was not found.");

            if (subjectId == null)
            {
                return student.Marks.ToList();
            }
            else
            {
                return student.Marks.Where(m => m.SubjectId == subjectId).ToList();
            }
        }

        public List<AverageForSubject> GetAveragesPerSubject(int studentId)
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var student = ctx.Students.Include(s => s.Marks).FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new EntityNotFoundException($"A student with an id of {studentId} was not found.");

            return student.Marks.GroupBy(m => m.SubjectId).Select(g => new AverageForSubject { SubjectId = g.Key, Average = g.Average( m => m.Value)}).ToList();
        }

        public List<StudentWithAverageToGet> GetAllStudentsOrdered(bool? orderDescending )
        {
            using var ctx = new CatalogueDbContext(this.connectionString);

            var allStudentsWithMarks = ctx.Students.Include(s => s.Marks).ToList();
            List<StudentWithAverageToGet> result = new List<StudentWithAverageToGet>();

            if ((bool)orderDescending)
            {
                result = allStudentsWithMarks.OrderByDescending(s => s.Marks.Average(m => m.Value)).Select(s => new StudentWithAverageToGet(
                    s.Id,
                    s.FirstName + s.LastName,
                    s.Age, 
                    s.Marks.Average(m => m.Value))).ToList();
            }
            else
            {
                result = allStudentsWithMarks.OrderBy(s => s.Marks.Average(m => m.Value)).Select(s => new StudentWithAverageToGet(
                    s.Id,
                    s.FirstName + s.LastName,
                    s.Age,
                    s.Marks.Average(m => m.Value))).ToList();
            }

            return result;
        }
    }
}
