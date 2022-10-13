using Data.Models;
using OnlineCatalogue.DTOs;
using TemaLab19.DTOs;

namespace TemaLab19.Extensions
{
    public static class DtoToEntityExtensions
    {
        public static Student ToEntity(this StudentToCreate studentToCreate) =>
            new Student
            {
                FirstName = studentToCreate.FirstName,
                LastName = studentToCreate.LastName,
                Age = studentToCreate.Age
            };
        public static Student ToEntity(this StudentToUpdate studentToCreate) =>
           new Student
           {
               FirstName = studentToCreate.FirstName,
               LastName = studentToCreate.LastName,
               Age = studentToCreate.Age
           };

        public static Address ToEntity(this AddressToUpdate addressToUpdate) =>
            new Address
            {
                City = addressToUpdate.City,
                Street = addressToUpdate.Street,
                Number = addressToUpdate.Number
            };

        public static Teacher ToEntity(this TeacherToCreate teacherToCreate) =>
            new Teacher
            {
                Name = teacherToCreate.Name,
                Rank = teacherToCreate.Rank
            };

    }
}
