using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Airlink
{
    class Pdf
    {
        private string pdf_name;

        public Pdf(string name) {

            pdf_name = name;

        }

        public string Name // This is your property
        {
            get { return pdf_name; }
            set { pdf_name = value; }
        }
    }
}