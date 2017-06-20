using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Preferences;

namespace Airlink
{
	[Activity(Label = "Airlink", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            
            ISharedPreferencesEditor editor;

            editor = prefs.Edit();

            editor.Remove("notes_list");
            editor.Apply();

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "";

            //EditText phoneword_input = FindViewById<EditText>(Resource.Id.phoneword_input);
            TextView reference_guidelines_button = FindViewById<TextView>(Resource.Id.reference_guidelines_button);
            TextView personal_notes_button = FindViewById<TextView>(Resource.Id.personal_notes_button);
            TextView reference_medications_button = FindViewById<TextView>(Resource.Id.reference_medications_button);
            TextView calculations_button = FindViewById<TextView>(Resource.Id.calculations_button);
            TextView suggestions_button = FindViewById<TextView>(Resource.Id.suggestions_button);

            Typeface myCustomFont = Typeface.CreateFromAsset(Assets, "fonts/unicode.palatino.ttf");
            reference_guidelines_button.Typeface = myCustomFont;
            personal_notes_button.Typeface = myCustomFont;
            reference_medications_button.Typeface = myCustomFont;
            calculations_button.Typeface = myCustomFont;
            suggestions_button.Typeface = myCustomFont;


            reference_guidelines_button.Click += (object sender, EventArgs e) =>
            {
                Intent reference_guidelines = new Intent(this, typeof(ReferenceGuidelines));
                StartActivity(reference_guidelines);
                
            };

            calculations_button.Click += (object sender, EventArgs e) =>
            {
                Intent calculations = new Intent(this, typeof(Calculations));
                StartActivity(calculations);

            };

            personal_notes_button.Click += (object sender, EventArgs e) =>
            {
                Intent personal_notes = new Intent(this, typeof(NotesListItems));
                StartActivity(personal_notes);

            };



        }
	}
}

