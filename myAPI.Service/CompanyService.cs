using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using myAPI.Contracts;
using myAPI.Contracts.MessageBroker.EventBus;
using myAPI.Entities.Exceptions;
using myAPI.Service.Contracts;
using myAPI.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Service
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IEventBus eventBus)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            _repositoryManager.Save();
            var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;

        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
        public async Task<string> Handle(string message)
        {
            await _eventBus.PublishAsync(
                new MessageForPublish
                {
                    Message = message
                });
            return "Ok";
        }
    }
}
