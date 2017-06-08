using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Airlink
{
    class PdfAdapter : RecyclerView.Adapter
    {
        public event EventHandler<PdfAdapterClickEventArgs> ItemClick;
        public event EventHandler<PdfAdapterClickEventArgs> ItemLongClick;
        Pdf[] items;

        public PdfAdapter(Pdf[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.pdf_item, parent, false);

            var vh = new PdfAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            PdfAdapterViewHolder holder = viewHolder as PdfAdapterViewHolder;
            holder.pdf_name.Text = items[position].Name;
        }

        public override int ItemCount => items.Length;

        void OnClick(PdfAdapterClickEventArgs args) => ItemClick?.Invoke(this, args) ;
        void OnLongClick(PdfAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PdfAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView pdf_name { get; set; }


        public PdfAdapterViewHolder(View itemView, Action<PdfAdapterClickEventArgs> clickListener,
                            Action<PdfAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            pdf_name = itemView.FindViewById<TextView>(Resource.Id.pdf_name);
            itemView.Click += (sender, e) => clickListener(new PdfAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PdfAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PdfAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }


    }
}