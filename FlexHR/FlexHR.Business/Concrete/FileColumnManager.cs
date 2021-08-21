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

        public string FileUploadCreateFolder(FileUploadViewModel fuvm)
        {
            return _fileColumnDal.FileUploadCreateFolder(fuvm);
        }

        public GenericResultViewModel LoadDataFromExcel(FileUploadViewModel fuvm)
        {
            return _fileColumnDal.LoadDataFromExcel(fuvm);
        }
        public GenericResultViewModel ReadExcelFile(FileUploadViewModel fuvm)
        {
            return _fileColumnDal.LoadDataFromExcel(fuvm);
        }
    }
}
