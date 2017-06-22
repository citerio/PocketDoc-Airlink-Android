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
    class Medicine
    {
        private string title;
        private string dose;
        private string info;

        public Medicine(string m_title, string m_dose, string m_info)
        {

            title = m_title;
            dose = m_dose;
            info = m_info;

        }

        public string Title
        {

            get { return title; }
            set { title = value; }


        }

        public string Dose
        {

            get { return dose; }
            set { dose = value; }


        }

        public string Info
        {

            get { return info; }
            set { info = value; }


        }
    }
}