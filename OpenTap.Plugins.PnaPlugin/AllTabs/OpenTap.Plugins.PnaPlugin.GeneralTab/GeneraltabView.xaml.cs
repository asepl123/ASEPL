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
using Plugger.Contract;

namespace OpenTap.Plugins.PnaPlugin.GeneralTab
{
	/// <summary>
	/// Interaction logic for GeneraltabView.xaml
	/// </summary>
	public partial class GeneraltabView : UserControl,IPlugger
	{
		public GeneraltabView()
		{
			InitializeComponent();
		}

		public string PluggerName { get; set; } = "General";
		public UserControl GetPlugger() => this;
	}
}
