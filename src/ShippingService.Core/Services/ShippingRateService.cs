using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using ShippingService.Core.Dtos;
using ShippingService.Core.Mapper;
using ShippingService.Core.Models;
using ShippingService.Core.Repositories;
using ShippingService.Core.Util;

namespace ShippingService.Core.Services
{
    public interface IShippingRateService
    {
        Task saveFile(List<IFormFile> files, string directory, string subDirectory, CancellationToken cancellationToken);
        string SizeConverter(long bytes);
        List<ExpressDto> retrieveExpress(CancellationToken cancellationToken);
    }

    public class ShippingRateService : IShippingRateService
    {
        private readonly IShippingExpressRepository _shippingExpressRepository;
        private readonly IShippingBulkRepository _shippingBulkRepository;
        private readonly IShippingPostalRepository _shippingPostalRepository;

        public ShippingRateService(IShippingExpressRepository shippingExpressRepository, IShippingBulkRepository shippingBulkRepository, IShippingPostalRepository shippingPostalRepository)
        {
            _shippingExpressRepository = shippingExpressRepository;
            _shippingBulkRepository = shippingBulkRepository;
            _shippingPostalRepository = shippingPostalRepository;
        }

        CultureInfo cultures = new CultureInfo("en-US");

        public async Task saveFile(List<IFormFile> files, string directory, string subDirectory, CancellationToken cancellationToken)
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine(directory, subDirectory);

            Directory.CreateDirectory(target);

            foreach (IFormFile file in files)
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
                                shippingExpress.id = row - 1;
                                for (int col = 1; col < colCount; col++)
                                {
                                    string currValue = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString();
                                    switch (col)
                                    {
                                        case 1:
                                            // break if there is no fake rows
                                            if (!currValue.Equals("Express", StringComparison.OrdinalIgnoreCase))
                                            {
                                                goto ExpressEnd;
                                            }
                                            shippingExpress.type = currValue;
                                            break;
                                        case 2:
                                            shippingExpress.trackable = currValue;
                                            break;
                                        case 3:
                                            shippingExpress.service_level = currValue;
                                            break;
                                        case 4:
                                            shippingExpress.country = currValue;
                                            break;
                                        case 5:
                                            shippingExpress.country_code = currValue;
                                            break;
                                        case 6:
                                            shippingExpress.rate_flag = NumberUtil.convertStringToInt(currValue);
                                            break;
                                        case 7:
                                            shippingExpress.weight = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 8:
                                            shippingExpress.dhl_express = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 9:
                                            shippingExpress.sf_economy = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 10:
                                            shippingExpress.ninja_van = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 11:
                                            shippingExpress.zone = NumberUtil.convertStringToInt(currValue);
                                            break;
                                    }
                                }
                                lsShippingExpress.Add(shippingExpress);
                            }

                        ExpressEnd:
                            {
                                Console.WriteLine(lsShippingExpress.Count);

                                // delete everything in the database
                                await _shippingExpressRepository.deleteAllRecords(cancellationToken);
                                var result = await _shippingExpressRepository.insertRecords(lsShippingExpress, cancellationToken);
                            }

                            // process bulk file
                            worksheet = package.Workbook.Worksheets["Bulk Mail"];
                            colCount = worksheet.Dimension.End.Column;  //get Column Count
                            rowCount = worksheet.Dimension.End.Row;
                            List<bulk> lsShippingBulk = new List<bulk>();
                            for (int row = 4; row < rowCount; row++)
                            {
                                bulk shippingBulk = new bulk();
                                shippingBulk.id = row - 3;
                                for (int col = 1; col < colCount; col++)
                                {
                                    string currValue = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString();
                                    switch (col)
                                    {
                                        case 1:
                                            // break if there is no fake rows
                                            if (!currValue.Equals("Bulk", StringComparison.OrdinalIgnoreCase))
                                            {
                                                goto BulkEnd;
                                            }
                                            shippingBulk.type = currValue;
                                            break;
                                        case 2:
                                            shippingBulk.trackable = currValue;
                                            break;
                                        case 3:
                                            shippingBulk.service_level = currValue;
                                            break;
                                        case 4:
                                            shippingBulk.country = currValue;
                                            break;
                                        case 5:
                                            shippingBulk.country_code = currValue;
                                            break;
                                        case 6:
                                            shippingBulk.item_weight_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 7:
                                            shippingBulk.total_weight_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 8:
                                            shippingBulk.ascendia_item_rate = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 9:
                                            shippingBulk.ascendia_rate_per_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 10:
                                            shippingBulk.singpost_item_rate = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 11:
                                            shippingBulk.singpost_rate_per_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 12:
                                            shippingBulk.dai_item_rate = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 13:
                                            shippingBulk.dai_rate_per_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                    }
                                }

                                lsShippingBulk.Add(shippingBulk);
                            }

                        BulkEnd:
                            {
                                Console.WriteLine(lsShippingBulk.Count);

                                // delete everything in the database
                                await _shippingBulkRepository.deleteAllRecords(cancellationToken);
                                var result = await _shippingBulkRepository.insertRecords(lsShippingBulk, cancellationToken);
                            }


                            // process bulk file
                            worksheet = package.Workbook.Worksheets["Postal"];
                            colCount = worksheet.Dimension.End.Column;  //get Column Count
                            rowCount = worksheet.Dimension.End.Row;
                            List<postal> lsShippingPostal = new List<postal>();
                            for (int row = 4; row < rowCount; row++)
                            {
                                postal shippingPostal = new postal();
                                shippingPostal.id = row - 3;
                                for (int col = 1; col < colCount; col++)
                                {
                                    string currValue = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString();
                                    switch (col)
                                    {
                                        case 1:
                                            // break if there is no fake rows
                                            if (!currValue.Equals("Postal", StringComparison.OrdinalIgnoreCase))
                                            {
                                                goto PostalEnd;
                                            }
                                            shippingPostal.type = currValue;
                                            break;
                                        case 2:
                                            shippingPostal.trackable = currValue;
                                            break;
                                        case 3:
                                            shippingPostal.service_level_days = currValue;
                                            break;
                                        case 4:
                                            shippingPostal.country = currValue;
                                            break;
                                        case 5:
                                            shippingPostal.country_code = currValue;
                                            break;
                                        case 6:
                                            shippingPostal.item_weight_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 7:
                                            shippingPostal.singpost_item_rate = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                        case 8:
                                            shippingPostal.singpost_rate_per_kg = NumberUtil.convertStringToDouble(currValue);
                                            break;
                                    }
                                }

                                lsShippingPostal.Add(shippingPostal);
                            }

                        PostalEnd:
                            {
                                Console.WriteLine(lsShippingPostal.Count);

                                // delete everything in the database
                                await _shippingPostalRepository.deleteAllRecords(cancellationToken);
                                var result = await _shippingPostalRepository.insertRecords(lsShippingPostal, cancellationToken);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
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

        public List<ExpressDto> retrieveExpress(CancellationToken cancellationToken)
        {
            List<express> lsExpress = _shippingExpressRepository.retrieveAll(cancellationToken);
            List<ExpressDto> lsExpressDto = new List<ExpressDto>();
            foreach(express e in lsExpress)
            {
                ExpressDto expressDto = ExpressDtoMapper.map(e);

                // calculate dhlexpress
                double dhlexpressPrice = e.weight * e.dhl_express;

                // calculate ninjavan
                double ninjavanPrice = e.weight * e.ninja_van;

                // calculate sfeconomy
                double sfeconomyPrice = e.weight * e.sf_economy;

                if ((dhlexpressPrice == 0) && (ninjavanPrice == 0))
                {
                    // sfeconomy
                    expressDto = ExpressDtoMapper.update(expressDto, NameConstant.sfeconomy, e.sf_economy);
                }
                else if ((ninjavanPrice == 0) && (sfeconomyPrice == 0))
                {
                    // dhl
                    expressDto = ExpressDtoMapper.update(expressDto, NameConstant.dhlexpress, e.dhl_express);
                }
                else if ((dhlexpressPrice == 0) && (sfeconomyPrice == 0))
                {
                    // ninjavan
                    expressDto = ExpressDtoMapper.update(expressDto, NameConstant.ninjavan, e.ninja_van);
                }
                else if (dhlexpressPrice == 0)
                {
                    // compare ninjavan and sfeconomy
                    if (ninjavanPrice < sfeconomyPrice)
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.ninjavan, e.ninja_van);
                    else
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.sfeconomy, e.sf_economy);
                }
                else if (ninjavanPrice == 0)
                {
                    // compare dhlexpress and sfeconomy
                    if (dhlexpressPrice < sfeconomyPrice)
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.dhlexpress, e.dhl_express);
                    else
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.sfeconomy, e.sf_economy);
                }
                else if (sfeconomyPrice == 0)
                {
                    if (dhlexpressPrice < ninjavanPrice)
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.dhlexpress, e.dhl_express);
                    else
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.ninjavan, e.ninja_van);
                }
                else
                {
                    if ((dhlexpressPrice < sfeconomyPrice) && (dhlexpressPrice < ninjavanPrice))
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.dhlexpress, e.dhl_express);
                    else if ((sfeconomyPrice < dhlexpressPrice) && (sfeconomyPrice < ninjavanPrice))
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.sfeconomy, e.sf_economy);
                    else
                        expressDto = ExpressDtoMapper.update(expressDto, NameConstant.ninjavan, e.ninja_van);
                }


                lsExpressDto.Add(expressDto);
            }
            return lsExpressDto;
        }
    }
}
