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
	/// Interaction logic for CreateAndEditMenu.xaml
	/// </summary>
	public partial class CreateAndEditMenu : Window
	{
		Item item;
		bool EditMode;
		public CreateAndEditMenu()
		{
			InitializeComponent();
			this.Title = "Opret";
			EditMode = false;
		}

		public CreateAndEditMenu(Item item)
		{
			InitializeComponent();
			this.item = item;
			this.Title = "Rediger";
			fornavn.Text = item.Fornavn;
			efternavn.Text = item.Efternavn;
			efternavn.Text = item.Efternavn;
			adresse.Text = item.Adresse;
			postnr.Text = item.Postnr;
			by.Text = item.City;
			email.Text = item.Email;
			udloebsdato.SelectedDate = item.UdloebsDato;
			korttype.Text = item.AarskortType;
			tlf.Text = item.Tlf;
			kommentar.Text = item.Kommentar;
			EditMode = true;
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{

			if (fornavn.Text != "" && efternavn.Text != "" && adresse.Text != "" && postnr.Text != "" && by.Text != "" && email.Text != "" && tlf.Text != "" && udloebsdato.SelectedDate.Value.Date.ToString("yyyy-MM-dd") != "" && korttype.Text != "")
			{
				
				SQLSimplifier sql = new SQLSimplifier();
				sql.Connect("ealdb1.eal.local", "EJL34_DB", "ejl34_usr", "Baz1nga34");
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("Fornavn", fornavn.Text);
				data.Add("Efternavn", efternavn.Text);
				data.Add("Mail", email.Text);
				data.Add("Adresse", adresse.Text);
				data.Add("Postnr", postnr.Text);
				data.Add("city", by.Text);
				data.Add("Tlf", tlf.Text);
				string today = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd");
				
				data.Add("UdloebsDato", udloebsdato.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
				data.Add("AarskortType", korttype.Text);
				data.Add("Andet", kommentar.Text);
				if (EditMode)
				{
					if (sql.Update("AarskortMomentum", data, item.ID) > 0)
					{
						MessageBox.Show("Årskortet er ændret med success!");
						this.Close();
					}else
					{
						MessageBox.Show("Der skete en fejl, prøv igen.");
					}
				}else
				{
					data.Add("IndloestDato", today);
					if (sql.Insert("AarskortMomentum", data) > 0)
					{
						MessageBox.Show("Årskort er nu oprettet");
						this.Close();
					}
					else
					{
						MessageBox.Show("Der skete en fejl, prøv igen.");
					}
				}
				
				

			}
			else
			{
				MessageBox.Show("Du har glemt at udfylde et felt.");
			}

		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
