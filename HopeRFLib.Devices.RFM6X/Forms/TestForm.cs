﻿using SemtechLib.Controls;
using SemtechLib.Devices.SX1231;
using SemtechLib.Devices.SX1231.Enumerations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SemtechLib.Devices.SX1231.Forms
{
	public class TestForm : Form
	{
		private string previousValue = "";
		private byte currentAddrValue;
		private byte currentDataValue;
		private byte newAddrValue;
		private byte newDataValue;
		private SX1231 sx1231;
		private bool testEnabled;
		private TableLayoutPanel tlRegisters;
		private Button btnWrite;
		private Button btnRead;
		private StatusStrip ssStatus;
		private ToolStripStatusLabel tsLblStatus;
		private Label lblAddress;
		private Label lblDataWrite;
		private TextBox tBoxRegAddress;
		private TextBox tBoxRegValue;
		private GroupBoxEx groupBox3;
		private BackgroundWorker backgroundWorker1;
		private GroupBoxEx groupBox4;
		private Panel pnlRfPaSwitchEnable;
		private RadioButton rBtnRfPaSwitchAuto;
		private RadioButton rBtnRfPaSwitchOff;
		private Label label2;
		private Label label1;
		private Label label4;
		private Label label3;
		private RadioButton rBtnRfPaSwitchManual;
		private PictureBox pBoxRfOut4;
		private PictureBox pBoxRfOut3;
		private PictureBox pBoxRfOut2;
		private PictureBox pBoxRfOut1;
		private Panel pnlRfPaSwitchSel;
		private RadioButton rBtnRfPaSwitchPaIo;
		private RadioButton rBtnRfPaSwitchIoPa;
		private Label label44;
		private Label label5;
		private Label label34;
		private Label label32;
		private Label label43;
		private Label label6;
		private Label label35;
		private Label label37;
		private Label label42;
		private Label label33;
		private Label label38;
		private Label label36;
		private Label label41;
		private Label label31;
		private Label label39;
		private Label label40;

		public SX1231 SX1231
		{
			set
			{
				if (this.sx1231 == value)
					return;
				this.sx1231 = value;
				this.sx1231.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.sx1231_PropertyChanged);
				switch (this.sx1231.RfPaSwitchEnabled)
				{
					case 0:
						this.rBtnRfPaSwitchAuto.Checked = false;
						this.rBtnRfPaSwitchManual.Checked = false;
						this.rBtnRfPaSwitchOff.Checked = true;
						break;
					case 1:
						this.rBtnRfPaSwitchAuto.Checked = false;
						this.rBtnRfPaSwitchManual.Checked = true;
						this.rBtnRfPaSwitchOff.Checked = false;
						break;
					case 2:
						this.rBtnRfPaSwitchAuto.Checked = true;
						this.rBtnRfPaSwitchManual.Checked = false;
						this.rBtnRfPaSwitchOff.Checked = false;
						break;
				}
			}
		}

		public bool TestEnabled
		{
			get
			{
				return this.testEnabled;
			}
			set
			{
				this.testEnabled = value;
				this.OnTestEnabledChanged(EventArgs.Empty);
			}
		}

		public event EventHandler TestEnabledChanged;

		public TestForm()
		{
			this.InitializeComponent();
		}

		private void UpdatePaSwitchSelCheck()
		{
			this.pBoxRfOut1.Visible = false;
			this.pBoxRfOut2.Visible = false;
			this.pBoxRfOut3.Visible = false;
			this.pBoxRfOut4.Visible = false;
			if (this.sx1231.Mode == OperatingModeEnum.Tx)
			{
				switch (this.sx1231.PaMode)
				{
					case PaModeEnum.PA0:
						switch (this.sx1231.RfPaSwitchSel)
						{
							case RfPaSwitchSelEnum.RF_IO_RFIO:
								this.pBoxRfOut2.Visible = true;
								return;
							case RfPaSwitchSelEnum.RF_IO_PA_BOOST:
								this.pBoxRfOut4.Visible = true;
								return;
							default:
								return;
						}
					case PaModeEnum.PA1:
					case PaModeEnum.PA1_PA2:
						switch (this.sx1231.RfPaSwitchSel)
						{
							case RfPaSwitchSelEnum.RF_IO_RFIO:
								this.pBoxRfOut1.Visible = true;
								return;
							case RfPaSwitchSelEnum.RF_IO_PA_BOOST:
								this.pBoxRfOut3.Visible = true;
								return;
							default:
								return;
						}
				}
			}
			else
			{
				switch (this.sx1231.RfPaSwitchSel)
				{
					case RfPaSwitchSelEnum.RF_IO_RFIO:
						this.pBoxRfOut2.Visible = true;
						break;
					case RfPaSwitchSelEnum.RF_IO_PA_BOOST:
						this.pBoxRfOut4.Visible = true;
						break;
				}
			}
		}

		private void OnSX1231PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Mode":
				case "PaMode":
					this.UpdatePaSwitchSelCheck();
					break;
				case "RfPaSwitchSel":
					this.rBtnRfPaSwitchPaIo.CheckedChanged -= new EventHandler(this.rBtnRfPaSwitchSel_CheckedChanged);
					this.rBtnRfPaSwitchIoPa.CheckedChanged -= new EventHandler(this.rBtnRfPaSwitchSel_CheckedChanged);
					if (this.sx1231.RfPaSwitchEnabled != 2)
					{
						this.UpdatePaSwitchSelCheck();
						switch (this.sx1231.RfPaSwitchSel)
						{
							case RfPaSwitchSelEnum.RF_IO_RFIO:
								this.rBtnRfPaSwitchPaIo.Checked = true;
								this.rBtnRfPaSwitchIoPa.Checked = false;
								break;
							case RfPaSwitchSelEnum.RF_IO_PA_BOOST:
								this.rBtnRfPaSwitchPaIo.Checked = false;
								this.rBtnRfPaSwitchIoPa.Checked = true;
								break;
						}
					}
					this.rBtnRfPaSwitchPaIo.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchSel_CheckedChanged);
					this.rBtnRfPaSwitchIoPa.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchSel_CheckedChanged);
					break;
				case "RfPaSwitchEnabled":
					this.rBtnRfPaSwitchAuto.CheckedChanged -= new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
					this.rBtnRfPaSwitchManual.CheckedChanged -= new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
					this.rBtnRfPaSwitchOff.CheckedChanged -= new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
					switch (this.sx1231.RfPaSwitchEnabled)
					{
						case 0:
							this.rBtnRfPaSwitchAuto.Checked = false;
							this.rBtnRfPaSwitchManual.Checked = false;
							this.rBtnRfPaSwitchOff.Checked = true;
							break;
						case 1:
							this.rBtnRfPaSwitchAuto.Checked = false;
							this.rBtnRfPaSwitchManual.Checked = true;
							this.rBtnRfPaSwitchOff.Checked = false;
							break;
						case 2:
							this.rBtnRfPaSwitchAuto.Checked = true;
							this.rBtnRfPaSwitchManual.Checked = false;
							this.rBtnRfPaSwitchOff.Checked = false;
							break;
					}
					this.pnlRfPaSwitchSel.Enabled = this.rBtnRfPaSwitchManual.Checked;
					this.rBtnRfPaSwitchAuto.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
					this.rBtnRfPaSwitchManual.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
					this.rBtnRfPaSwitchOff.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
					break;
			}
		}

		private void OnTestEnabledChanged(EventArgs e)
		{
			if (this.TestEnabledChanged == null)
				return;
			this.TestEnabledChanged((object)this, e);
		}

		private void sx1231_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.InvokeRequired)
				this.BeginInvoke((Delegate)new TestForm.SX1231DataChangedDelegate(this.OnSX1231PropertyChanged), sender, (object)e);
			else
				this.OnSX1231PropertyChanged(sender, e);
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
		}

		private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
			this.TestEnabled = false;
		}

		private void TestForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.Handled = true;
				SendKeys.Send("{TAB}");
			}
			else
			{
				if (e.KeyData != (Keys.T | Keys.Control | Keys.Alt))
					return;
				this.Hide();
				this.TestEnabled = false;
			}
		}

		private void TestForm_Activated(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.tsLblStatus.Text = "-";
				this.Refresh();
			}
			catch (Exception ex)
			{
				this.tsLblStatus.Text = ex.Message;
			}
			finally
			{
				this.Refresh();
				this.Cursor = Cursors.Default;
			}
		}

		private void btnWrite_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.tsLblStatus.Text = "-";
				this.Refresh();
				if (!this.sx1231.Write(this.newAddrValue, this.newDataValue))
					throw new Exception("ERROR: Writing command");
				this.currentAddrValue = this.newAddrValue;
				this.tBoxRegAddress.ForeColor = SystemColors.WindowText;
				this.currentDataValue = this.newDataValue;
				this.tBoxRegValue.ForeColor = SystemColors.WindowText;
			}
			catch (Exception ex)
			{
				this.tsLblStatus.Text = ex.Message;
			}
			finally
			{
				this.Refresh();
				this.Cursor = Cursors.Default;
			}
		}

		private void btnRead_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.tsLblStatus.Text = "-";
				this.Refresh();
				byte data = (byte)0;
				if (!this.sx1231.Read(this.newAddrValue, ref data))
					throw new Exception("ERROR: Reading command");
				this.currentAddrValue = this.newAddrValue;
				this.tBoxRegAddress.ForeColor = SystemColors.WindowText;
				this.tBoxRegValue.Text = "0x" + data.ToString("X02");
				this.currentDataValue = this.newDataValue = data;
				this.tBoxRegValue.ForeColor = SystemColors.WindowText;
			}
			catch (Exception ex)
			{
				this.tsLblStatus.Text = ex.Message;
			}
			finally
			{
				this.Refresh();
				this.Cursor = Cursors.Default;
			}
		}

		private void tBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			try
			{
				if (textBox == this.tBoxRegAddress)
				{
					byte num = this.currentAddrValue;
					if (textBox.Text != "0x" + num.ToString("X02"))
						textBox.ForeColor = Color.Red;
					else
						textBox.ForeColor = SystemColors.WindowText;
					if (textBox.Text == "0x")
						return;
					this.newAddrValue = Convert.ToByte(textBox.Text, 16);
				}
				else
				{
					if (textBox != this.tBoxRegValue)
						return;
					byte num = this.currentDataValue;
					if (textBox.Text != "0x" + num.ToString("X02"))
						textBox.ForeColor = Color.Red;
					else
						textBox.ForeColor = SystemColors.WindowText;
					if (textBox.Text == "0x")
						return;
					this.newDataValue = Convert.ToByte(textBox.Text, 16);
				}
			}
			catch (Exception)
			{
			}
		}

		private void txtBox_Enter(object sender, EventArgs e)
		{
			this.previousValue = ((Control)sender).Text;
		}

		private void txtBox_Validating(object sender, CancelEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			byte num1 = (byte)0;
			byte num2 = byte.MaxValue;
			try
			{
				int num3 = (int)Convert.ToByte(textBox.Text, 16);
			}
			catch (Exception ex)
			{
				int num3 = (int)MessageBox.Show(ex.Message + "\rInput Format: Hex 0x" + num1.ToString("X02") + " - 0x" + num2.ToString("X02"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				textBox.Text = this.previousValue;
			}
		}

		private void txtBox_Validated(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = "0x" + Convert.ToByte(textBox.Text, 16).ToString("X02");
		}

		private void rBtnRfPaSwitchEnable_CheckedChanged(object sender, EventArgs e)
		{
			this.sx1231.RfPaSwitchEnabled = !this.rBtnRfPaSwitchAuto.Checked ? (!this.rBtnRfPaSwitchManual.Checked ? 0 : 1) : 2;
			this.pnlRfPaSwitchSel.Enabled = this.rBtnRfPaSwitchManual.Checked;
		}

		private void rBtnRfPaSwitchSel_CheckedChanged(object sender, EventArgs e)
		{
			this.sx1231.RfPaSwitchSel = this.rBtnRfPaSwitchPaIo.Checked ? RfPaSwitchSelEnum.RF_IO_RFIO : RfPaSwitchSelEnum.RF_IO_PA_BOOST;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TestForm));
			this.ssStatus = new StatusStrip();
			this.tsLblStatus = new ToolStripStatusLabel();
			this.backgroundWorker1 = new BackgroundWorker();
			this.groupBox4 = new GroupBoxEx();
			this.pBoxRfOut4 = new PictureBox();
			this.pBoxRfOut3 = new PictureBox();
			this.pBoxRfOut2 = new PictureBox();
			this.pBoxRfOut1 = new PictureBox();
			this.pnlRfPaSwitchSel = new Panel();
			this.rBtnRfPaSwitchPaIo = new RadioButton();
			this.rBtnRfPaSwitchIoPa = new RadioButton();
			this.label44 = new Label();
			this.label5 = new Label();
			this.label34 = new Label();
			this.label32 = new Label();
			this.label43 = new Label();
			this.label6 = new Label();
			this.label35 = new Label();
			this.label37 = new Label();
			this.label42 = new Label();
			this.label33 = new Label();
			this.label38 = new Label();
			this.label36 = new Label();
			this.label41 = new Label();
			this.label31 = new Label();
			this.label39 = new Label();
			this.label40 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.pnlRfPaSwitchEnable = new Panel();
			this.rBtnRfPaSwitchAuto = new RadioButton();
			this.rBtnRfPaSwitchManual = new RadioButton();
			this.rBtnRfPaSwitchOff = new RadioButton();
			this.groupBox3 = new GroupBoxEx();
			this.btnRead = new Button();
			this.tlRegisters = new TableLayoutPanel();
			this.lblAddress = new Label();
			this.lblDataWrite = new Label();
			this.tBoxRegAddress = new TextBox();
			this.tBoxRegValue = new TextBox();
			this.btnWrite = new Button();
			this.ssStatus.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.pnlRfPaSwitchSel.SuspendLayout();
			this.pnlRfPaSwitchEnable.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tlRegisters.SuspendLayout();
			this.SuspendLayout();
			this.ssStatus.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.tsLblStatus
      });
			this.ssStatus.Location = new Point(0, 273);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new Size(319, 22);
			this.ssStatus.TabIndex = 1;
			this.ssStatus.Text = "statusStrip1";
			this.tsLblStatus.Name = "tsLblStatus";
			this.tsLblStatus.Size = new Size(11, 17);
			this.tsLblStatus.Text = "-";
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.groupBox4.Controls.Add((Control)this.pBoxRfOut4);
			this.groupBox4.Controls.Add((Control)this.pBoxRfOut3);
			this.groupBox4.Controls.Add((Control)this.pBoxRfOut2);
			this.groupBox4.Controls.Add((Control)this.pBoxRfOut1);
			this.groupBox4.Controls.Add((Control)this.pnlRfPaSwitchSel);
			this.groupBox4.Controls.Add((Control)this.label44);
			this.groupBox4.Controls.Add((Control)this.label5);
			this.groupBox4.Controls.Add((Control)this.label34);
			this.groupBox4.Controls.Add((Control)this.label32);
			this.groupBox4.Controls.Add((Control)this.label43);
			this.groupBox4.Controls.Add((Control)this.label6);
			this.groupBox4.Controls.Add((Control)this.label35);
			this.groupBox4.Controls.Add((Control)this.label37);
			this.groupBox4.Controls.Add((Control)this.label42);
			this.groupBox4.Controls.Add((Control)this.label33);
			this.groupBox4.Controls.Add((Control)this.label38);
			this.groupBox4.Controls.Add((Control)this.label36);
			this.groupBox4.Controls.Add((Control)this.label41);
			this.groupBox4.Controls.Add((Control)this.label31);
			this.groupBox4.Controls.Add((Control)this.label39);
			this.groupBox4.Controls.Add((Control)this.label40);
			this.groupBox4.Controls.Add((Control)this.label4);
			this.groupBox4.Controls.Add((Control)this.label3);
			this.groupBox4.Controls.Add((Control)this.label2);
			this.groupBox4.Controls.Add((Control)this.label1);
			this.groupBox4.Controls.Add((Control)this.pnlRfPaSwitchEnable);
			this.groupBox4.Location = new Point(12, 109);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(295, 154);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Antena switch control";
			this.pBoxRfOut4.Image = (Image)resources.GetObject("pBoxRfOut4.Image");
			this.pBoxRfOut4.Location = new Point(272, 97);
			this.pBoxRfOut4.Name = "pBoxRfOut4";
			this.pBoxRfOut4.Size = new Size(16, 15);
			this.pBoxRfOut4.TabIndex = 21;
			this.pBoxRfOut4.TabStop = false;
			this.pBoxRfOut4.Visible = false;
			this.pBoxRfOut3.Image = (Image)resources.GetObject("pBoxRfOut3.Image");
			this.pBoxRfOut3.Location = new Point(272, 79);
			this.pBoxRfOut3.Name = "pBoxRfOut3";
			this.pBoxRfOut3.Size = new Size(16, 15);
			this.pBoxRfOut3.TabIndex = 22;
			this.pBoxRfOut3.TabStop = false;
			this.pBoxRfOut3.Visible = false;
			this.pBoxRfOut2.Image = (Image)resources.GetObject("pBoxRfOut2.Image");
			this.pBoxRfOut2.Location = new Point(272, 62);
			this.pBoxRfOut2.Name = "pBoxRfOut2";
			this.pBoxRfOut2.Size = new Size(16, 15);
			this.pBoxRfOut2.TabIndex = 24;
			this.pBoxRfOut2.TabStop = false;
			this.pBoxRfOut1.Image = (Image)resources.GetObject("pBoxRfOut1.Image");
			this.pBoxRfOut1.Location = new Point(272, 44);
			this.pBoxRfOut1.Name = "pBoxRfOut1";
			this.pBoxRfOut1.Size = new Size(16, 15);
			this.pBoxRfOut1.TabIndex = 23;
			this.pBoxRfOut1.TabStop = false;
			this.pBoxRfOut1.Visible = false;
			this.pnlRfPaSwitchSel.AutoSize = true;
			this.pnlRfPaSwitchSel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlRfPaSwitchSel.Controls.Add((Control)this.rBtnRfPaSwitchPaIo);
			this.pnlRfPaSwitchSel.Controls.Add((Control)this.rBtnRfPaSwitchIoPa);
			this.pnlRfPaSwitchSel.Enabled = false;
			this.pnlRfPaSwitchSel.Location = new Point(70, 43);
			this.pnlRfPaSwitchSel.Name = "pnlRfPaSwitchSel";
			this.pnlRfPaSwitchSel.Size = new Size(20, 67);
			this.pnlRfPaSwitchSel.TabIndex = 4;
			this.rBtnRfPaSwitchPaIo.AutoSize = true;
			this.rBtnRfPaSwitchPaIo.Checked = true;
			this.rBtnRfPaSwitchPaIo.Location = new Point(3, 3);
			this.rBtnRfPaSwitchPaIo.MinimumSize = new Size(0, 28);
			this.rBtnRfPaSwitchPaIo.Name = "rBtnRfPaSwitchPaIo";
			this.rBtnRfPaSwitchPaIo.Size = new Size(14, 28);
			this.rBtnRfPaSwitchPaIo.TabIndex = 0;
			this.rBtnRfPaSwitchPaIo.TabStop = true;
			this.rBtnRfPaSwitchPaIo.UseVisualStyleBackColor = true;
			this.rBtnRfPaSwitchPaIo.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchSel_CheckedChanged);
			this.rBtnRfPaSwitchIoPa.AutoSize = true;
			this.rBtnRfPaSwitchIoPa.Location = new Point(3, 36);
			this.rBtnRfPaSwitchIoPa.Margin = new Padding(3, 4, 3, 3);
			this.rBtnRfPaSwitchIoPa.MinimumSize = new Size(0, 28);
			this.rBtnRfPaSwitchIoPa.Name = "rBtnRfPaSwitchIoPa";
			this.rBtnRfPaSwitchIoPa.Size = new Size(14, 28);
			this.rBtnRfPaSwitchIoPa.TabIndex = 0;
			this.rBtnRfPaSwitchIoPa.UseVisualStyleBackColor = true;
			this.rBtnRfPaSwitchIoPa.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchSel_CheckedChanged);
			this.label44.AutoSize = true;
			this.label44.Location = new Point(96, 99);
			this.label44.Margin = new Padding(3);
			this.label44.Name = "label44";
			this.label44.Size = new Size(23, 12);
			this.label44.TabIndex = 16;
			this.label44.Text = "Pin";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(96, 46);
			this.label5.Margin = new Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new Size(23, 12);
			this.label5.TabIndex = 15;
			this.label5.Text = "Pin";
			this.label34.AutoSize = true;
			this.label34.Location = new Point(194, 64);
			this.label34.Margin = new Padding(3);
			this.label34.Name = "label34";
			this.label34.Size = new Size(23, 12);
			this.label34.TabIndex = 20;
			this.label34.Text = "<=>";
			this.label32.AutoSize = true;
			this.label32.Location = new Point(124, 64);
			this.label32.Margin = new Padding(3);
			this.label32.Name = "label32";
			this.label32.Size = new Size(29, 12);
			this.label32.TabIndex = 19;
			this.label32.Text = "RFIO";
			this.label43.AutoSize = true;
			this.label43.Location = new Point(194, 99);
			this.label43.Margin = new Padding(3);
			this.label43.Name = "label43";
			this.label43.Size = new Size(23, 12);
			this.label43.TabIndex = 18;
			this.label43.Text = "<=>";
			this.label6.AutoSize = true;
			this.label6.Location = new Point(96, 64);
			this.label6.Margin = new Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new Size(23, 12);
			this.label6.TabIndex = 17;
			this.label6.Text = "Pin";
			this.label35.AutoSize = true;
			this.label35.Location = new Point(225, 64);
			this.label35.Margin = new Padding(3);
			this.label35.Name = "label35";
			this.label35.Size = new Size(35, 12);
			this.label35.TabIndex = 7;
			this.label35.Text = "RF_IO";
			this.label37.AutoSize = true;
			this.label37.Location = new Point(96, 81);
			this.label37.Margin = new Padding(3);
			this.label37.Name = "label37";
			this.label37.Size = new Size(23, 12);
			this.label37.TabIndex = 8;
			this.label37.Text = "Pin";
			this.label42.AutoSize = true;
			this.label42.Location = new Point(225, 99);
			this.label42.Margin = new Padding(3);
			this.label42.Name = "label42";
			this.label42.Size = new Size(35, 12);
			this.label42.TabIndex = 5;
			this.label42.Text = "RF_PA";
			this.label33.AutoSize = true;
			this.label33.Location = new Point(194, 46);
			this.label33.Margin = new Padding(3);
			this.label33.Name = "label33";
			this.label33.Size = new Size(23, 12);
			this.label33.TabIndex = 6;
			this.label33.Text = "<=>";
			this.label38.AutoSize = true;
			this.label38.Location = new Point(124, 81);
			this.label38.Margin = new Padding(3);
			this.label38.Name = "label38";
			this.label38.Size = new Size(53, 12);
			this.label38.TabIndex = 9;
			this.label38.Text = "PA_BOOST";
			this.label36.AutoSize = true;
			this.label36.Location = new Point(225, 46);
			this.label36.Margin = new Padding(3);
			this.label36.Name = "label36";
			this.label36.Size = new Size(35, 12);
			this.label36.TabIndex = 13;
			this.label36.Text = "RF_PA";
			this.label41.AutoSize = true;
			this.label41.Location = new Point(225, 81);
			this.label41.Margin = new Padding(3);
			this.label41.Name = "label41";
			this.label41.Size = new Size(35, 12);
			this.label41.TabIndex = 10;
			this.label41.Text = "RF_IO";
			this.label31.AutoSize = true;
			this.label31.Location = new Point(124, 46);
			this.label31.Margin = new Padding(3);
			this.label31.Name = "label31";
			this.label31.Size = new Size(53, 12);
			this.label31.TabIndex = 11;
			this.label31.Text = "PA_BOOST";
			this.label39.AutoSize = true;
			this.label39.Location = new Point(194, 81);
			this.label39.Margin = new Padding(3);
			this.label39.Name = "label39";
			this.label39.Size = new Size(23, 12);
			this.label39.TabIndex = 12;
			this.label39.Text = "<=>";
			this.label40.AutoSize = true;
			this.label40.Location = new Point(124, 99);
			this.label40.Margin = new Padding(3);
			this.label40.Name = "label40";
			this.label40.Size = new Size(29, 12);
			this.label40.TabIndex = 14;
			this.label40.Text = "RFIO";
			this.label4.Location = new Point(45, 120);
			this.label4.Name = "label4";
			this.label4.Size = new Size(192, 26);
			this.label4.TabIndex = 3;
			this.label4.Text = "To be used only on antenna diversity ref design.";
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
			this.label3.Location = new Point(6, 120);
			this.label3.Name = "label3";
			this.label3.Size = new Size(38, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Note:";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 71);
			this.label2.Name = "label2";
			this.label2.Size = new Size(65, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "Selection:";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new Size(47, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "Switch:";
			this.pnlRfPaSwitchEnable.AutoSize = true;
			this.pnlRfPaSwitchEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlRfPaSwitchEnable.Controls.Add((Control)this.rBtnRfPaSwitchAuto);
			this.pnlRfPaSwitchEnable.Controls.Add((Control)this.rBtnRfPaSwitchManual);
			this.pnlRfPaSwitchEnable.Controls.Add((Control)this.rBtnRfPaSwitchOff);
			this.pnlRfPaSwitchEnable.Location = new Point(70, 18);
			this.pnlRfPaSwitchEnable.Name = "pnlRfPaSwitchEnable";
			this.pnlRfPaSwitchEnable.Size = new Size(166, 22);
			this.pnlRfPaSwitchEnable.TabIndex = 1;
			this.rBtnRfPaSwitchAuto.AutoSize = true;
			this.rBtnRfPaSwitchAuto.Location = new Point(3, 3);
			this.rBtnRfPaSwitchAuto.Name = "rBtnRfPaSwitchAuto";
			this.rBtnRfPaSwitchAuto.Size = new Size(47, 16);
			this.rBtnRfPaSwitchAuto.TabIndex = 0;
			this.rBtnRfPaSwitchAuto.Text = "Auto";
			this.rBtnRfPaSwitchAuto.UseVisualStyleBackColor = true;
			this.rBtnRfPaSwitchAuto.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
			this.rBtnRfPaSwitchManual.AutoSize = true;
			this.rBtnRfPaSwitchManual.Location = new Point(56, 2);
			this.rBtnRfPaSwitchManual.Name = "rBtnRfPaSwitchManual";
			this.rBtnRfPaSwitchManual.Size = new Size(59, 16);
			this.rBtnRfPaSwitchManual.TabIndex = 0;
			this.rBtnRfPaSwitchManual.Text = "Manual";
			this.rBtnRfPaSwitchManual.UseVisualStyleBackColor = true;
			this.rBtnRfPaSwitchManual.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
			this.rBtnRfPaSwitchOff.AutoSize = true;
			this.rBtnRfPaSwitchOff.Checked = true;
			this.rBtnRfPaSwitchOff.Location = new Point(122, 3);
			this.rBtnRfPaSwitchOff.Name = "rBtnRfPaSwitchOff";
			this.rBtnRfPaSwitchOff.Size = new Size(41, 16);
			this.rBtnRfPaSwitchOff.TabIndex = 0;
			this.rBtnRfPaSwitchOff.TabStop = true;
			this.rBtnRfPaSwitchOff.Text = "OFF";
			this.rBtnRfPaSwitchOff.UseVisualStyleBackColor = true;
			this.rBtnRfPaSwitchOff.CheckedChanged += new EventHandler(this.rBtnRfPaSwitchEnable_CheckedChanged);
			this.groupBox3.Controls.Add((Control)this.btnRead);
			this.groupBox3.Controls.Add((Control)this.tlRegisters);
			this.groupBox3.Controls.Add((Control)this.btnWrite);
			this.groupBox3.Location = new Point(12, 11);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(295, 92);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Registers";
			this.btnRead.Location = new Point(151, 63);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new Size(65, 21);
			this.btnRead.TabIndex = 2;
			this.btnRead.Text = "Read";
			this.btnRead.UseVisualStyleBackColor = true;
			this.btnRead.Click += new EventHandler(this.btnRead_Click);
			this.tlRegisters.AutoSize = true;
			this.tlRegisters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlRegisters.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			this.tlRegisters.ColumnCount = 2;
			this.tlRegisters.ColumnStyles.Add(new ColumnStyle());
			this.tlRegisters.ColumnStyles.Add(new ColumnStyle());
			this.tlRegisters.Controls.Add((Control)this.lblAddress, 0, 0);
			this.tlRegisters.Controls.Add((Control)this.lblDataWrite, 1, 0);
			this.tlRegisters.Controls.Add((Control)this.tBoxRegAddress, 0, 1);
			this.tlRegisters.Controls.Add((Control)this.tBoxRegValue, 1, 1);
			this.tlRegisters.Location = new Point(75, 18);
			this.tlRegisters.Name = "tlRegisters";
			this.tlRegisters.RowCount = 2;
			this.tlRegisters.RowStyles.Add(new RowStyle());
			this.tlRegisters.RowStyles.Add(new RowStyle());
			this.tlRegisters.Size = new Size(145, 42);
			this.tlRegisters.TabIndex = 0;
			this.lblAddress.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lblAddress.Location = new Point(4, 1);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new Size(65, 18);
			this.lblAddress.TabIndex = 0;
			this.lblAddress.Text = "Address";
			this.lblAddress.TextAlign = ContentAlignment.MiddleCenter;
			this.lblDataWrite.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lblDataWrite.Location = new Point(76, 1);
			this.lblDataWrite.Name = "lblDataWrite";
			this.lblDataWrite.Size = new Size(65, 18);
			this.lblDataWrite.TabIndex = 1;
			this.lblDataWrite.Text = "Data";
			this.lblDataWrite.TextAlign = ContentAlignment.MiddleCenter;
			this.tBoxRegAddress.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tBoxRegAddress.Location = new Point(1, 20);
			this.tBoxRegAddress.Margin = new Padding(0);
			this.tBoxRegAddress.MaxLength = 4;
			this.tBoxRegAddress.Name = "tBoxRegAddress";
			this.tBoxRegAddress.Size = new Size(71, 21);
			this.tBoxRegAddress.TabIndex = 2;
			this.tBoxRegAddress.Text = "0x00";
			this.tBoxRegAddress.TextAlign = HorizontalAlignment.Center;
			this.tBoxRegAddress.TextChanged += new EventHandler(this.tBox_TextChanged);
			this.tBoxRegAddress.Enter += new EventHandler(this.txtBox_Enter);
			this.tBoxRegAddress.Validating += new CancelEventHandler(this.txtBox_Validating);
			this.tBoxRegAddress.Validated += new EventHandler(this.txtBox_Validated);
			this.tBoxRegValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tBoxRegValue.Location = new Point(73, 20);
			this.tBoxRegValue.Margin = new Padding(0);
			this.tBoxRegValue.MaxLength = 4;
			this.tBoxRegValue.Name = "tBoxRegValue";
			this.tBoxRegValue.Size = new Size(71, 21);
			this.tBoxRegValue.TabIndex = 3;
			this.tBoxRegValue.Text = "0x00";
			this.tBoxRegValue.TextAlign = HorizontalAlignment.Center;
			this.tBoxRegValue.TextChanged += new EventHandler(this.tBox_TextChanged);
			this.tBoxRegValue.Enter += new EventHandler(this.txtBox_Enter);
			this.tBoxRegValue.Validating += new CancelEventHandler(this.txtBox_Validating);
			this.tBoxRegValue.Validated += new EventHandler(this.txtBox_Validated);
			this.btnWrite.Location = new Point(79, 63);
			this.btnWrite.Name = "btnWrite";
			this.btnWrite.Size = new Size(65, 21);
			this.btnWrite.TabIndex = 1;
			this.btnWrite.Text = "Write";
			this.btnWrite.UseVisualStyleBackColor = true;
			this.btnWrite.Click += new EventHandler(this.btnWrite_Click);
			this.AutoScaleDimensions = new SizeF(6f, 12f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new Size(319, 295);
			this.Controls.Add((Control)this.groupBox4);
			this.Controls.Add((Control)this.groupBox3);
			this.Controls.Add((Control)this.ssStatus);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = (Icon)resources.GetObject("$this.Icon");
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MaximumSize = new Size(1200, 1110);
			this.Name = "TestForm";
			this.StartPosition = FormStartPosition.Manual;
			this.Text = "Test";
			this.Activated += new EventHandler(this.TestForm_Activated);
			this.FormClosing += new FormClosingEventHandler(this.TestForm_FormClosing);
			this.Load += new EventHandler(this.TestForm_Load);
			this.KeyDown += new KeyEventHandler(this.TestForm_KeyDown);
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.pnlRfPaSwitchSel.ResumeLayout(false);
			this.pnlRfPaSwitchSel.PerformLayout();
			this.pnlRfPaSwitchEnable.ResumeLayout(false);
			this.pnlRfPaSwitchEnable.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tlRegisters.ResumeLayout(false);
			this.tlRegisters.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private delegate void SX1231DataChangedDelegate(object sender, PropertyChangedEventArgs e);
	}
}
