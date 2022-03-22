// -----------------------------------------------------------------------
//  <copyright file="DetailsActivity.cs" />
// -----------------------------------------------------------------------
using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using AndroidX.AppCompat.App;

using Core.Utilities;
using ContentList.Android.Fragments;

namespace ContentList.Android.Activities
{
    [Activity(Label = "@string/DetailsScreenTitle",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class DetailsActivity : AppCompatActivity
    {
        public const string TypeExtra = "sample.Type";
        public const string DetailsExtra = "sample.Details";

        #region Activity Lifecycle
        /// <summary>
        /// Initialize instance of <see cref="DetailsActivity"/>
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.a_details);

            var contentType = Intent.GetIntExtra(TypeExtra, 0);
            var contentDetails = Intent.GetStringExtra(DetailsExtra);

            var fragment = new BaseFragment();
            var fragmentTransaction = SupportFragmentManager.BeginTransaction();
            switch((EnumUtils.ModelType)contentType)
            {
                case EnumUtils.ModelType.ZooAnimal:
                    fragment = new ZooFragmentDetails();
                    break;
                case EnumUtils.ModelType.Anime:
                    fragment = new AnimeDetailsFragment();
                    break;
                case EnumUtils.ModelType.Museum:
                    fragment = new MuseumDetailsFragment();
                    break;
                case EnumUtils.ModelType.Self:
                    fragment = new SelfDetailsFragment();
                    break;
            }
            fragment.Details = contentDetails;
            fragmentTransaction.Replace(Resource.Id.container, fragment);
            fragmentTransaction.Commit();
        }
        #endregion

        #region Overridables
        /// <summary>
        /// hardware Back pressing overriding
        /// </summary>
        public override void OnBackPressed()
        {
            Finish();
        }
        #endregion
    }
}