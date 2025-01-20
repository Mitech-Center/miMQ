using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using myAPI.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }
        /// <summary>
        /// Liệt kê danh sách Công ty
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCompanies()
        {
            //throw new Exception("Exception");
            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

        }
        [HttpGet("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id,false);
            return Ok(company);
        }
    }
}
