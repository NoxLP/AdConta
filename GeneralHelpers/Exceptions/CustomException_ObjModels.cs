using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AdConta
{
    public class CustomException_ObjModels : System.Exception
    {
        public CustomException_ObjModels() : base() { }

        public CustomException_ObjModels(string message) : base(message) { }

        public CustomException_ObjModels(string message, Exception innerException) : base(message, innerException) { }

        public CustomException_ObjModels(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
