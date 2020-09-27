using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Common
{
    public class PageResultDto<T> : ResponseDto
    {
        public int TotalRecord { get; set; }
        public List<T> Items { get; set; }
    }
}
