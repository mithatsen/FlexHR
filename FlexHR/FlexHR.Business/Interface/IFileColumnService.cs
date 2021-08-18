using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
   public  interface IFileColumnService : IGenericService<FileColumn>
    {
        GenericResultViewModel ReadCompanyExcelFile(string xlsPath, string xlsFileName);
    }
}
