using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IFileColumnDal : IGenericDal<FileColumn>
    {
        GenericResultViewModel ReadCompanyExcelFile(string xlsPath, string xlsFileName);
    }
}
