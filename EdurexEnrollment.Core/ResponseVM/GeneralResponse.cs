using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EdurexEnrollment.Core.ResponseVM
{
    public class GeneralResponse<T>
    {
        public string Code { get; set; }
        public bool Successful { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
    public abstract class DataResponseAbstractDTO
    {
        public HttpStatusCode status { get; set; }
        public bool successful { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class ErrorResponseDTO : DataResponseAbstractDTO
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public ErrorResponseDTO(HttpStatusCode statusCode, IEnumerable<string> errors)
        {
            status = statusCode;
            ErrorMessages = errors;
            successful = false;
        }
    }

}