using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;



namespace Airlink
{
    [Activity(Label = "")]
    public class ReferenceGuidelines : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.reference_guidelines);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Airlink";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            TextView adult_guidelines_button = FindViewById<TextView>(Resource.Id.adult_guidelines_button);
            TextView pediatric_guidelines_button = FindViewById<TextView>(Resource.Id.pediatric_guidelines_button);
            TextView ob_guidelines_button = FindViewById<TextView>(Resource.Id.ob_guidelines_button);
            TextView trauma_guidelines_button = FindViewById<TextView>(Resource.Id.trauma_guidelines_button);
            TextView procedures_button = FindViewById<TextView>(Resource.Id.procedures_button);

            Typeface myCustomFont = Typeface.CreateFromAsset(Assets, "fonts/unicode.palatino.ttf");
            adult_guidelines_button.Typeface = myCustomFont;
            pediatric_guidelines_button.Typeface = myCustomFont;
            ob_guidelines_button.Typeface = myCustomFont;
            trauma_guidelines_button.Typeface = myCustomFont;
            procedures_button.Typeface = myCustomFont;

            adult_guidelines_button.Click += (object sender, EventArgs e) =>
            {
                Intent adult_guidelines = new Intent(this, typeof(PDFListItems));
                adult_guidelines.PutExtra("type", "Adult");
                StartActivity(adult_guidelines);

            };

            pediatric_guidelines_button.Click += (object sender, EventArgs e) =>
            {
                Intent pediatric_guidelines = new Intent(this, typeof(PDFListItems));
                pediatric_guidelines.PutExtra("type", "Pediatric");
                StartActivity(pediatric_guidelines);

            };

            ob_guidelines_button.Click += (object sender, EventArgs e) =>
            {
                Intent ob_guidelines = new Intent(this, typeof(PDFListItems));
                ob_guidelines.PutExtra("type", "OB");
                StartActivity(ob_guidelines);

            };

            trauma_guidelines_button.Click += (object sender, EventArgs e) =>
            {
                Intent trauma_guidelines = new Intent(this, typeof(PDFListItems));
                trauma_guidelines.PutExtra("type", "Trauma");
                StartActivity(trauma_guidelines);

            };

            procedures_button.Click += (object sender, EventArgs e) =>
            {
                Intent procedures = new Intent(this, typeof(PDFListItems));
                procedures.PutExtra("type", "Procedures");
                StartActivity(procedures);

            };


            // Create your application here
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}