using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Momentum_2._0
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		public void SubmitLogin(object sender, RoutedEventArgs e)
		{
			
			//MessageBox.Show("");
			//return;
			//MessageBox.Show("Brugernavn: "+textBox.Text+"\nAdgangskode: "+passwordBox.Password);
			string brugernavn = textBox.Text;
			string adgangskode = passwordBox.Password;
			/*
			SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
			try
			{
				sql.Open();
				sql.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("Der skete en fejl.");
			}
			sql.Open();
			string query = "SELECT TOP 1 Brugernavn, Adgangskode FROM MomentumLogin WHERE Brugernavn = @brugernavn AND Adgangskode = @adgangskode collate Latin1_General_Bin";
			SqlCommand cmd = new SqlCommand(query, sql);
			cmd.Parameters.AddWithValue("brugernavn", brugernavn);
			cmd.Parameters.AddWithValue("adgangskode", adgangskode);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				reader.Read();
				if(brugernavn == (string)reader["Brugernavn"] && adgangskode == (string)reader["Adgangskode"])
				{
					sql.Close();
					sql.Dispose();*/
					MainMenu win = new MainMenu();
					win.Username(textBox.Text);
					win.Show();
					this.Close();/*
					return;
				}
			}
			else
			{
				MessageBox.Show("Forkert brugernavn eller adgangskode");
				sql.Close();
				sql.Dispose();
			}
			*/
		}
	}
}

