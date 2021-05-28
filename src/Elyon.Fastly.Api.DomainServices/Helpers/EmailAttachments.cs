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

using System.Collections.Generic;
using System.Linq;
using Elyon.Fastly.Api.DomainServices.AttachmentsFiles;
using Elyon.Fastly.Api.DomainServices.Properties;

namespace Elyon.Fastly.Api.DomainServices.Helpers
{
    public static class EmailAttachments
    {
        public static List<AttachmentFileData> GetOnboardingAttachments()
        {
            var attachments = new List<AttachmentFileData>();
            var file = new AttachmentFileData { FileName = "01 Schulungspräsentation.pdf" };
            file.SetContent(Resources._01_Schulungspräsentation);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "02 Informationsflyer_compressed.pdf" };
            file.SetContent(Resources._02_Informationsflyer_compressed);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "03 Offizielles Informationsschreiben des Kanton Baselland.pdf" };
            file.SetContent(Resources._03_Offizielles_Informationsschreiben_des_Kanton_Baselland);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "04 Vereinbarung zwischen Kanton und Betrieb.pdf" };
            file.SetContent(Resources._04_Vereinbarung_zwischen_Kanton_und_Betrieb);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "05 Einverständniserklärung für den Teilnehmenden.pdf" };
            file.SetContent(Resources._05_Einverständniserklärung_für_den_Teilnehmenden);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "06 Anmeldeformular Depooling für die Teilnehmenden.pdf" };
            file.SetContent(Resources._06_Anmeldeformular_Depooling_für_die_Teilnehmenden);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "07 Anfahrtsplan Labor Muttenz.pdf" };
            file.SetContent(Resources._07_Anfahrtsplan_Labor_Muttenz);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "Infoblatt Betriebe.pdf" };
            file.SetContent(Resources.Infoblatt_Betriebe);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "Infoblatt KMU.pdf" };
            file.SetContent(Resources.Infoblatt_KMU);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "Infoblatt Mitarbeitende.pdf" };
            file.SetContent(Resources.Infoblatt_Mitarbeitende);
            attachments.Add(file);

            return attachments;
        }

        public static List<string> GetCompanyOnboardingAttachmentHashes()
        {
            List<AttachmentFileNameAndHash> attachmentsFileNamesAndHashes = new List<AttachmentFileNameAndHash>()
            {
                new AttachmentFileNameAndHash(){ FileName = "01 Schulungspräsentation.pdf", Hash = "PkIkrg==" },
                new AttachmentFileNameAndHash(){ FileName = "02 Informationsflyer_compressed.pdf", Hash = "i0OvzQ==" },
                new AttachmentFileNameAndHash(){ FileName = "03 Offizielles Informationsschreiben des Kanton Baselland.pdf", Hash = "jt9jHg==" },
                new AttachmentFileNameAndHash(){ FileName = "04 Vereinbarung zwischen Kanton und Betrieb.pdf", Hash = "e46z5g==" },
                new AttachmentFileNameAndHash(){ FileName = "05 Einverständniserklärung für den Teilnehmenden.pdf", Hash = "gJbpgg==" },
                new AttachmentFileNameAndHash(){ FileName = "06 Anmeldeformular Depooling für die Teilnehmenden.pdf", Hash = "6ah2dQ==" },
                new AttachmentFileNameAndHash(){ FileName = "07 Anfahrtsplan Labor Muttenz.pdf", Hash = "aDVddw==" },
                new AttachmentFileNameAndHash(){ FileName = "Infoblatt Betriebe.pdf", Hash = "49XrNw==" },
                new AttachmentFileNameAndHash(){ FileName = "Infoblatt Mitarbeitende.pdf", Hash = "gYOqww==" }
            };

            return attachmentsFileNamesAndHashes.Select(a => a.Hash).ToList();
        }

        public static List<string> GetSMEOnboardingAttachmentHashes()
        {
            List<AttachmentFileNameAndHash> attachmentsFileNamesAndHashes = new List<AttachmentFileNameAndHash>()
            {
                new AttachmentFileNameAndHash(){ FileName = "01 Schulungspräsentation.pdf", Hash = "PkIkrg==" },
                new AttachmentFileNameAndHash(){ FileName = "02 Informationsflyer_compressed.pdf", Hash = "i0OvzQ==" },
                new AttachmentFileNameAndHash(){ FileName = "03 Offizielles Informationsschreiben des Kanton Baselland.pdf", Hash = "jt9jHg==" },
                new AttachmentFileNameAndHash(){ FileName = "04 Vereinbarung zwischen Kanton und Betrieb.pdf", Hash = "e46z5g==" },
                new AttachmentFileNameAndHash(){ FileName = "05 Einverständniserklärung für den Teilnehmenden.pdf", Hash = "gJbpgg==" },
                new AttachmentFileNameAndHash(){ FileName = "06 Anmeldeformular Depooling für die Teilnehmenden.pdf", Hash = "6ah2dQ==" },
                new AttachmentFileNameAndHash(){ FileName = "07 Anfahrtsplan Labor Muttenz.pdf", Hash = "aDVddw==" },
                new AttachmentFileNameAndHash(){ FileName = "Infoblatt KMU.pdf", Hash = "Q3wJDg==" },
                new AttachmentFileNameAndHash(){ FileName = "Infoblatt Mitarbeitende.pdf", Hash = "gYOqww==" }
            };

            return attachmentsFileNamesAndHashes.Select(a => a.Hash).ToList();
        }
    }
}
