using AutoMapper;
using myAPI.Contracts;
using myAPI.Entities.Exceptions;
using myAPI.Entities.Models;
using myAPI.Service.Contracts;
using myAPI.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Service
{
    public sealed class EmployeeService:IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper) 
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public EmployeeDto GetEmployee(Guid employeeId, bool trackChanges)
        {
            var employee = _repositoryManager.EmployeeRepository.GetEmployee(employeeId,trackChanges);
            if (employee == null) throw new EmployeeNotFoundException(employeeId);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company == null) throw new CompanyNotFoundException(companyId);
            var employees = _repositoryManager.EmployeeRepository.GetEmployees(companyId, trackChanges);
            
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }
    }
}
