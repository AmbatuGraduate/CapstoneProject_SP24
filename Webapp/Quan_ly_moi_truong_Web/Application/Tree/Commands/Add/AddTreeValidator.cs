﻿using FluentValidation;

namespace Application.Tree.Commands.Add
{
    public class AddTreeValidator : AbstractValidator<AddTreeCommand>
    {
        /// <summary>
        /// Set rule for request addTree
        /// </summary>
        public AddTreeValidator()
        {
            RuleFor(x => x.TreeCode).NotEmpty();
            RuleFor(x => x.TreeLocation).NotEmpty();
            RuleFor(x => x.BodyDiameter).NotEmpty();
            RuleFor(x => x.PlantTime).NotEmpty();
            RuleFor(x => x.CutTime).NotEmpty();
            RuleFor(x => x.IntervalCutTime).NotEmpty();
            RuleFor(x => x.TreeTypeId).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
