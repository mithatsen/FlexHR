using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ReadGenericTableViewModel
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }

    public class ReadGenericTableMainViewModel
    {
        public List<List<ReadGenericTableViewModel>> list { get; set; }

        public ReadGenericTableMainViewModel()
        {
            list = new List<List<ReadGenericTableViewModel>>();
        }

        public bool IsExistControl(ReadGenericTableMainViewModel list, string columnName, string value)
        {
            var isAnyRecord = false;
            foreach (var item in list.list)
            {
                isAnyRecord = item.Any(x => x.ColumnName == columnName && x.Value == value);
                if (isAnyRecord)
                {
                    return isAnyRecord;
                }
            }
            return isAnyRecord;
        }
    }
}
