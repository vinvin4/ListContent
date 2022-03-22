//-----------------------------------------------------------------------
//  <copyright file="BaseFragment.cs" />
// -----------------------------------------------------------------------
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using ContentList.Android.Activities;
using Fragment = AndroidX.Fragment.App.Fragment;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

namespace ContentList.Android.Fragments
{
    public class BaseFragment : Fragment
    {
        protected string TAG = nameof(BaseFragment);
        protected LinearLayout container;

        /// <summary>
        /// Parent activity
        /// </summary>
        protected DetailsActivity ParentActivity
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

        /// <summary>
        /// Creates and returns the view hierarchy associated with the fragment.
        /// </summary>
        /// <param name="inflater">LayoutInflater object that can be used to inflate any views in the fragment.</param>
        /// <param name="container">Parent view that the fragment's UI should be attached to.</param>
        /// <param name="savedInstanceState">Previous saved state.</param>
        /// <returns>View for the fragment's UI, or nul.</returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.f_base_container, container, false);
        }

        /// <summary>
        /// Called when the fragment's activity has been created and this fragment's view hierarchy instantiated.
        /// </summary>
        /// <param name="savedInstanceState">Previous saved state.</param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            container = View.FindViewById<LinearLayout>(Resource.Id.contentContainer);
            InitializeView();
        }

        /// <summary>
        /// Initialize view
        /// </summary>
        protected virtual void InitializeView()
        {
            var factsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Details);

            foreach (KeyValuePair<string, string> item in factsDictionary.AsEnumerable())
            {
                container.AddView(new DescritionView(ParentActivity, item.Key, item.Value).GetView());
            }
        }

        /// <summary>
        /// Local class to visualize information
        /// </summary>
        protected class DescritionView
        {
            private readonly LinearLayout container;

            #region Constructors
            /// <summary>
            /// Initialize new instance of <see cref="DescritionView"/>
            /// </summary>
            /// <param name="context">Current context</param>
            /// <param name="title">Fact title</param>
            /// <param name="value">Fact value</param>
            public DescritionView(Context context, string title, string value)
                : this(context, Resource.Layout.t_details_fact, title, value)
            {
            }

            /// <summary>
            /// Initialize new instance of <see cref="DescritionView"/>
            /// </summary>
            /// <param name="context">Current context</param>
            /// <param name="layoutId">layout ID</param>
            /// <param name="title">Fact title</param>
            /// <param name="value">Fact Value</param>
            public DescritionView(Context context, int layoutId, string title, string value)
            {
                container = (LinearLayout)LayoutInflater.From(context).Inflate(layoutId, null);
                container.FindViewById<TextView>(Resource.Id.titleTV).Text = title;
                container.FindViewById<TextView>(Resource.Id.contentTV).Text = value;
            }
            #endregion

            public View GetView() => container;
        }
    }
}