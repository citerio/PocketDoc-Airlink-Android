using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Preferences;
using System.Threading.Tasks;
using Android;



namespace Airlink
{
	[Activity(Label = "Airlink", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{

        string phone_number;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            //ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            
            //ISharedPreferencesEditor editor;

            //editor = prefs.Edit();

            //editor.Remove("notes_list");
            //editor.Apply();

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
            Button call_rod_button = FindViewById<Button>(Resource.Id.call_rod);
            Button call_helenka_button = FindViewById<Button>(Resource.Id.call_helenka);


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

            reference_medications_button.Click += (object sender, EventArgs e) =>
            {
                Intent reference_medications = new Intent(this, typeof(MedicineListItems));
                StartActivity(reference_medications);

            };

            call_rod_button.Click += (object sender, EventArgs e) =>
            {
                setNotification("Rod", "8307089137");

            };

            call_helenka_button.Click += (object sender, EventArgs e) =>
            {
                setNotification("Helenka", "5414195220");

            };            


        }


        public void setNotification(string name, string number)
        {

            phone_number = number;
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle("Call");
            builder.SetMessage("Do you wish to call " + name + "?");
            builder.SetCancelable(true);
            builder.SetPositiveButton("YES", delegate {
                // Create intent to dial phone
                //var callIntent = new Intent(Intent.ActionCall);
                //callIntent.SetData(Android.Net.Uri.Parse("tel:" + number));
                //StartActivity(callIntent);
                TryGetLocationAsync();
            });

            builder.SetNegativeButton("CANCEL", (senderAlert, args) => {

            });

            builder.Show();
        }

        public void makeCall() {

            // Create intent to dial phone
            var callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse("tel:" + phone_number));
            StartActivity(callIntent);

        }

        /// <summary>
        /// runtime permissions for Android M or higher
        /// </summary>
        /// <returns></returns>
        readonly string[] PermissionsLocation =
        {
          Manifest.Permission.CallPhone          
        };

        const int RequestLocationId = 0;

        async Task TryGetLocationAsync()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                makeCall();
                return;
            }

            await GetLocationCompatAsync();
        }

        async Task GetLocationCompatAsync()
        {
            const string permission = Manifest.Permission.CallPhone;
            if (ContextCompat.CheckSelfPermission(this, permission) == (int)Permission.Granted)
            {
                makeCall();
                return;
            }

            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, permission))
            {
                //Explain to the user why we need to read the contacts           

                var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                builder.SetTitle("Grant Persmission");
                builder.SetMessage("Phone call permission is required to make calls.");
                builder.SetCancelable(true);
                builder.SetPositiveButton("OK", delegate {
                    ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId);
                });

                builder.Show();

                return;
            }

            ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId);
        }


        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            //Permission Denied :(
                            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                            builder.SetTitle("Grant Persmission");
                            builder.SetMessage("Phone call permission is granted");
                            builder.SetCancelable(true);
                            builder.SetPositiveButton("OK", delegate {

                                makeCall();

                            });

                            builder.Show();
                            
                        }
                        else
                        {
                            //Permission Denied :(
                            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                            builder.SetTitle("Grant Persmission");
                            builder.SetMessage("Phone call permission is denied");
                            builder.SetCancelable(true);
                            builder.SetPositiveButton("OK", delegate {
                                
                            });

                            builder.Show();
                        }
                    }
                    break;
            }
        }




    }
}

