using System;
using System.Windows;
using Keysight.OpenTap.Wpf;
using OpenTap.Plugins.PnaPlugin.GeneralTab;

// This file shows how to implement a custom dockable panel. The panel can be enabled/disabled under 
// the View menu choice in the TAP GUI. The panel can be configured to be either floating or docked.

namespace OpenTap.Plugins.PnaPlugin.MyDockablePanel
{
    [Display("All In One User Control")]
    // A custom dockable panel has to implement ITapDockPanel. 
    public class AllInOneUserControl : ITapDockPanel
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
            GeneraltabView generalView = new GeneraltabView();

            // Display UserControl "generalView" into a Window

            return generalView;
        }

    }
}