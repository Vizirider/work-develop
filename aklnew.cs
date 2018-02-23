﻿/*
 * Created by SharpDevelop.
 * User: vizir
 * Date: 2017-09-12
 * Time: 01:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

namespace Registers
{
	/// <summary>
	/// Description of aklnew.
	/// </summary>
	public partial class aklnew : Form
	{
		private readonly Liquidinster.MainForm frm1;
		public aklnew(string mws, string po, Liquidinster.MainForm frm)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.comboBox2.Text = mws;
			this.comboBox3.Text = mws;
			this.comboBox1.Text = po;
			frm1 = frm;
			this.Button3Click(null, null);
		}
		void Button3Click(object sender, EventArgs e)
		{
		using (SqlConnection connection =  new SqlConnection("server=gmacsm0001dp;database=Production_test;Integrated Security=SSPI"))
		{
	    SqlCommand command =
	    new SqlCommand("select * from dbo.akla WHERE POszam=('" + comboBox1.Text +"')", connection);
	    connection.Open();
	
	    SqlDataReader read= command.ExecuteReader();

			    while (read.Read())
			    {
			        comboBox1.Text = (read["POszam"].ToString());
			        textBox1.Text = (read["Anyagkod"].ToString());
			        textBox2.Text = (read["Anyagnev"].ToString());
			        checkBox1.Checked = (bool)read["Kimerve"];
			        checkBox2.Checked = (bool)read["Csomomentes"];
			        checkBox3.Checked = (bool)read["Felcimkezve"];
			        checkBox5.Checked = (bool)read["Tulmeres"];
			        textBox5.Text = (read["Lastzsakok"].ToString());
			        textBox3.Text = (read["Komment"].ToString());
			        dateTimePicker1.Text = Convert.ToDateTime(read["Datum"]).ToString();
			        comboBox2.Text = (read["Ellenorzo"].ToString());
			        comboBox3.Text = (read["Ki"].ToString());
			        textBox4.Text = (read["Kimervenon"].ToString());
			        textBox6.Text = (read["Csomomentesnon"].ToString());
			        textBox7.Text = (read["Felcimkezvenon"].ToString());
			        textBox8.Text = (read["Tulmeresnon"].ToString());
			      
			    }
			    read.Close();
			}
	
	
		}
		void Button2Click(object sender, EventArgs e)
		{
			SqlConnection conn = new SqlConnection("server=gmacsm0001dp;database=Production_test;Integrated Security=SSPI");
			conn.Open();
			SqlCommand cmd = new SqlCommand(@"Update dbo.akla set Ellenorizve = 1, Ki='" + comboBox3.Text + "' WHERE POszam LIKE ('" + comboBox1.Text +"%')",conn);
			cmd.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show("Sikeresen ellenőrizted a PO-t", "Üzenet");
			frm1.Refresh();
			this.Close();
		}
		void Button1Click(object sender, EventArgs e)
		{
			SqlConnection conn = new SqlConnection("server=gmacsm0001dp;database=Production_test;Integrated Security=SSPI");
			conn.Open();
			SqlCommand cmd = new SqlCommand(@"Update dbo.akla set POszam = @POszam, Anyagkod = @Anyagkod, Anyagnev = @Anyagnev, Kimerve = @Kimerve, Csomomentes = @Csomomentes, Felcimkezve = @Felcimkezve, Tulmeres = @Tulmeres, Lastzsakok = @Lastzsakok, Komment =@Komment, Datum = @Datum, Ellenorzo = @Ellenorzo, Ellenorizve = @Ellenorizve, Ki = @Ki,
			Kimervenon = @Kimervenon, Csomomentesnon = @Csomomentesnon, Felcimkezvenon = @Felcimkezvenon, Tulmeresnon = @Tulmeresnon 
			WHERE POszam = ('" + comboBox1.Text +"')",conn);
			cmd.Parameters.Add(new SqlParameter("@POszam", comboBox1.Text));
			cmd.Parameters.Add(new SqlParameter("@Anyagkod", textBox1.Text));
			cmd.Parameters.Add(new SqlParameter("@Anyagnev", textBox2.Text));
			cmd.Parameters.Add(new SqlParameter("@Kimerve", checkBox1.Checked));
			cmd.Parameters.Add(new SqlParameter("@Csomomentes", checkBox2.Checked));
			cmd.Parameters.Add(new SqlParameter("@Felcimkezve", checkBox3.Checked));
			cmd.Parameters.Add(new SqlParameter("@Tulmeres", checkBox5.Checked));
			cmd.Parameters.Add(new SqlParameter("@Lastzsakok", textBox5.Text));
			cmd.Parameters.Add(new SqlParameter("@Komment", textBox3.Text));
			cmd.Parameters.Add(new SqlParameter("@Datum", dateTimePicker1.Value.Date));
			cmd.Parameters.Add(new SqlParameter("@Ellenorzo", comboBox2.Text));
			cmd.Parameters.Add(new SqlParameter("@Ellenorizve", checkBox4.Checked));
			cmd.Parameters.Add(new SqlParameter("@Ki", comboBox3.Text));
			cmd.Parameters.Add(new SqlParameter("@Kimervenon", textBox4.Text));
			cmd.Parameters.Add(new SqlParameter("@Csomomentesnon", textBox6.Text));
			cmd.Parameters.Add(new SqlParameter("@Felcimkezvenon", textBox7.Text));
			cmd.Parameters.Add(new SqlParameter("@Tulmeresnon", textBox8.Text));
			cmd.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show("Sikeresen módosítottad a PO-t", "Üzenet"); 
	
		}
		void AklnewLoad(object sender, EventArgs e)
		{
			if(checkBox1.Checked == false)
			{
				checkBox1.BackColor = Color.Red;
			}
			if(checkBox2.Checked == false)
			{
				checkBox2.BackColor = Color.Red;
			}
			if(checkBox3.Checked == false)
			{
				checkBox3.BackColor = Color.Red;
			}	
			if(checkBox5.Checked == true)
			{
				checkBox5.BackColor = Color.Red;
			}	
		}
	}
}
