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
using Android.Preferences;
using Org.Json;

namespace Airlink
{
    [Activity(Label = "")]
    public class NotesListItems : AppCompatActivity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        NoteAdapter mAdapter;
        Note[] notes_items;
        public static NotesListItems main_context;
        MyBroadcastReceiver receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            main_context = this;
            receiver = new MyBroadcastReceiver();
            // Create your application here
            SetContentView(Resource.Layout.notes_items_list);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //SupportActionBar.Title = Intent.Extras.GetString("type");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.notes_list);
            Button add_note_button = FindViewById<Button>(Resource.Id.add_note_button);

            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);


            fillNotes();

            add_note_button.Click += (object sender, EventArgs e) =>
            {

                Intent note_details = new Intent(this, typeof(NoteDetails));
                note_details.PutExtra("note_title", "Title Note Here");
                note_details.PutExtra("note_description", "");
                note_details.PutExtra("position", 0);
                note_details.PutExtra("save_or_update", "Save");
                StartActivity(note_details);


            };




        }

        public void fillNotes() {

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            string notes = prefs.GetString("notes_list", null);

            if (notes != null) {

                try
                {
                    JSONArray notes_jarray = new JSONArray(notes);

                    if (notes_jarray.Length() > 0) {

                        notes_items = new Note[notes_jarray.Length()];                        

                        for (int i = 0; i < notes_jarray.Length(); i++) {


                            notes_items[i] = new Note(notes_jarray.GetJSONObject(i).GetString("title"), notes_jarray.GetJSONObject(i).GetString("description"), notes_jarray.GetJSONObject(i).GetString("date"));

                        }

                        mAdapter = new NoteAdapter(notes_items);
                        mAdapter.ItemClick += OnItemClick;
                        mRecyclerView.SetAdapter(mAdapter);


                    }

                    


                }
                catch (Exception e)
                {

                    throw;
                }


            }


        }

        void OnItemClick(Object sender, NoteAdapterClickEventArgs e)
        {

            TextView title = e.View.FindViewById<TextView>(Resource.Id.note_title);
            TextView description = e.View.FindViewById<TextView>(Resource.Id.note_description);
            int position = e.Position;

            Intent note_details = new Intent(this, typeof(NoteDetails));
            note_details.PutExtra("note_title", title.Text);
            note_details.PutExtra("note_description", description.Text);
            note_details.PutExtra("position", position);
            note_details.PutExtra("save_or_update", "Update");
            StartActivity(note_details);
            

            //Toast.MakeText(this, "PDF item clicked" + pdf_name.Text, ToastLength.Short).Show();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
                

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(receiver, new IntentFilter("notes_list"));
            
        }


        protected override void OnDestroy()
        {
            UnregisterReceiver(receiver);
            base.OnDestroy();
        }
        

        //This is the handler that will manager to process the broadcast intent
        [BroadcastReceiver]
        public class MyBroadcastReceiver : BroadcastReceiver        {            

            public override void OnReceive(Context context, Intent intent)
            {

                main_context.Recreate();
                
            }
        }







    }
}