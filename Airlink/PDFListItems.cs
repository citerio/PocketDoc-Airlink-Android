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
    public class PDFListItems : AppCompatActivity
    {

        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        PdfAdapter mAdapter;
        Pdf[] pdf_items;
        
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pdf_items_list);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = Intent.Extras.GetString("type");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.pdf_items_list);

            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);

            mRecyclerView.SetItemAnimator(new DefaultItemAnimator());
            mRecyclerView.AddItemDecoration(new SimpleItemDecoration(this));

            string type = Intent.Extras.GetString("type");

            if (type.Equals("Adult") ){

                string[] items_type = Resources.GetStringArray(Resource.Array.adult_guidelines);
                fillPdf(items_type);

            }

            if (type.Equals("OB"))
            {

                string[] items_type = Resources.GetStringArray(Resource.Array.ob_guidelines);
                fillPdf(items_type);

            }

            if (type.Equals("Pediatric"))
            {

                string[] items_type = Resources.GetStringArray(Resource.Array.pediatric_guidelines);
                fillPdf(items_type);

            }

            if (type.Equals("Trauma"))
            {

                string[] items_type = Resources.GetStringArray(Resource.Array.trauma_guidelines);
                fillPdf(items_type);

            }

            if (type.Equals("Procedures"))
            {

                string[] items_type = Resources.GetStringArray(Resource.Array.procedures);
                fillPdf(items_type);

            }

            mAdapter = new PdfAdapter(pdf_items);
            mAdapter.ItemClick += OnItemClick;
            mRecyclerView.SetAdapter(mAdapter);


        }


        public void fillPdf(string[] items_type) {

            pdf_items = new Pdf[items_type.Length];

            for (int i = 0; i < items_type.Length; i++) {

                pdf_items[i] = new Pdf(items_type[i]);

            }

            pdf_items = pdf_items.OrderBy(s => s.Name).ToArray();
        }

        void OnItemClick(Object sender, PdfAdapterClickEventArgs e) {

            TextView pdf_name = e.View.FindViewById<TextView>(Resource.Id.pdf_name);

            Intent pdf_viewer = new Intent(this, typeof(PDFViewer));
            pdf_viewer.PutExtra("type", Intent.Extras.GetString("type"));
            pdf_viewer.PutExtra("item", pdf_name.Text);
            StartActivity(pdf_viewer);

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