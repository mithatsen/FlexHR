using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
   public  interface IFileColumnService : IGenericService<FileColumn>
    {
        string FileUploadCreateFolder(FileUploadViewModel fuvm);
        GenericResultViewModel LoadDataFromExcel(FileUploadViewModel fuvm);
        GenericResultViewModel ReadExcelFile(FileUploadViewModel fuvm);
        List<FileColumn> FileColumnListByTypeId(int typeId);
        ReadGenericTableMainViewModel GetStaffTrackkingData(string tableName, string whereParam = "", int recordCount = 0);
        bool UpdateGenericSqlTable(FileUploadViewModel fuvm);
    }
}
