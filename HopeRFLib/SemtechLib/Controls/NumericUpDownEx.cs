﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace SemtechLib.Controls
{
	[DesignerCategory("code")]
	public class NumericUpDownEx : NumericUpDown
	{
		private TextBox tBox;
		private Control udBtn;
		private bool mouseOver;

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("Mouse")]
		public new event EventHandler MouseEnter;

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[Category("Mouse")]
		public new event EventHandler MouseLeave;

		public NumericUpDownEx()
		{
			this.tBox = (TextBox)this.GetPrivateField("upDownEdit");
			if (this.tBox == null)
				throw new ArgumentNullException(this.GetType().FullName + ": Can't find internal TextBox field.");
			this.udBtn = this.GetPrivateField("upDownButtons");
			if (this.udBtn == null)
				throw new ArgumentNullException(this.GetType().FullName + ": Can't find internal UpDown buttons field.");
			this.tBox.MouseEnter += new EventHandler(this.MouseEnterLeave);
			this.tBox.MouseLeave += new EventHandler(this.MouseEnterLeave);
			this.udBtn.MouseEnter += new EventHandler(this.MouseEnterLeave);
			this.udBtn.MouseLeave += new EventHandler(this.MouseEnterLeave);
			base.MouseEnter += new EventHandler(this.MouseEnterLeave);
			base.MouseLeave += new EventHandler(this.MouseEnterLeave);
		}

		protected Control GetPrivateField(string name)
		{
			return (Control)this.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).GetValue((object)this);
		}

		private void MouseEnterLeave(object sender, EventArgs e)
		{
			bool flag = this.RectangleToScreen(this.ClientRectangle).Contains(Control.MousePosition);
			if (!(this.mouseOver ^ flag))
				return;
			this.mouseOver = flag;
			if (this.mouseOver)
			{
				if (this.MouseEnter == null)
					return;
				this.MouseEnter((object)this, EventArgs.Empty);
			}
			else
			{
				if (this.MouseLeave == null)
					return;
				this.MouseLeave((object)this, EventArgs.Empty);
			}
		}
	}
}