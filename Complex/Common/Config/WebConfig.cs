using Complex.Common.Encryption;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Complex.Common.Config
{
    /// <summary>
    /// 此类是对webconfig的一个封装，每个使用配置的地方都使用这个
    /// </summary>
    public sealed class WebConfig
    {

        public static string AdminUser
        {
            get { return ConfigurationManager.AppSettings["useradmin"]; }
        }

        public static string IsOpenButton
        {
            get { return ConfigurationManager.AppSettings["isopenbut"]; }
        }
#if DEBUG

        /// <summary>
        /// 商城数据库连接字符串
        /// </summary>
        public static string JinHuaGameConnStr
        {
            get { return ConfigurationManager.ConnectionStrings["JinHuaGameConnStr"].ConnectionString; }
        }

        /// <summary>
        /// 报表系统
        /// </summary>
        public static string GameReportConnStr
        {
            get { return ConfigurationManager.ConnectionStrings["GameReportConnStr"].ConnectionString; }
        }

        /// <summary>
        /// 后台用户操作日志记录库
        /// </summary>
        public static string AdminLogConnStr
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["AdminLogDb"].ConnectionString; }
        }
#else
        public static string JinHuaGameConnStr
        {
            get { return EncryptionHelper.DecryptTextMainDbConnStr(); }
        }

            /// <summary>
        /// 报表系统
        /// </summary>
        public static string GameReportConnStr
        {
            get { return ConfigurationManager.ConnectionStrings["GameReportConnStr"].ConnectionString; }
        }

        public static string AdminLogConnStr
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["AdminLogDb"].ConnectionString; }
        }
#endif

    }
}
