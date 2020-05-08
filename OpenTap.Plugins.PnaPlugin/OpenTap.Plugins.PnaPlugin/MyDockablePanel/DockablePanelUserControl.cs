using System;
using System.Windows;
using System.IO;
using Keysight.OpenTap.Wpf;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Windows.Controls;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using Label = System.Windows.Forms.Label;
using System.Windows.Media;
using OpenTap.Plugins.PnaPlugin.MainTab;

// This file shows how to implement a custom dockable panel. The panel can be enabled/disabled under 
// the View menu choice in the TAP GUI. The panel can be configured to be either floating or docked.

namespace OpenTap.Plugins.PnaPlugin.MyDockablePanel
{
    [Display("Dockable Panel User Control")]
    // A custom dockable panel has to implement ITapDockPanel.
    public class DockablePanelUserControl : ITapDockPanel
    {
        // Default panel dimensions
        public double? DesiredWidth { get { return 400; } }

        public double? DesiredHeight { get { return 400; } }



        static TraceSource Log = OpenTap.Log.CreateSource("DockExample");

        // In this method the layout of the dockable panel is defined/setup.
        // The ITapDockContext enables you to set the TestPlan, attach ResultListeners,
        // configure Settings and start execution of a TestPlan.


        public FrameworkElement CreateElement(ITapDockContext context)
        {

            MaintabView mainView = new MaintabView();

            // Display UserControl "generalView" into a Window

            return mainView;
        }

    }
}