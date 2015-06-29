#region
/* 
 *作者：shenjk http://www.shenjk.com
 *时间：2009-10-26
 *描述：整数验证
 */
#endregion

using System;

namespace Complex.Common.Utility.Validate
{
    /// <summary>
    /// 
    /// </summary>
    public static class IntValidationHelper
    {
        /// <summary>
        /// 验证<see cref="System.Int32"/>类型的参数的值大于一定值.    
        /// </summary>
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static ValidationHelper<int> Min(this ValidationHelper<int> current, int min)
        {
            if (!current.Passed)
                return current;
            if (current.Value < min)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.INT_LESS, current.Lang)/*"{0}不能小于{1}"*/, current.Name, min);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}不能小于{1}", current.Name,min), current.Name);
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.Int32"/>类型的参数的值小于一定值.    
        /// </summary>
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static ValidationHelper<int> Max(this ValidationHelper<int> current, int max)
        {

            if (current.Value > max)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.INT_OVERFLOW, current.Lang)/*"{0}不能大于{1}"*/, current.Name, max);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}不能大于{1}", current.Name, max), current.Name);
            }
            return current;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ValidationHelper<int> Range(this ValidationHelper<int> current,int min, int max) {
            if (!current.Passed)
                return current;
            if (current.Value < min || current.Value>max)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.INT_RANGE, current.Lang)/*"{0}不能大于{1}，且不能小于{2}"*/, current.Name, max,min);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}不能大于{1}", current.Name, max), current.Name);
            }
            return current;
        }
    }
}
