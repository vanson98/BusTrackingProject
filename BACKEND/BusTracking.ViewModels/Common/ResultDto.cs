﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Common
{
    public class ResultDto<T> : ResponseDto
    {
        public ResultDto(string code, string mess, T result)
        {
            this.StatusCode = code;
            this.Message = mess;
            this.Result = result;
        }
        public ResultDto() { }
        public T Result { get; set; }
    }
}
