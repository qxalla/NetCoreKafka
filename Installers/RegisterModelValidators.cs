using LogApi.Contracts;
using LogApi.DTO;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogApi.Installers
{
    public class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<PersonDTO>, PersonDTOValidator>();
            services.AddTransient<IValidator<DataLogDTO>, DataLogDTOValidator>();

        }
    }
}
