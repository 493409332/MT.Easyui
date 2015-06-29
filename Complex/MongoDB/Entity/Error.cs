using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Mongodb.Entity
{
    public class ErrorLog
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///Message
        /// </summary>
        public string Message { get; set; }
         /// <summary>
        ///TargetSite
        /// </summary>
        public string TargetSite { get; set; }
        /// <summary>
        ///Source
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        ///StackTrace
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        ///ExceptionAttributeMessages
        /// </summary>
        public string ExceptionAttributeMessages { get; set; }

 
     
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }



    }
}
