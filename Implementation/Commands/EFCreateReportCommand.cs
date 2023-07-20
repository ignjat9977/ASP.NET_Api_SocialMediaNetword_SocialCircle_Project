using Application;
using Application.Commands;
using Application.Dto;
using DataAcess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateReportCommand : ICreateReportCommand
    {
        private readonly IApplicationActor _actor;
        private readonly MyDbContext _context;
        private readonly CreateReportValidator _validator;

        public EFCreateReportCommand(IApplicationActor actor, MyDbContext context, CreateReportValidator validator)
        {
            _actor = actor;
            _context = context;
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Sending report using EF";

        public void Execute(ReportDto request)
        {
            _validator.ValidateAndThrow(request);

            _context.Reports.Add(new Domain.Report
            {
                UserId = _actor.Id,
                ReportedUserId = request.ReportedUserId,
                Description = request.Description,
                PostId = request.PostId,
            });
            _context.SaveChanges();
        }
    }
}
