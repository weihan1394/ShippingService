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

                    List<express> lsExpressDto = new List<express>();
                    for (int row = 2; row < rowCount; row++)
                    {
                        express expressDto = new express();
                        for (int col = 1; col < colCount; col++)
                        {
                            string currValue = worksheet.Cells[row, col].Value.ToString().Trim();
                            switch (col)
                            {
                                case 1:
                                    expressDto.Type = currValue;
                                    break;
                                case 2:
                                    expressDto.Trackable = currValue;
                                    break;
                                case 3:
                                    expressDto.ServiceLevel = currValue;
                                    break;
                                case 4:
                                    expressDto.Country = currValue;
                                    break;
                                case 5:
                                    expressDto.Country = currValue;
                                    break;
                                case 6:
                                    expressDto.RateFlag = NumberUtil.convertStringToInt(currValue);
                                    break;
                                case 7:
                                    expressDto.Weight = NumberUtil.convertStringToDouble(currValue);
                                    break;
                                case 8:
                                    expressDto.DHLExpress = NumberUtil.convertStringToDouble(currValue);
                                    break;
                                case 9:
                                    expressDto.SFEconomy = NumberUtil.convertStringToDouble(currValue);
                                    break;
                                case 10:
                                    expressDto.Zone = NumberUtil.convertStringToInt(currValue);
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
