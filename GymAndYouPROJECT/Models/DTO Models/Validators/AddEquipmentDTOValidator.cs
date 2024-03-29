﻿using FluentValidation;
using GymAndYou.Entities;
using GymAndYou.StaticData;

namespace GymAndYou.DTO_Models.Validators
{
    public class AddEquipmentDTOValidator: AbstractValidator<UpsertEquipmentDTO>
    {
        public AddEquipmentDTOValidator()
        {
            RuleFor(p => p.BodyPart)
                .NotEmpty()
                .NotNull()
                .Custom((value,context) => 
                {
                    if(!Static.BodyParts.Contains(value))
                    {
                        context.AddFailure("BodyParts", $"Sorry but system accept only data in [ {String.Join(',', Static.BodyParts)} ] ");
                    }
                });

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(p => p.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(255);


            RuleFor(p => p.MaxWeight)
                .NotEmpty()
                .NotNull();
                
        }
    }
}
