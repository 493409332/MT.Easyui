using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Common.Data
{
    public static class FilterTranslator
    {

        /// <summary>
        /// 将操作符代码转换为SQL的操作符号
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        private static string GetOperatorQueryText(string op)
        {
            switch ( op.ToLower() )
            {
                case "eq": return " = ";
                case "gt": return " > ";
                case "ge": return " >= ";
                case "nu": return " IS NULL ";
                case "nn": return " IS NOT NULL ";
                case "lt": return " < ";
                case "le": return " <= ";
                case "cn": return " like ";
                case "bw": return " like ";
                case "ew": return " like ";
                case "ne": return " <> ";
                case "in": return " IN ";
                case "ni": return " NOT IN ";
                default: return " = ";
            }
        }

        public static string ToSql(string jsonFilter)
        {
            if ( string.IsNullOrEmpty(jsonFilter) )
                return " 1=1 ";
            FilterGroup fg = Complex.Common.Utility.JSONhelper.ConvertToObject<FilterGroup>(jsonFilter);
            return ToSql(fg);
        }

        private static string ToSql(FilterGroup fg)
        {
            StringBuilder sb = new StringBuilder();

            if ( fg == null )
                return " 1=1 ";

            sb.Append("(");
            bool flag = false;
            if ( fg.Rules != null )
            {
                foreach ( var rule in fg.Rules )
                {
                    if ( flag )
                        sb.Append(" " + fg.groupOp.ToString().Replace("'","''") + " ");
                    sb.Append(TranslateRule(rule));
                    flag = true;
                }
            }

            if ( fg.Groups != null )
            {
                foreach ( var subgroup in fg.Groups )
                {
                    if ( flag )
                        sb.Append(" " + fg.groupOp.ToString().Replace("'", "''") + " ");
                    sb.Append(ToSql(subgroup));
                    flag = true;
                }
            }

            sb.Append(")");
            return sb.ToString();
        }

        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> ToLinqWhere<T>(string jsonFilter)
        {
            if ( string.IsNullOrEmpty(jsonFilter) )
                return True<T>();
            FilterGroup fg = Complex.Common.Utility.JSONhelper.ConvertToObject<FilterGroup>(jsonFilter);
            return ToLinqWhere<T>(fg);
        }
        	
 
        private static Expression<Func<T, bool>> ToLinqWhere<T>(FilterGroup fg)
        {

            StringBuilder sb = new StringBuilder();

            if ( fg == null )
                return True<T>();


            sb.Append("(");



            bool flag = false;
            if ( fg.Rules != null )
            {
                foreach ( var rule in fg.Rules )
                {
                    if ( flag )
                        sb.Append(" " + fg.groupOp.ToString().Replace("'", "''") + " ");
                    sb.Append(TranslateRule(rule));
                    flag = true;
                }
            }

            if ( fg.Groups != null )
            {
                foreach ( var subgroup in fg.Groups )
                {
                    if ( flag )
                        sb.Append(" " + fg.groupOp.ToString().Replace("'", "''") + " ");
                    sb.Append(ToSql(subgroup));
                    flag = true;
                }
            }

            sb.Append(")");
          



            return null;
        }
        public static Expression<Func<T, bool>> GenerateEx<T>(FilterRule rule) 
        {

            ParameterExpression param = Expression.Parameter(typeof(T), "p");

            PropertyInfo pi = typeof(T).GetProperty(rule.field);  

            Expression right = Expression.Constant(rule.data);//键

            Expression left = Expression.Property(param, pi);//建立属性
           
            
            //Expression<Func<T, bool>> two2 = c => { 
               
            //    pi.GetValue(c,null).ToString().StartsWith(rule.data.ToString());
            // return true;
            //};


            switch (rule.op.ToLower() )
            {
                case "eq": return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param); 
                case "gt": return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(left, right), param);
                case "ge": return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(left, right), param);
                case "nu":
                    right = Expression.Constant(null);
                    return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);
                case "nn":
                    right = Expression.Constant(null);
                    return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(left, right), param);
                case "lt": return Expression.Lambda<Func<T, bool>>(Expression.LessThan(left, right), param);
                case "le": return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(left, right), param);

                //case "cn": return Expression.Lambda<Func<T, bool>>(Expression.(left, right), param);
                //case "bw": return  Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);
                //case "ew": return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);
                //case "ne": return  Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);
                //case "in": return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);
                //case "ni": return  Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);

                default: return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), param);
            }

             
        }

 


        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);

        } 
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);

        }



        private static string TranslateRule(FilterRule rule)
        {
            StringBuilder sb = new StringBuilder();
            if ( rule == null ) return " 1=1 ";

            if ( !string.IsNullOrEmpty(rule.op) )
            {
                string _op = GetOperatorQueryText(rule.op);
                switch ( rule.op )
                {
                    case "bw":
                        sb.Append(rule.field.ToString().Replace("'", "''") + _op + "'" + rule.data.ToString().Replace("'", "''") + "%'");
                        break;
                    case "ew":
                        sb.Append(rule.field.ToString().Replace("'", "''") + _op + "'%" + rule.data.ToString().Replace("'", "''") + "'");
                        break;
                    case "cn":
                    case "nc":
                        sb.Append(rule.field.ToString().Replace("'", "''") + _op + "'%" + rule.data.ToString().Replace("'", "''") + "%'");
                        break;
                    case "in":
                    case "ni":
                        sb.Append(rule.field.ToString().Replace("'", "''") + _op + "(" + rule.data.ToString().Replace("'", "''") + ")");
                        break;
                    case "nu":
                    case "nn":
                        sb.Append(rule.field.ToString().Replace("'", "''") + _op);
                        break;
                    default:
                        sb.Append(rule.field.ToString().Replace("'", "''") + _op + "'" + rule.data.ToString().Replace("'", "''") + "'");
                        break;
                }
            }
            return sb.ToString();
        }
    }

    public class FilterGroup
    {
        /// <summary>
        /// 筛选条件组合方式 and or
        /// </summary>
        public GropuOp groupOp { get; set; }
        public IList<FilterRule> Rules { get; set; }
        public IList<FilterGroup> Groups { get; set; }
    }

    public enum GropuOp
    {
        AND,
        OR
    }

    public class FilterRule
    {
        public FilterRule()
        {
        }

        public FilterRule(string field, object data, string op)
        {
            this.field = field;
            this.data = data;
            this.op = op;
        }

        /// <summary>
        /// 字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 比较符号
        /// </summary>
        public string op { get; set; }
    }
}
