using OfficeOpenXml;
using System;
using VkOnlineChecking.Entities;

namespace VkOnlineChecking.Methods
{
    public class ReportExcel
    {
        public byte[] Generate(Profile profile)
        {
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Profile report");

            sheet.Cells["B2"].Value = "Profile:";
            sheet.Cells["C2"].Value = profile.ProfileUri;

            if (profile.ProfileStatistics == null)
            {
                return package.GetAsByteArray();
            }

            sheet.Cells["B4"].Value = "DateTime";
            sheet.Cells["C4"].Value = "Status";

            int row = 5;
            foreach (var item in profile.ProfileStatistics)
            {
                sheet.Cells[row, 2].Value = item.DateTime;
                sheet.Cells[row, 3].Value = item.ProfileStatus == 0 ? "offline" : "ONLINE";
                row++;
            }

            sheet.Cells[1, 1, row - 1, 3].AutoFitColumns();
            sheet.Column(2).Width = 25;
            sheet.Cells[4, 1, row - 1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells["B2:C2"].Style.Font.Bold = true;
            sheet.Cells["B4:C4"].Style.Font.Bold = true;
            sheet.Cells[1, 1, row - 1, 3].Style.Font.Size = 14;

            sheet.Cells[4, 2, row - 1, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);
            sheet.Cells[4, 2, 4, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Protection.IsProtected = true;

            return package.GetAsByteArray();
        }
    }
}
