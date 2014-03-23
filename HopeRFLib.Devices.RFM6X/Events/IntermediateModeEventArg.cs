﻿using SemtechLib.Devices.SX1231.Enumerations;
using System;

namespace SemtechLib.Devices.SX1231.Events
{
	public class IntermediateModeEventArg : EventArgs
	{
		private IntermediateModeEnum value;

		public IntermediateModeEnum Value
		{
			get
			{
				return this.value;
			}
		}

		public IntermediateModeEventArg(IntermediateModeEnum value)
		{
			this.value = value;
		}
	}
}