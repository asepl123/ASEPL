using Plugger.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace OpenTap.Plugins.PnaPlugin.MainTab
{
	/// <summary>
	/// Interaction logic for MaintabView.xaml
	/// </summary>
	public partial class MaintabView : UserControl
	{
		public MaintabView()
		{
			InitializeComponent();
            LoadView();
		}

        /// <summary>
        /// Load all IPlggers available in PlugBoard Folder
        /// </summary>
        public void LoadView()
        {
            try
            {
                //Configure path of folder to access all calculate libraries
                //string plugName = @"C:\Program Files\OpenTAP\Packages\0_Plugin\";
                string plugPath = Directory.GetCurrentDirectory();
                string plugName = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Packages", "0_Plugin");
                TabItem buttonA = new TabItem();
                int myHeight = 40;
                buttonA.Header = "Welcome";
                buttonA.Height = myHeight;
                buttonA.Content = "You welcome :)\n" + plugName;
                tabPlugs.Items.Add(buttonA);

                var connectors = Directory.GetDirectories(plugName);
                foreach (var connect in connectors)
                {
                    string dllPath = GetPluggerDll(connect);
                    Assembly _Assembly = Assembly.LoadFile(dllPath);
                    var types = _Assembly.GetTypes()?.ToList();
                    var type = types?.Find(a => typeof(IPlugger).IsAssignableFrom(a));
                    var win = (IPlugger)Activator.CreateInstance(type);
                    TabItem button = new TabItem
                    {
                        Header = win.PluggerName,
                        Height = myHeight,
                        Content = win.GetPlugger()
                    };
                    tabPlugs.Items.Add(button);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Internal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetPluggerDll(string connect)
        {
            var files = Directory.GetFiles(connect, "*.dll");
            foreach (var file in files)
            {
                if (FileVersionInfo.GetVersionInfo(file).ProductName.StartsWith("PnaPlugins"))
                    return file;
            }
            return string.Empty;
        }
    }
}
