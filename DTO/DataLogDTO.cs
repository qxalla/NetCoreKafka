using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.DTO
{
    public class DataLogDTO
    {
        public string Mensaje { get; set; }

    }

    public class DataLogDTOValidator : AbstractValidator<DataLogDTO>
    {
        public DataLogDTOValidator()
        {
            RuleFor(o => o.Mensaje).NotEmpty();
        }
    }
}
