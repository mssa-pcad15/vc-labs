﻿using Application = Microsoft.Maui.Controls.Application;

namespace XPlat
{
    public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}
	}
}
