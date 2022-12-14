using Data.Models;
using OnlineCatalogue.DTOs;

namespace OnlineCatalogue.Extensions
{
    public static class EntityToDtoExtensions
    {       
        public static StudentToGet ToDto(this Student student)
        {
            if (student == null)
            {
                return null;
            }
            StudentToGet dto = new StudentToGet();
            dto.Id = student.Id;
            dto.FirstName = student.FirstName;
            dto.LastName = student.LastName;
            dto.Age = student.Age;
            dto.Address = student.Address.ToDto();

            return dto;
        }
        public static AddressToGet ToDto(this Address address)
        {
            if (address == null)
            {
                return null;
            }

            return new AddressToGet
            {
                City = address.City,
                Street = address.Street,
                Number = address.Number
            };
        }

        public static SubjectToGet ToDto(this Subject subject)
        {
            if (subject == null)
                return null;

            return new SubjectToGet
            {
                Id = subject.Id,
                Name = subject.Name,
                TeacherId = subject.TeacherId
            };
        }

        public static MarkToGet ToDto(this Mark mark)
        {
            if (mark == null)
                return null;

            return new MarkToGet
            {
                Id = mark.Id,
                CreationDate = mark.CreationDate,
                StudentId = mark.StudentId,
                SubjectId = mark.SubjectId,
                Value = mark.Value
            };
        }

        public static TeacherToGet ToDto(this Teacher teacher)
        {
            if (teacher == null)
            {
                return null;
            }

            TeacherToGet dto = new TeacherToGet();
            dto.Id = teacher.Id;
            dto.Name = teacher.Name;
            dto.Rank = teacher.Rank;
            dto.Subject = teacher.Subject.ToDto();
            dto.Address = teacher.Address.ToDto();

            return dto;
        }

        public static StudentWithAverageToGetDto ToDtoAverage(this Student student)
        {
            StudentWithAverageToGetDto dto = new StudentWithAverageToGetDto();
            dto.Id = student.Id;
            dto.Name = student.FirstName + student.LastName;
            dto.Age = student.Age;
            dto.Average = student.Marks.Average(m => m.Value);

            return dto;
        }
    }
}
