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
     
    public partial class usersingup : System.Web.UI.Page
    {
        private string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //sign up button click event
        protected void SignUpButton_Click(object sender, EventArgs e)
        {

            if (CheckMemberExist())
            {
                Response.Write("<script>alert('Member already Exist ');</script>");
            }
            else
            {
                singUpNewUser();
            }
            
        }

        bool CheckMemberExist()
        {

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from member_master_tbl where member_id = '"+ userIdTextBox.Text.Trim() + "';", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count >= 1)
                {
                    return true;
                }

                else
                {
                    return false;
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }

            
        }

        private void singUpNewUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("insert into member_master_tbl(full_name,dob,contact_no,email,state,pincode,full_address,member_id,password,account_status,city)Values(@full_name,@dob,@contact_no,@email,@state,@pincode,@full_address,@member_id,@password,@account_status,@city)", con);
                cmd.Parameters.AddWithValue("@full_name", fullNameTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", dobTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", contactNoTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@email", emailTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@state", statsDropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@pincode", pincodeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", fullAddressTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", userIdTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@password", passwordTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");
                cmd.Parameters.AddWithValue("@city", cityTextBox.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('user is added');</script>");
            }
            catch (Exception)
            {
                Response.Write("<script>alert(Data Not Save);</script>");
            }
        }
    }
}