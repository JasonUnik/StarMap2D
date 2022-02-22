﻿using Eto.Drawing;
using Eto.Forms;
using System;

namespace StarMap2D.Eto
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Globals.Settings.CreateApplicationSettingsFolder("VPKSoft", nameof(StarMap2D));
            Globals.Settings.Load(Globals.Settings.GetApplicationSettingsFile("VPKSoft", nameof(StarMap2D)));

            new Application().Run(new MainForm());
            //new Application(Eto.Platform.Detect).Run(new MainForm());
        }
    }
}
