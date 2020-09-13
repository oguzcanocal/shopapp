using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Common.Dto
{
    public class ResultMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Css { get; set; }//succes warning danger
    }
}
