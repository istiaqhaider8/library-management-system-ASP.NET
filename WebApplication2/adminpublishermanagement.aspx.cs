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
    public partial class adminpublishermanagement : System.Web.UI.Page
    {
        private string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PublisherAddButton_Click(object sender, EventArgs e)
        {

            if (ClickIfPublisherExist())
            {
                Response.Write("<script>alert('Publisher Id Already Exist');</script>");
            }
            else
            {

                addPublisher();
            }
        }

        protected void PublisherUpdateButton_Click(object sender, EventArgs e)
        {
                if (ClickIfPublisherExist())
                {
                    updatePublisher();


                }
                else
                {

                    Response.Write("<script>alert('Publisher Id not Exist');</script>");
                }
        }

        protected void PublisherDeleteButton_Click(object sender, EventArgs e)
        {
            if (ClickIfPublisherExist())
            {
                deletePublisher();


            }
            else
            {

                Response.Write("<script>alert('Publisher Id not Exist');</script>");
            }
        }


        bool ClickIfPublisherExist()
        {
            try
            {
                SqlConnection connection = new SqlConnection(strcon);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                SqlCommand command = new SqlCommand("select * from publisher_master_tbl where publisher_id = '"+PublisherIDTextBox.Text.Trim()+"'", connection);
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
                connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        void addPublisher()
        {
            SqlConnection connection = new SqlConnection(strcon);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            SqlCommand command = new SqlCommand("insert into publisher_master_tbl(publisher_id, publisher_name)values(@publisher_id,@publisher_name)", connection);
            command.Parameters.AddWithValue("@publisher_id", PublisherIDTextBox.Text.Trim());
            command.Parameters.AddWithValue("@publisher_name", PublisherNameTextBox.Text.Trim());
            command.ExecuteNonQuery();
            Response.Write("<script>alert('Publisher Add Successfully Complete');</script>");
            ClearInputBox();

        }

        void updatePublisher()
        {
            try
            {

                SqlConnection connection = new SqlConnection(strcon);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("update publisher_master_tbl set publisher_name = @publisher_name where publisher_id = '" + PublisherIDTextBox.Text.Trim() + "'", connection);
                command.Parameters.AddWithValue("@publisher_name", PublisherNameTextBox.Text.Trim());
                command.ExecuteNonQuery();
                command.Clone();
                Response.Write("<script>alert('publisher is Updated');</script>");

                ClearInputBox();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        void deletePublisher()
        {
            try
            {
                SqlConnection connection = new SqlConnection(strcon);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("delete publisher_master_tbl where publisher_id = '"+PublisherIDTextBox.Text.Trim()+"'", connection);
                command.ExecuteNonQuery();
                Response.Write("<script>alert('publisher is Deleted');</script>");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        void ClearInputBox()
        {
            PublisherIDTextBox.Text = "";
            PublisherNameTextBox.Text = "";
        }
    }
}