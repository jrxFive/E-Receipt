using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_ReceiptTest
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            savePostedFile();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
           // saveFile();
        }

        private void savePostedFile()
        {
            foreach (string f in Request.Files.AllKeys)
            {
                HttpPostedFile file = Request.Files[f];
                saveFile(file);
            }
           
        }

        private String saveFile(HttpPostedFile postedFile)
        {
            int userID = 0;
            string userName = getUserNameFromFile(postedFile);
            EReceiptLINQDataContext context = new EReceiptLINQDataContext();

            var lastReceiptNumber = from num in context.Receipts
                                    select num;

            int maxReceiptValue = (from c in lastReceiptNumber select c.RECEIPT_ID).Max();



            string savePath = ("~/IMG/receiptImage/" + userName + "/receipt" + (maxReceiptValue + 1) + ".jpg");
            DirectoryCheck(userName);
            FileUpload1.SaveAs(Server.MapPath(savePath));

            string pathOfFile = "http://localhost:49765/" + savePath.Substring(savePath.IndexOf("~") + 2);

            Receipt receiptImage = new Receipt
            {
                RECEIPT_URL = pathOfFile,
                UPLOAD_DATE = DateTime.Now,

            };
            context.Receipts.InsertOnSubmit(receiptImage);
            context.SubmitChanges();

            var userIDNumber = from u in context.Customers
                               where u.USER_NAME == userName
                               select u;

             
            foreach (var x in userIDNumber)
            {
                userID = x.USER_ID;
                break;
            }


            CustomerReceipt custRec = new CustomerReceipt
            {
                USER_ID = userID,
                RECEIPT_ID = (maxReceiptValue + 1),

            };
            context.CustomerReceipts.InsertOnSubmit(custRec);
            context.SubmitChanges();

            context.Connection.Close();
            Label1.Text = "Upload Successful";
            return savePath;

        }

        private String getUserNameFromFile(HttpPostedFile postedFile)
        {
            string fileName = postedFile.FileName;
            string userName = fileName.Substring(0, fileName.IndexOf("_"));
            return userName;

        }

        private void DirectoryCheck(String userName)
        {
            var receiptImageDir = new System.IO.DirectoryInfo(Server.MapPath("~/IMG/"));

            if (!receiptImageDir.Exists)
            {
                var createReceiptImageDir = new System.IO.DirectoryInfo(Server.MapPath("~"));
                createReceiptImageDir.CreateSubdirectory("IMG/");
            }


            receiptImageDir = new System.IO.DirectoryInfo(Server.MapPath("~/IMG/receiptImage/"));

            if (!receiptImageDir.Exists)
            {
                var createReceiptImageDir = new System.IO.DirectoryInfo(Server.MapPath("~/IMG"));
                createReceiptImageDir.CreateSubdirectory("receiptImage/");
            }

            receiptImageDir = new System.IO.DirectoryInfo(Server.MapPath("~/IMG/receiptImage/"
                + userName + "/"));

            if (!receiptImageDir.Exists)
            {
                var createReceiptImageDir = new System.IO.DirectoryInfo(Server.MapPath("~/IMG/receiptImage/"));
                createReceiptImageDir.CreateSubdirectory(userName + "/");
            }



        }        
    }
}