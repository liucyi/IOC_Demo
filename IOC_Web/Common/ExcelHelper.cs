using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace IOC_Web.Common
{
    public static class ExcelHelper
    {
        

     
    }

    public static class NPOIHelper
    {
        #region NPOI
        /// <summary>
        /// 输出excel
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="fileName"></param>
        public static void Export(IWorkbook workBook, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            workBook.Write(ms);
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.AddHeader("Pragma", "public");
            httpResponse.AddHeader("Cache-Control", "max-age=0");
            httpResponse.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
            //httpResponse.AddHeader("content-type", "application/x-msdownload");
            httpResponse.ContentEncoding = Encoding.UTF8;
            httpResponse.ContentType = "application/vnd.ms-excel";
            httpResponse.BinaryWrite(ms.ToArray());
            workBook = null;
            ms.Close();
            ms.Dispose();
        }

        /// <summary>
        /// NPOI Excel转DataSet
        /// </summary>
        /// <param name="excelServerPath"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelServerPath, bool hasTitle = true)
        {
            FileStream fs = System.IO.File.OpenRead(excelServerPath);
            IWorkbook workBook = WorkbookFactory.Create(fs, ImportOption.All);
            fs.Close();
            fs.Dispose();

            DataSet ds = new DataSet();
            for (int i = 0; i < workBook.NumberOfSheets; i++)
            {
                ISheet sheet = workBook.GetSheetAt(i);
                DataTable dt = null;
                if (hasTitle)
                {
                    dt = SheetToDataTableHasTitle(sheet);
                }
                else
                {
                    dt = SheetToDataTable(sheet);
                }
                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// NPOI Excel转DataTable
        /// </summary>
        /// <param name="excelServerPath"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelServerPath, bool hasTitle = true)
        {
            FileStream fs = System.IO.File.OpenRead(excelServerPath);
            IWorkbook workBook = WorkbookFactory.Create(fs, ImportOption.All);

            ISheet sheet = workBook.GetSheetAt(0);
            DataTable dt = null;
            if (hasTitle)
            {
                dt = SheetToDataTableHasTitle(sheet);
            }
            else
            {
                dt = SheetToDataTable(sheet);
            }
            fs.Close();
            fs.Dispose();
            workBook.Close();
            return dt;
        }

        /// <summary>
        /// NPOI DataTable转Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="contentEncode"></param>
        public static void Export(DataTable dt, string fileName, Dictionary<int, List<string>> dic = null)
        {
            if (dt == null || dt.Columns.Count <= 0)
            {
                return;
            }
            IWorkbook workBook = new HSSFWorkbook();
            DataTableFillWorkBook(dt, workBook, dic);
            Export(workBook, fileName);
        }

        /// <summary>
        /// NPOI DataSet转Excel
        /// </summary>
        public static void Export(DataSet ds, string fileName, Dictionary<int, Dictionary<int, List<string>>> dic = null)
        {
            if (ds == null || ds.Tables.Count <= 0)
            {
                return;
            }
            IWorkbook workBook = new HSSFWorkbook();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                var dt = ds.Tables[i];
                Dictionary<int, List<string>> itemDic = null;
                if (dic != null && dic.ContainsKey(i))
                {
                    itemDic = dic[i];
                }
                DataTableFillWorkBook(dt, workBook, itemDic);
            }
            Export(workBook, fileName);
        }

        /// <summary>
        /// NPOI Excel转DataTable
        /// </summary>
        /// <param name="excelServerPath"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(Stream stream)
        {
            IWorkbook workBook = WorkbookFactory.Create(stream, ImportOption.All);

            ISheet sheet = workBook.GetSheetAt(0);
            DataTable dt = SheetToDataTable(sheet);

            workBook.Close();
            return dt;
        }

        /// <summary>
        /// 清空单元格内容
        /// </summary>
        public static void RemoveSheetContent(ISheet sheet, bool isRemoveRow, CellRangeAddress address)
        {
            for (int i = address.FirstRow; i <= address.LastRow; i++)
            {
                //移除全部合并
                for (int n = sheet.NumMergedRegions - 1; n >= 0; n--)
                {
                    sheet.RemoveMergedRegion(n);
                }

                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }
                if (isRemoveRow == true)
                {
                    sheet.RemoveRow(row);
                    continue;
                }
                for (int c = address.FirstColumn; c <= address.LastColumn; c++)
                {
                    ICell cell = row.GetCell(c);
                    if (cell == null)
                    {
                        continue;
                    }
                    cell.SetCellValue("");
                }
            }
        }

        #region 私有方法
        private static DataTable SheetToDataTableHasTitle(ISheet sheet)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrWhiteSpace(sheet.SheetName))
            {
                dt.TableName = sheet.SheetName;
            }
            IRow firstRow = sheet.GetRow(0);
            for (int i = 0; i < firstRow.Cells.Count; i++)
            {
                ICell cell = firstRow.GetCell(i);
                if (cell != null)
                {
                    var colName = firstRow.GetCell(i).ToString();
                    colName = Regex.Replace(colName, @"\s", "");
                    if (dt.Columns[colName] == null)
                    {
                        dt.Columns.Add(colName);
                    }
                    else
                    {
                        dt.Columns.Add();
                    }
                }
                else
                {
                    dt.Columns.Add();
                }
            }
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dataRow = dt.NewRow();
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }
                for (int j = 0; j < firstRow.LastCellNum; j++)
                {
                    ICell cell = row.GetCell(j);
                    if (cell == null)
                    {
                        dataRow[j] = "";
                        continue;
                    }

                    switch (cell.CellType)
                    {
                        case CellType.Boolean:
                            dataRow[j] = cell.BooleanCellValue;
                            break;
                        case CellType.Numeric:
                            dataRow[j] = cell.NumericCellValue;
                            break;
                        case CellType.String:
                            dataRow[j] = cell.StringCellValue;
                            break;
                        default:
                            dataRow[j] = cell.ToString();
                            break;
                    }
                }
                dt.Rows.Add(dataRow);
            }

            return dt;
        }

        /// <summary>
        /// NPOI Sheet转Datatable
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private static DataTable SheetToDataTable(ISheet sheet)
        {
            if (sheet.LastRowNum <= 0)
            {
                return null;
            }

            DataTable dt = new DataTable(sheet.SheetName);

            int maxColumnCount = 0;
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null || row.LastCellNum <= maxColumnCount)
                {
                    continue;
                }
                maxColumnCount = row.LastCellNum;
            }

            for (int i = 0; i < maxColumnCount; i++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                DataRow dataRow = dt.NewRow();
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    ICell cell = row.GetCell(j);
                    if (cell == null)
                    {
                        dataRow[j] = "";
                        continue;
                    }

                    switch (cell.CellType)
                    {
                        case CellType.Boolean:
                            dataRow[j] = cell.BooleanCellValue;
                            break;
                        case CellType.Numeric:
                            dataRow[j] = cell.NumericCellValue;
                            break;
                        case CellType.String:
                            dataRow[j] = cell.StringCellValue;
                            break;
                        default:
                            dataRow[j] = cell.ToString();
                            break;
                    }
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 导出Excel数据填充
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="workBook"></param>
        private static void DataTableFillWorkBook(DataTable dt, IWorkbook workBook, Dictionary<int, List<string>> dic = null)
        {
            var sheetName = dt.TableName;
            if (string.IsNullOrWhiteSpace(sheetName))
            {
                sheetName = string.Format("sheet{0}", workBook.NumberOfSheets + 1);
            }
            ISheet sheet = workBook.CreateSheet(sheetName);

            if (dic != null && dic.Count > 0)
            {
                foreach (var item in dic.Keys)
                {
                    if (dic[item] != null && dic[item].Count > 0)
                    {
                        CellRangeAddressList regions = new CellRangeAddressList(1, 65535, item, item);
                        DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(dic[item].ToArray());
                        HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
                        sheet.AddValidationData(dataValidate);
                    }
                }
            }

            IRow firstRow = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                firstRow.CreateCell(i, CellType.String).SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                DataRow dtRow = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j, CellType.String).SetCellValue(dtRow[j].ToString());
                }
            }
        }
        #endregion


        #endregion
    }
    public class GridsView
    {
        public static void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.Charset = "utf-8";  


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid  
                    Table table = new Table();
                    table.GridLines = GridLines.Both;  //单元格之间添加实线  

                    //  add the header row to the table  
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table  
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table  
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter  
                    table.RenderControl(htw);
                    //  render the htmlwriter into the response  
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>  
        /// Replace any of the contained controls with literals  
        /// </summary>  
        /// <param name="control"></param>  
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }


        /// <summary>  
        /// 导出Grid的数据(全部)到Excel  
        /// 字段全部为BoundField类型时可用  
        /// 要是字段为TemplateField模板型时就取不到数据  
        /// </summary>  
        /// <param name="grid">grid的ID</param>  
        /// <param name="dt">数据源</param>  
        /// <param name="excelFileName">要导出Excel的文件名</param>  
        public static void OutputExcel(GridView grid, DataTable dt, string excelFileName)
        {
            Page page = (Page)HttpContext.Current.Handler;
            page.Response.Clear();
            string fileName = System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(excelFileName));
            page.Response.AddHeader("Content-Disposition", "attachment:filename=" + fileName + ".xls");
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.Charset = "utf-8";

            StringBuilder s = new StringBuilder();
            s.Append("<HTML><HEAD><TITLE>" + fileName + "</TITLE><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body>");

            int count = grid.Columns.Count;

            s.Append("<table border=1>");
            s.AppendLine("<tr>");
            for (int i = 0; i < count; i++)
            {

                if (grid.Columns[i].GetType() == typeof(BoundField))
                    s.Append("<td>" + grid.Columns[i].HeaderText + "</td>");

                //s.Append("<td>" + grid.Columns[i].HeaderText + "</td>");  

            }
            s.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {
                s.AppendLine("<tr>");
                for (int n = 0; n < count; n++)
                {
                    if (grid.Columns[n].Visible && grid.Columns[n].GetType() == typeof(BoundField))
                        s.Append("<td>" + dr[((BoundField)grid.Columns[n]).DataField].ToString() + "</td>");

                }
                s.AppendLine("</tr>");
            }

            s.Append("</table>");
            s.Append("</body></html>");

            page.Response.BinaryWrite(System.Text.Encoding.GetEncoding("utf-8").GetBytes(s.ToString()));
            page.Response.End();
        }
    }
}
