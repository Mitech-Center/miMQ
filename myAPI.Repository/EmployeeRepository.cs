using myAPI.Contracts;
using myAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Repository
{
    public class EmployeeRepository:RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
            
        }

        public Employee GetEmployee(Guid employeeId, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(employeeId), trackChanges).OrderBy(e => e.Name).SingleOrDefault();


        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).ToList();
        
    }
}
