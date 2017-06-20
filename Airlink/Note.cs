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
    class Note
    {
        private string title;
        private string description;
        private string date;

        public Note(string n_title, string n_description, string n_date) {

            title = n_title;
            description = n_description;
            date = n_date;

        }

        public string Title {

            get { return title; }
            set { title = value; }


        }

        public string Description
        {

            get { return description; }
            set { description = value; }


        }

        public string Date
        {

            get { return date; }
            set { date = value; }


        }


    }
}