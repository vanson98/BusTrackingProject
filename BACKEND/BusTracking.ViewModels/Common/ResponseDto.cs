using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Common
{
    public class ResponseDto
    {
        public ResponseDto(string code, string mess)
        {
            this.StatusCode = code;
            this.Message = mess;
        }
        public ResponseDto() { }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
