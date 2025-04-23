using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Feature.Files.Commands
{
    internal class CreateFileInDbCommand
    {
    }

    /*
     
     public class CreateStudentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Standard { get; set; }
        public int Rank { get; set; }
        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
        {
            private readonly IAppDbContext context;
            public CreateStudentCommandHandler(IAppDbContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
            {
                var student = new Student();
                student.Name = command.Name;
                student.Standard = command.Standard;
                student.Rank = command.Rank;
 
                context.Students.Add(student);
                await context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
     
     */
}
