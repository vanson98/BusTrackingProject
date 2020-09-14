using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Common
{
    public class ResultDto<T>
    {
        public ResultDto(int code, string mess, T result)
        {
            this.Code = code;
            this.Message = mess;
            this.Result = result;
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
