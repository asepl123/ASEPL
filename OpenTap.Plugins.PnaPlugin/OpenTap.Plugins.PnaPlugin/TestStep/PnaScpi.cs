using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using OpenTap;


namespace OpenTap.Plugins.PnaPlugin
{
    public enum EnumData { REAL, ASCii};
    public enum EnumSweepType { LINear, LOGarithmic, POWer, CW, SEGMent, PHASe };
    public enum EnumCalcFormat { MLINear, MLOGarithmic, PHASe, UPHase, IMAGinary, REAL, POLar, SMITh, SADMittance, SWR, GDELay, KELVin, FAHRenheit, CELSius };

    [Display(Name:"Sweep TestStep", Groups:new[] { "PNA"}, Description: "")]
    public class PnaScpi : TestStep
    {

        //public class Sparameter
        //{
        //    [Display("Name")]
        //    public string Name { get; set; }
        //}

        [Display("SCPI_PNA", "", "Instruments", 1)]
        public KeysightPna SCPI_PNA { get; set; }

        [Display("Channel 1", "Choose Channel Number", "Channels", 2)]
        public bool Ch1 { get; set; } = false;

        [Display(Name: "S11", Description: "Choose S-parameter", Groups: new[] { "Channel 1", "S Parameters" }, Order: 2.1)]
        [EnabledIf("Ch1", true)]
        public bool Ch1_S11 { get; set; } = false;

        [Display(Name: "S12", Description: "Choose S-parameter", Groups: new[] { "Channel 1", "S Parameters" }, Order: 2.1)]
        [EnabledIf("Ch1", true)]
        public bool Ch1_S12 { get; set; } = false;

        [Display(Name: "S21", Description: "Choose S-parameter", Groups: new[] { "Channel 1", "S Parameters" }, Order: 2.1)]
        [EnabledIf("Ch1", true)]
        public bool Ch1_S21 { get; set; } = false;

        [Display(Name: "S22", Description: "Choose S-parameter", Groups: new[] { "Channel 1", "S Parameters" }, Order: 2.1)]
        [EnabledIf("Ch1", true)]
        public bool Ch1_S22 { get; set; } = false;

        [Display("Channel 2", "Choose S-parameter", "Channels", 2)]
        public bool Ch2 { get; set; } = false;

        [Display(Name: "S11", Description: "Choose S-parameter", Groups: new[] { "Channel 2", "S Parameters" }, Order: 2.2)]
        [EnabledIf("Ch2",true)]
        public bool Ch2_S11 { get; set; } = false;
        
        [Display(Name: "S12", Description: "Choose S-parameter", Groups: new[] { "Channel 2", "S Parameters" }, Order: 2.2)]
        [EnabledIf("Ch2", true)]
        public bool Ch2_S12 { get; set; } = false;
        
        [Display(Name: "S12", Description: "Choose S-parameter", Groups: new[] { "Channel 2", "S Parameters" }, Order: 2.2)]
        [EnabledIf("Ch2", true)]
        public bool Ch2_S21 { get; set; } = false;

        [Display(Name: "S22", Description: "Choose S-parameter", Groups: new[] { "Channel 2", "S Parameters" }, Order: 2.2)]
        [EnabledIf("Ch2", true)]
        public bool Ch2_S22 { get; set; } = false;

        //private IList<Sparameter> _Sparameters = new List<Sparameter>();
        //[Display("S parameter")]
        //public IList<Sparameter> Sparameters
        //{
        //    get
        //    {
        //        return _Sparameters;
        //    }
        //    private set
        //    {
        //        if (S11)Sparameters.Add(new Sparameter() { Name = "S11" });
        //        if (S12)Sparameters.Add(new Sparameter() { Name = "S12" });
        //        if (S21)Sparameters.Add(new Sparameter() { Name = "S21" });
        //        if (S22)Sparameters.Add(new Sparameter() { Name = "S22" });
        //    }
        //}

        [Display(Name: "Data Format", "", "Input Parameters", 4)]
        public EnumData Format_Data { get; set; } = EnumData.REAL;

        //[Display("Sweep Channel", "", "Input Parameters", 4)]
		//public uint Sweep_Channel { get; set; } = 1u;

		[Display("Sweep Type", "", "Input Parameters", 4)]
		public EnumSweepType Sweep_Type { get; set; } = EnumSweepType.LINear;

		//[Display("Calc Channel", "", "Input Parameters", 4)]
		//public uint Calc_Channel { get; set; } = 1u;

        [Display("Start Sweep Freq", "Hz", "Input Parameters", 4)]
        [Unit("GHz")]
        public double Start_Sweep_Freq { get; set; } = 1D;

		[Display("Stop Sweep Freq", "", "Input Parameters", 4)]
        [Unit("GHz")]
		public double Stop_Sweep_Freq { get; set; } = 20D;

		[Display("Sweep Points", "", "Input Parameters", 4)]
		public int Sweep_Points { get; set; } = 1001;

        [Display("Calculation Format", "", "Input Parameters", 4)]
        public EnumCalcFormat Calc_Format { get; set; } = EnumCalcFormat.MLINear;

        private double Sweep_Time = 10D;

        private double Q_Time = 0D;

        private double QTime;


        [BrowsableAttribute(true)]
        [Display(Name: "Ecal", Order: 1000)]
        public void WriteCode()
        {
            Log.Info("Ecal Started");
        }

        private void ProcessResults()
        {
            var LogPna = OpenTap.Log.CreateSource("PnaIO");
            LogPna.Info($"Sweep Time : {QTime} and {Q_Time}");
        }

        public override void PrePlanRun()
        {
            base.PrePlanRun();
        }


        public override void Run()
        {
            SCPI_PNA.ScpiCommand("*RST");
            SCPI_PNA.ScpiCommand(":OUTPut:STATe 0");
            SCPI_PNA.ScpiCommand(":FORMat:DATA " + Format_Data);

            if (Ch1)
            {
                uint Channel = 1u;
                if (Ch1_S11)RepeatScpiCmdForChannel(Channel, "S11");
                if (Ch1_S12)RepeatScpiCmdForChannel(Channel, "S12");
                if (Ch1_S21)RepeatScpiCmdForChannel(Channel, "S21");
                if (Ch1_S22)RepeatScpiCmdForChannel(Channel, "S22");
            }

            if (Ch2)
            {
                uint Channel = 2u;
                if (Ch2_S11)RepeatScpiCmdForChannel(Channel, "S22");
                if (Ch2_S12)RepeatScpiCmdForChannel(Channel, "S12");
                if (Ch2_S21)RepeatScpiCmdForChannel(Channel, "S21");
                if (Ch2_S22)RepeatScpiCmdForChannel(Channel, "S22");
            }
            
            //QTime = SCPI_PNA.ScpiQuery<System.Double>(Scpi.Format(":SENSe{0}:SWEep:TIME?", Sweep_Channel), true);
            //Q_Time = SCPI_PNA.ScpiQuery<System.Double>(Scpi.Format(":SENSe{0}:SWEep:TIME?", Sweep_Channel), true);
            this.ProcessResults();
        }

        public void RepeatScpiCmdForChannel(uint Calc_Channel, string Calc_Param)
        {
            string Calc_Name = "Ch" + Calc_Channel.ToString() + "_" + Calc_Param;
            SCPI_PNA.ScpiCommand(":CALCulate{0}:PARameter:DEFine:EXTended {1},{2}", Calc_Channel, Calc_Name, Calc_Param);
            SCPI_PNA.ScpiCommand(":CALCulate{0}:PARameter:SELect {1}", Calc_Channel, Calc_Name);
            SCPI_PNA.ScpiCommand(":SENSe{0}:SWEep:TYPE {1}", Calc_Channel, Sweep_Type);
            SCPI_PNA.ScpiCommand(":SENSe{0}:FREQuency:STARt {1}", Calc_Channel, Start_Sweep_Freq * 1.0E06);
            SCPI_PNA.ScpiCommand(":SENSe{0}:FREQuency:STOP {1}", Calc_Channel, Stop_Sweep_Freq * 1.0E06);
            SCPI_PNA.ScpiCommand(":SENSe{0}:SWEep:POINts {1}", Calc_Channel, Sweep_Points);
            SCPI_PNA.ScpiCommand(":SENSe{0}:SWEep:TIME {1}", Calc_Channel, Sweep_Time);
            SCPI_PNA.ScpiCommand(":CALCulate{0}:FORMat {1}", Calc_Channel, Calc_Format);
        }

    }
}
