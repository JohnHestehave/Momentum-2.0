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

namespace Momentum_2._0
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Window
	{
		public MainMenu()
		{
			InitializeComponent();
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

		void Soeg(object sender, RoutedEventArgs e)
		{
			string soeg = soegBox.Text;
			if(soeg != "")
			{
				List<char> nums = new List<char>() {'1', '2', '3','4','5','6','7','8','9','0'};
				SQLSimplifier sql = new SQLSimplifier();
				sql.Connect("ealdb1.eal.local", "EJL34_DB", "ejl34_usr", "Baz1nga34");
				if (soeg.Contains('@')) // E-mail
				{
					Dictionary<string, string> data = new Dictionary<string, string>();
					data = sql.Select("*", "AarskortMomentum", "WHERE Email LIKE '%email%'");
					UpdateData(data);
				}
				else if (nums.Any(s=>soeg.Contains(s))) // Tlf nr.
				{
					Dictionary<string, string> data = new Dictionary<string, string>();
					data = sql.Select("*", "AarskortMomentum", "WHERE Tlf LIKE '%tlf%'");
					UpdateData(data);
				}
				else // Navn
				{
					Dictionary<string, string> data = new Dictionary<string, string>();
					data = sql.Select("*", "AarskortMomentum", "WHERE Fornavn LIKE '%fornavn%' OR Efternavn LIKE '%efternavn%'");
					UpdateData(data);
				}
			}
		}

		void UpdateData(Dictionary<string, string> data)
		{

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
			MessageBox.Show("Opret");
		}
		void EditPerson(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Rediger");
		}
		void DeletePerson(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Slet");
		}

		void UpdateDataGrid(object sender, RoutedEventArgs e)
		{
			//SQLSimplifier sql = new SQLSimplifier();
			//sql.Connect("ealdb1.eal.local", "EJL34_DB", "ejl34_usr", "Baz1nga34");

			//dataGrid.Items.Clear();
			//dataGrid.Columns.Clear();
			dataGrid.Items.Clear();
			dataGrid.Items.Refresh();
			


			dataGrid.Items.Add(new Item() { ID = "1", Fornavn = "Lars", Efternavn = "Larsen", Foedselsdato = "01-05-1990", Email = "LarsLarsen@LL.dk", Tlf = "12457889" });
			dataGrid.Items.Add(new Item() { ID = "2", Fornavn = "Hans", Efternavn = "Hansen", Foedselsdato = "04-11-1985", Email = "HansHansen@HH.dk", Tlf = "45986578" });
			dataGrid.Items.Add(new Item() { ID = "3", Fornavn = "Simon", Efternavn = "Simonsen", Foedselsdato = "06-06-1960", Email = "SimonSimonsen@SS.dk", Tlf = "12548723" });
			dataGrid.Items.Add(new Item() { ID = "4", Fornavn = "Ole", Efternavn = "Olsen", Foedselsdato = "15-10-1940", Email = "OleOlsen@OO.dk", Tlf = "56874457" });

		}

	}
	public class Item
	{
		public string ID { get; set; }
		public string Fornavn { get; set; }
		public string Efternavn { get; set; }
		public string Foedselsdato { get; set; }
		public string Email { get; set; }
		public string Tlf { get; set; }
	}
}
