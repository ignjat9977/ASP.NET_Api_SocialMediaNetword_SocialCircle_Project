using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateGroupCommand : ICreateGroupCommand
    {
        private readonly MyDbContext _context;
        private readonly CreateGroupValidator _validator;

        public EFCreateGroupCommand(MyDbContext context, CreateGroupValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Creating Group using EF";

        public void Execute(GroupInsertDto request)
        {
            _validator.ValidateAndThrow(request);

            var group = new Groupp
            {
                Name = request.Name,
                Description = request.Description,
                PrivacyId = request.PrivacyId
            };

            _context.Groups.Add(group);
            _context.SaveChanges();
        }

    }
}
