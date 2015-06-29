using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.ICO_AOP.Attribute
{
    /// <summary>
    ///ICO注册工厂提供允许注册与AOP拦截是否开启的权限 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ICO_AOPEnableAttribute : System.Attribute
    {
       /// <summary>
       /// 
       /// </summary>
        /// <param name="icoEnable">是否允许</param>
        /// <param name="aopEnable">是否允许(默认允许)</param>
        public ICO_AOPEnableAttribute(bool icoEnable, bool aopEnable = true)
        {
            this.ICOEnable = icoEnable;
            this.AOPEnable = aopEnable;
        }

        /// <summary>
        /// 是否允许
        /// </summary>
        public bool ICOEnable
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许
        /// </summary>
        public bool AOPEnable
        {
            get;
            set;
        }
   
    }
}
