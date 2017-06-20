using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Airlink
{
    class NoteAdapter : RecyclerView.Adapter
    {
        public event EventHandler<NoteAdapterClickEventArgs> ItemClick;
        public event EventHandler<NoteAdapterClickEventArgs> ItemLongClick;
        Note[] items;

        public NoteAdapter(Note[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.note_item, parent, false);

            var vh = new NoteAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            NoteAdapterViewHolder holder = viewHolder as NoteAdapterViewHolder;
            holder.title.Text = items[position].Title;
            holder.description.Text = items[position].Description;
            holder.date.Text = items[position].Date;
        }

        public override int ItemCount => items.Length;

        void OnClick(NoteAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(NoteAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class NoteAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView title { get; set; }
        public TextView description { get; set; }
        public TextView date { get; set; }


        public NoteAdapterViewHolder(View itemView, Action<NoteAdapterClickEventArgs> clickListener,
                            Action<NoteAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            title = itemView.FindViewById<TextView>(Resource.Id.note_title);
            description = itemView.FindViewById<TextView>(Resource.Id.note_description);
            date = itemView.FindViewById<TextView>(Resource.Id.note_date);
            itemView.Click += (sender, e) => clickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class NoteAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}