using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class ColorCodeManager : GenericManager<ColorCode>, IColorCodeService
    {
        private readonly IColorCodeDal _colorCodeDal;
        public ColorCodeManager(IColorCodeDal colorCodeDal) : base(colorCodeDal)
        {
            _colorCodeDal = colorCodeDal;
        }


    }
}
