﻿using AutoMapper;
using myAPI.Contracts;
using myAPI.Contracts.MessageBroker.EventBus;
using myAPI.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IEventBus eventBus)
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, loggerManager, mapper, eventBus));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, loggerManager, mapper));
        }
        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}
