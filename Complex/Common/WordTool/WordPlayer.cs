using System;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;


namespace Component_Library.Common.WordTool
{
    /********************************************************************************************************************************
     * * 类名     : WordPlayer
     * * 声明     : 
     * * 创建者   : 刘洋
     * * 创建日期 : 2014-05-04
     * * 修改者   : 刘洋
     * * 最新修改日期 : 2014-05-04
     ********************************************************************************************************************************/
    public class WordPlayer : IDisposable
    {
        #region - 属性 -
        private  Microsoft.Office.Interop.Word._Application oWord = null;
        private Microsoft.Office.Interop.Word._Document odoc = null;
        private Microsoft.Office.Interop.Word._Document oDoc
        {
            get
            {
                if (odoc == null)
                {
                    object oTemplate = System.Web.HttpContext.Current.Server.MapPath("~/word") + "/MyTemplate.dotx";
                    //object oTemplate = "E:\\01CSharpProject\\WordDemo\\word\\MyTemplate3.dotx";
                    odoc = oWord.Documents.Add(ref oTemplate, ref Nothing, ref Nothing, ref Nothing);
                }

                return odoc;
            }
            set
            {
                if (value != null)
                {
                    odoc = value;
                }
            }
        }
        private object Nothing = System.Reflection.Missing.Value;
        public enum Orientation
        {
            横板,
            竖板
        }
        public enum Alignment
        {
            左对齐,
            居中,
            右对齐,

        }
        public enum TextStyle
        {
            一级标题,
            二级标题,
            三级标题,
            正文,
            图表名,
            默认
        }
        public enum RowSpaceing
        {
            单倍行距,
            二分之三倍行距,
            两倍行距
        }
        #endregion

        #region - 添加文档 -

        #region - 创建并打开一个空的word文档进行编辑 -
        public void OpenNewWordFileToEdit()
        {
            oDoc = oWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        }
        #endregion

        #endregion

        #region - 创建新Word -
        public bool CreateWord(bool isVisible)
        {
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oWord.Visible = isVisible;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CreateWord()
        {
            return CreateWord(false);
        }
        #endregion

        #region - 打开文档 -
        public bool Open(string filePath, bool isVisible)
        {
            try
            {
                oWord.Visible = isVisible;

                object path = filePath;
                oDoc = oWord.Documents.Open(ref path,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 插入表格 -
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="haveBorder">是否有边框</param>
        /// <param name="colWidths">边距</param>
        /// <param name="bookmark">标签</param>
        /// <param name="wd">自动调整方式</param>
        /// <returns></returns>
        public bool InsertTable(string bookmark, System.Data.DataTable dt, bool haveBorder, double[] colWidths, WdAutoFitBehavior wd)
        {
          
            try
            {
                object oBookmark = bookmark;//WORD中预定义的书签
                Range tableLocation = oDoc.Bookmarks.get_Item(ref oBookmark).Range;//获取当前文档的书签位置。
                //oWord.Selection.GoTo(oBookmark);
               // Range tableLocation2 = oWord.Selection.Range;
                object Nothing = System.Reflection.Missing.Value;
                tableLocation.InsertParagraphAfter();//插入一个段落，在此段落上插入一个2行一列的表格。 
                // Microsoft.Office.Interop.Word.Table newTable = oDoc.Tables.Add(tableLocation, 2, 1, ref Nothing, ref Nothing);
                //添加Word表格     
                Microsoft.Office.Interop.Word.Table table = oDoc.Tables.Add(tableLocation, dt.Rows.Count, dt.Columns.Count, ref Nothing, ref Nothing);
              

                if (colWidths != null)
                {
                    for (int i = 0; i < colWidths.Length; i++)
                    {
                        table.Columns[i + 1].Width = (float)(28.5F * colWidths[i]);
                    }
                }
               

                ///设置TABLE的样式
                table.AllowAutoFit = true;
                table.AutoFitBehavior(wd);//设置自动调整方式
                table.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAtLeast;
                //table.Rows.Height = oWord.CentimetersToPoints(float.Parse("0.8"));
                table.Range.Font.Size = 9.0F;
                table.Range.Font.Name = "宋体";
                table.Range.Font.Bold = 0;
                table.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                table.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                table.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                if (haveBorder == true)
                {
                    //设置外框样式
                    table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    //样式设置结束
                }

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        table.Cell(row + 1, col + 1).Range.Text = dt.Rows[row][col].ToString();
                    }
                }
                             

                return true;
            }
            catch (Exception e)
            {
                //   MessageBox.Show(e.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="dt">数据表</param>
        /// <param name="haveBorder">是否有边框</param>
        /// <param name="colWidths">边距</param>
        /// <returns></returns>
        public bool InsertTable(string bookmark, System.Data.DataTable dt, bool haveBorder, double[] colWidths)
        {
            return InsertTable(bookmark, dt, haveBorder, null, WdAutoFitBehavior.wdAutoFitWindow);
        }

        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="dt">数据表</param>
        /// <param name="haveBorder">是否有边框</param>
        /// <param name="wd">自动调整方式</param>
        /// <returns></returns>
        public bool InsertTable(string bookmark, System.Data.DataTable dt, bool haveBorder, WdAutoFitBehavior wd)
        {
            return InsertTable(bookmark, dt, haveBorder, null, wd);
        }
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="dt">数据表</param>
        /// <param name="haveBorder">是否有边框</param>
        /// <returns></returns>
        public bool InsertTable(string bookmark, System.Data.DataTable dt, bool haveBorder)
        {
            return InsertTable(bookmark, dt, haveBorder, null, WdAutoFitBehavior.wdAutoFitWindow);
        }
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="dt">数据表</param>
        /// <returns></returns>
        public bool InsertTable(string bookmark, System.Data.DataTable dt)
        {
            return InsertTable(bookmark, dt, false, null, WdAutoFitBehavior.wdAutoFitWindow);
        }
        #endregion

        #region - 合并单元格 -
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="tableNo"></param>
        /// <param name="columnNo"></param>
        /// <param name="rowNo"></param>
        public void mergeTable(int tableNo, int startRowNo, int endRowNo, int startColumnNo, int endColumnNo)
        {
            Cell cell = oDoc.Tables[tableNo].Cell(startRowNo, startColumnNo);//列合并 
            cell.Merge(oDoc.Tables[tableNo].Cell(endRowNo, endColumnNo));
            //Cell cell1 = oDoc.Tables[tableNo].Cell(rowNo, columnNo);//行合并 
            //cell1.Merge(oDoc.Tables[tableNo].Cell(rowNo + 1, columnNo));

        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="tableNo"></param>
        /// <param name="columnNo"></param>
        /// <param name="rowNo"></param>
        public void mergeTable(string bookmark, int tableNo,int startRowNo, int endRowNo, int startColumnNo, int endColumnNo)
        {
            object oBookmark = bookmark;//WORD中预定义的书签
            Range tableLocation = oDoc.Bookmarks.get_Item(ref oBookmark).Range;//获取当前文档的书签位置。
            Cell cell = tableLocation.Tables[tableNo].Cell(startRowNo, startColumnNo);
           // Cell cell = oDoc.Tables[tableNo].Cell(startRowNo, startColumnNo);//列合并 
            cell.Merge(tableLocation.Tables[tableNo].Cell(endRowNo, endColumnNo));
            //Cell cell1 = oDoc.Tables[tableNo].Cell(rowNo, columnNo);//行合并 
            //cell1.Merge(oDoc.Tables[tableNo].Cell(rowNo + 1, columnNo));

        }
        #endregion

        #region - 拆分单元格 -
        /// <summary>
        /// 拆分单元格
        /// </summary>
        /// <param name="tableNo">表格编号</param>
        /// <param name="TargetRow">待拆分的单元格行</param>
        /// <param name="TargetColumn">待拆分的单元格列</param>
        /// <param name="RowNoSplitTo">拆成的行数</param>
        /// <param name="ColumnNoSplitTo">拆成的列数</param>
        public void splitTable(int tableNo, int TargetRow, int TargetColumn, object RowNoSplitTo, object ColumnNoSplitTo)
        {
            Cell cell = oDoc.Tables[tableNo].Cell(TargetRow, TargetColumn);
            cell.Split(ref RowNoSplitTo, ref ColumnNoSplitTo);//拆成几行几列
        }
        /// <summary>
        /// 拆分单元格
        /// </summary>
        ///  <param name="bookmark">书签</param>
        /// <param name="tableNo">表格编号</param>
        /// <param name="TargetRow">待拆分的单元格行</param>
        /// <param name="TargetColumn">待拆分的单元格列</param>
        /// <param name="RowNoSplitTo">拆成的行数</param>
        /// <param name="ColumnNoSplitTo">拆成的列数</param>
        public void splitTable(string bookmark, int tableNo, int TargetRow, int TargetColumn, object RowNoSplitTo, object ColumnNoSplitTo)
        {
            object oBookmark = bookmark;//WORD中预定义的书签
            Range tableLocation = oDoc.Bookmarks.get_Item(ref oBookmark).Range;//获取当前文档的书签位置。
            Cell cell = tableLocation.Tables[tableNo].Cell(TargetRow, TargetColumn);
            cell.Split(ref RowNoSplitTo, ref ColumnNoSplitTo);//拆成几行几列
        }
        #endregion

        #region - 插入段落 -
        /// <summary>
        /// 插入段落
        /// </summary>
        /// <param name="bookmark">位置标签</param>
        /// <param name="paraText">段落内容</param>
        /// <param name="textStyle">内容级别</param>
        /// <param name="rowSpaceing">行间距</param>
        /// <param name="indent">首行缩进（字符）</param>
        /// <param name="indent"></param>
        public void insertParagraph(string bookmark, string paraText, TextStyle textStyle, RowSpaceing rowSpaceing, int indent)
        {
            object oBookmark = bookmark;
            object range = oDoc.Bookmarks.get_Item(ref oBookmark).Range;
            Word.Paragraph oPara;
            oPara = oDoc.Content.Paragraphs.Add(ref range);
            oPara.Range.Text = paraText;
            //设置段落字体格式
            System.Drawing.Font font = setFont(textStyle);
            oPara.Range.Font.Name = font.Name;
            oPara.Range.Font.Size = font.Size;
            if (font.Style == FontStyle.Bold)
            {
                oPara.Range.Font.Bold = 1;
            }
            oPara.Format.SpaceBefore = 0;
            oPara.Format.SpaceAfter = 0;//24 pt spacing after paragraph. 
            // oPara.Format.FirstLineIndent = oWord.CentimetersToPoints(24f);
            oPara.Format.CharacterUnitFirstLineIndent = indent;//首行缩进2字符
            oPara.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oPara.Format.LineSpacingRule = setRowSpaceing(rowSpaceing);
            // oPara.Range.InsertParagraphAfter();

        }
        #endregion

        #region -插入标题—
        /// <summary>
        /// 插入标题
        /// </summary>
        /// <param name="titleText">标题内容</param>
        /// <param name="textStyle">标题级别</param>
        /// <param name="alignment">标题对齐方式</param>
        /// <param name="rowSpaceing">标题行距</param>
        public void InsertTitle(string bookmark, string titleText, TextStyle textStyle, Alignment alignment, RowSpaceing rowSpaceing)
        {
            object oBookmark = bookmark;
            object range = oDoc.Bookmarks.get_Item(ref oBookmark).Range;
            //Word段落
            Word.Paragraph p;
            p = oDoc.Content.Paragraphs.Add(ref range);

            //设置段落中的内容文本
            p.Range.Text = titleText;
            //设置为一号标题
            object style = SetTextStyle(textStyle);
            p.set_Style(ref style);
            System.Drawing.Font font = setFont(textStyle);
            p.Range.Font.Name = font.Name;
            p.Range.Font.Size = font.Size;
            if (font.Style == FontStyle.Bold)
            {
                p.Range.Font.Bold = 1;
            };
            p.Format.SpaceBefore = 0;
            p.Format.SpaceAfter = 0;
            p.Format.Alignment = SetAlignment(alignment);
            p.Format.LineSpacingRule = setRowSpaceing(rowSpaceing);
            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }
        #endregion

        #region - 插入图片-
        /// <summary>
        /// 插入图片 
        /// </summary>
        /// <param name="bookmark">位置标签</param>
        /// <param name="picturePath">图片路径</param>
        /// <param name="width">图片宽度（cm）</param>
        /// <param name="height">图片高度（cm）</param>

        public void InsertPicture(string bookmark, string picturePath, float width, float height)
        {
            object oBookmark = bookmark;//WORD中预定义的书签
            Object linkToFile = false;       //图片是否为外部链接 
            Object saveWithDocument = true;  //图片是否随文档一起保存  
            object range = oDoc.Bookmarks.get_Item(ref oBookmark).Range;//图片插入位置 
            Word.InlineShape shape = oWord.ActiveDocument.InlineShapes.AddPicture(picturePath, ref linkToFile, ref saveWithDocument, ref range);
            shape.Width = oWord.CentimetersToPoints(width); ;  //设置图片宽度             
            shape.Height = oWord.CentimetersToPoints(height); ;  //设置图片高度   
            // oDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape().WrapFormat.Type = Word.WdWrapType.wdWrapNone;//设置环绕的方式
        }
        #endregion

        #region -插入EXCEL—
        /// <summary>
        /// 插入EXCEL
        /// </summary>
        /// <param name="filePath">excel地址</param>
        /// <param name="bookmark">位置标签</param>
        public void InsertExcel(string filePath, string bookmark)
        {
            object oEndOfDoc = bookmark;//WORD中预定义的书签
            Object linkToFile = false;       //是否为外部链接 
            Object saveWithDocument = true;  //是否随文档一起保存  
            object range = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;//插入位置 
            object type = @"Excel.Sheet.12";//插入的excel 格式，这里我用的是excel 2010，所以是.12
            object filename = filePath;//插入的excel的位置
            oWord.ActiveDocument.InlineShapes.AddOLEObject(ref type, ref filename, linkToFile, ref Nothing, ref Nothing, ref Nothing, ref Nothing, range);//执行插入操作

        }
        public void InsertExcel(string filePath)
        {
            string oEndOfDoc = "\\endofdoc";
            InsertExcel(filePath, oEndOfDoc);
        }
        #endregion

        #region - 插入文本 -
        public bool InsertText(string strText, TextStyle textStyle, Alignment alignment, bool isAfter)
        {
            try
            {
                Word.Range rng = oDoc.Content;
                int lenght = oDoc.Characters.Count - 1;
                object start = lenght;
                object end = lenght;

                rng = oDoc.Range(ref start, ref end);

                if (isAfter == true)
                {
                    strText += "\r\n";
                }

                rng.Text = strText;
                System.Drawing.Font font = setFont(textStyle);
                rng.Font.Name = font.Name;
                rng.Font.Size = font.Size;
                if (font.Style == FontStyle.Bold)
                {
                    rng.Font.Bold = 1;
                } //设置单元格中字体为粗体

                SetAlignment(rng, alignment);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertText(string strText)
        {
            return InsertText(strText, TextStyle.默认, Alignment.左对齐, false);
        }
        #endregion

        #region - 设置行间距-
        /// <summary>
        /// 设置行间距
        /// </summary>
        /// <param name="rowSpaceing"></param>
        private WdLineSpacing setRowSpaceing(RowSpaceing rowSpaceing)
        {
            if (rowSpaceing == RowSpaceing.单倍行距)
            {
                return Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
            }
            else if (rowSpaceing == RowSpaceing.二分之三倍行距)
            {
                return Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpace1pt5;
            }
            else if (rowSpaceing == RowSpaceing.两倍行距)
            {
                return Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceDouble;
            }
            else
            {
                return Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpace1pt5;
            }

        }
        #endregion

        #region - 设置对齐方式 -
        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private Microsoft.Office.Interop.Word.WdParagraphAlignment SetAlignment(Range rng, Alignment alignment)
        {
            rng.ParagraphFormat.Alignment = SetAlignment(alignment);
            return SetAlignment(alignment);
        }
        private Microsoft.Office.Interop.Word.WdParagraphAlignment SetAlignment(Alignment alignment)
        {
            if (alignment == Alignment.居中)
            {
                return Word.WdParagraphAlignment.wdAlignParagraphCenter;
            }
            else if (alignment == Alignment.左对齐)
            {
                return Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }
            else
            {
                return Word.WdParagraphAlignment.wdAlignParagraphRight;
            }
        }
        #endregion

        #region - 设置文本级别 -
        /// <summary>
        /// 设置文本级别
        /// </summary>
        /// <param name="textStyle"></param>
        /// <returns></returns>
        private Microsoft.Office.Interop.Word.WdBuiltinStyle SetTextStyle(TextStyle textStyle)
        {
            if (textStyle == TextStyle.一级标题)
            {
                return Word.WdBuiltinStyle.wdStyleHeading1;
            }
            else if (textStyle == TextStyle.二级标题)
            {
                return Word.WdBuiltinStyle.wdStyleHeading2;
            }
            else if (textStyle == TextStyle.三级标题)
            {
                return Word.WdBuiltinStyle.wdStyleHeading3;
            }

            else
            {
                return Word.WdBuiltinStyle.wdStyleBodyText;
            }
        }
        #endregion

        #region - 设置字体样式-
        /// <summary>
        ///字号‘八号’对应磅值5 
        ///字号‘七号’对应磅值5.5 
        ///字号‘小六’对应磅值6.5 
        ///字号‘六号’对应磅值7.5 
        ///字号‘小五’对应磅值9 
        ///字号‘五号’对应磅值10.5 
        ///字号‘小四’对应磅值12 
        ///字号‘四号’对应磅值14 
        ///字号‘小三’对应磅值15
        ///字号‘三号’对应磅值16 
        ///字号‘小二’对应磅值18 
        ///字号‘二号’对应磅值22 
        ///字号‘小一’对应磅值24 
        ///字号‘一号’对应磅值26 
        ///字号‘小初’对应磅值36 
        ///字号‘初号’对应磅值42
        /// </summary>
        /// <param name="textStyle"></param>
        /// <returns></returns>

        private System.Drawing.Font setFont(TextStyle textStyle)
        {
            if (textStyle == TextStyle.一级标题)
            {
                return new System.Drawing.Font("宋体", 15, FontStyle.Bold);
            }
            else if (textStyle == TextStyle.二级标题)
            {
                return new System.Drawing.Font("宋体", 15, FontStyle.Bold);
            }
            else if (textStyle == TextStyle.三级标题)
            {
                return new System.Drawing.Font("宋体", 14, FontStyle.Bold);
            }
            else if (textStyle == TextStyle.图表名)
            {
                return new System.Drawing.Font("宋体", 10.5F, FontStyle.Regular);
            }
            else if (textStyle == TextStyle.默认)
            {
                return new System.Drawing.Font("宋体", 10.5F, FontStyle.Regular);
            }
            else
            {
                return new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            }
        }
        #endregion

        #region - 页面设置 -
        /// <summary>
        ///  页面设置
        /// </summary>
        /// <param name="orientation">方向</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="topMargin">上边距</param>
        /// <param name="leftMargin">左边距</param>
        /// <param name="rightMargin">右边距</param>
        /// <param name="bottomMargin">下边距</param>
        public void SetPage(Orientation orientation, double width, double height, double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            oDoc.PageSetup.PageWidth = oWord.CentimetersToPoints((float)width);
            oDoc.PageSetup.PageHeight = oWord.CentimetersToPoints((float)height);

            if (orientation == Orientation.横板)
            {
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
            }
            else if (orientation == Orientation.竖板)
            {
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;
            }

            oDoc.PageSetup.TopMargin = (float)(topMargin * 25);//上边距 
            oDoc.PageSetup.LeftMargin = (float)(leftMargin * 25);//左边距 
            oDoc.PageSetup.RightMargin = (float)(rightMargin * 25);//右边距 
            oDoc.PageSetup.BottomMargin = (float)(bottomMargin * 25);//下边距
        }
        /// <summary>
        ///  页面设置（A4）
        /// </summary>
        /// <param name="orientation">方向</param>
        /// <param name="topMargin">上边距</param>
        /// <param name="leftMargin">左边距</param>
        /// <param name="rightMargin">右边距</param>
        /// <param name="bottomMargin">下边距</param>
        public void SetPage(Orientation orientation, double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            SetPage(orientation, 21, 29.7, topMargin, leftMargin, rightMargin, bottomMargin);
        }
        /// <summary>
        ///  页面设置(竖版，A4)
        /// </summary>
        /// <param name="topMargin">上边距</param>
        /// <param name="leftMargin">左边距</param>
        /// <param name="rightMargin">右边距</param>
        /// <param name="bottomMargin">下边距</param>
        public void SetPage(double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            SetPage(Orientation.竖板, 21, 29.7, topMargin, leftMargin, rightMargin, bottomMargin);
        }
        /// <summary>
        /// 页面设置
        /// </summary>
        /// <param name="orientation">方向</param>
        /// <param name="pageSize">纸张大小</param>
        /// <param name="topMargin">上边距</param>
        /// <param name="leftMargin">左边距</param>
        /// <param name="rightMargin">右边距</param>
        /// <param name="bottomMargin">下边距</param>
        public void SetPage(Orientation orientation, string pageSize, double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            if (pageSize == "A5")
            {
                oDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA5;
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
            }
            else if (pageSize == "A4")
            {
                oDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;
            }
        }
        #endregion

        #region - 插入分页符 -
        /// <summary>
        /// 插入分页符
        /// </summary>
        public void InsertBreak()
        {
            object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;//分页符 
            oDoc.Characters.Last.InsertBreak(ref oPageBreak);
        }
        #endregion

        #region - 关闭当前文档 -
        public bool CloseDocument()
        {
            try
            {
                object doNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                oDoc.Close(ref doNotSaveChanges, ref Nothing, ref Nothing);
                oDoc = null;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion



        #region - 保存文档 -
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        public bool Save(string savePath)
        {
            return Save(savePath, false);
        }
        public bool Save(string savePath, bool isClose)
        {
            try
            {
                object fileName = savePath;
                oDoc.SaveAs(ref fileName, ref Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing);

                if (isClose)
                {
                    return CloseDocument();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 插入页脚 -
        /// <summary>
        /// 插入页脚
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public bool InsertPageFooter(string text, System.Drawing.Font font, WordPlayer.Alignment alignment)
        {
            try
            {
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;//页脚 
                oWord.Selection.InsertAfter(text);
                GetWordFont(oWord.Selection.Font, font);

                SetAlignment(oWord.Selection.Range, alignment);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InsertPageFooterNumber(System.Drawing.Font font, WordPlayer.Alignment alignment)
        {
            try
            {
                oWord.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;
                oWord.Selection.WholeStory();
                oWord.Selection.ParagraphFormat.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument;

                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;//页脚 
                oWord.Selection.TypeText("第");

                object page = WdFieldType.wdFieldPage;
                oWord.Selection.Fields.Add(oWord.Selection.Range, ref page, ref Nothing, ref Nothing);

                oWord.Selection.TypeText("页/共");
                object pages = WdFieldType.wdFieldNumPages;

                oWord.Selection.Fields.Add(oWord.Selection.Range, ref pages, ref Nothing, ref Nothing);
                oWord.Selection.TypeText("页");

                GetWordFont(oWord.Selection.Font, font);
                SetAlignment(oWord.Selection.Range, alignment);
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 字体格式设定 -
        /// <summary>
        /// 字体格式设定
        /// </summary>
        /// <param name="wordFont"></param>
        /// <param name="font"></param>
        public void GetWordFont(Microsoft.Office.Interop.Word.Font wordFont, System.Drawing.Font font)
        {
            wordFont.Name = font.Name;
            wordFont.Size = font.Size;
            if (font.Bold)
            {
                wordFont.Bold = 1;
            }
            if (font.Italic)
            {
                wordFont.Italic = 1;
            }
            if (font.Underline == true)
            {
                wordFont.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            }
            wordFont.UnderlineColor = Microsoft.Office.Interop.Word.WdColor.wdColorAutomatic;

            if (font.Strikeout)
            {
                wordFont.StrikeThrough = 1;//删除线
            }
        }
        #endregion

        #region - 获取Word中的颜色 -
        public WdColor GetWordColor(Color c)
        {
            UInt32 R = 0x1, G = 0x100, B = 0x10000;
            return (Microsoft.Office.Interop.Word.WdColor)(R * c.R + G * c.G + B * c.B);
        }
        #endregion

        #region - 生成目录-

        public void insertContent() //利用标题样式生成目录
        {
            object unit;
            unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            oWord.Selection.HomeKey(ref unit, ref Nothing);

            // GoToTheBeginning();

            object start = 0;
            object end = 0;
            Word.Range myRange = oWord.ActiveDocument.Range(ref start, ref end); //位置区域
            object useHeadingStyle = true; //使用Head样式
            object upperHeadingLevel = 1;  //最大一级
            object lowerHeadingLevel = 3;  //最小三级
            object useHypeLinks = true;
            //TablesOfContents的Add方法添加目录
            oDoc.TablesOfContents.Add(myRange, ref useHeadingStyle,
                ref upperHeadingLevel, ref lowerHeadingLevel,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref useHypeLinks, ref Nothing, ref Nothing);
            InsertBreak();
            oDoc.TablesOfContents[1].UpdatePageNumbers(); //更新页码

        }
        public void GoToTheEnd()
        {
            // VB :  Selection.EndKey Unit:=wdStory  
            object unit;
            unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            oWord.Selection.EndKey(ref unit, ref Nothing);
        }
        public void GoToTheBeginning()
        {
            // VB : Selection.HomeKey Unit:=wdStory  
            object unit;
            unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            oWord.Selection.HomeKey(ref unit, ref Nothing);
        }
        #endregion


        #region - 关闭程序 -
        public bool Quit()
        {
            try
            {
                object saveOption = Word.WdSaveOptions.wdDoNotSaveChanges;
                oWord.Quit(ref saveOption, ref Nothing, ref Nothing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc);
                oWord = null;
                oDoc = null;
                odoc = null;
                GC.Collect();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 释放资源 -
        public void Dispose()
        {
            Quit();
        }
        #endregion
    }
}
