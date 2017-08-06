using Reflection.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reflection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Model1 model = new Model1() { ID = 1, Name = "jacky" };
            PrintData(model);
        }
        private void PrintData(dynamic objVo)
        {
            Type objType = objVo.GetType();
            PropertyInfo[] propInfos = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propInfo in propInfos)
            {
                var pType = propInfo.PropertyType;
                bool chkType = pType == typeof(Int32);
                string pName = propInfo.Name;
                var pValue = propInfo.GetValue(objVo, null);

                bool readable = propInfo.CanRead;
                bool writable = propInfo.CanWrite;

                if (readable)
                {
                    MethodInfo getAccessor = propInfo.GetMethod;
                    Console.WriteLine("   Visibility:    {0}",
                                      GetVisibility(getAccessor));
                }
                if (writable)
                {
                    MethodInfo setAccessor = propInfo.SetMethod;
                    Console.WriteLine("   Visibility:    {0}",
                                      GetVisibility(setAccessor));
                }

            }

        }
        public static String GetVisibility(MethodInfo accessor)
        {
            if (accessor.IsPublic)
                return "Public";
            else if (accessor.IsPrivate)
                return "Private";
            else if (accessor.IsFamily)
                return "Protected";
            else if (accessor.IsAssembly)
                return "Internal/Friend";
            else
                return "Protected Internal/Friend";
        }
    }
}
