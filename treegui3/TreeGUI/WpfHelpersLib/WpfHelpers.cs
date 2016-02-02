using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfHelpersLib
{
    public static class WpfApplication
    {
        private static readonly Assembly _Assembly = null;

        private static readonly AssemblyCompanyAttribute _Company = null;
        private static readonly AssemblyProductAttribute _Product = null;

        public static string CompanyName { get; private set; }
        public static string ProductName { get; private set; }

        static WpfApplication()
        {
            CompanyName = String.Empty;
            ProductName = String.Empty;

            _Assembly = Assembly.GetEntryAssembly();

            if (_Assembly != null)
            {
                object[] attributes = _Assembly.GetCustomAttributes(false);

                foreach (object attribute in attributes)
                {
                    Type type = attribute.GetType();

                    if (type == typeof(AssemblyCompanyAttribute))
                        _Company = (AssemblyCompanyAttribute)attribute;

                    if (type == typeof(AssemblyProductAttribute))
                        _Product = (AssemblyProductAttribute)attribute;
                }
            }

            if (_Company != null) CompanyName = _Company.Company;
            if (_Product != null) ProductName = _Product.Product;
        }
    }
}