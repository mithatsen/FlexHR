using ExcelDataReader;
using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfFileColumnRepository : EfGenericRepository<FileColumn>, IFileColumnDal
    {
        private readonly FlexHRContext _context;
        public EfFileColumnRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }


        // 1-> (Ana Metod) - Excelden veri yükleme
        public GenericResultViewModel LoadDataFromExcel(FileUploadViewModel fuvm)
        {
            try
            {
                var result = new GenericResultViewModel()
                {
                    IsSuccess = true,
                    Message = "",
                };

                var readExcelFileResult = ReadExcelFile(fuvm);

                if (readExcelFileResult.IsSuccess)
                {
                    result = NewDataValidateExcelData(fuvm);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = readExcelFileResult.Message;
                    File.Delete(fuvm.xlsPath);
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

        // 2-> Excell Dosyasını okuma işlemi
        public GenericResultViewModel ReadExcelFile(FileUploadViewModel fuvm)
        {
            try
            {
                var result = new GenericResultViewModel()
                {
                    IsSuccess = true,
                    Message = "",
                };
                var rows = new List<string[]>();
                var dataObj = new string[] { };

                #region Colunm Count

                int columnListCount = _context.FileColumn.Where(x => x.IsActive && x.CompanyFileTypeGeneralSubTypeId == fuvm.fileUploadTypeID).OrderBy(x => x.ColumnSequence).ToList().Count;

                #endregion

                #region Read Excel                

                var fileName = fuvm.xlsPath;
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
                            if (index == 0 && reader.FieldCount != columnListCount)
                            {
                                result.IsSuccess = false;
                                int columnDifferenceCount = 0;
                                if (reader.FieldCount > columnListCount)
                                {
                                    columnDifferenceCount = reader.FieldCount - columnListCount;
                                    result.Message = "Yüklenen dosyada " + columnDifferenceCount + " adet fazla kolon bulunmaktadır!";
                                }
                                else if (reader.FieldCount < columnListCount)
                                {
                                    columnDifferenceCount = columnListCount - reader.FieldCount;
                                    result.Message = "Yüklenen dosyada " + columnDifferenceCount + " adet eksik kolon bulunmaktadır!";
                                }
                            }

                            // Excel Read
                            if (index > 0 && reader.FieldCount == columnListCount)
                            {
                                for (int i = 0; i < columnListCount; i++)
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

                #endregion

                #region Insert Read Data

                if (result.IsSuccess && rows.Count() > 0)
                {
                    fuvm.rows = rows;
                }

                #endregion

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

        // 3-> Excel'deki yeni data validasyonu ve db'deki eski dataların silinmesi
        public GenericResultViewModel NewDataValidateExcelData(FileUploadViewModel fuvm)
        {
            try
            {
                var result = new GenericResultViewModel()
                {
                    IsSuccess = true,
                    Message = "İşlem Başarılı",
                };

                fuvm.columnList = _context.FileColumn.Where(x => x.IsActive && x.CompanyFileTypeGeneralSubTypeId == fuvm.fileUploadTypeID).OrderBy(x => x.ColumnSequence).ToList();

                var isCreatedTable = CreateGenericSqlTable(fuvm);

                if (isCreatedTable)
                {
                    var readList = new Dictionary<string, List<string>>();

                    for (int t = 0; t < fuvm.rows.Count; t++)
                    {
                        #region Excel'de mükerrer kayıt kontrolü

                        if (fuvm.columnCount > 0)
                        {
                            for (int c = 0; c < fuvm.columnCount; c++)
                            {
                                //if (fuvm.columnList[c].FileUploadColumn_FileUploadColumnProperties.Any(x => x.FileUploadColumnPropertiesId == EnumFileUploadColumnProperties.IsExistControl.GetHashCode()))
                                if (false)
                                {
                                    if (readList.ContainsKey(fuvm.columnList[c].ColumnName))
                                    {
                                        if (readList.FirstOrDefault(x => x.Key == fuvm.columnList[c].ColumnName).Value.Any(x => x.Equals($"{fuvm.rows[t][c]}")))
                                        {
                                            return new GenericResultViewModel()
                                            {
                                                IsSuccess = false,
                                                Message = $"Dosyanın {t + 2}. satırında mükerrer kayıt bulundu! Mükerrer kayıt : {fuvm.columnList[c].ColumnDescription}",
                                            };
                                        }
                                        else
                                        {
                                            readList.FirstOrDefault(x => x.Key == fuvm.columnList[c].ColumnName).Value.Add($"{fuvm.rows[t][c]}");
                                        }
                                    }
                                    else
                                    {
                                        readList.Add(fuvm.columnList[c].ColumnName, new List<string> { $"{fuvm.rows[t][c]}" });
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    // Bulk Insert    
                    result = SubmitWorkOrderFormData(fuvm);

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

        // 4-> Generic Excel Tablosu Oluşturma
        public bool CreateGenericSqlTable(FileUploadViewModel fuvm)
        {
            try
            {
                var dynamicFields = "";

                foreach (var item in fuvm.columnList)
                {
                    dynamicFields += item.ColumnName.Trim().Replace(" ", "_") + " nvarchar(MAX) NULL,";
                }

                //var hasCreateTable = CheckSQLTableExists($"{fuvm.tableName}");
                var hasCreateTable = false;

                if (!hasCreateTable)
                {
                    var query = $"CREATE TABLE {fuvm.tableName} " +
                                "(" +
                                "Id int IDENTITY(1,1) NOT NULL," +
                                dynamicFields +
                                "IsActive bit NULL DEFAULT 1 " +
                                ")";

                    ExecuteSqlCommand(query);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 5-> Excel Data Bulk Insert işlemi
        public GenericResultViewModel SubmitWorkOrderFormData(FileUploadViewModel fuvm)
        {
            var result = new GenericResultViewModel()
            {
                IsSuccess = true,
                Message = "İşlem Başarılı",
            };
            var index = 0;
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cn = new SqlConnection("Data Source=DESKTOP-GRI9IBK\\SQLEXPRESS;Initial Catalog=FlexHR;Integrated Security= True");
                using (SqlConnection con = new SqlConnection(cn.ConnectionString))
                {
                    foreach (var column in fuvm.columnList)
                    {
                        dt.Columns.Add(column.ColumnName);
                    }

                    for (int i = 0; i < fuvm.rows.Count; i++)
                    {
                        index++;
                        DataRow dr = dt.NewRow();

                        for (int k = 0; k < fuvm.columnList.Count; k++)
                        {
                            var key = fuvm.columnList[k].ColumnName;
                            var value = fuvm.rows[i][k].ToString();
                            dr[$"{key}"] = $"{value}";
                        }
                        dt.Rows.Add(dr);
                    }

                    SqlBulkCopy objbulk = new SqlBulkCopy(con);
                    objbulk.DestinationTableName = $"{fuvm.tableName}";
                    for (int u = 0; u < fuvm.columnList.Count; u++)
                    {
                        objbulk.ColumnMappings.Add($"{fuvm.columnList[u].ColumnName}", $"{fuvm.columnList[u].ColumnName}");
                    }

                    con.Open();
                    objbulk.WriteToServer(dt);
                    con.Close();
                }
                result = new GenericResultViewModel()
                {
                    IsSuccess = true,
                    Message = $"{fuvm.rows.Count} adet kayıt sisteme eklendi",
                };
                return result;
            }
            catch (Exception ex)
            {
                return new GenericResultViewModel()
                {
                    IsSuccess = false,
                    Message = "Veriler kaydedilemedi!",
                };
            }
        }

    }
}
