using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.Configuration;

namespace WebApplication2.Handlers
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
                try
                {
                    HttpFileCollection files = context.Request.Files;
                    string x = files.AllKeys[0].ToString();
                    string fname = "";

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        fname = context.Server.MapPath("~/UploadCollFile/" + x);
                        file.SaveAs(fname);
                    }

                    string conString = string.Empty;
                    conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    DataTable dt = new DataTable();
                    conString = string.Format(conString, fname);
                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();
                                context.Response.Write(dt);
                            }
                        }
                    }
                }

                catch (OleDbException ex)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Invalid Premium File is Uploaded!");
                }
            
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}