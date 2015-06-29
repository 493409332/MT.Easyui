#region
/* 
 *作者：shenjk http://www.shenjk.com
 *时间：2009-10-26
 *描述：验证类扩展
 */
#endregion

using Utility;

namespace Complex.Common.Utility.Validate
{
    /// <summary>
    /// 
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="argName"></param>
        /// <param name="lang">语言</param>
        /// <returns></returns>
        public static ValidationHelper<T> InitValidation<T>(this T value, string argName,Language lang= Language.ZH_CN)
        {
            return new ValidationHelper<T>(value, argName,lang);
        }
    }
}
