﻿using System;
using System.Windows.Forms;

namespace SX1231SKB
{
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			bool testMode = false;
			if (args.Length == 1)
			{
				foreach (string str in args)
				{
					if (str == "-test")
						testMode = true;
				}
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.Run((Form)new MainForm(testMode));
		}
	}
}