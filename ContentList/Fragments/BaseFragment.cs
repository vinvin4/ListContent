//-----------------------------------------------------------------------
//  <copyright file="BaseFragment.cs" />
// -----------------------------------------------------------------------
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace ContentList
{
    public class BaseFragment : Fragment
    {
        /// <summary>
        /// Parent activity
        /// </summary>
        public DetailsActivity ParentActivity
        {
            get
            {
                return (DetailsActivity)Activity;
            }
        }

        /// <summary>
        /// Details value, received from server
        /// </summary>
        public string Details { get; set; }

        protected LinearLayout container;

        /// <summary>
        /// Creates and returns the view hierarchy associated with the fragment.
        /// </summary>
        /// <param name="inflater">LayoutInflater object that can be used to inflate any views in the fragment.</param>
        /// <param name="container">Parent view that the fragment's UI should be attached to.</param>
        /// <param name="savedInstanceState">Previous saved state.</param>
        /// <returns>View for the fragment's UI, or nul.</returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.f_self_details, container, false);
        }

        /// <summary>
        /// Called when the fragment's activity has been created and this fragment's view hierarchy instantiated.
        /// </summary>
        /// <param name="savedInstanceState">Previous saved state.</param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            InitializeView();
        }

        /// <summary>
        /// Initialize view
        /// </summary>
        public virtual void InitializeView()
        {
            container = View.FindViewById<LinearLayout>(Resource.Id.contentContainer);
        }

        /// <summary>
        /// Local class to visualize information
        /// </summary>
        public class DescritionView
        {
            private LinearLayout container;
            public DescritionView(Context context, string title, string value)
            {
                container = (LinearLayout)LayoutInflater.From(context).Inflate(Resource.Layout.t_details_fact, null);
                container.FindViewById<TextView>(Resource.Id.titleTV).Text = title;
                container.FindViewById<TextView>(Resource.Id.contentTV).Text = value;
            }

            public View GetView()
            {
                return container;
            }
        }
    }
}