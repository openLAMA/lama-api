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

using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using Elyon.Fastly.Api.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elyon.Fastly.Api.Validators
{
    public class TestingPersonnelInvitationConfirmValidator : AbstractValidator<TestingPersonnelInvitationConfirmDto>
    {
        public TestingPersonnelInvitationConfirmValidator()
        {
            RuleFor(x => x.Shifts)
                .NotEmpty()
                .WithMessage("You should add at least one shift");

            RuleFor(x => x.Shifts.Select(sh => sh))
                .Must(sh => CheckForValidShifts(sh))
                .WithMessage("Shift number is not correct");

            RuleFor(x => x.Shifts.Select(sh => sh))
               .Must(sh => CheckForDuplicateShifts(sh))
               .WithMessage("There are duplicated shifts");
        }

        private static bool CheckForValidShifts(IEnumerable<ShiftNumber> shiftNumbers)
        {
            if (shiftNumbers.Any())
            {
                foreach(var shiftNumber in shiftNumbers)
                {
                    if (!Enum.IsDefined(typeof(ShiftNumber), shiftNumber))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool CheckForDuplicateShifts(IEnumerable<ShiftNumber> shiftNumbers)
        {
            if (shiftNumbers.Any())
            {
                var distincedShifts = shiftNumbers.Distinct();

                if (distincedShifts.Count() != shiftNumbers.Count())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
