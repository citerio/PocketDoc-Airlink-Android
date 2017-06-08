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
using Android.Webkit;

namespace Airlink
{
    [Activity(Label = "")]
    public class PDFViewer : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PDFViewer);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Airlink";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            WebView pdf_viewer = FindViewById<WebView>(Resource.Id.pdf_viewer);
            var settings = pdf_viewer.Settings;
            settings.JavaScriptEnabled = true;
            settings.AllowFileAccessFromFileURLs = true;
            settings.AllowUniversalAccessFromFileURLs = true;
            settings.BuiltInZoomControls = true;
            pdf_viewer.SetWebChromeClient(new WebChromeClient());
            pdf_viewer.LoadUrl("file:///android_asset/pdfjs/web/viewer.html?file=" + "file:///android_asset/pdf/" + Intent.Extras.GetString("item") + ".pdf" );


        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}