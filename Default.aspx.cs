using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileProjectUpload
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Enctype = "multipart/form-data";
            if (!IsPostBack)
            {
                PopulateUploadFiles();
            }

        }
        private void PopulateUploadFiles()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                GridView1.DataSource = dc.UploadedFIles.ToList();
                GridView1.DataBind();
            }
        }
        protected void btnUploadAll_Click(object sender, EventArgs e)
        {

            HttpFileCollection filesColl = Request.Files;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                foreach (string uploader in filesColl)
                {
                    HttpPostedFile file = filesColl[uploader];
                    if (file.ContentLength > 0)
                    {

                        BinaryReader br = new BinaryReader(file.InputStream);
                        byte[] buffer = br.ReadBytes(file.ContentLength);

                        dc.UploadedFIles.Add(new UploadedFIle
                        {

                            FileID = 0,
                            FileName = file.FileName,
                            ContentType = file.ContentType,
                            FileExtention = Path.GetExtension(file.FileName),
                            FileSize = file.ContentLength,
                            FileContetnt = buffer

                        });



                    }
                }
                dc.SaveChanges();
            }
            PopulateUploadFiles();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                int fileID = Convert.ToInt32(e.CommandArgument.ToString());
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    var v = dc.UploadedFIles.Where(a => a.FileID.Equals(fileID)).FirstOrDefault();
                    if (v != null)
                    {
                        Response.ContentType = v.ContentType;
                        Response.AddHeader("content-disposition", "attachment; filename" + v.FileName);
                        Response.BinaryWrite(v.FileContetnt);
                        Response.Flush();
                        Response.End();

                    }
                }
            }
        }
    }
}