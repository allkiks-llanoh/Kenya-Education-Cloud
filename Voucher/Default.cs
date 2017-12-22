using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using WebApplication1;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WebApplication1
{
    public class Default
    {
       // protected global::System.Web.UI.WebControls.FileUpload Upload;
       // protected global::System.Web.UI.WebControls.Button Button1;
       
        public System.Web.UI.WebControls.Button Button1 {
            get {
                return Button1;
               
            }

           

        }


        public System.Web.UI.WebControls.FileUpload Upload;

        public void Page_Load(object sender, EventArgs e)


        {
            bool IsPostBack = false;
            if (IsPostBack && Upload.HasFile)
            {
                if (Path.GetExtension(Upload.FileName).Equals(".xlsx"))
                {
                    var excel = new ExcelPackage(Upload.FileContent);
                    var dt = excel.ToDataTable();
                    var table = "cessss";
                    using (var conn = new SqlConnection("Server=DESKTOP-IUMRVSI;Database=web;Integrated Security=SSPI"))
                    {
                        var bulkCopy = new SqlBulkCopy(conn);
                        bulkCopy.DestinationTableName = table;
                        conn.Open();
                        var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt);
                    }
                }
            }

        }
    }
}