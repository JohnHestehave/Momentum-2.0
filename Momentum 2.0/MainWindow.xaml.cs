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
			//MessageBox.Show("Brugernavn: "+textBox.Text+"\nAdgangskode: "+passwordBox.Password);
			MainMenu win = new MainMenu();
			win.Username(textBox.Text);
			win.Show();
			this.Close();
		}
	}
}
