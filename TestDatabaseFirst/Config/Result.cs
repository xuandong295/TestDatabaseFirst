using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIForStudent.Config
{
    public class GetResult<T>
    {
        public int code { get; set; }
        public List<T> data { get; set; }
        public string message { get; set; }
        public void Config (int _code, List<T> _data, string _message){
            code = _code;
            data = _data;
            message = _message;
        }
    }
    public class PostPutDeleteResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public void Config(int _code, string _message)
        {
            code = _code;
            message = _message;
        }
    }


}
