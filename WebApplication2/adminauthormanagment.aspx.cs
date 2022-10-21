using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
	public partial class adminauthormanagment : System.Web.UI.Page
	{
        private string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addAuthorManagmentButton_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                Console.WriteLine("<script>alert('Author with this ID Already Exist you can't another Author with the same Author ID');</script>");
            }
            else
            {
                addNewAuthor();
            }
        }

        protected void updateAuthorManagmentButton_Click(object sender, EventArgs e)
        {

            if (checkIfAuthorExists())
            {
                updateAuthor();
            }
            else
            {
                Console.WriteLine("<script>alert('Can't update');</script>");
            }
        }

        protected void deleteAuthorManagmentButton_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                deleteAuthor();
            }
            else
            {
                Console.WriteLine("<script>alert('Can't delete');</script>");
            }
        }

        protected void goAuthorManagmentButton_Click(object sender, EventArgs e)
        {

        }



        void deleteAuthor()
        {
            try
            {
                SqlConnection connection = new SqlConnection(strcon);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("delete author_master_tbl where author_id = @auth_id", connection);
                command.Parameters.AddWithValue("@auth_id", AuthorIdTextBox.Text.Trim());
                command.ExecuteNonQuery();
                command.Clone();
                Response.Write("<script>alert('Author is deleted');</script>");
                clearTextField();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        void updateAuthor()
        {
            try
            {
                
                SqlConnection connection = new SqlConnection(strcon);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("update author_master_tbl set author_name = @auth_name where author_id = '"+AuthorIdTextBox.Text.Trim()+"'", connection);
                command.Parameters.AddWithValue("@auth_name", AuthorNameTextBox.Text.Trim());
                command.ExecuteNonQuery();
                command.Clone();
                Response.Write("<script>alert('Author is Updated');</script>");

                clearTextField();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        void addNewAuthor()
        {
            try
            {
                SqlConnection connection = new SqlConnection(strcon);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand command = new SqlCommand("Insert into author_master_tbl(author_id,author_name)Values(@author_id, @author_name)", connection);

                command.Parameters.AddWithValue("@author_id", AuthorIdTextBox.Text.Trim());
                command.Parameters.AddWithValue("@author_name", AuthorNameTextBox.Text.Trim());

                command.ExecuteNonQuery();
                connection.Close();

                Response.Write("<script>alert('Author is added');</script>");
                clearTextField();
                adminAuthorGridView.DataBind();


            }


            catch (Exception exception)
            {
                Console.WriteLine("<script>alert('" + exception.Message + "');</script>");
                throw;
            }
        }

        bool checkIfAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                //= '" + adminMemberIdTextBox.Text.Trim() + "' and password = '" + passwordTextBox.Text.Trim() + "'

                SqlCommand command = new SqlCommand("select * from author_master_tbl where author_id = '" + AuthorIdTextBox.Text.Trim() + "' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("<script>alert('"+exception.Message+"');</script>");
                return false;
            }
        }

        void clearTextField()
        {
            AuthorIdTextBox.Text = "";
            AuthorNameTextBox.Text = "";
        }

    }
}