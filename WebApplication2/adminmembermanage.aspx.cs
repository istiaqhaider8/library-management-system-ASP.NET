using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class adminmembermanage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            AdminGridView.DataBind();
        }

        protected void GoLinkButton_Click(object sender, EventArgs e)
        {

            getMemberById();

        }

        protected void ActiveLinkButton_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("Active");
        }

        protected void PendingLinkButton_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("Pending");
        }

        protected void deactiveLinkButton_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("Deactive");
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteMemberByID();
        }

        bool checkifMemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand command = new SqlCommand("  select * from member_master_tbl where member_id = '" + MemberIdTextBox.Text.Trim() + "'");
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
            catch (Exception ex)
            {
                Response.Write("<Script>alert('" + ex.Message + "')");
                return false;
            }
        }

        void getMemberById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand command = new SqlCommand("select * from member_master_tbl where member_id = '"+MemberIdTextBox.Text.Trim()+"'", con);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MemberIdTextBox.Text = reader.GetValue(7).ToString();
                        MemberFullNameTextBox.Text = reader.GetValue(0).ToString();
                        MemberAccountStatusTextBox.Text = reader.GetValue(9).ToString();
                        MemberDOBTextBox.Text = reader.GetValue(1).ToString();
                        MemberContactNoTextBox.Text = reader.GetValue(2).ToString();
                        MemberEmailIdTextBox.Text = reader.GetValue(3).ToString();
                        StateTextBox.Text = reader.GetValue(4).ToString();
                        MemberCityTextBox.Text = reader.GetValue(10).ToString();
                        MemberPinCodeTextBox.Text = reader.GetValue(5).ToString();
                        FullPostalAddressTextBox.Text = reader.GetValue(6).ToString();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('" + ex.Message + "')</Script>");
               
            }
        }

        void updateMemberStatusByID(string status)
        {
            SqlConnection connection = new SqlConnection(strcon);
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand("update member_master_tbl set account_status = '"+status+"' where member_id = '"+MemberIdTextBox.Text.Trim()+"'", connection);
            command.ExecuteNonQuery();
            
            connection.Close();

            Response.Write("<script>alert('Empolyee status Updated');</script>");
        }

        void DeleteMemberByID()
        {
            SqlConnection connection = new SqlConnection(strcon);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand("Delete member_master_tbl where member_id = '" + MemberIdTextBox.Text.Trim() + "'", connection);
            command.ExecuteNonQuery();
            connection.Close();
            Response.Write("<script>alert('Empolyee Deleted');</script>");
            clearFrom();
            AdminGridView.DataBind();
        }

        void clearFrom()
        {
            MemberIdTextBox.Text = " ";
            MemberFullNameTextBox.Text = " ";
            MemberAccountStatusTextBox.Text = " ";
            MemberDOBTextBox.Text = " ";
            MemberContactNoTextBox.Text = " ";
            MemberEmailIdTextBox.Text = " ";
            StateTextBox.Text = " ";
            MemberCityTextBox.Text = " ";
            MemberPinCodeTextBox.Text = " ";
            FullPostalAddressTextBox.Text = " ";

        }
    }
}