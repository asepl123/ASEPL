using System;
using System.Windows;
using Keysight.OpenTap.Wpf;

using GeneralView = GeneralTab.GeneralView;


// This file shows how to implement a custom dockable panel. The panel can be enabled/disabled under 
// the View menu choice in the TAP GUI. The panel can be configured to be either floating or docked.

namespace OpenTap.Plugins.PnaPlugin.MyDockablePanel
{
    [Display("Dockable Panel User Control 1")]
    // A custom dockable panel has to implement ITapDockPanel. 
    public class DockablePanelUserControl1 : ITapDockPanel
    {
        // Default panel dimensions
        public double? DesiredWidth { get { return 850; } }

        public double? DesiredHeight { get { return 500; } }



        static TraceSource Log = OpenTap.Log.CreateSource("DockExample");

        // In this method the layout of the dockable panel is defined/setup. 
        // The ITapDockContext enables you to set the TestPlan, attach ResultListeners, 
        // configure Settings and start execution of a TestPlan. 


        [Obsolete]
        public FrameworkElement CreateElement(ITapDockContext context)
        {
            GeneralView generalView = new GeneralView();

            // Display UserControl "generalView" into a Window

            return generalView;
        }

    }
}