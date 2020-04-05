using System.Windows.Forms;
using OpenTap;  // Use OpenTAP infrastructure/core components (log,TestStep definition, etc)

namespace OpenTap.Plugins.PnaPlugin.MyInstrument
{
	[Display("PNA", Groups: new[] { "SAC" }, Description: "A Keysight PNA Instrument")]
	public class KeysightPna : ScpiInstrument
	{

        public KeysightPna()
		{
            Name = "PNA N5224A";
            VisaAddress = "Simulate";
		}

        public override void Open()
        {
            // This will be called at the beginning of TestPlan execution.
            base.Open();

            // Instruments also have access to a Log object.
            MessageBox.Show($"Connection to {VisaAddress} successful");
            Log.Info(string.Format("Connection to {0} successful", VisaAddress));

        }

        // Close procedure for the instrument.
        public override void Close()
        {
            // Add code needed for closing the resource here.
            // This will be called at the end of TestPlan execution.
            base.Close();
        }

        // Public methods can be called from Test Steps.
        public void SetInputData(double[] inputData)
        {
            // This would be some instrument control code to write the input data to generator.

            // ScpiCommand("INPut {0}", inputData);
        }

    }
}
