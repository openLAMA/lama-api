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
using Elyon.Fastly.Api.DomainServices.Properties;

namespace Elyon.Fastly.Api.DomainServices.AttachmentsFiles
{
    public static class AttachmentsByTypes
    {
        public static List<AttachmentFileData> CompanyOnboarding()
        {
            var attachments = new List<AttachmentFileData>();
            var file = new AttachmentFileData { FileName = "01 Schulungspräsentation_compressed.pdf", Hash = "QawYbA==" };
            file.SetContent(Resources._01_Schulungspräsentation_compressed);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "02 Informationsflyer_compressed.pdf", Hash = "i0OvzQ==" };
            file.SetContent(Resources._02_Informationsflyer_compressed);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "03 Offizielles Informationsschreiben des Kanton Baselland.pdf", Hash = "jt9jHg==" };
            file.SetContent(Resources._03_Offizielles_Informationsschreiben_des_Kanton_Baselland);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "04 Vereinbarung zwischen Kanton und Betrieb.pdf", Hash = "e46z5g==" };
            file.SetContent(Resources._04_Vereinbarung_zwischen_Kanton_und_Betrieb);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "05 Einverständniserklärung für den Teilnehmenden (Erwachsene).pdf", Hash = "g5Y/WA==" };
            file.SetContent(Resources._05_Einverständniserklärung_für_den_Teilnehmenden__Erwachsene_);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "05 Einverständniserklärung für den Teilnehmenden (Minderjährige).pdf", Hash = "uZKSQQ==" };
            file.SetContent(Resources._05_Einverständniserklärung_für_den_Teilnehmenden__Minderjährige_);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "06 Anmeldeformular Depooling für die Teilnehmenden (Erwachsene).pdf", Hash = "Uar04Q==" };
            file.SetContent(Resources._06_Anmeldeformular_Depooling_für_die_Teilnehmenden__Erwachsene_);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "06 Anmeldeformular Depooling für die Teilnehmenden (Minderjährige).pdf", Hash = "GUo8iQ==" };
            file.SetContent(Resources._06_Anmeldeformular_Depooling_für_die_Teilnehmenden__Minderjährige_);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "07 Anfahrtsplan Labor Muttenz.pdf", Hash = "aDVddw==" };
            file.SetContent(Resources._07_Anfahrtsplan_Labor_Muttenz);
            attachments.Add(file);

            file = new AttachmentFileData { FileName = "Handbuch BTBL BBL_KMU.pdf", Hash = "HX8s5Q==" };
            file.SetContent(Resources.Handbuch_BTBL_BBL_KMU);
            attachments.Add(file);

            return attachments;
        }
    }
}
