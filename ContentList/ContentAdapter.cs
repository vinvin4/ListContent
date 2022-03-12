//-----------------------------------------------------------------------
//  <copyright file="ContentAdapter.cs" />
// -----------------------------------------------------------------------
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Core.Models;
using Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ContentList
{
    public class ContentAdapter : BaseAdapter<AdapterModel>
    {
        #region Internal members
        private Context context;
        private List<AdapterModel> items;
        private Dictionary<int, Bitmap> picturesDictionary;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="ContentAdapter"/>
        /// </summary>
        /// <param name="context">Activity context</param>
        /// <param name="items">Items to visualize</param>
        public ContentAdapter(Context context, List<AdapterModel> items = null)
        {
            this.context = context;
            this.items = items ?? new List<AdapterModel>();
            this.picturesDictionary = new Dictionary<int, Bitmap>();
        }
        #endregion

        #region Adapter Overridables
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.t_adapter_view, null);
            }

            var item = items[position];
            convertView.FindViewById<LinearLayout>(Resource.Id.textFieldArea)
                .SetBackgroundColor(Utilities.GetItemTypeColor(context, item.ModelType));
            convertView.FindViewById<TextView>(Resource.Id.item_title).Text = item.Title;
            var imageView = convertView.FindViewById<ImageView>(Resource.Id.item_picture);
            if (item.ModelType == (int)EnumUtils.ModelType.Self)
            {
                imageView.SetImageDrawable(context.GetDrawable(Resource.Drawable.SelfPhoto));
            }
            else
            {
                if (picturesDictionary.ContainsKey(position))
                {
                    imageView.SetImageBitmap(picturesDictionary[position]);
                }
                else if (item.Image?.Any() ?? false)
                {
                    var imageBitmap = BitmapFactory.DecodeByteArray(item.Image, 0, item.Image.Length);
                    if (imageBitmap.Height > Constants.ImageSizeLimit || imageBitmap.Width > Constants.ImageSizeLimit)
                    {
                        imageBitmap = Bitmap.CreateScaledBitmap(imageBitmap, Constants.BitmapSize, Constants.BitmapSize, false);
                    }

                    picturesDictionary[position] = imageBitmap;
                    imageView.SetImageBitmap(imageBitmap);
                }
                else
                {
                    imageView.SetImageDrawable(context.GetDrawable(Resource.Drawable.ic_search));
                }
            }
            return convertView;
        }

        public override int Count { get => items.Count; }

        public override AdapterModel this[int position] => items[position];
        #endregion

        /// <summary>
        /// Public callback to add items in Adapter
        /// </summary>
        /// <param name="items">Items to be added</param>
        public void AddItems(List<AdapterModel> items)
        {
            this.items.AddRange(items);
        }
    }
}