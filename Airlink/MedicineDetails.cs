using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.Widget;


namespace Airlink
{
    [Activity(Label = "")]
    public class MedicineDetails : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.medicine_details);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //SupportActionBar.Title = Intent.Extras.GetString("type");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            TextView title = FindViewById<TextView>(Resource.Id.medicine_title);
            TextView dose = FindViewById<TextView>(Resource.Id.medicine_dose);
            TextView info = FindViewById<TextView>(Resource.Id.medicine_info);

            title.Text = Intent.Extras.GetString("title");
            dose.Text = Intent.Extras.GetString("dose");
            info.Text = Intent.Extras.GetString("info");
        }        

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}