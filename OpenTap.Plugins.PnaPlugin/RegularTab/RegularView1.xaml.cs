using Plugger.Contract;
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

namespace RegularTab
{
	/// <summary>
	/// Interaction logic for RegularView1.xaml
	/// </summary>
	public partial class RegularView1 : UserControl,IPlugger
	{
		public RegularView1()
		{
			InitializeComponent();
		}

		public string PluggerName { get; set; } = "General Settings";

		public UserControl GetPlugger() => this;
	}
}
