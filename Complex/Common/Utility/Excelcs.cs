using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Complex.Common.Utility
{
    public class Excelcs : Controller
    {
        public static MemoryStream getExcel(DataTable dt, HttpContextBase content)
        {
            var book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                    row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
            }


            //写入到客户端
            var ms = new MemoryStream();
            book.Write(ms);
            book = null;
            ms.Close();
            ms.Dispose();
            return ms;

        }
        public FileContentResult getFileResult(DataTable dt, HttpContextBase curContext)
        {
       

            System.Web.UI.WebControls.DataGrid dgExport = null;

            // 当前对话  


            // IO用于导出并返回excel文件  

            System.IO.StringWriter strWriter = null;

            System.Web.UI.HtmlTextWriter htmlWriter = null;

            string filename = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_"

            + DateTime.Now.Hour + "_" + DateTime.Now.Minute;

            byte[] str = null;



            if (dt != null)
            {

                // 设置编码和附件格式

                curContext.Response.Charset = "GB2312";

                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");

                curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文

                curContext.Response.ContentType = "application/vnd.ms-excel";

                //System.Text.Encoding.UTF8;

                // 导出excel文件  

                strWriter = new System.IO.StringWriter();

                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid  

                dgExport = new System.Web.UI.WebControls.DataGrid();

                dgExport.DataSource = dt.DefaultView;

                dgExport.AllowPaging = false;

                dgExport.DataBind();

                dgExport.RenderControl(htmlWriter);



                // 返回客户端  

                str = System.Text.Encoding.UTF8.GetBytes(strWriter.ToString());

            }

            return File(str, "attachment;filename=" + filename + ".xls");

        }
    }
}