using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Resources;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using HighEnergy.Collections;

namespace HighEnergy.TreeView.Demo
{
    public class App : Application
    {
        public App()
        {
            MainPage = new DemoPage();
        }
    }
}