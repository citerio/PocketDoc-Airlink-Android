using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Airlink
{
    class MedicineAdapter : RecyclerView.Adapter
    {
        public event EventHandler<MedicineAdapterClickEventArgs> ItemClick;
        public event EventHandler<MedicineAdapterClickEventArgs> ItemLongClick;
        Medicine[] items;

        public MedicineAdapter(Medicine[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.medicine, parent, false);

            var vh = new MedicineAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            MedicineAdapterViewHolder holder = viewHolder as MedicineAdapterViewHolder;
            holder.title.Text = items[position].Title;
            holder.dose.Text = items[position].Dose;
            holder.info.Text = items[position].Info;
        }

        public override int ItemCount => items.Length;

        void OnClick(MedicineAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(MedicineAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class MedicineAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView title { get; set; }
        public TextView dose { get; set; }
        public TextView info { get; set; }


        public MedicineAdapterViewHolder(View itemView, Action<MedicineAdapterClickEventArgs> clickListener,
                            Action<MedicineAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            title = itemView.FindViewById<TextView>(Resource.Id.medicine_title);
            dose = itemView.FindViewById<TextView>(Resource.Id.medicine_dose);
            info = itemView.FindViewById<TextView>(Resource.Id.medicine_info);
            itemView.Click += (sender, e) => clickListener(new MedicineAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new MedicineAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class MedicineAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}