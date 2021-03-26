#region Copyright
// openLAMA is an open source platform which has been developed by the
// Swiss Kanton Basel Landschaft, with the goal of automating and managing
// large scale Covid testing programs or any other pandemic/viral infections.

// Copyright(C) 2021 Kanton Basel Landschaft, Switzerland
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// See LICENSE.md in the project root for license information.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see https://www.gnu.org/licenses/.
#endregion

using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elyon.Fastly.Api.Validators
{
    public class OrganizationDtoValidator : AbstractValidator<OrganizationDto>
    {
        public OrganizationDtoValidator()
        {
            RuleFor(x => x.Contacts)
                .NotEmpty()
                .WithMessage("You should add at least one contact");

            RuleFor(x => x.Contacts.Select(e => e.Email))
                .Must(e => CheckForDuplicates(e))
                .WithMessage("There shouldn't be duplicated emails");

            RuleFor(x => x.Contacts.Select(e => e.LandLineNumber))
                .Must(e => CheckForNullableValue(e))
                .WithMessage("There shouldn't be null values for land line numbers");

            RuleFor(x => x.Zip)
                .Must(z => ValidateZip(z))
                .WithMessage("Zip should not be more than 10 characters");

            RuleFor(x => x.NumberOfSamples)
                .GreaterThan(0)
                .WithMessage("Number of samples should be greater than 0");

            RuleFor(x => x.NumberOfPools)
                .Must(x => ValidateNullabelInteger(x))
                .WithMessage("Number of pools should be greater than 0");

            RuleFor(x => x.SupportPersonId)
                .NotNull()
                .Must(x => x != default)
                .WithMessage("Invalid support person id");

            RuleFor(x => x.StudentsCount)
                .Must(x => ValidateNullabelInteger(x))
                .WithMessage("Students count should be greater than 0");

            RuleFor(x => x.EmployeesCount)
                .Must(x => ValidateNullabelInteger(x))
                .WithMessage("Employees count should be greater than 0");

            RuleFor(x => x.NumberOfBags)
                .Must(x => ValidateNullabelInteger(x))
                .WithMessage("Number of bags should be greater than 0");

            RuleFor(x => x.AdditionalTestTubes)
                .Must(x => ValidateNullabelInteger(x))
                .WithMessage("Additional test tubes should be greater than 0");

            RuleFor(x => x.NumberOfRakoBoxes)
                .Must(x => ValidateNullabelInteger(x))
                .WithMessage("Number of Rako boxes should be greater than 0");

            RuleFor(x => x.SchoolType)
               .Must(x => ValidateSchoolTypeValue(x))
               .WithMessage("School type value is not correct");
        }

        private static bool CheckForDuplicates(IEnumerable<string> emails)
        {
            if (emails.Any())
            {
                var distincedEmails = emails.Distinct();

                if (distincedEmails.Count() != emails.Count())
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckForNullableValue(IEnumerable<string> landLineNumbers)
        {
            if (landLineNumbers.Any(x => string.IsNullOrEmpty(x)))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateZip(string zip)
        {
            if (!string.IsNullOrEmpty(zip))
            {
                return zip.Length <= 10;
            }

            return true;
        }

        private static bool ValidateNullabelInteger(int? number)
        {
            return !number.HasValue || number.Value >= 0;
        }

        private static bool ValidateSchoolTypeValue(SchoolType? schoolType)
        {
            if (schoolType.HasValue && !Enum.IsDefined(typeof(SchoolType), schoolType))
            {
                return false;
            }

            return true;
        }
    }
}
