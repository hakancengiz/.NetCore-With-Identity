using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class ResponseModel
    {
        public ResponseModel() { }
        public ResponseModel(int code, string message, ResponseTypeModel type)
        {
            Code = code;
            Message = message;
            Type = type;
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public ResponseTypeModel Type { get; set; }
        public long id { get; set; }

    }
}
