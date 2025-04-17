using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class ResponseModel
    {
        #region props
        public string Message { get; set; }
        public string Title { get; set; }
        public string Function { get; set; }
        public bool Response { get; set; }
        public dynamic Result { get; set; }
        #endregion

        #region constructor
        public ResponseModel() { }
        public ResponseModel(bool _response, string _message, string _title = "", dynamic? _result = null, string _function = "")
        {
            Message = _message;
            Title = _title;
            Function = _function;
            Result = _result;
            Title = _title;
            Response = _response;
        }
        #endregion

        #region methods
        public void SetResponse(bool _response, string _message, string _title = "", dynamic? _result = null, string _function = "")
        {
            Message = _message;
            Title = _title;
            Function = _function;
            Result = _result;
            Title = _title;
            Response = _response;
        }
        #endregion
    }
}
