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

using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Elyon.Fastly.Api.PostgresRepositories.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Elyon.Fastly.Api.PostgresRepositories.DataSeed
{
    public static class DataSeeder
    {
        public static List<OrganizationType> SeedOrganizationTypes()
        {
            var organizationTypes = new List<OrganizationType>()
            {
                new OrganizationType()
                {
                    Id = 82000,
                    Name = "Company"
                },
                new OrganizationType()
                {
                    Id = 82001,
                    Name = "Pharmacy"
                },
                new OrganizationType()
                {
                    Id = 82002,
                    Name = "School"
                },
                new OrganizationType()
                {
                    Id = 82003,
                    Name = "Nursing Home"
                },
                new OrganizationType()
                {
                    Id = 82004,
                    Name = "Hospital"
                },
                new OrganizationType()
                {
                    Id = 99990,
                    Name = "SME"
                },
                new OrganizationType()
                {
                    Id = 82006,
                    Name = "Camp"
                }
            };

            return organizationTypes;
        }

        public static Country SeedCountry()
        {
            return new Country { Id = Guid.Parse("506fcee9-0ae2-49fa-a0ff-f9d3ac6d68bb"), Name = "Schweiz", ShortName = "CH" };
        }

        public static Canton SeedCanton()
        {
            return new Canton
            {
                Id = Guid.Parse("13f87683-8736-49e3-9a96-bceafb2d6846"),
                Name = "Basel",
                ShortName = "BL",
                CountryId = Guid.Parse("506fcee9-0ae2-49fa-a0ff-f9d3ac6d68bb")
            };
        }

        public static List<City> SeedCities()
        {
            var result = new List<City>();
            var citiesBytesArray = Resources.cities_list;

            if (citiesBytesArray.Any())
            {
                using var ms = new MemoryStream(citiesBytesArray);
                using StreamReader reader = new StreamReader(ms);
                var json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<City>>(json);
                result.ForEach(x =>
                {
                    x.CantonId = Guid.Parse("13f87683-8736-49e3-9a96-bceafb2d6846");
                });
            }

            return result;
        }

        public static List<LamaCompany> SeedLamaCompanies()
        {
            var lamaCompanies = new List<LamaCompany>()
            {
                new LamaCompany()
                {
                    Id = Guid.Parse("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"),
                    Name = "University",
                    RoleType = RoleType.University
                },
                new LamaCompany()
                {
                    Id = Guid.Parse("dd015958-aa5d-4568-850a-557a3c73f336"),
                    Name = "Laboratory",
                    RoleType = RoleType.Laboratory
                },
                new LamaCompany()
                {
                    Id = Guid.Parse("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"),
                    Name = "Logistics",
                    RoleType = RoleType.Logistics
                },
                new LamaCompany()
                {
                    Id = Guid.Parse("bae8c907-4851-40ae-99ee-fbdf8dc43c83"),
                    Name = "State",
                    RoleType = RoleType.State
                }
            };

            return lamaCompanies;
        }

        public static List<User> SeedDefaulSupportUsersForLamaCompanies()
        {
            var users = new List<User>()
            {
                new User
                {
                    Id = Guid.Parse("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                    LamaCompanyId = Guid.Parse("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"),
                    Email = "dUmJTbaQV6uSRgNo6qSxdMMo7Vt2kqQ/GD7C8q6Spmo=",
                    Name = "dHJp3o7Hkp/HS/nmMIa4kA==",
                    PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg==",
                    LandLineNumber = "SSEfX6+eGdQho09y+G6bag=="
                },
                new User
                {
                    Id = Guid.Parse("917aea9f-89d1-43e9-95ad-43969a1f276c"),
                    LamaCompanyId = Guid.Parse("dd015958-aa5d-4568-850a-557a3c73f336"),
                    Email = "NOApzKyRTRHuc8nvOH2APp8ij4KTJGxTQNQPGEpbu1k=",
                    Name = "xmSc5gwiON+AwwxsabEpig==",
                    PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg===",
                    LandLineNumber = "SSEfX6+eGdQho09y+G6bag=="
                },
                new User
                {
                    Id = Guid.Parse("4491038d-39a8-410a-97b0-60f7f604d5ac"),
                    LamaCompanyId = Guid.Parse("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"),
                    Email = "FSR76pjB9FuzJcAyT0xu0Wcoe2pISxF64mK+mhI6ZiI=",
                    Name = "VKoWZeB+QezSZdJzg/G6ig==",
                    PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg==",
                    LandLineNumber = "SSEfX6+eGdQho09y+G6bag=="
                },
                new User
                {
                    Id = Guid.Parse("076466c2-1540-4a4d-98eb-12bea4a8c71b"),
                    LamaCompanyId = Guid.Parse("bae8c907-4851-40ae-99ee-fbdf8dc43c83"),
                    Email = "XTBEsQ6hodiv2AxLjves1GHtrafYSmYGHwZYbCSMKYo=",
                    Name = "e5rRqXltNck37nfPTN/RAw==",
                    PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg==",
                    LandLineNumber = "SSEfX6+eGdQho09y+G6bag=="
                },
                new User
                {
                    Id = Guid.Parse("f54d712f-94d1-447f-becb-62df0d72b216"),
                    LamaCompanyId = Guid.Parse("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"),
                    Email = "gnHWLPNRW2ZdXA2qHX2XAD4vx1slAqetnp1KV1yV6ac=",
                    Name = "5z5VQaw55Tw7a0KbuzkQmvDLXgKyF0hTsbC7/k6RI4w=",
                    PhoneNumber = "dwPhC2kEaekmMahSecP+Zw==",
                    LandLineNumber = "dwPhC2kEaekmMahSecP+Zw=="
                }
            };

            return users;
        }

        public static List<SupportPersonOrgTypeDefaultMapping> SeedDefaultSupportPersonOrgTypeRelation()
        {
            var users = new List<SupportPersonOrgTypeDefaultMapping>()
            {
                new SupportPersonOrgTypeDefaultMapping
                {
                    Id = Guid.Parse("fc3498d0-af10-4a7d-882b-4cfcce073a4d"),
                    UserId = Guid.Parse("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                    OrganizationTypeId = 82000
                },
                new SupportPersonOrgTypeDefaultMapping
                {
                    Id = Guid.Parse("d54768ec-27e4-4f2d-b081-7df9b9825aab"),
                    UserId = Guid.Parse("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                    OrganizationTypeId = 82001
                },
                new SupportPersonOrgTypeDefaultMapping
                {
                    Id = Guid.Parse("b9b33e39-f133-43f0-b05c-59771164a092"),
                    UserId = Guid.Parse("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                    OrganizationTypeId = 82002
                },
                new SupportPersonOrgTypeDefaultMapping
                {
                    Id = Guid.Parse("2604552a-fe6c-4570-93d0-8c9013a83a6a"),
                    UserId = Guid.Parse("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                    OrganizationTypeId = 82003
                },
                new SupportPersonOrgTypeDefaultMapping
                {
                    Id = Guid.Parse("c3fa9ce0-03b5-4f90-be92-c222d6161ec7"),
                    UserId = Guid.Parse("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                    OrganizationTypeId = 82004
                },
                new SupportPersonOrgTypeDefaultMapping
                {
                    Id = Guid.Parse("b902522e-c816-4a79-bcf4-9eb0896ab78a"),
                    UserId = Guid.Parse("f54d712f-94d1-447f-becb-62df0d72b216"),
                    OrganizationTypeId = 99990
                }
            };

            return users;
        }

        public static List<TestingPersonnelStatus> SeedDefaultTestingPersonnelStatuses()
        {
            var users = new List<TestingPersonnelStatus>()
            {
                new TestingPersonnelStatus
                {
                    Id = Guid.Parse("0c66bd94-5f3e-4aca-ab58-2b2b8c12d526"),
                    Name = "BSc"
                },
                new TestingPersonnelStatus
                {
                    Id = Guid.Parse("d95e09c1-4b57-423c-a180-6c1c3011d959"),
                    Name = "MSc"
                },
                new TestingPersonnelStatus
                {
                    Id = Guid.Parse("c67d1068-255a-4c89-b590-a4531154508a"),
                    Name = "MSc (Head)"
                },
                new TestingPersonnelStatus
                {
                    Id = Guid.Parse("d58d11d7-740a-4acf-b380-21d4b88d4aec"),
                    Name = "BSc (Head)"
                }
            };

            return users;
        }

        public static AttachmentsSeed SeedAttachmentsSeed()
        {
            return new AttachmentsSeed { Id = Guid.Parse("0c5e5ef7-4a46-4f53-93ad-3f0dc89016ad"), IsSeeded = false };
        }
    }
}
