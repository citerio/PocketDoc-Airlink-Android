using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using System;
using Android.Text;

namespace Airlink
{
    [Activity(Label = "")]
    public class Calculations : AppCompatActivity
    {

        EditText height_in_inches_input;
        ToggleButton switch_gender;
        TextView ideal_body_weight;
        TextView ideal_vt;
        TextView ketamine_rsi_dose;
        TextView etomidate_rsi_dose;
        TextView rocuronium_rsi_dose;
        TextView succi_rsi_dose;
        Button calculate_button;
        double hgt2;
        int inches;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.calculations);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Airlink";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            // Create your application here
            height_in_inches_input = FindViewById<EditText>(Resource.Id.height_in_inches_input);
            switch_gender = FindViewById<ToggleButton>(Resource.Id.switch_gender);
            ideal_body_weight = FindViewById<TextView>(Resource.Id.ideal_body_weight);
            ideal_vt = FindViewById<TextView>(Resource.Id.ideal_vt);
            ketamine_rsi_dose = FindViewById<TextView>(Resource.Id.ketamine_rsi_dose);
            etomidate_rsi_dose = FindViewById<TextView>(Resource.Id.etomidate_rsi_dose);
            rocuronium_rsi_dose = FindViewById<TextView>(Resource.Id.rocuronium_rsi_dose);
            succi_rsi_dose = FindViewById<TextView>(Resource.Id.succi_rsi_dose);
            calculate_button = FindViewById<Button>(Resource.Id.calculate_button);


            calculate_button.Click += (object sender, EventArgs e) =>
            {

                calcButton();

            };

            height_in_inches_input.TextChanged += new EventHandler<TextChangedEventArgs>(OnTextChange);

            switch_gender.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e) {
                calcButton();
            };

        }

        public void OnTextChange(object sender, EventArgs e)
        {
            calcButton();
        }


        public void calcButton()
        {

            string i = height_in_inches_input.Text;


            if (i.Equals("") || int.Parse(i) == 0)
            {

                ideal_vt.Text = "";
                ideal_body_weight.Text = "";
                //Toast.MakeText(this, "Empty or 0", ToastLength.Short).Show();
                setNotification("Error in data.", "Please enter a height in inches.");
            }
            else
            {
                inches = int.Parse(i);
                Calculate(inches);


            }

            //textFieldDoneEditing(sender: height)
            

        }


        public void Calculate(int inches)
        {

            if (switch_gender.Checked)
            {
                //("male")
                //Toast.MakeText(this, "Man", ToastLength.Short).Show();
                double cmHt = (double)inches * 2.5;
                double predMaleBW = 50 + 0.91 * (cmHt - 152.4);
                //print("predicted bw \(predMaleBW)")
                //let hgt = inches
                //let hgt2:Double = Double(100 + ((hgt - 60) * 7)) / 2.2
                double hgt2 = predMaleBW;
                calculations(hgt2);
                int HighVT = (int)(hgt2 * 8);
                int LowVT = (int)(hgt2 * 6);
                int hgt4 = (int)hgt2;
                string hgtWeight = hgt4.ToString();
                ideal_body_weight.Text = hgtWeight + "kg";
                ideal_vt.Text = LowVT.ToString() + "cc - " + HighVT.ToString() + "cc";

            }
            else if (!switch_gender.Checked)
            {
                //female
                //Toast.MakeText(this, "Woman", ToastLength.Short).Show();
                double cmHt = (double)inches * 2.5;
                double predFemaleBW = 45.5 + 0.91 * (cmHt - 152.4);
                //print(predFemaleBW)
                double hgt2 = predFemaleBW;
                calculations(hgt2);
                int hgt3 = (int)(hgt2 * 8);
                int LowVT = (int)(hgt2 * 6);
                int hgt4 = (int)hgt2;
                string hgtWeight = hgt4.ToString();
                ideal_body_weight.Text = hgtWeight + "kg";
                string hgtTidal = hgt3.ToString();
                ideal_vt.Text = LowVT.ToString() + "cc - " + hgtTidal.ToString() + "cc";
            }

        }

        public void calculations(double hgt2)
        {
            double height = hgt2;
            succi_rsi_dose.Text = ((int)(height * 1.5)).ToString() + "mg";
            ketamine_rsi_dose.Text = ((int)(height * 1.5)).ToString() + "mg";
            etomidate_rsi_dose.Text = ((int)(height * 0.3)).ToString() + "mg";
            rocuronium_rsi_dose.Text = ((int)(height * 1)).ToString() + "mg";

        }




        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }


        public void setNotification(string title, string message) {

            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);            
            builder.SetTitle(title);            
            builder.SetMessage(message);            
            builder.SetCancelable(true);
            builder.SetPositiveButton("OK", (senderAlert, args) => {
                
            });

            builder.Show();
        }



    }        

        
    

    
}