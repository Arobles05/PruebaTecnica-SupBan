using FluentValidation;
using Prueba.Tecnica.Web.Application.Feature.Files.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Prueba.Tecnica.Web.Application.Validatos
{
    

    public class SaveFileCommandValidator : AbstractValidator<SaveFileCommand>
    {
        private const long MaxFileSizeBytes = 10 * 1024 * 1024;  

        public SaveFileCommandValidator()
        {
            RuleFor(x => x.File).NotNull().WithMessage("El archivo es requrido.");
            RuleFor(x => x.File.Length)
                .GreaterThan(0).WithMessage("El Archivo no debe estar vacio.")
                .LessThanOrEqualTo(MaxFileSizeBytes).WithMessage("El archivo no debe de exceder los  10 MB.");

            RuleFor(x => x.File.ContentType)
                .NotEmpty().WithMessage("Content-TypeEs requerido.")
                .Must(IsSupportedContentType).WithMessage("Tipo de Archivo no soportado.");
        }

        private bool IsSupportedContentType(string contentType)
        {
             
            var allowedTypes = new[] { "image/png", "image/jpg", "application/pdf", "text/plain", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
            return allowedTypes.Contains(contentType);
        }
    }
}
