using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Entities.Exceptions
{
    public sealed class CompanyNotFoundException:NotFoundException
    {
        public CompanyNotFoundException(Guid companyId)
            :base($"Méo tìm thấy công ty {companyId}")
        {
            
        }
    }
}
