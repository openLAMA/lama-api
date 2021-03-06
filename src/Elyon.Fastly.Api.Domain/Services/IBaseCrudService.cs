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

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Dtos;
using Elyon.Fastly.Api.Domain.Helpers;

namespace Elyon.Fastly.Api.Domain.Services
{
    public interface IBaseCrudService<TDto>: IBaseService
        where TDto: BaseDtoWithId
    {
        Task<TDto> AddAsync(TDto item);

        Task<TDto> AddAsync<TSpecDto>(TSpecDto item);

        Task<bool> AnyAsync(Expression<Func<TDto, bool>> predicate);

        Task DeleteAsync(TDto item);

        Task DeleteAsync(Guid id);

        Task<TDto> GetByIdAsync(Guid id);

        Task<TDto> GetAsync(Expression<Func<TDto, bool>> dtoFilter, bool asNoTracking = true);

        Task<PagedResults<TDto>> GetListAsync(Paginator paginator, 
            Expression<Func<TDto, bool>> filter, 
            Expression<Func<TDto, object>> orderBy, 
            bool ascending = true);

        Task<TDto> SaveAsync(TDto item);

        Task UpdateAsync(TDto item);
    }
}
