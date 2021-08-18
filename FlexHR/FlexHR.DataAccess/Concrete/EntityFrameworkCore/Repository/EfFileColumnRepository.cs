using ExcelDataReader;
using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfFileColumnRepository : EfGenericRepository<FileColumn>, IFileColumnDal
    {
        private readonly FlexHRContext _context;
        public EfFileColumnRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        // Excell Dosyasını okuma işlemi
        public GenericResultViewModel ReadCompanyExcelFile(string xlsPath, string xlsFileName)
        {
            try
            {
                var result = new GenericResultViewModel()
                {
                    IsSuccess = true,
                    Message = "",
                };

                var columnList = _context.FileColumn.Where(x => x.IsActive && x.CompanyFileTypeGeneralSubTypeId == 132).OrderBy(x => x.ColumnSequence).ToList();

                var rows = new List<string[]>();
                var dataObj = new string[] { };

                var fileName = xlsPath + "/" + xlsFileName;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var index = 0;
                        string value;

                        while (reader.Read())
                        {
                            // Excel Validation - (Column Difference Count)
                            if (index == 0 && reader.FieldCount != columnList.Count)
                            {
                                result.IsSuccess = false;
                                int columnDifferenceCount = 0;
                                if (reader.FieldCount > columnList.Count)
                                {
                                    columnDifferenceCount = reader.FieldCount - columnList.Count;
                                    result.Message = "Yüklenen dosyada " + columnDifferenceCount + " adet fazla kolon bulunmaktadır!";
                                }
                                else if (reader.FieldCount < columnList.Count)
                                {
                                    columnDifferenceCount = columnList.Count - reader.FieldCount;
                                    result.Message = "Yüklenen dosyada " + columnDifferenceCount + " adet eksik kolon bulunmaktadır!";
                                }
                            }

                            // Excel Read
                            if (index > 0 && reader.FieldCount == columnList.Count)
                            {
                                for (int i = 0; i < columnList.Count; i++)
                                {
                                    value = (reader.GetValue(i) == null) ? "" : reader.GetValue(i).ToString();
                                    dataObj = dataObj.Concat(new string[] { value }).ToArray();
                                }
                            }

                            // Add Data List
                            if (dataObj.Any(x => !x.Equals("")))
                            {
                                rows.Add(dataObj);
                            }

                            dataObj = new string[] { };
                            index++;
                        }
                    }
                }

                if (result.IsSuccess && rows.Count() > 0)
                {
                    //result = AddOrUpdateExcelData(rows);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new GenericResultViewModel()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        // "WorkOrderFormData" Ekleme/Güncelleme İşlemleri
        //public GenericResultViewModel AddOrUpdateExcelData(List<string[]> rows)
        //{
        //    try
        //    {

        //        var result = new GenericResultViewModel()
        //        {
        //            IsSuccess = true,
        //            Message = "İşlem Başarılı",
        //        };

        //        var isCreatedTable = CreateWorkOrderFormDataTable();

        //        if (isCreatedTable)
        //        {
        //            var workOrderColumnList = GetWorkOrderColumnList();
        //            var excelUniqColumnsCount = workOrderColumnList.Count(x => x.IsExistControl);
        //            var readList = new Dictionary<string, List<string>>();

        //            for (int t = 0; t < rows.Count; t++)
        //            {
        //                #region Excel'de mükerrer kayıt kontrolü

        //                if (excelUniqColumnsCount > 0)
        //                {
        //                    for (int c = 0; c < workOrderColumnList.Count; c++)
        //                    {
        //                        if (workOrderColumnList[c].IsExistControl)
        //                        {
        //                            if (readList.ContainsKey(workOrderColumnList[c].ColumnName))
        //                            {
        //                                if (readList.FirstOrDefault(x => x.Key == workOrderColumnList[c].ColumnName).Value.Any(x => x.Equals($"{rows[t][c]}")))
        //                                {
        //                                    return new GenericResultViewModel()
        //                                    {
        //                                        IsSuccess = false,
        //                                        Message = $"Dosyanın {t + 2}. satırında mükerrer kayıt bulundu! Mükerrer kayıt : {workOrderColumnList[c].ColumnDescription}",
        //                                    };
        //                                }
        //                                else
        //                                {
        //                                    readList.FirstOrDefault(x => x.Key == workOrderColumnList[c].ColumnName).Value.Add($"{rows[t][c]}");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                readList.Add(workOrderColumnList[c].ColumnName, new List<string> { $"{rows[t][c]}" });
        //                            }
        //                        }
        //                    }
        //                }

        //                #endregion
        //            }

        //            // Remove Query
        //            var removeQuery = "DELETE FROM WorkOrderFormData";
        //            var removeResult = ExecuteSqlCommand(removeQuery);

        //            if (removeResult)
        //            {
        //                // Bulk Insert    
        //                result = SubmitWorkOrderFormData(rows, workOrderColumnList);
        //            }
        //            else
        //            {
        //                result = new GenericResultViewModel()
        //                {
        //                    IsSuccess = false,
        //                    Message = "Eski veriler silinemedi!",
        //                };
        //            }
        //        }
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResultViewModel()
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        // "WorkOrderFormData" Tablosu Oluşturma
        //public bool CreateWorkOrderFormDataTable()
        //{
        //    try
        //    {

        //        var dynamicFields = "";
        //        var workOrderColumns = GetWorkOrderColumnList();
        //        foreach (var item in workOrderColumns)
        //        {
        //            dynamicFields += item.ColumnName.Trim().Replace(" ", "_") + " nvarchar(MAX) NULL,";
        //        }

        //        var hasCreateTable = CheckSQLTableExists("WorkOrderFormData");

        //        if (!hasCreateTable)
        //        {
        //            var query = "CREATE TABLE WorkOrderFormData " +
        //                        "(" +
        //                        "Id int IDENTITY(1,1) NOT NULL," +
        //                        dynamicFields +
        //                        "IsActive bit NULL," +
        //                        ")";

        //            ExecuteSqlCommand(query);
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}
        //public async Task<bool> CheckSQLTableExists(string tableName)
        //{           
        //    try
        //    {
        //        var query = $"SELECT Count(*) as Result FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{tableName}'";
        //        var r =await _context.Abcd.FromSqlRaw(query).FirstOrDefaultAsync();
        //        return (r==null) ? true : false;
        //    }
        //    catch (Exception )
        //    {
        //        return false;
        //    }
        //}
    }
}
