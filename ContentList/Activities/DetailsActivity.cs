// -----------------------------------------------------------------------
//  <copyright file="DetailsActivity.cs" />
// -----------------------------------------------------------------------
using Android.App;
using Android.Content;
using Android.OS;
using Fragment = AndroidX.Fragment.App.Fragment;
using AndroidX.AppCompat.App;
using Core.Utilities;

namespace ContentList
{
    [Activity(Label = "DetailsActivity",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
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

            int contentType = Intent.GetIntExtra(TypeExtra, 0);
            string contentDetails = Intent.GetStringExtra(DetailsExtra);

            Fragment fragment = new Fragment();
            var fragmentTransaction = SupportFragmentManager.BeginTransaction();
            switch((EnumUtils.ModelType)contentType)
            {
                case EnumUtils.ModelType.ZooAnimal:
                    fragment = new ZooFragmentDetails()
                    {
                        Details = contentDetails
                    };
                    break;
                case EnumUtils.ModelType.Anime:
                    fragment = new AnimeDetailsFragment()
                    {
                        Details = contentDetails
                    };
                    break;
                case EnumUtils.ModelType.Museum:
                    fragment = new MuseumDetailsFragment()
                    {
                        Details = contentDetails
                    };
                    break;
                case EnumUtils.ModelType.Self:
                    fragment = new SelfDetailsFragment();
                    break;
            }
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