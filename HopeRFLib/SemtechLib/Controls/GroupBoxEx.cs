﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SemtechLib.Controls
{
	[DesignerCategory("code")]
	public class GroupBoxEx : GroupBox
	{
		private bool mouseOver;

		[Browsable(true)]
		[Category("Mouse")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler MouseEnter;

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[Category("Mouse")]
		public new event EventHandler MouseLeave;

		public GroupBoxEx()
		{
			base.MouseEnter += new EventHandler(this.MouseEnterLeave);
			base.MouseLeave += new EventHandler(this.MouseEnterLeave);
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