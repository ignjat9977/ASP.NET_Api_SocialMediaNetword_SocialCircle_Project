using Application.Commands;
using Application.Dto;
using Application.Exceptions;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFUpdateGroupCommand : IUpdateGroupCommand
    {
        private readonly MyDbContext _context;
        private readonly CreateGroupValidator _validator;

        public EFUpdateGroupCommand(MyDbContext context, CreateGroupValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Updating group info using EF";

        public void Execute(GroupInsertDto request)
        {
            _validator.ValidateAndThrow(request);

            var group = _context.Groups.Find(request.Id);
            if (group == null)
            {
                throw new EntityNotFoundException((int)request.Id, typeof(Groupp));
            }

            group.Name = request.Name;
            group.Description = request.Description;
            group.PrivacyId = request.PrivacyId;

            _context.SaveChanges();
        }
    }
}
