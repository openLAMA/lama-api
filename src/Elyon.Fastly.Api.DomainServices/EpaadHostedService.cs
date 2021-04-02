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

using Elyon.Fastly.Api.Domain.Services;
using Microsoft.Extensions.Hosting;
using Prime.Sdk.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class EpaadHostedService : BackgroundService
    {
        private const int TaskDelayInMilliSeconds = 600000;

        private readonly IEpaadService _epaadService;
        private readonly ILog _log;

        public EpaadHostedService(IEpaadService epaadService, ILogFactory logFactory)
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            _epaadService = epaadService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _log.Info($"Getting all available organizations from ePaad");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _epaadService
                      .UpdateRegisteredEmployeesAsync()
                      .ConfigureAwait(false);

                    await Task.Delay(TaskDelayInMilliSeconds, stoppingToken)
                        .ConfigureAwait(false);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
                {
                    _log.Error(ex, ex.Message);
                }
            }            
        }
    }
}
