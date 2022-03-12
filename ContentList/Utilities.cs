// -----------------------------------------------------------------------
//  <copyright file="Utilities.cs" />
// -----------------------------------------------------------------------
using Android.Content;
using Android.Graphics;
using Core.Utilities;

namespace ContentList
{
    public static class Utilities
    {
        /// <summary>
        /// Convert Item type to Color
        /// </summary>
        /// <param name="ctx">Current activity context</param>
        /// <param name="itemType">Received item type</param>
        /// <returns>Parsed Color</returns>
        public static Color GetItemTypeColor(Context ctx, int itemType)
        {
            int colorResourceId = 0;
            switch((EnumUtils.ModelType)itemType)
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
            colorResourceId = ctx.GetColor(colorResourceId);
            return new Color(colorResourceId);
        }
    }
}