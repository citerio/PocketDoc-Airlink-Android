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
using Org.Json;

namespace Airlink
{
    [Activity(Label = "")]
    public class MedicineListItems : AppCompatActivity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        MedicineAdapter mAdapter;
        Medicine[] medicines_items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.medicines_list);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = Resources.GetString(Resource.String.ApplicationName);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.medicines_item_list);

            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerView.SetItemAnimator(new DefaultItemAnimator());
            mRecyclerView.AddItemDecoration(new SimpleItemDecoration(this));

            fillMedicines();

            mAdapter = new MedicineAdapter(medicines_items);
            mAdapter.ItemClick += OnItemClick;
            mRecyclerView.SetAdapter(mAdapter);
        }


        public void fillMedicines() {


            try
            {

                string[] medicines = Resources.GetStringArray(Resource.Array.medicines_list);

                medicines_items = new Medicine[medicines.Length];
                JSONObject medicine_jobject;

                for (int i = 0; i < medicines.Length; i++)
                {

                    medicine_jobject = new JSONObject(medicines[i]);
                    medicines_items[i] = new Medicine(medicine_jobject.GetString("title"), medicine_jobject.GetString("dose"), medicine_jobject.GetString("info"));                    

                }

                medicines_items = medicines_items.OrderBy(s => s.Title).ToArray();

            } catch (Exception e) {

                throw;

            }         



        }

        void OnItemClick(Object sender, MedicineAdapterClickEventArgs e)
        {

            TextView title = e.View.FindViewById<TextView>(Resource.Id.medicine_title);
            TextView dose = e.View.FindViewById<TextView>(Resource.Id.medicine_dose);
            TextView info = e.View.FindViewById<TextView>(Resource.Id.medicine_info);

            Intent medicine_details = new Intent(this, typeof(MedicineDetails));
            medicine_details.PutExtra("title", title.Text);
            medicine_details.PutExtra("dose", dose.Text);
            medicine_details.PutExtra("info", info.Text);

            StartActivity(medicine_details);


            //Toast.MakeText(this, "PDF item clicked" + pdf_name.Text, ToastLength.Short).Show();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}