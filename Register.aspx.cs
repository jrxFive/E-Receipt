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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {



            String generatedKey = "p+1o,";
            String saltedKey = CalculateMD5Hash(passwordTextBox.Text.ToString() + generatedKey);
            String hashedKey = CalculateMD5Hash(saltedKey); //saltedKey.GetHashCode().ToString();
            using (EReceiptLINQDataContext context = new EReceiptLINQDataContext())
            {

                var potentialUserName = from u in context.Customers
                                        where u.USER_NAME == usernameTextBox.Text
                                        select u;

                if (Enumerable.Count(potentialUserName) == 1)
                {
                  //  UserNameTaken.Text = "Username Already Taken";
                    Response.Redirect("Today.aspx", false);
                }
                else
                {

                    Customer customer = new Customer
                    {
                        USER_NAME = usernameTextBox.Text,
                        FIRST_NAME = firstNameTextBox.Text,
                        LAST_NAME = lastNameTextBox.Text,
                        EMAIL = emailTextBox.Text,
                        SALT_PASSWORD = saltedKey,
                        HASHED_PASSWORD = hashedKey,
                    };
                    context.Customers.InsertOnSubmit(customer);
                    context.SubmitChanges();
                    Session["LoggedInUsername"] = usernameTextBox.Text;
                    Response.Redirect("Today.aspx", false);
                }
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