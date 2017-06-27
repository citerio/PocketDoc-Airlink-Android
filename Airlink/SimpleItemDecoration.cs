﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Airlink
{
    public class SimpleItemDecoration : RecyclerView.ItemDecoration
    {
        private Drawable divider;
        private int[] attributes = new int[] { Android.Resource.Attribute.ListDivider };

        public SimpleItemDecoration(Context context)
        {
            TypedArray ta = context.ObtainStyledAttributes(attributes);
            divider = ta.GetDrawable(0);
            ta.Recycle();
        }

        public override void OnDraw(Android.Graphics.Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            base.OnDraw(c, parent, state);

            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;

            for (int i = 0; i < parent.ChildCount; i++)
            {
                View child = parent.GetChildAt(i);

                var parameters = child.LayoutParameters.JavaCast<RecyclerView.LayoutParams>();                

                int top = child.Bottom + parameters.BottomMargin;
                int bottom = top + divider.IntrinsicHeight;

                divider.SetBounds(left, top, right, bottom);
                divider.Draw(c);
            }
        }
    }
}