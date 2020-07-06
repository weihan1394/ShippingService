using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OfficeOpenXml;
using ShippingService.Core.Dtos;
using ShippingService.Core.Models;

namespace ShippingService.Core.Util
{
    public class FileReader
    {
        public static void ExcelReader(MemoryStream fileStream)
        {
            try
            {
                // add to read encoded 1252 values
                using (var package = new ExcelPackage(fileStream))
                {
                    
                    // Express worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Express"];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;

                    List<Express> lsExpressDto = new List<Express>();
                    for (int row = 2; row < rowCount; row++)
                    {
                        Express expressDto = new Express();
                        for (int col = 1; col < colCount; col++)
                        {
                            string currValue = worksheet.Cells[row, col].Value.ToString().Trim();
                            switch (col)
                            {
                                case 1:
                                    expressDto.type = currValue;
                                    break;
                                case 2:
                                    expressDto.trackable = currValue;
                                    break;
                                case 3:
                                    expressDto.service_level = currValue;
                                    break;
                                case 4:
                                    expressDto.country = currValue;
                                    break;
                                case 5:
                                    expressDto.country_code = currValue;
                                    break;
                                case 6:
                                    expressDto.rate_flag = NumberUtil.convertStringToInt(currValue);
                                    break;
                                case 7:
                                    expressDto.weight = NumberUtil.convertStringToDouble(currValue);
                                    break;
                                case 8:
                                    expressDto.dhl_express = NumberUtil.convertStringToDouble(currValue);
                                    break;
                                case 9:
                                    expressDto.sf_economy = NumberUtil.convertStringToDouble(currValue);
                                    break;
                                case 10:
                                    expressDto.zone = NumberUtil.convertStringToInt(currValue);
                                    break;
                            }       
                        }
                        lsExpressDto.Add(expressDto);
                    }

                    Console.WriteLine(lsExpressDto.Count);
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
