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
using Java.Util;
using Java.Text;
using Android.Support.V4.Content.Res;
using Android.Support.V4.Content;

namespace Airlink
{
    [Activity(Label = "")]
    public class NoteDetails : AppCompatActivity
    {
        EditText note_title_input;
        EditText note_description_input;
        Button save_update_button;
        int position;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.note_details);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = Resources.GetString(Resource.String.notes_list);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            note_title_input = FindViewById<EditText>(Resource.Id.note_title_input);
            note_description_input = FindViewById<EditText>(Resource.Id.note_description_input);
            save_update_button = FindViewById<Button>(Resource.Id.save_update_button);

            viewDidLoad();

            save_update_button.Click += (object sender, EventArgs e) =>
            {
                saveOrupdateNote();

            };

        }

        public void viewDidLoad() {

            note_title_input.Text = Intent.Extras.GetString("note_title");
            note_description_input.Text = Intent.Extras.GetString("note_description");
            save_update_button.Text = Intent.Extras.GetString("save_or_update");
            position = Intent.Extras.GetInt("position");


        }

        public void saveOrupdateNote() {


            if (note_title_input.Text.Equals("")) {

                setNotification("Error", "Give your note a title.");

            }else if (save_update_button.Text.Equals("Save")) {
                
                saveNote(note_title_input.Text, note_description_input.Text);

            }else if (save_update_button.Text.Equals("Update"))
            {

                updateNote(note_title_input.Text, note_description_input.Text);

            }


        }

        public void saveNote(string title, string description) {

            try
            {

                prefs = PreferenceManager.GetDefaultSharedPreferences(this);

                editor = prefs.Edit();

                string notes = prefs.GetString("notes_list", null);

                if (notes == null)
                {

                    JSONArray notes_jarray = new JSONArray();

                    JSONObject note_jobject = new JSONObject();
                    note_jobject.Put("title", title);
                    note_jobject.Put("description", description);
                    SimpleDateFormat dateFormat = new SimpleDateFormat("MM-dd-yyyy", Locale.English);
                    string date = dateFormat.Format(new Date());
                    note_jobject.Put("date", date);

                    notes_jarray.Put(note_jobject);

                    editor.PutString("notes_list", notes_jarray.ToString());
                    editor.Apply();


                }
                else
                {


                    JSONArray notes_jarray = new JSONArray(notes);
                    JSONArray notes_jarray_copy = new JSONArray();

                    JSONObject note_jobject = new JSONObject();
                    note_jobject.Put("title", title);
                    note_jobject.Put("description", description);
                    SimpleDateFormat dateFormat = new SimpleDateFormat("MM-dd-yyyy", Locale.English);
                    string date = dateFormat.Format(new Date());
                    note_jobject.Put("date", date);


                    notes_jarray_copy.Put(note_jobject);

                    for (int i = 0; i < notes_jarray.Length(); i++)
                    {

                        notes_jarray_copy.Put(notes_jarray.Get(i));

                    }

                    editor.PutString("notes_list", notes_jarray_copy.ToString());
                    editor.Apply();


                }

                save_update_button.Background = ContextCompat.GetDrawable(this, Resource.Drawable.rounded_button_green);
                save_update_button.Text = "Update";
                reloadNotesList();


            }
            catch (JSONException e)
            {

                throw;

            }


        }


        public void updateNote(string title, string description)
        {

            try
            {

                prefs = PreferenceManager.GetDefaultSharedPreferences(this);

                editor = prefs.Edit();

                string notes = prefs.GetString("notes_list", null);

                if (notes == null)
                {

                    JSONArray notes_jarray = new JSONArray();

                    JSONObject note_jobject = new JSONObject();
                    note_jobject.Put("title", title);
                    note_jobject.Put("description", description);
                    SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy", Locale.English);
                    string date = dateFormat.Format(new Date());
                    note_jobject.Put("date", date);

                    notes_jarray.Put(note_jobject);

                    editor.PutString("notes_list", notes_jarray.ToString());
                    editor.Apply();


                }
                else
                {


                    JSONArray notes_jarray = new JSONArray(notes);

                    notes_jarray.GetJSONObject(position).Remove("title");
                    notes_jarray.GetJSONObject(position).Remove("description");
                    notes_jarray.GetJSONObject(position).Put("title", title);                    
                    notes_jarray.GetJSONObject(position).Put("description", description);    
                                     

                    editor.PutString("notes_list", notes_jarray.ToString());
                    editor.Apply();


                }

                save_update_button.Background = ContextCompat.GetDrawable(this, Resource.Drawable.rounded_button_green);
                save_update_button.Text = "Update";
                reloadNotesList();
                setNotification("Updated", "Your note was updated!");


            }
            catch (JSONException e)
            {

                throw;

            }


        }

        public void reloadNotesList() {

            Intent notes_list = new Intent("notes_list");
            SendBroadcast(notes_list);

        }

        public void setNotification(string title, string message)
        {

            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetCancelable(true);
            builder.SetPositiveButton("OK", (senderAlert, args) => {

            });

            builder.Show();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}