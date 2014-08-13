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
    public static class App
    {
        static Assembly _reflectionAssembly;
        internal static readonly MethodInfo GetDependency;

        static App()
        {
            GetDependency = typeof(DependencyService)
                .GetRuntimeMethods()
                .Single((method)=>
                    method.Name.Equals("Get"));
        }

        public static void Init(Assembly assembly)
        {
            System.Threading.Interlocked.CompareExchange(ref _reflectionAssembly, assembly, null);
        }
    }
}