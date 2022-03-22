//-----------------------------------------------------------------------
//  <copyright file="ContentAdapter.cs" />
// -----------------------------------------------------------------------
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

using Core.Models;
using Core.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentList.Android
{
    public class ContentAdapter : BaseAdapter<AdapterModel>
    {
        #region Internal members
        private readonly Context context;
        private readonly List<AdapterModel> items;
        private readonly Dictionary<int, Bitmap> picturesDictionary;
        private readonly int ImageSizeLimit;
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
            ImageSizeLimit = GetSizeLimits();
        }
        #endregion

        #region Adapter Overridables
        public override Java.Lang.Object GetItem(int position) => position;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.t_adapter_view, null);
            }

            var item = items[position];
            convertView.FindViewById<LinearLayout>(Resource.Id.textFieldArea)
                .SetBackgroundColor(GetItemTypeColor(context, item.ModelType));
            convertView.FindViewById<TextView>(Resource.Id.item_title).Text = item.Title;
            var imageView = convertView.FindViewById<ImageView>(Resource.Id.item_picture);
            if (item.ModelType == (int)EnumUtils.ModelType.Self)
            {
                imageView.SetImageDrawable(context.GetDrawable(Resource.Drawable.SelfPhoto));
            }
            else
            {
                InitializePicture(imageView, position);
            }
            return convertView;
        }

        public override int Count => items.Count;

        public override AdapterModel this[int position] => items[position];
        #endregion

        /// <summary>
        /// Public callback to add items in Adapter
        /// </summary>
        /// <param name="items">Items to be added</param>
        public void AddItems(List<AdapterModel> items) => this.items.AddRange(items);

        /// <summary>
        /// Convert Item type to Color
        /// </summary>
        /// <param name="ctx">Current activity context</param>
        /// <param name="itemType">Received item type</param>
        /// <returns>Parsed Color</returns>
        private Color GetItemTypeColor(Context ctx, int itemType)
        {
            int colorResourceId = 0;
            switch ((EnumUtils.ModelType)itemType)
            {
                case EnumUtils.ModelType.ZooAnimal:
                    colorResourceId = Resource.Color.ZooItemColor;
                    break;
                case EnumUtils.ModelType.Anime:
                    colorResourceId = Resource.Color.AnimeItemColor;
                    break;
                case EnumUtils.ModelType.Museum:
                    colorResourceId = Resource.Color.MuseumItemColor;
                    break;
                case EnumUtils.ModelType.Self:
                    colorResourceId = Resource.Color.SelfItemColor;
                    break;
            }
            colorResourceId = GetColorWrapper(colorResourceId);
            return new Color(colorResourceId);
        }

        /// <summary>
        /// Helper function to process image
        /// </summary>
        /// <param name="imageView">Image widget to update</param>
        /// <param name="position">Current processed position</param>
        private void InitializePicture(ImageView imageView, int position)
        {
            if (picturesDictionary.ContainsKey(position))
            {
                imageView.SetImageBitmap(picturesDictionary[position]);
            }
            else if (items[position].Image?.Any() ?? false)
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InJustDecodeBounds = true;
                BitmapFactory.DecodeByteArray(items[position].Image, 0, items[position].Image.Length, options);

                options.InJustDecodeBounds = false;
                options.InSampleSize = CalculateInSampleSize(options.OutWidth, options.OutHeight);
                var imageBitmap = BitmapFactory.DecodeByteArray(items[position].Image, 0, items[position].Image.Length, options);
                
                picturesDictionary[position] = imageBitmap;
                imageView.SetImageBitmap(imageBitmap);
            }
            else
            {
                imageView.SetImageDrawable(context.GetDrawable(Resource.Drawable.ic_search));
            }
        }

        /// <summary>
        /// Helper function to get radio to resize image
        /// </summary>
        /// <param name="width">Incoming width</param>
        /// <param name="height">Incomint height</param>
        /// <returns>Ratio</returns>
        private int CalculateInSampleSize(int width, int height)
        {
            double widthRatio = (double)width / ImageSizeLimit;
            double heightRatio = (double)height / ImageSizeLimit;
            double ratio = Math.Min(widthRatio, heightRatio);
            float n = 1.0f;
            while ((n * 2) <= ratio)
            {
                n *= 2;
            }
            return (int)n;
        }

        /// <summary>
        /// Get Color Wrapper depends of Android version
        /// </summary>
        /// <param name="id">Color id</param>
        /// <returns>Color</returns>
        private int GetColorWrapper(int id)
        {
            return (Build.VERSION.SdkInt >= BuildVersionCodes.M) ?
                context.GetColor(id) : context.Resources.GetColor(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSizeLimits()
        {
            int constLimit = Constants.BitmapSize;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M && Build.VERSION.SdkInt < BuildVersionCodes.P) // Andr 7-8
            {
                constLimit *= 2;
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                constLimit *= 3;
            }
            return constLimit;
        }
    }
}