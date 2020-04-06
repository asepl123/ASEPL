using Plugger.Contract;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

//using Plugger;

namespace GeneralTab
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        [Obsolete]
        public GeneralView()
        {
            InitializeComponent();
            LoadView();
        }

        /// <summary>
        /// Load all IPlggers available in PlugBoard Folder
        /// </summary>
        [Obsolete]
        public void LoadView()
        {
            try
            {
                //Configure path of PlugBoard folder to access all calculate libraries 
                //string plugName = ConfigurationSettings.AppSettings["Plugs"].ToString();
                string plugName = @"C:\Program Files\OpenTAP\Packages\0_Plugin\";
                TabItem buttonA = new TabItem();
                int myHeight = 40;
                buttonA.Header = "Welcome";
                buttonA.Height = myHeight;
                buttonA.Content = "You welcome :)";
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
