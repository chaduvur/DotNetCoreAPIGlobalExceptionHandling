using GlobalExceptionHandlerMiddleware.DAO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalExceptionHandlerMiddleware.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private ConcurrentDictionary<int, Student> _repository;

        public StudentRepository()
        {
            _repository = new ConcurrentDictionary<int, Student>();
            _repository.TryAdd(1, new Student() { Id = 1, name = "Ramesh" });
            _repository.TryAdd(2, new Student() { Id = 2, name = "Kumar" });
        }
       
        public IEnumerable<Student> getAll()
        {
            //throw new Exception("Exception while fetching students details.");
            throw new InvalidOperationException("Invalid operation access.");
            //return _repository.Values.AsEnumerable<Student>();
        }
    }
}
