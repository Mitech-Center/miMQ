using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Shared.DTO
{
    public record MessageForPublish
    {
        public string Message { get; init; } = string.Empty;
    }
}
