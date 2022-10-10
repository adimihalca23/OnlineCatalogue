using Data.Models;

namespace Data
{
    public interface IDataLayer
    {   
        void ChangeStudentAddress(int studentId, Address newAddress);
        void ChangeStudentData(int studentId, Student newStudentData);
        Student CreateStudent(Student studentToCreate);
        void DeleteStudent(int studentId, bool deleteAddress);
        List<Student> GetAllStudents();
        Student GetStudentById(int studentId);
    }
}