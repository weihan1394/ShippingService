using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ShippingService.Core.Models;
using ShippingService.Core.Repositories;
using ShippingService.Core.Util;

namespace ShippingService.Core.Services
{
    public interface IShippingRateService
    {
        void SaveFile(List<IFormFile> files, string directory, string subDirectory, CancellationToken cancellationToken);
        string SizeConverter(long bytes);
    }

    public class ShippingRateService : IShippingRateService
    {
        private readonly IShippingExpressRepository _shippingExpressRepository;

        public ShippingRateService(IShippingExpressRepository shippingExpressRepository)
        {
            _shippingExpressRepository = shippingExpressRepository;
        }

        CultureInfo cultures = new CultureInfo("en-US");

        public void SaveFile([FromForm] List<IFormFile> files, string directory, string subDirectory, CancellationToken cancellationToken)
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine(directory, subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                    try
                    {
                        // add to read encoded 1252 values
                        using (var package = new ExcelPackage(stream))
                        {

                            // Express worksheet
                            ExcelWorksheet worksheet = package.Workbook.Worksheets["Express"];
                            int colCount = worksheet.Dimension.End.Column;  //get Column Count
                            int rowCount = worksheet.Dimension.End.Row;

                            List<express> lsShippingExpress = new List<express>();
                            for (int row = 2; row < rowCount; row++)
                            {
                                express shippingExpress = new express();
                                for (int col = 1; col < colCount; col++)
                                {
                                    string currValue = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString();
                                    switch (col)
                                    {
                                        case 1:
                                            // break if there is no fake rows
                                            if (!currValue.Equals("Express", StringComparison.OrdinalIgnoreCase))
                                            {
                                                goto End;
                                            }
                                            shippingExpress.Type = currValue;
                                            break;
                                        case 2:
                                            shippingExpress.Trackable = currValue;
                                            break;
                                        case 3:
                                            shippingExpress.ServiceLevel = currValue;
                                            break;
                                        case 4:
                                            shippingExpress.Country = currValue;
                                            break;
                                        case 5:
                                            shippingExpress.Country = currValue;
                                            break;
                                        case 6:
                                            shippingExpress.RateFlag = NumberUtil.convertStringToInt(currValue);
                                            break;
                                        case 7:
                                            shippingExpress.Weight = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 8:
                                            shippingExpress.DHLExpress = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 9:
                                            shippingExpress.SFEconomy = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 10:
                                            shippingExpress.Zone = NumberUtil.convertStringToInt(currValue);
                                            break;
                                    }
                                }
                                lsShippingExpress.Add(shippingExpress);
                            }

                        End:
                            Console.WriteLine(lsShippingExpress.Count);
                            var result = _shippingExpressRepository.InsertAllAsync(lsShippingExpress);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            });
        }

        public string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }
    }
}
