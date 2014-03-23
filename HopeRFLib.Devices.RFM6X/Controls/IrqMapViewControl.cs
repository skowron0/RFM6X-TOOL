﻿using SemtechLib.Controls;
using SemtechLib.Devices.SX1231.Enumerations;
using SemtechLib.Devices.SX1231.Events;
using SemtechLib.General.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SemtechLib.Devices.SX1231.Controls
{
	public class IrqMapViewControl : UserControl, INotifyDocumentationChanged
	{
		private Decimal frequencyXo = new Decimal(32000000);
		private OperatingModeEnum mode = OperatingModeEnum.Stdby;
		private DataModeEnum dataMode;
		private IContainer components;
		private ErrorProvider errorProvider;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label lblOperatingMode;
		private ComboBox cBoxDio5Mapping;
		private ComboBox cBoxDio4Mapping;
		private ComboBox cBoxDio3Mapping;
		private ComboBox cBoxDio2Mapping;
		private ComboBox cBoxDio1Mapping;
		private ComboBox cBoxDio0Mapping;
		private Label lblDataMode;
		private Label label13;
		private Label label15;
		private ComboBox cBoxClockOut;
		private Led ledFifoOverrun;
		private Led ledSyncAddressMatch;
		private Led ledPllLock;
		private Led ledFifoLevel;
		private Led ledAutoMode;
		private Led ledTxReady;
		private Led ledFifoNotEmpty;
		private Led ledTimeout;
		private Led ledRxReady;
		private Led ledFifoFull;
		private Led ledRssi;
		private Led ledModeReady;
		private Label label28;
		private Label label29;
		private Label label30;
		private Label label31;
		private Label label24;
		private Label label25;
		private Label label26;
		private Label label27;
		private Label label20;
		private Label label21;
		private Label label22;
		private Label label23;
		private Label label17;
		private Label label18;
		private Label label19;
		private Label lbModeReady;
		private Label label16;
		private GroupBoxEx gBoxIrqFlags;
		private Led ledLowBat;
		private Led ledCrcOk;
		private Led ledPayloadReady;
		private Led ledPacketSent;
		private Label lblBitSynchroniser;
		private Label label8;
		private GroupBoxEx gBoxDeviceStatus;
		private GroupBoxEx gBoxClockOut;
		private GroupBoxEx gBoxDioMapping;

		public Decimal FrequencyXo
		{
			get
			{
				return this.frequencyXo;
			}
			set
			{
				int num1 = (int)this.ClockOut;
				this.frequencyXo = value;
				this.cBoxClockOut.Items.Clear();
				int num2 = 1;
				while (num2 <= 32)
				{
					this.cBoxClockOut.Items.Add((object)Math.Round(this.frequencyXo / (Decimal)num2, MidpointRounding.AwayFromZero).ToString());
					num2 <<= 1;
				}
				this.cBoxClockOut.Items.Add((object)"RC");
				this.cBoxClockOut.Items.Add((object)"OFF");
				this.ClockOut = (ClockOutEnum)num1;
			}
		}

		public OperatingModeEnum Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
				this.PopulateDioCbox();
				switch (this.mode)
				{
					case OperatingModeEnum.Sleep:
						this.lblOperatingMode.Text = "Sleep";
						break;
					case OperatingModeEnum.Stdby:
						this.lblOperatingMode.Text = "Standby";
						break;
					case OperatingModeEnum.Fs:
						this.lblOperatingMode.Text = "Synthesizer";
						break;
					case OperatingModeEnum.Tx:
						this.lblOperatingMode.Text = "Transmitter";
						break;
					case OperatingModeEnum.Rx:
						this.lblOperatingMode.Text = "Receiver";
						break;
				}
			}
		}

		public DataModeEnum DataMode
		{
			get
			{
				return this.dataMode;
			}
			set
			{
				this.dataMode = value;
				this.PopulateDioCbox();
				switch (this.dataMode)
				{
					case DataModeEnum.Packet:
						this.lblBitSynchroniser.Text = "ON";
						this.lblDataMode.Text = "Packet";
						break;
					case DataModeEnum.Reserved:
						this.lblBitSynchroniser.Text = "";
						this.lblDataMode.Text = "";
						break;
					case DataModeEnum.ContinuousBitSync:
						this.lblBitSynchroniser.Text = "ON";
						this.lblDataMode.Text = "Continuous";
						break;
					case DataModeEnum.Continuous:
						this.lblBitSynchroniser.Text = "OFF";
						this.lblDataMode.Text = "Continuous";
						break;
				}
			}
		}

		public DioMappingEnum Dio0Mapping
		{
			get
			{
				return (DioMappingEnum)this.cBoxDio0Mapping.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxDio0Mapping.SelectedIndexChanged -= new EventHandler(this.cBoxDio0Mapping_SelectedIndexChanged);
					this.cBoxDio0Mapping.SelectedIndex = (int)value;
					this.cBoxDio0Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio0Mapping_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxDio0Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio0Mapping_SelectedIndexChanged);
				}
			}
		}

		public DioMappingEnum Dio1Mapping
		{
			get
			{
				return (DioMappingEnum)this.cBoxDio1Mapping.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxDio1Mapping.SelectedIndexChanged -= new EventHandler(this.cBoxDio1Mapping_SelectedIndexChanged);
					this.cBoxDio1Mapping.SelectedIndex = (int)value;
					this.cBoxDio1Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio1Mapping_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxDio1Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio1Mapping_SelectedIndexChanged);
				}
			}
		}

		public DioMappingEnum Dio2Mapping
		{
			get
			{
				return (DioMappingEnum)this.cBoxDio2Mapping.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxDio2Mapping.SelectedIndexChanged -= new EventHandler(this.cBoxDio2Mapping_SelectedIndexChanged);
					this.cBoxDio2Mapping.SelectedIndex = (int)value;
					this.cBoxDio2Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio2Mapping_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxDio2Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio2Mapping_SelectedIndexChanged);
				}
			}
		}

		public DioMappingEnum Dio3Mapping
		{
			get
			{
				return (DioMappingEnum)this.cBoxDio3Mapping.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxDio3Mapping.SelectedIndexChanged -= new EventHandler(this.cBoxDio3Mapping_SelectedIndexChanged);
					this.cBoxDio3Mapping.SelectedIndex = (int)value;
					this.cBoxDio3Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio3Mapping_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxDio3Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio3Mapping_SelectedIndexChanged);
				}
			}
		}

		public DioMappingEnum Dio4Mapping
		{
			get
			{
				return (DioMappingEnum)this.cBoxDio4Mapping.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxDio4Mapping.SelectedIndexChanged -= new EventHandler(this.cBoxDio4Mapping_SelectedIndexChanged);
					this.cBoxDio4Mapping.SelectedIndex = (int)value;
					this.cBoxDio4Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio4Mapping_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxDio4Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio4Mapping_SelectedIndexChanged);
				}
			}
		}

		public DioMappingEnum Dio5Mapping
		{
			get
			{
				return (DioMappingEnum)this.cBoxDio5Mapping.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxDio5Mapping.SelectedIndexChanged -= new EventHandler(this.cBoxDio5Mapping_SelectedIndexChanged);
					this.cBoxDio5Mapping.SelectedIndex = (int)value;
					this.cBoxDio5Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio5Mapping_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxDio5Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio5Mapping_SelectedIndexChanged);
				}
			}
		}

		public ClockOutEnum ClockOut
		{
			get
			{
				return (ClockOutEnum)this.cBoxClockOut.SelectedIndex;
			}
			set
			{
				try
				{
					this.cBoxClockOut.SelectedIndexChanged -= new EventHandler(this.cBoxClockOut_SelectedIndexChanged);
					this.cBoxClockOut.SelectedIndex = (int)value;
					this.cBoxClockOut.SelectedIndexChanged += new EventHandler(this.cBoxClockOut_SelectedIndexChanged);
				}
				catch
				{
					this.cBoxClockOut.SelectedIndexChanged += new EventHandler(this.cBoxClockOut_SelectedIndexChanged);
				}
			}
		}

		public bool ModeReady
		{
			get
			{
				return this.ledModeReady.Checked;
			}
			set
			{
				this.ledModeReady.Checked = value;
			}
		}

		public bool RxReady
		{
			get
			{
				return this.ledRxReady.Checked;
			}
			set
			{
				this.ledRxReady.Checked = value;
			}
		}

		public bool TxReady
		{
			get
			{
				return this.ledTxReady.Checked;
			}
			set
			{
				this.ledTxReady.Checked = value;
			}
		}

		public bool PllLock
		{
			get
			{
				return this.ledPllLock.Checked;
			}
			set
			{
				this.ledPllLock.Checked = value;
			}
		}

		public bool Rssi
		{
			get
			{
				return this.ledRssi.Checked;
			}
			set
			{
				this.ledRssi.Checked = value;
			}
		}

		public bool Timeout
		{
			get
			{
				return this.ledTimeout.Checked;
			}
			set
			{
				this.ledTimeout.Checked = value;
			}
		}

		public bool AutoMode
		{
			get
			{
				return this.ledAutoMode.Checked;
			}
			set
			{
				this.ledAutoMode.Checked = value;
			}
		}

		public bool SyncAddressMatch
		{
			get
			{
				return this.ledSyncAddressMatch.Checked;
			}
			set
			{
				this.ledSyncAddressMatch.Checked = value;
			}
		}

		public bool FifoFull
		{
			get
			{
				return this.ledFifoFull.Checked;
			}
			set
			{
				this.ledFifoFull.Checked = value;
			}
		}

		public bool FifoNotEmpty
		{
			get
			{
				return this.ledFifoNotEmpty.Checked;
			}
			set
			{
				this.ledFifoNotEmpty.Checked = value;
			}
		}

		public bool FifoLevel
		{
			get
			{
				return this.ledFifoLevel.Checked;
			}
			set
			{
				this.ledFifoLevel.Checked = value;
			}
		}

		public bool FifoOverrun
		{
			get
			{
				return this.ledFifoOverrun.Checked;
			}
			set
			{
				this.ledFifoOverrun.Checked = value;
			}
		}

		public bool PacketSent
		{
			get
			{
				return this.ledPacketSent.Checked;
			}
			set
			{
				this.ledPacketSent.Checked = value;
			}
		}

		public bool PayloadReady
		{
			get
			{
				return this.ledPayloadReady.Checked;
			}
			set
			{
				this.ledPayloadReady.Checked = value;
			}
		}

		public bool CrcOk
		{
			get
			{
				return this.ledCrcOk.Checked;
			}
			set
			{
				this.ledCrcOk.Checked = value;
			}
		}

		public bool LowBat
		{
			get
			{
				return this.ledLowBat.Checked;
			}
			set
			{
				this.ledLowBat.Checked = value;
			}
		}

		public event DioMappingEventHandler DioMappingChanged;

		public event ClockOutEventHandler ClockOutChanged;

		public event DocumentationChangedEventHandler DocumentationChanged;

		public IrqMapViewControl()
		{
			this.InitializeComponent();
		}

		private void PopulateDioCbox()
		{
			int[] numArray = new int[6]
      {
        (int) this.Dio0Mapping,
        (int) this.Dio1Mapping,
        (int) this.Dio2Mapping,
        (int) this.Dio3Mapping,
        (int) this.Dio4Mapping,
        (int) this.Dio5Mapping
      };
			this.cBoxDio0Mapping.Items.Clear();
			this.cBoxDio1Mapping.Items.Clear();
			this.cBoxDio2Mapping.Items.Clear();
			this.cBoxDio3Mapping.Items.Clear();
			this.cBoxDio4Mapping.Items.Clear();
			this.cBoxDio5Mapping.Items.Clear();
			if (dataMode == DataModeEnum.Packet || dataMode == DataModeEnum.Reserved)
			{
				switch (this.mode)
				{
					case OperatingModeEnum.Sleep:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "FifoLevel", "FifoFull", "FifoNotEmpty", "-" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "FifoNotEmpty", "-", "LowBat", "AutoMode" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "FifoFull", "-", "LowBat", "-" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Stdby:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "FifoLevel", "FifoFull", "FifoNotEmpty", "-" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "FifoNotEmpty", "-", "LowBat", "AutoMode" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "FifoFull", "-", "LowBat", "-" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "-", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Fs:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "PllLock" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "FifoLevel", "FifoFull", "FifoNotEmpty", "PllLock" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "FifoNotEmpty", "-", "LowBat", "AutoMode" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "FifoFull", "-", "LowBat", "PllLock" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "PllLock" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "-", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Tx:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "PacketSent", "TxReady", "LowBat", "PllLock" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "FifoLevel", "FifoFull", "FifoNotEmpty", "PllLock" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "FifoNotEmpty", "Data", "LowBat", "AutoMode" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "FifoFull", "TxReady", "LowBat", "PllLock" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "ModeReady", "TxReady", "LowBat", "PllLock" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "Data", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Rx:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "CrcOk", "PayloadReady", "SyncAddress", "Rssi" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "FifoLevel", "FifoFull", "FifoNotEmpty", "Timeout" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "FifoNotEmpty", "Data", "LowBat", "AutoMode" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "FifoFull", "Rssi", "SyncAddress", "PllLock" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "Timeout", "Rssi", "RxReady", "PllLock" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "Data", "LowBat", "ModeReady" });
						break;
				}
			}
			if (dataMode == DataModeEnum.ContinuousBitSync || dataMode == DataModeEnum.Continuous)
			{
				switch (this.mode)
				{
					case OperatingModeEnum.Sleep:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "ModeReady" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "-", "-", "-", "-" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "-", "-", "AutoMode", "-" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Stdby:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "ModeReady" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "-", "-", "-", "-" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "-", "-", "AutoMode", "-" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "-" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "-", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Fs:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "PllLock", "-", "LowBat", "Modeready" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "PllLock" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "-", "-", "-", "-" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "-", "-", "AutoMode", "-" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "-", "-", "LowBat", "PllLock" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "-", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Tx:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "PllLock", "TxReady", "LowBat", "ModeReady" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "Dclk", "TxReady", "LowBat", "PllLock" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "Data", "Data", "Data", "Data" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "TxReady", "TxReady", "AutoMode", "TxReady" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "TxReady", "TxReady", "LowBat", "PllLock" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "ClkOut", "LowBat", "ModeReady" });
						break;
					case OperatingModeEnum.Rx:
						this.cBoxDio0Mapping.Items.AddRange(new object[4] { "SyncAddress", "Timeout", "Rssi", "ModeReady" });
						this.cBoxDio1Mapping.Items.AddRange(new object[4] { "Dclk", "RxReady", "LowBat", "SyncAddress" });
						this.cBoxDio2Mapping.Items.AddRange(new object[4] { "Data", "Data", "Data", "Data" });
						this.cBoxDio3Mapping.Items.AddRange(new object[4] { "Rssi", "RxReady", "AutoMode", "Timeout" });
						this.cBoxDio4Mapping.Items.AddRange(new object[4] { "Timeout", "RxReady", "SyncAddress", "PllLock" });
						this.cBoxDio5Mapping.Items.AddRange(new object[4] { "ClkOut", "Rssi", "LowBat", "ModeReady" });
						break;
				}
			}
			this.Dio0Mapping = (DioMappingEnum)numArray[0];
			this.Dio1Mapping = (DioMappingEnum)numArray[1];
			this.Dio2Mapping = (DioMappingEnum)numArray[2];
			this.Dio3Mapping = (DioMappingEnum)numArray[3];
			this.Dio4Mapping = (DioMappingEnum)numArray[4];
			this.Dio5Mapping = (DioMappingEnum)numArray[5];
		}

		private void OnDioMappingChanged(byte id, DioMappingEnum value)
		{
			if (this.DioMappingChanged == null)
				return;
			this.DioMappingChanged((object)this, new DioMappingEventArg(id, value));
		}

		private void OnClockOutChanged(ClockOutEnum value)
		{
			if (this.ClockOutChanged == null)
				return;
			this.ClockOutChanged((object)this, new ClockOutEventArg(value));
		}

		private void cBoxDio0Mapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnDioMappingChanged((byte)0, (DioMappingEnum)this.cBoxDio0Mapping.SelectedIndex);
		}

		private void cBoxDio1Mapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnDioMappingChanged((byte)1, (DioMappingEnum)this.cBoxDio1Mapping.SelectedIndex);
		}

		private void cBoxDio2Mapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnDioMappingChanged((byte)2, (DioMappingEnum)this.cBoxDio2Mapping.SelectedIndex);
		}

		private void cBoxDio3Mapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnDioMappingChanged((byte)3, (DioMappingEnum)this.cBoxDio3Mapping.SelectedIndex);
		}

		private void cBoxDio4Mapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnDioMappingChanged((byte)4, (DioMappingEnum)this.cBoxDio4Mapping.SelectedIndex);
		}

		private void cBoxDio5Mapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnDioMappingChanged((byte)5, (DioMappingEnum)this.cBoxDio5Mapping.SelectedIndex);
		}

		private void cBoxClockOut_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnClockOutChanged((ClockOutEnum)this.cBoxClockOut.SelectedIndex);
		}

		private void control_MouseEnter(object sender, EventArgs e)
		{
			if (sender == this.gBoxDeviceStatus)
				this.OnDocumentationChanged(new DocumentationChangedEventArgs("Common", "Device status"));
			else if (sender == this.gBoxDioMapping)
				this.OnDocumentationChanged(new DocumentationChangedEventArgs("Irq mapping", "DIO mapping"));
			else if (sender == this.gBoxClockOut)
			{
				this.OnDocumentationChanged(new DocumentationChangedEventArgs("Irq mapping", "Clock out"));
			}
			else
			{
				if (sender != this.gBoxIrqFlags)
					return;
				this.OnDocumentationChanged(new DocumentationChangedEventArgs("Irq mapping", "Irq flags"));
			}
		}

		private void control_MouseLeave(object sender, EventArgs e)
		{
			this.OnDocumentationChanged(new DocumentationChangedEventArgs(".", "Overview"));
		}

		private void OnDocumentationChanged(DocumentationChangedEventArgs e)
		{
			if (this.DocumentationChanged == null)
				return;
			this.DocumentationChanged((object)this, e);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
				this.components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = (IContainer)new Container();
			this.errorProvider = new ErrorProvider(this.components);
			this.cBoxDio5Mapping = new ComboBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.lblOperatingMode = new Label();
			this.cBoxDio4Mapping = new ComboBox();
			this.cBoxDio3Mapping = new ComboBox();
			this.cBoxDio2Mapping = new ComboBox();
			this.cBoxDio1Mapping = new ComboBox();
			this.cBoxDio0Mapping = new ComboBox();
			this.label13 = new Label();
			this.lblDataMode = new Label();
			this.label15 = new Label();
			this.cBoxClockOut = new ComboBox();
			this.label16 = new Label();
			this.label17 = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.lbModeReady = new Label();
			this.label20 = new Label();
			this.label21 = new Label();
			this.label22 = new Label();
			this.label23 = new Label();
			this.label24 = new Label();
			this.label25 = new Label();
			this.label26 = new Label();
			this.label27 = new Label();
			this.label28 = new Label();
			this.label29 = new Label();
			this.label30 = new Label();
			this.label31 = new Label();
			this.gBoxIrqFlags = new GroupBoxEx();
			this.ledModeReady = new Led();
			this.ledLowBat = new Led();
			this.ledFifoOverrun = new Led();
			this.ledSyncAddressMatch = new Led();
			this.ledPllLock = new Led();
			this.ledCrcOk = new Led();
			this.ledFifoLevel = new Led();
			this.ledAutoMode = new Led();
			this.ledTxReady = new Led();
			this.ledPayloadReady = new Led();
			this.ledFifoNotEmpty = new Led();
			this.ledTimeout = new Led();
			this.ledRxReady = new Led();
			this.ledPacketSent = new Led();
			this.ledFifoFull = new Led();
			this.ledRssi = new Led();
			this.label8 = new Label();
			this.lblBitSynchroniser = new Label();
			this.gBoxDeviceStatus = new GroupBoxEx();
			this.gBoxDioMapping = new GroupBoxEx();
			this.gBoxClockOut = new GroupBoxEx();
			this.gBoxIrqFlags.SuspendLayout();
			this.gBoxDeviceStatus.SuspendLayout();
			this.gBoxDioMapping.SuspendLayout();
			this.gBoxClockOut.SuspendLayout();
			this.SuspendLayout();
			this.errorProvider.ContainerControl = (ContainerControl)this;
			this.cBoxDio5Mapping.FormattingEnabled = true;
			this.cBoxDio5Mapping.Location = new Point(162, 19);
			this.cBoxDio5Mapping.Name = "cBoxDio5Mapping";
			this.cBoxDio5Mapping.Size = new Size(100, 21);
			this.cBoxDio5Mapping.TabIndex = 1;
			this.cBoxDio5Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio5Mapping_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 57);
			this.label1.Margin = new Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new Size(85, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Operating mode:";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 23);
			this.label2.Margin = new Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new Size(35, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "DIO5:";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(6, 50);
			this.label3.Margin = new Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new Size(35, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "DIO4:";
			this.label3.TextAlign = ContentAlignment.MiddleLeft;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(6, 77);
			this.label4.Margin = new Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new Size(35, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "DIO3:";
			this.label4.TextAlign = ContentAlignment.MiddleLeft;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(6, 104);
			this.label5.Margin = new Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new Size(35, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "DIO2:";
			this.label5.TextAlign = ContentAlignment.MiddleLeft;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(6, 131);
			this.label6.Margin = new Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new Size(35, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "DIO1:";
			this.label6.TextAlign = ContentAlignment.MiddleLeft;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(6, 158);
			this.label7.Margin = new Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new Size(35, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "DIO0:";
			this.label7.TextAlign = ContentAlignment.MiddleLeft;
			this.lblOperatingMode.AutoSize = true;
			this.lblOperatingMode.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
			this.lblOperatingMode.Location = new Point(159, 57);
			this.lblOperatingMode.Margin = new Padding(3);
			this.lblOperatingMode.Name = "lblOperatingMode";
			this.lblOperatingMode.Size = new Size(39, 13);
			this.lblOperatingMode.TabIndex = 5;
			this.lblOperatingMode.Text = "Sleep";
			this.lblOperatingMode.TextAlign = ContentAlignment.MiddleLeft;
			this.cBoxDio4Mapping.FormattingEnabled = true;
			this.cBoxDio4Mapping.Location = new Point(162, 46);
			this.cBoxDio4Mapping.Name = "cBoxDio4Mapping";
			this.cBoxDio4Mapping.Size = new Size(100, 21);
			this.cBoxDio4Mapping.TabIndex = 3;
			this.cBoxDio4Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio4Mapping_SelectedIndexChanged);
			this.cBoxDio3Mapping.FormattingEnabled = true;
			this.cBoxDio3Mapping.Location = new Point(162, 73);
			this.cBoxDio3Mapping.Name = "cBoxDio3Mapping";
			this.cBoxDio3Mapping.Size = new Size(100, 21);
			this.cBoxDio3Mapping.TabIndex = 5;
			this.cBoxDio3Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio3Mapping_SelectedIndexChanged);
			this.cBoxDio2Mapping.FormattingEnabled = true;
			this.cBoxDio2Mapping.Location = new Point(162, 100);
			this.cBoxDio2Mapping.Name = "cBoxDio2Mapping";
			this.cBoxDio2Mapping.Size = new Size(100, 21);
			this.cBoxDio2Mapping.TabIndex = 7;
			this.cBoxDio2Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio2Mapping_SelectedIndexChanged);
			this.cBoxDio1Mapping.FormattingEnabled = true;
			this.cBoxDio1Mapping.Location = new Point(162, (int)sbyte.MaxValue);
			this.cBoxDio1Mapping.Name = "cBoxDio1Mapping";
			this.cBoxDio1Mapping.Size = new Size(100, 21);
			this.cBoxDio1Mapping.TabIndex = 9;
			this.cBoxDio1Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio1Mapping_SelectedIndexChanged);
			this.cBoxDio0Mapping.FormattingEnabled = true;
			this.cBoxDio0Mapping.Location = new Point(162, 154);
			this.cBoxDio0Mapping.Name = "cBoxDio0Mapping";
			this.cBoxDio0Mapping.Size = new Size(100, 21);
			this.cBoxDio0Mapping.TabIndex = 11;
			this.cBoxDio0Mapping.SelectedIndexChanged += new EventHandler(this.cBoxDio0Mapping_SelectedIndexChanged);
			this.label13.AutoSize = true;
			this.label13.Location = new Point(6, 38);
			this.label13.Margin = new Padding(3);
			this.label13.Name = "label13";
			this.label13.Size = new Size(62, 13);
			this.label13.TabIndex = 2;
			this.label13.Text = "Data mode:";
			this.label13.TextAlign = ContentAlignment.MiddleLeft;
			this.lblDataMode.AutoSize = true;
			this.lblDataMode.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
			this.lblDataMode.Location = new Point(159, 38);
			this.lblDataMode.Margin = new Padding(3);
			this.lblDataMode.Name = "lblDataMode";
			this.lblDataMode.Size = new Size(47, 13);
			this.lblDataMode.TabIndex = 3;
			this.lblDataMode.Text = "Packet";
			this.lblDataMode.TextAlign = ContentAlignment.MiddleLeft;
			this.label15.AutoSize = true;
			this.label15.Location = new Point(5, 23);
			this.label15.Name = "label15";
			this.label15.Size = new Size(60, 13);
			this.label15.TabIndex = 0;
			this.label15.Text = "Frequency:";
			this.label15.TextAlign = ContentAlignment.MiddleLeft;
			this.cBoxClockOut.FormattingEnabled = true;
			this.cBoxClockOut.Location = new Point(162, 19);
			this.cBoxClockOut.Name = "cBoxClockOut";
			this.cBoxClockOut.Size = new Size(100, 21);
			this.cBoxClockOut.TabIndex = 1;
			this.cBoxClockOut.SelectedIndexChanged += new EventHandler(this.cBoxClockOut_SelectedIndexChanged);
			this.label16.AutoSize = true;
			this.label16.Location = new Point(268, 23);
			this.label16.Name = "label16";
			this.label16.Size = new Size(20, 13);
			this.label16.TabIndex = 2;
			this.label16.Text = "Hz";
			this.label16.TextAlign = ContentAlignment.MiddleLeft;
			this.label17.AutoSize = true;
			this.label17.Location = new Point(114, 83);
			this.label17.Name = "label17";
			this.label17.Size = new Size(42, 13);
			this.label17.TabIndex = 7;
			this.label17.Text = "PllLock";
			this.label18.AutoSize = true;
			this.label18.Location = new Point(114, 62);
			this.label18.Name = "label18";
			this.label18.Size = new Size(50, 13);
			this.label18.TabIndex = 5;
			this.label18.Text = "TxReady";
			this.label19.AutoSize = true;
			this.label19.Location = new Point(114, 41);
			this.label19.Name = "label19";
			this.label19.Size = new Size(51, 13);
			this.label19.TabIndex = 3;
			this.label19.Text = "RxReady";
			this.lbModeReady.AutoSize = true;
			this.lbModeReady.Location = new Point(114, 20);
			this.lbModeReady.Name = "lbModeReady";
			this.lbModeReady.Size = new Size(65, 13);
			this.lbModeReady.TabIndex = 1;
			this.lbModeReady.Text = "ModeReady";
			this.label20.AutoSize = true;
			this.label20.Location = new Point(254, 83);
			this.label20.Name = "label20";
			this.label20.Size = new Size(99, 13);
			this.label20.TabIndex = 15;
			this.label20.Text = "SyncAddressMatch";
			this.label21.AutoSize = true;
			this.label21.Location = new Point(254, 62);
			this.label21.Name = "label21";
			this.label21.Size = new Size(56, 13);
			this.label21.TabIndex = 13;
			this.label21.Text = "AutoMode";
			this.label22.AutoSize = true;
			this.label22.Location = new Point(254, 41);
			this.label22.Name = "label22";
			this.label22.Size = new Size(45, 13);
			this.label22.TabIndex = 11;
			this.label22.Text = "Timeout";
			this.label23.AutoSize = true;
			this.label23.Location = new Point(254, 20);
			this.label23.Name = "label23";
			this.label23.Size = new Size(27, 13);
			this.label23.TabIndex = 9;
			this.label23.Text = "Rssi";
			this.label24.AutoSize = true;
			this.label24.Location = new Point(394, 83);
			this.label24.Name = "label24";
			this.label24.Size = new Size(62, 13);
			this.label24.TabIndex = 23;
			this.label24.Text = "FifoOverrun";
			this.label25.AutoSize = true;
			this.label25.Location = new Point(394, 62);
			this.label25.Name = "label25";
			this.label25.Size = new Size(50, 13);
			this.label25.TabIndex = 21;
			this.label25.Text = "FifoLevel";
			this.label26.AutoSize = true;
			this.label26.Location = new Point(394, 41);
			this.label26.Name = "label26";
			this.label26.Size = new Size(70, 13);
			this.label26.TabIndex = 19;
			this.label26.Text = "FifoNotEmpty";
			this.label27.AutoSize = true;
			this.label27.Location = new Point(394, 20);
			this.label27.Name = "label27";
			this.label27.Size = new Size(40, 13);
			this.label27.TabIndex = 17;
			this.label27.Text = "FifoFull";
			this.label28.AutoSize = true;
			this.label28.Location = new Point(534, 83);
			this.label28.Name = "label28";
			this.label28.Size = new Size(43, 13);
			this.label28.TabIndex = 31;
			this.label28.Text = "LowBat";
			this.label29.AutoSize = true;
			this.label29.Location = new Point(534, 62);
			this.label29.Name = "label29";
			this.label29.Size = new Size(37, 13);
			this.label29.TabIndex = 29;
			this.label29.Text = "CrcOk";
			this.label30.AutoSize = true;
			this.label30.Location = new Point(534, 41);
			this.label30.Name = "label30";
			this.label30.Size = new Size(76, 13);
			this.label30.TabIndex = 27;
			this.label30.Text = "PayloadReady";
			this.label31.AutoSize = true;
			this.label31.Location = new Point(534, 20);
			this.label31.Name = "label31";
			this.label31.Size = new Size(63, 13);
			this.label31.TabIndex = 25;
			this.label31.Text = "PacketSent";
			this.gBoxIrqFlags.Controls.Add((Control)this.ledModeReady);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledLowBat);
			this.gBoxIrqFlags.Controls.Add((Control)this.lbModeReady);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledFifoOverrun);
			this.gBoxIrqFlags.Controls.Add((Control)this.label19);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledSyncAddressMatch);
			this.gBoxIrqFlags.Controls.Add((Control)this.label18);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledPllLock);
			this.gBoxIrqFlags.Controls.Add((Control)this.label17);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledCrcOk);
			this.gBoxIrqFlags.Controls.Add((Control)this.label23);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledFifoLevel);
			this.gBoxIrqFlags.Controls.Add((Control)this.label22);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledAutoMode);
			this.gBoxIrqFlags.Controls.Add((Control)this.label21);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledTxReady);
			this.gBoxIrqFlags.Controls.Add((Control)this.label20);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledPayloadReady);
			this.gBoxIrqFlags.Controls.Add((Control)this.label27);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledFifoNotEmpty);
			this.gBoxIrqFlags.Controls.Add((Control)this.label26);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledTimeout);
			this.gBoxIrqFlags.Controls.Add((Control)this.label25);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledRxReady);
			this.gBoxIrqFlags.Controls.Add((Control)this.label24);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledPacketSent);
			this.gBoxIrqFlags.Controls.Add((Control)this.label31);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledFifoFull);
			this.gBoxIrqFlags.Controls.Add((Control)this.label30);
			this.gBoxIrqFlags.Controls.Add((Control)this.ledRssi);
			this.gBoxIrqFlags.Controls.Add((Control)this.label29);
			this.gBoxIrqFlags.Controls.Add((Control)this.label28);
			this.gBoxIrqFlags.Location = new Point(48, 379);
			this.gBoxIrqFlags.Name = "gBoxIrqFlags";
			this.gBoxIrqFlags.Size = new Size(703, 111);
			this.gBoxIrqFlags.TabIndex = 3;
			this.gBoxIrqFlags.TabStop = false;
			this.gBoxIrqFlags.Text = "Irq flags";
			this.gBoxIrqFlags.Visible = false;
			this.gBoxIrqFlags.MouseLeave += new EventHandler(this.control_MouseLeave);
			this.gBoxIrqFlags.MouseEnter += new EventHandler(this.control_MouseEnter);
			this.ledModeReady.BackColor = Color.Transparent;
			this.ledModeReady.LedColor = Color.Green;
			this.ledModeReady.LedSize = new Size(11, 11);
			this.ledModeReady.Location = new Point(93, 19);
			this.ledModeReady.Name = "ledModeReady";
			this.ledModeReady.Size = new Size(15, 15);
			this.ledModeReady.TabIndex = 0;
			this.ledModeReady.Text = "Mode Ready";
			this.ledLowBat.BackColor = Color.Transparent;
			this.ledLowBat.LedColor = Color.Green;
			this.ledLowBat.LedSize = new Size(11, 11);
			this.ledLowBat.Location = new Point(513, 82);
			this.ledLowBat.Name = "ledLowBat";
			this.ledLowBat.Size = new Size(15, 15);
			this.ledLowBat.TabIndex = 30;
			this.ledLowBat.Text = "led1";
			this.ledFifoOverrun.BackColor = Color.Transparent;
			this.ledFifoOverrun.LedColor = Color.Green;
			this.ledFifoOverrun.LedSize = new Size(11, 11);
			this.ledFifoOverrun.Location = new Point(373, 82);
			this.ledFifoOverrun.Name = "ledFifoOverrun";
			this.ledFifoOverrun.Size = new Size(15, 15);
			this.ledFifoOverrun.TabIndex = 22;
			this.ledFifoOverrun.Text = "led1";
			this.ledSyncAddressMatch.BackColor = Color.Transparent;
			this.ledSyncAddressMatch.LedColor = Color.Green;
			this.ledSyncAddressMatch.LedSize = new Size(11, 11);
			this.ledSyncAddressMatch.Location = new Point(233, 82);
			this.ledSyncAddressMatch.Name = "ledSyncAddressMatch";
			this.ledSyncAddressMatch.Size = new Size(15, 15);
			this.ledSyncAddressMatch.TabIndex = 14;
			this.ledSyncAddressMatch.Text = "led1";
			this.ledPllLock.BackColor = Color.Transparent;
			this.ledPllLock.LedColor = Color.Green;
			this.ledPllLock.LedSize = new Size(11, 11);
			this.ledPllLock.Location = new Point(93, 82);
			this.ledPllLock.Name = "ledPllLock";
			this.ledPllLock.Size = new Size(15, 15);
			this.ledPllLock.TabIndex = 6;
			this.ledPllLock.Text = "led1";
			this.ledCrcOk.BackColor = Color.Transparent;
			this.ledCrcOk.LedColor = Color.Green;
			this.ledCrcOk.LedSize = new Size(11, 11);
			this.ledCrcOk.Location = new Point(513, 61);
			this.ledCrcOk.Name = "ledCrcOk";
			this.ledCrcOk.Size = new Size(15, 15);
			this.ledCrcOk.TabIndex = 28;
			this.ledCrcOk.Text = "led1";
			this.ledFifoLevel.BackColor = Color.Transparent;
			this.ledFifoLevel.LedColor = Color.Green;
			this.ledFifoLevel.LedSize = new Size(11, 11);
			this.ledFifoLevel.Location = new Point(373, 61);
			this.ledFifoLevel.Name = "ledFifoLevel";
			this.ledFifoLevel.Size = new Size(15, 15);
			this.ledFifoLevel.TabIndex = 20;
			this.ledFifoLevel.Text = "led1";
			this.ledAutoMode.BackColor = Color.Transparent;
			this.ledAutoMode.LedColor = Color.Green;
			this.ledAutoMode.LedSize = new Size(11, 11);
			this.ledAutoMode.Location = new Point(233, 61);
			this.ledAutoMode.Name = "ledAutoMode";
			this.ledAutoMode.Size = new Size(15, 15);
			this.ledAutoMode.TabIndex = 12;
			this.ledAutoMode.Text = "led1";
			this.ledTxReady.BackColor = Color.Transparent;
			this.ledTxReady.LedColor = Color.Green;
			this.ledTxReady.LedSize = new Size(11, 11);
			this.ledTxReady.Location = new Point(93, 61);
			this.ledTxReady.Name = "ledTxReady";
			this.ledTxReady.Size = new Size(15, 15);
			this.ledTxReady.TabIndex = 4;
			this.ledTxReady.Text = "led1";
			this.ledPayloadReady.BackColor = Color.Transparent;
			this.ledPayloadReady.LedColor = Color.Green;
			this.ledPayloadReady.LedSize = new Size(11, 11);
			this.ledPayloadReady.Location = new Point(513, 40);
			this.ledPayloadReady.Name = "ledPayloadReady";
			this.ledPayloadReady.Size = new Size(15, 15);
			this.ledPayloadReady.TabIndex = 26;
			this.ledPayloadReady.Text = "led1";
			this.ledFifoNotEmpty.BackColor = Color.Transparent;
			this.ledFifoNotEmpty.LedColor = Color.Green;
			this.ledFifoNotEmpty.LedSize = new Size(11, 11);
			this.ledFifoNotEmpty.Location = new Point(373, 40);
			this.ledFifoNotEmpty.Name = "ledFifoNotEmpty";
			this.ledFifoNotEmpty.Size = new Size(15, 15);
			this.ledFifoNotEmpty.TabIndex = 18;
			this.ledFifoNotEmpty.Text = "led1";
			this.ledTimeout.BackColor = Color.Transparent;
			this.ledTimeout.LedColor = Color.Green;
			this.ledTimeout.LedSize = new Size(11, 11);
			this.ledTimeout.Location = new Point(233, 40);
			this.ledTimeout.Name = "ledTimeout";
			this.ledTimeout.Size = new Size(15, 15);
			this.ledTimeout.TabIndex = 10;
			this.ledTimeout.Text = "led1";
			this.ledRxReady.BackColor = Color.Transparent;
			this.ledRxReady.LedColor = Color.Green;
			this.ledRxReady.LedSize = new Size(11, 11);
			this.ledRxReady.Location = new Point(93, 40);
			this.ledRxReady.Name = "ledRxReady";
			this.ledRxReady.Size = new Size(15, 15);
			this.ledRxReady.TabIndex = 2;
			this.ledRxReady.Text = "led1";
			this.ledPacketSent.BackColor = Color.Transparent;
			this.ledPacketSent.LedColor = Color.Green;
			this.ledPacketSent.LedSize = new Size(11, 11);
			this.ledPacketSent.Location = new Point(513, 19);
			this.ledPacketSent.Name = "ledPacketSent";
			this.ledPacketSent.Size = new Size(15, 15);
			this.ledPacketSent.TabIndex = 24;
			this.ledPacketSent.Text = "led1";
			this.ledFifoFull.BackColor = Color.Transparent;
			this.ledFifoFull.LedColor = Color.Green;
			this.ledFifoFull.LedSize = new Size(11, 11);
			this.ledFifoFull.Location = new Point(373, 19);
			this.ledFifoFull.Name = "ledFifoFull";
			this.ledFifoFull.Size = new Size(15, 15);
			this.ledFifoFull.TabIndex = 16;
			this.ledFifoFull.Text = "led1";
			this.ledRssi.BackColor = Color.Transparent;
			this.ledRssi.LedColor = Color.Green;
			this.ledRssi.LedSize = new Size(11, 11);
			this.ledRssi.Location = new Point(233, 19);
			this.ledRssi.Name = "ledRssi";
			this.ledRssi.Size = new Size(15, 15);
			this.ledRssi.TabIndex = 8;
			this.ledRssi.Text = "led1";
			this.label8.AutoSize = true;
			this.label8.Location = new Point(6, 19);
			this.label8.Margin = new Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new Size(86, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Bit Synchronizer:";
			this.label8.TextAlign = ContentAlignment.MiddleLeft;
			this.lblBitSynchroniser.AutoSize = true;
			this.lblBitSynchroniser.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
			this.lblBitSynchroniser.Location = new Point(159, 19);
			this.lblBitSynchroniser.Margin = new Padding(3);
			this.lblBitSynchroniser.Name = "lblBitSynchroniser";
			this.lblBitSynchroniser.Size = new Size(25, 13);
			this.lblBitSynchroniser.TabIndex = 1;
			this.lblBitSynchroniser.Text = "ON";
			this.lblBitSynchroniser.TextAlign = ContentAlignment.MiddleLeft;
			this.gBoxDeviceStatus.Controls.Add((Control)this.lblBitSynchroniser);
			this.gBoxDeviceStatus.Controls.Add((Control)this.lblOperatingMode);
			this.gBoxDeviceStatus.Controls.Add((Control)this.label13);
			this.gBoxDeviceStatus.Controls.Add((Control)this.label1);
			this.gBoxDeviceStatus.Controls.Add((Control)this.label8);
			this.gBoxDeviceStatus.Controls.Add((Control)this.lblDataMode);
			this.gBoxDeviceStatus.Location = new Point(253, 90);
			this.gBoxDeviceStatus.Name = "gBoxDeviceStatus";
			this.gBoxDeviceStatus.Size = new Size(293, 77);
			this.gBoxDeviceStatus.TabIndex = 0;
			this.gBoxDeviceStatus.TabStop = false;
			this.gBoxDeviceStatus.Text = "Device status";
			this.gBoxDeviceStatus.MouseLeave += new EventHandler(this.control_MouseLeave);
			this.gBoxDeviceStatus.MouseEnter += new EventHandler(this.control_MouseEnter);
			this.gBoxDioMapping.Controls.Add((Control)this.cBoxDio5Mapping);
			this.gBoxDioMapping.Controls.Add((Control)this.cBoxDio4Mapping);
			this.gBoxDioMapping.Controls.Add((Control)this.label2);
			this.gBoxDioMapping.Controls.Add((Control)this.label7);
			this.gBoxDioMapping.Controls.Add((Control)this.cBoxDio3Mapping);
			this.gBoxDioMapping.Controls.Add((Control)this.cBoxDio0Mapping);
			this.gBoxDioMapping.Controls.Add((Control)this.label3);
			this.gBoxDioMapping.Controls.Add((Control)this.cBoxDio1Mapping);
			this.gBoxDioMapping.Controls.Add((Control)this.label4);
			this.gBoxDioMapping.Controls.Add((Control)this.cBoxDio2Mapping);
			this.gBoxDioMapping.Controls.Add((Control)this.label5);
			this.gBoxDioMapping.Controls.Add((Control)this.label6);
			this.gBoxDioMapping.Location = new Point(253, 173);
			this.gBoxDioMapping.Name = "gBoxDioMapping";
			this.gBoxDioMapping.Size = new Size(293, 179);
			this.gBoxDioMapping.TabIndex = 1;
			this.gBoxDioMapping.TabStop = false;
			this.gBoxDioMapping.Text = "DIO mapping";
			this.gBoxDioMapping.MouseLeave += new EventHandler(this.control_MouseLeave);
			this.gBoxDioMapping.MouseEnter += new EventHandler(this.control_MouseEnter);
			this.gBoxClockOut.Controls.Add((Control)this.cBoxClockOut);
			this.gBoxClockOut.Controls.Add((Control)this.label15);
			this.gBoxClockOut.Controls.Add((Control)this.label16);
			this.gBoxClockOut.Location = new Point(253, 358);
			this.gBoxClockOut.Name = "gBoxClockOut";
			this.gBoxClockOut.Size = new Size(293, 45);
			this.gBoxClockOut.TabIndex = 2;
			this.gBoxClockOut.TabStop = false;
			this.gBoxClockOut.Text = "Clock out";
			this.gBoxClockOut.MouseLeave += new EventHandler(this.control_MouseLeave);
			this.gBoxClockOut.MouseEnter += new EventHandler(this.control_MouseEnter);
			this.AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add((Control)this.gBoxClockOut);
			this.Controls.Add((Control)this.gBoxDioMapping);
			this.Controls.Add((Control)this.gBoxDeviceStatus);
			this.Controls.Add((Control)this.gBoxIrqFlags);
			this.Name = "IrqMapViewControl";
			this.Size = new Size(799, 493);
			this.gBoxIrqFlags.ResumeLayout(false);
			this.gBoxIrqFlags.PerformLayout();
			this.gBoxDeviceStatus.ResumeLayout(false);
			this.gBoxDeviceStatus.PerformLayout();
			this.gBoxDioMapping.ResumeLayout(false);
			this.gBoxDioMapping.PerformLayout();
			this.gBoxClockOut.ResumeLayout(false);
			this.gBoxClockOut.PerformLayout();
			this.ResumeLayout(false);
		}
	}
}