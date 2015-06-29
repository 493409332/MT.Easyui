using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Common.Config
{
   public  class Extraction_AppConfig
    {
       /// <summary>
       /// 自动抽取每天定时执行 小时 分钟
       /// </summary>
       public static string HOURS = System.Configuration.ConfigurationManager.AppSettings["EXTRACTION_HOURS"];
       public static string MINUTES = System.Configuration.ConfigurationManager.AppSettings["EXTRACTION_MINUTES"];
        
    }
}
