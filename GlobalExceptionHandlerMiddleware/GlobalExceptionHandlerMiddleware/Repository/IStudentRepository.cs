using GlobalExceptionHandlerMiddleware.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalExceptionHandlerMiddleware.Repository
{
   public interface IStudentRepository
    {
        IEnumerable<Student> getAll();
    }
}
