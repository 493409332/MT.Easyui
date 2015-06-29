using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Complex.Common.Utility
{
    //  蔡鑫  2012-12-10
    public class JQHighchartsHelper
    {
        private string _xAxis;

        public JQHighchartsHelper()
        {
            this._xAxis = "";
        }

        /// <summary>
        /// 添加X轴
        /// </summary>
        /// <param name="xAxis">X轴内容</param>
        public void AppendXAxis(string xAxis)
        {
            this._xAxis += "'" + xAxis + "',";
        }
        /// <summary>
        /// 加入集合
        /// </summary>
        /// <param name="list"></param>
        public void AppendXAxis(List<string> list)
        {
            var sb = new StringBuilder();
            foreach (string str in list)
            {
                sb.Append("'" + str + "',");
            }
            this._xAxis = sb.ToString();
        }

        //清除X轴数据
        public void ClearXAxis()
        {
            this._xAxis = string.Empty;
        }

        //生成X轴内容
        public string BuildXAxis()
        {
            //生成X轴
            if (this._xAxis.Contains(","))
                this._xAxis = this._xAxis.Substring(0, this._xAxis.Length - 1);
           return "[" + this._xAxis + "]";
        }


        /// <summary>
        /// 根据数据返回 Highcharts的 json
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="removeColumns">需要移除的列</param>
        public  string GetHighChartsDataJson(DataTable dt, List<string> removeColumns)
        {
            var sb = new StringBuilder();
            var tempdc = (from DataColumn dc in dt.Columns select dc.ColumnName).ToList();
            //替换掉需要移除的列
            if (removeColumns.Count > 0)
                tempdc = tempdc.Except(removeColumns).Concat(removeColumns.Except(tempdc)).ToList();
            //如果列还有 遍历剩下的列
            if (tempdc.Count > 0)
            {
                for (int i = 0; i < tempdc.Count; i++)
                {
                    sb.Append("{name:'" + tempdc[i] + "',"); //生成 name 
                    if (dt.Rows.Count > 0)
                    {
                        var temp = "";
                        sb.Append("data:[");
                        //temp = dt.Rows.Cast<DataRow>()
                        //         .Aggregate(temp, (current, dr) => current + (Bases.ToInt(dr[(tempdc[i])])  + ","));
                        foreach (DataRow dr in dt.Rows)
                        {
                            temp += dr[tempdc[i]] + ",";
                        }
                        temp = temp.Substring(0, temp.Length - 1);
                        sb.Append(temp);
                        sb.Append("]");
                        sb.Append(tempdc.Count-1 == i ? "}" : "},");
                    }
                    else
                        sb.Append("data:[]}");
                }
            }
            if (sb.ToString().Length > 0)
                 return   "[" + sb + "]";
            return sb.ToString();
        }



        /// <summary>
        /// 将列替换成汉字
        /// </summary>
        /// <param name="colNames">列字典</param>
        /// <param name="dataJson">数据</param>
        /// <returns></returns>
        public string ReplaceColName(Dictionary<string, string> colNames, string dataJson)
        {
            return colNames.Aggregate(dataJson, (current, colName) => current.Replace(colName.Key, colName.Value));
        }

    }
}
