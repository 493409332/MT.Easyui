using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtAop.Metadata
{
    /// <summary>
    /// 参数元数据
    /// </summary>
    public class ParameterMetadata
    {
        private object _para;

        public ParameterMetadata(object para)
        {
            _para = para;
        }

        public virtual object Para
        {
            get { return _para; }
           
        }
    }
}
