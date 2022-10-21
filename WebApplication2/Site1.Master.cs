using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] ==null)
                {
                    //User Panel
                    ViewBooksLinkButton.Visible = false;
                    UserLoginLinkButton.Visible = true;
                    SignUpLinkButton.Visible = true;
                    LogoutLinkButton.Visible = false;
                    HelloUserLinkButton.Visible = false;

                    //Admin Panel
                    adminLogin.Visible = true;
                    authorManagement.Visible = false;
                    publisherManagement.Visible = false;
                    bookInventory.Visible = false;
                    bookIssuing.Visible = false;
                    memberManagement.Visible = false;
                }

                else if (Session["role"] !=  null && Session["role"].Equals("user"))
                {
                    //User Panel
                    ViewBooksLinkButton.Visible = true;
                    UserLoginLinkButton.Visible = false;
                    SignUpLinkButton.Visible = false;
                    LogoutLinkButton.Visible = true;
                    HelloUserLinkButton.Visible = true;
                    HelloUserLinkButton.Text = "Hello " + Session["username"].ToString();

                    //Admin Panel
                    adminLogin.Visible = true;
                    authorManagement.Visible = false;
                    publisherManagement.Visible = false;
                    bookInventory.Visible = false;
                    bookIssuing.Visible = false;
                    memberManagement.Visible = false;
                }
                else if (Session["role"] != null && Session["role"].Equals("admin"))
                {
                    //User Panel
                    ViewBooksLinkButton.Visible = true;
                    UserLoginLinkButton.Visible = false;
                    SignUpLinkButton.Visible = false;
                    LogoutLinkButton.Visible = true;
                    HelloUserLinkButton.Visible = true;
                    HelloUserLinkButton.Text = "Hello Admin";

                    //Admin Panel
                    adminLogin.Visible = false;
                    authorManagement.Visible = true;
                    publisherManagement.Visible = true;
                    bookInventory.Visible = true;
                    bookIssuing.Visible = true;
                    memberManagement.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void SignUpLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("usersingup.aspx");
        }

        protected void adminLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void authorManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminauthormanagment.aspx");
        }

        protected void publisherManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminpublishermanagement.aspx");
        }

        protected void bookInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookinventory.aspx");
        }

        protected void bookIssuing_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookissuing.aspx");
        }

        protected void memberManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminmembermanage.aspx");
        }

        protected void UserLoginLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            Session["username"] = null;
            Session["fullname"] = null;
            Session["role"] = null;
            Session["status"] = null;


            ViewBooksLinkButton.Visible = false;
            UserLoginLinkButton.Visible = true;
            SignUpLinkButton.Visible = true;
            LogoutLinkButton.Visible = false;
            HelloUserLinkButton.Visible = false;

            //Admin Panel
            adminLogin.Visible = true; 
            authorManagement.Visible = false;
            publisherManagement.Visible = false;
            bookInventory.Visible = false;
            bookIssuing.Visible = false;
            memberManagement.Visible = false;

            Response.Redirect("homepage.aspx");
        }
    }
}