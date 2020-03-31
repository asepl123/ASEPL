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

// This file shows how to implement a custom dockable panel. The panel can be enabled/disabled under 
// the View menu choice in the TAP GUI. The panel can be configured to be either floating or docked.

namespace OpenTap.Plugins.PnaPlugin
{
    [Display("Dockable Panel Example")]
    // A custom dockable panel has to implement ITapDockPanel. 
    public class DockablePanel : ITapDockPanel
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

            // Create the Grid

            Grid DynamicGrid = new Grid();
            DynamicGrid.Width = 400;
            DynamicGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            DockPanel.SetDock(DynamicGrid, Dock.Left);

            // Create Columns

            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);

            // Create Rows

            RowDefinition gridRow1 = new RowDefinition() { Height = new GridLength(45) };
            RowDefinition gridRow2 = new RowDefinition() { Height = new GridLength(45) };
            RowDefinition gridRow3 = new RowDefinition() { Height = new GridLength(45) };

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);

            // Add first column header

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Name of Project";
            txtBlock1.FontSize = 14;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Green);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            // Add second column header

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Age";
            txtBlock2.FontSize = 14;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Green);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(txtBlock2, 1);
            Grid.SetColumn(txtBlock2, 0);

            // Add column headers to the Grid

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);

            // Create first Row

            TextBlock authorText = new TextBlock();
            authorText.Text = "Mahesh Chand";
            authorText.FontSize = 12;
            authorText.FontWeight = FontWeights.Bold;
            authorText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(authorText, 0);
            Grid.SetColumn(authorText, 1);

            TextBlock ageText = new TextBlock();
            ageText.Text = "33";
            ageText.FontSize = 12;
            ageText.FontWeight = FontWeights.Bold;
            ageText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(ageText, 1);
            Grid.SetColumn(ageText, 1);

            // Add first row to Grid

            DynamicGrid.Children.Add(authorText);
            DynamicGrid.Children.Add(ageText);

            // Create second row

            authorText = new TextBlock();
            authorText.Text = "Mike Gold";
            authorText.FontSize = 12;
            authorText.FontWeight = FontWeights.Bold;
            Grid.SetRow(authorText, 2);
            Grid.SetColumn(authorText, 0);

            ageText = new TextBlock();
            ageText.Text = "35";
            ageText.FontSize = 12;
            ageText.FontWeight = FontWeights.Bold;
            Grid.SetRow(ageText, 2);
            Grid.SetColumn(ageText, 1);

            // Add second row to Grid

            DynamicGrid.Children.Add(authorText);
            DynamicGrid.Children.Add(ageText);

            // Display grid into a Window
            
            return DynamicGrid;
        }

    }
}