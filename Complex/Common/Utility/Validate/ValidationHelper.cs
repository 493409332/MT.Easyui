#region
/* 
*作者：shenjk http://www.shenjk.com
*时间：2009-10-24 9:50:02
*描述：数据合法性验证帮助类
*/
#endregion

using System;
using Utility;

namespace Complex.Common.Utility.Validate
{
    /// <summary>
    /// 数据合法性验证帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationHelper<T>
    {
        #region 成员
        private T m_Value;
        private string m_Name;
        private bool b_Passed;
        private string s_Msg;
        #endregion

        #region 属性
        /// <summary>    
        /// 获取待验证的参数的值.    
        /// </summary>
        public T Value
        {
            get { return m_Value; }
        }
        /// <summary>    
        /// 获取待验证的参数的名称.    
        /// </summary>    
        public string Name
        {
            get { return m_Name; }
        }
        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool Passed
        {
            get
            {
                return b_Passed;
            }
            set
            {
                b_Passed = value;
            }
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg
        {
            get
            {
                return s_Msg;
            }
            set
            {
                s_Msg = value;
            }
        }

        /// <summary>
        /// 语言
        /// </summary>
        public Language Lang{
            get;
            set;
        }

        #endregion


        #region 构造函数
        /// <summary>    
        /// 创建一个<see cref="ValidationHelper&lt;T&gt;"/>的对象.    
        /// </summary>    
        /// <param name="value">待验证的参数的值.</param>   
        /// /// <param name="name">待验证的参数的名称.</param>    
        public ValidationHelper(T value, string name,Language lang)
        {
            m_Value = value;
            m_Name = name;
            this.Lang = lang;
            b_Passed = true;
        }
        #endregion
        #region 基本方法
        /// <summary>    
        /// 验证参数不为其默认值.   
        ///  </summary>   
        ///<returns>this指针以方便链式调用.</returns>
        /// <exception cref="ArgumentException">参数为值类型且为默认值.</exception>    
        /// <exception cref="ArgumentNullException">参数为引用类型且为null.</exception>    
        public ValidationHelper<T> NotDefault()
        {

            //if (b_Passed && Value.Equals(default(T)))
            //{
                if (Value is ValueType && b_Passed && Value.Equals(default(T)))
                {
                    s_Msg = String.Format("{0}不能使用默认值", Name);
                    // throw new ArgumentException(String.Format("参数{0}不能使用默认值", Name), Name);
                    b_Passed = false;
                }
                else if (!(Value is ValueType) && b_Passed && Value==null)
                {
                    s_Msg = String.Format("{0}不能为空", Name);
                    // throw new ArgumentNullException(String.Format("参数{0}不能为null", Name), Name); 
                    b_Passed = false;
                }
              
            //}
            return this;
        }
        /// <summary>    
        /// 使用自定义方法进行验证.    
        /// </summary>    
        /// <param name="rule">用以验证的自定义方法.</param>    
        /// <returns>this指针以方便链式调用.</returns>    
        /// <exception cref="Exception">验证失败抛出相应异常.</exception>    
        /// <remarks><paramref name="rule"/>的第一个参数为参数值,第二个参数为参数名称.</remarks>    
        public ValidationHelper<T> CustomRule(Func<T, bool> rule,string msg)
        {
            if (b_Passed)
            {
              this.Passed= rule(Value);
              this.s_Msg = msg;
            }
            return this;
        }

        /// <summary>
        /// 自定义显示错误提示信息
        /// </summary>
        /// <param name="sme"></param>
        /// <returns></returns>
        public ValidationHelper<T> ShowMsg(Action<string> sme)
        {
            if (sme != null && !b_Passed)
            {               
                sme(s_Msg);
            }
            return this;
        }
        /// <summary>
        /// 显示错误提示信息
        /// </summary>
        public ValidationHelper<T> ShowMsg()
        {
            if (!b_Passed)
            {
               
            }
            return this;
        }


        /// <summary>
        /// 转到下一组验证
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="value"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public ValidationHelper<T1> ToNext<T1>(T1 value, string argName)
        {
            ValidationHelper<T1> v = new ValidationHelper<T1>(value, argName,this.Lang);
            v.Passed = b_Passed;
            v.Msg = s_Msg;
            return v;
        }
        #endregion
    }
}
