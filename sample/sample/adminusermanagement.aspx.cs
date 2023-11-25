using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sample
{
	public partial class adminusermanagement : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			GridView1.DataBind();
		}

		//search button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
			getUserByID();

		}
		//active button
		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			updateUserStatusByID("active");
		}
		//pending button
		protected void LinkButton2_Click(object sender, EventArgs e)
		{
			updateUserStatusByID("pending");
		}
		//deactive button
		protected void LinkButton3_Click(object sender, EventArgs e)
		{
			updateUserStatusByID("deactive");
		}
		//delete button
		protected void Button2_Click(object sender, EventArgs e)
		{
			deleteUserByID();

		}

		// user defined function
		bool checkIfMemberExists()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("SELECT * from user_master_tbl where user_id='" + TextBox1.Text.Trim() + "';", con);
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);

				if (dt.Rows.Count >= 1)
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
				Response.Write("<script>alert('" + ex.Message + "');</script>");
				return false;
			}
		}
		//search button work
		void getUserByID()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();

				}
				SqlCommand cmd = new SqlCommand("select * from user_master_tbl where user_id='" + TextBox1.Text.Trim() + "'", con);
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{
					while (dr.Read())
					{
						TextBox2.Text = dr.GetValue(0).ToString();
						TextBox7.Text = dr.GetValue(10).ToString();
						TextBox8.Text = dr.GetValue(1).ToString();
						TextBox3.Text = dr.GetValue(2).ToString();
						TextBox4.Text = dr.GetValue(3).ToString();
						TextBox9.Text = dr.GetValue(4).ToString();
						TextBox10.Text = dr.GetValue(5).ToString();
						TextBox11.Text = dr.GetValue(6).ToString();
						TextBox6.Text = dr.GetValue(7).ToString();

					}

				}
				else
				{
					Response.Write("<script>alert('Invalid credentials');</script>");
				}

			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void updateUserStatusByID(string status)
		{
			if (checkIfMemberExists())
			{
				try
				{
					SqlConnection con = new SqlConnection(strcon);
					if (con.State == ConnectionState.Closed)
					{
						con.Open();

					}
					SqlCommand cmd = new SqlCommand("UPDATE user_master_tbl SET account_status='" + status + "' WHERE user_id='" + TextBox1.Text.Trim() + "'", con);
					cmd.ExecuteNonQuery();
					con.Close();
					GridView1.DataBind();
					Response.Write("<script>alert('User Status Updated');</script>");


				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}
			}
			else
			{
				Response.Write("<script>alert('Invalid User ID');</script>");
			}

		}
		//delete work
		void deleteUserByID()
		{
			if (checkIfMemberExists())
			{
				try
				{
					SqlConnection con = new SqlConnection(strcon);
					if (con.State == ConnectionState.Closed)
					{
						con.Open();
					}

					SqlCommand cmd = new SqlCommand("DELETE from user_master_tbl WHERE user_id='" + TextBox1.Text.Trim() + "'", con);

					cmd.ExecuteNonQuery();
					con.Close();
					Response.Write("<script>alert('User Deleted Successfully');</script>");
					clearForm();
					GridView1.DataBind();

				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}

			}
			else
			{
				Response.Write("<script>alert('Invalid user ID');</script>");
			}
		}
		//clear
		void clearForm()
		{
			TextBox1.Text = "";
			TextBox2.Text = "";
			TextBox7.Text = "";
			TextBox8.Text = "";
			TextBox3.Text = "";
			TextBox4.Text = "";
			TextBox9.Text = "";
			TextBox10.Text = "";
			TextBox11.Text = "";
			TextBox6.Text = "";
		}
	}
}