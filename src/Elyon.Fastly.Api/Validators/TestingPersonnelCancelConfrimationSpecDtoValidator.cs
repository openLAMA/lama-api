﻿#region Copyright
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

using System;
using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using FluentValidation;

namespace Elyon.Fastly.Api.Validators
{
    public class TestingPersonnelCancelConfrimationSpecDtoValidator : AbstractValidator<TestingPersonnelCancelConfrimationSpecDto>
    {
        public TestingPersonnelCancelConfrimationSpecDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid user email address");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required")
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date should be greater than or equal today");
        }
    }
}
