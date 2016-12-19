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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Momentum_2._0
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Window
	{
		string search;
		public MainMenu()
		{
			InitializeComponent();
			search = "";
		}

		public void Username(string username)
		{
			mainmenulabel.Content = "Logget ind som: "+username;
		}

		void Logout(object sender, RoutedEventArgs e)
		{
			MainWindow win = new MainWindow();
			win.Show();
			this.Close();
		}

		public void Soeg_Focus(object sender, RoutedEventArgs e)
		{
			if (soegBox.Text == "Søg efter navn/tlf/email")
			{
				soegBox.Text = "";
			}
		}

		public void Soeg_Unfocus(object sender, RoutedEventArgs e)
		{
			if (soegBox.Text == "")
			{
				soegBox.Text = "Søg efter navn/tlf/email";
			}
		}

		void Soeg(object sender, RoutedEventArgs e)
		{
			search = soegBox.Text;
			UpdateDataGrid(null, null);
		}

		void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Item item = (Item)dataGrid.SelectedItem;
			if(item != null)
			{
				Edit.IsEnabled = true;
				Delete.IsEnabled = true;
			}
			//MessageBox.Show(item.ID);
		}

		void CreatePerson(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Opret");
			CreateAndEditMenu page = new CreateAndEditMenu();
			page.Show();
		}
		void EditPerson(object sender, RoutedEventArgs e)
		{
			Item item = (Item)dataGrid.SelectedItem;
			if(item != null)
			{
				CreateAndEditMenu page = new CreateAndEditMenu(item);
				page.Show();
			}
		}
		void DeletePerson(object sender, RoutedEventArgs e)
		{
			Item item = (Item)dataGrid.SelectedItem;
			if (item != null)
			{
				if (MessageBox.Show("Er du sikker på du vil slette denne kunde fra databasen?", "Slet kunde", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
				{
					SQLSimplifier sql = new SQLSimplifier();
					sql.Connect("ealdb1.eal.local", "EJL34_DB", "ejl34_usr", "Baz1nga34");
					if(sql.Delete("AarskortMomentum", "ID", item.ID.ToString()) > 0)
					{
						MessageBox.Show("Kunden er nu slettet!");
						UpdateDataGrid(null, null);
					}
				}
			}
		}

		void UpdateDataGrid(object sender, RoutedEventArgs e)
		{
			dataGrid.Items.Clear();
			dataGrid.Items.Refresh();

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
			string query = "SELECT ID, Fornavn, Efternavn, Mail, Adresse, Postnr, Tlf, IndloestDato, UdloebsDato, AarskortType, Andet, city FROM AarskortMomentum";
			SqlCommand cmd;
			if (search != "" && search != "Søg efter navn/tlf/email")
			{
				List<char> nums = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
				if (search.Contains('@')) // E-mail
				{
					query += " WHERE Mail LIKE @mail";
					cmd = new SqlCommand(query, sql);
					cmd.Parameters.AddWithValue("mail", "%"+search+"%");
				}
				else if (nums.Any(s => search.Contains(s))) // Tlf nr.
				{
					query += " WHERE Tlf LIKE @tlf";
					cmd = new SqlCommand(query, sql);
					cmd.Parameters.AddWithValue("tlf", "%"+soegBox.Text+"%");
				}
				else // Navn
				{
					query += " WHERE Fornavn LIKE @fornavn OR Efternavn LIKE @efternavn";
					cmd = new SqlCommand(query, sql);
					cmd.Parameters.AddWithValue("fornavn", "%"+soegBox.Text+"%");
					cmd.Parameters.AddWithValue("efternavn", "%"+soegBox.Text+"%");
				}
			}else
			{
				cmd = new SqlCommand(query, sql);
			}
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				int rows = 0;
				while (reader.Read())
				{
					rows++;
					dataGrid.Items.Add(new Item() { ID = Convert.ToInt32(reader["ID"]), Fornavn = reader["Fornavn"].ToString(), Efternavn = reader["Efternavn"].ToString(), Adresse = reader["Adresse"].ToString(), Postnr = reader["Postnr"].ToString()
						, City = reader["city"].ToString(), Email = reader["Mail"].ToString(), IndloestDato = Convert.ToDateTime(reader["IndloestDato"]), UdloebsDato = Convert.ToDateTime(reader["UdloebsDato"]), AarskortType = reader["AarskortType"].ToString()
						, Tlf = reader["Tlf"].ToString(), Kommentar = reader["Andet"].ToString()});
				}
				soegresultat.Content = rows + " resultater fundet";
			}
		}

		private void KopierMail(object sender, RoutedEventArgs e)
		{

		}
		private void KopierMailAlle(object sender, RoutedEventArgs e)
		{

		}

		private void UpdateDataGridUdloeb(object sender, RoutedEventArgs e)
		{
			dataGridUdloeb.Items.Clear();
			dataGridUdloeb.Items.Refresh();

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
			string query = "SELECT ID, UdloebsDato FROM AarskortMomentum";
			SqlCommand cmd = new SqlCommand(query, sql);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					dataGridUdloeb.Items.Add(new UdloebItem() { ID = Convert.ToInt32(reader["ID"]), UdloebsDato = Convert.ToDateTime(reader["UdloebsDato"])});
				}
			}
		}

		private void dataGridUdloeb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UdloebItem item = (UdloebItem)dataGridUdloeb.SelectedItem;
			if (item != null)
			{
				KopierMailButton.IsEnabled = true;
				KopierAlleMailButton.IsEnabled = true;
			}
		}
	}
	public class Item
	{
		public int ID { get; set; }
		public string Fornavn { get; set; }
		public string Efternavn { get; set; }
		public string Adresse { get; set; }
		public string Postnr { get; set; }
		public string City { get; set; }
		public string Email { get; set; }
		public DateTime IndloestDato { get; set; }
		public DateTime UdloebsDato { get; set; }
		public string AarskortType { get; set; }
		public string Tlf { get; set; }
		public string Kommentar { get; set; }
	}
	public class UdloebItem
	{
		public int ID { get; set; }
		public DateTime UdloebsDato { get; set; }
	}
}
