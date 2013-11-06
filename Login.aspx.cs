using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace E_ReceiptTest
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginButton_Click1(object sender, EventArgs e)
        {
            String generatedKey = "p+1o,";
            String saltedKey = CalculateMD5Hash(passwordTextBox.Text.ToString() + generatedKey);
            String hashedKey = CalculateMD5Hash(saltedKey); //saltedKey.GetHashCode().ToString();
            EReceiptLINQDataContext EReceiptDB = new EReceiptLINQDataContext();
            var userResults = from u in EReceiptDB.Customers
                              where u.USER_NAME == usernameTextBox.Text
                              && u.SALT_PASSWORD == saltedKey
                              && u.HASHED_PASSWORD == hashedKey
                              select u;

            if (Enumerable.Count(userResults) == 1)
            {
                Session["LoggedInUsername"] = usernameTextBox.Text;
                Response.Redirect("Today.aspx");

            }
            else
            {
                
            }

        }
        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}