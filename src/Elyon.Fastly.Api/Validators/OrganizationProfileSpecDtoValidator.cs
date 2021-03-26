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
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Elyon.Fastly.Api.Validators
{
    public class OrganizationProfileSpecDtoValidator : AbstractValidator<OrganizationProfileSpecDto>
    {
        public OrganizationProfileSpecDtoValidator()
        {
            RuleFor(x => x.Contacts)
                .NotEmpty()
                .WithMessage("You should add at least one contact");

            RuleFor(x => x.SupportPersonId)
                .NotNull()
                .Must(x => x != default)
                .WithMessage("Invalid support person id");

            RuleFor(x => x.Id)
                .NotNull()
                .Must(x => x != default)
                .WithMessage("Invalid id");
            
            RuleFor(x => x.Contacts.Select(e => e.Email))
                    .Must(e => CheckForDuplicates(e))
                    .WithMessage("There shouldn't be duplicated emails");

            RuleFor(x => x.Contacts.Select(e => e.LandLineNumber))
               .Must(e => CheckForNullableValue(e))
               .WithMessage("There shouldn't be null values for land line numbers");
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
    }
}
