using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IFileColumnDal : IGenericDal<FileColumn>
    {
        string FileUploadCreateFolder(FileUploadViewModel fuvm);
        GenericResultViewModel LoadDataFromExcel(FileUploadViewModel fuvm);
        GenericResultViewModel ReadExcelFile(FileUploadViewModel fuvm);
        List<FileColumn> FileColumnListByTypeId(int typeId);
        ReadGenericTableMainViewModel GetStaffTrackkingData(string tableName, string whereParam = "", int recordCount = 0);
        bool UpdateGenericSqlTable(FileUploadViewModel fuvm);
    }
}
