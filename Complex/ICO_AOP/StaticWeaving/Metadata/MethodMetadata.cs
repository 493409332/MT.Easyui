using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtAop.Metadata
{
    /// <summary>
    /// 方法元数据
    /// </summary>
    public class MethodMetadata
    {
        private string _methodName;

        public MethodMetadata(string methodName)
        {
            _methodName = methodName;
        }

        public virtual string MethodName
        {
            get{return _methodName;}
            
        }
    }
}
