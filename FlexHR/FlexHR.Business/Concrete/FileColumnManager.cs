using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class FileColumnManager : GenericManager<FileColumn>, IFileColumnService
    {
        private readonly IFileColumnDal _fileColumnDal;
        public FileColumnManager(IFileColumnDal fileColumnDal) : base(fileColumnDal)
        {
            _fileColumnDal = fileColumnDal;
        }

        public GenericResultViewModel ReadCompanyExcelFile(string xlsPath, string xlsFileName)
        {
            return _fileColumnDal.ReadCompanyExcelFile(xlsPath, xlsFileName);
        }
    }
}
