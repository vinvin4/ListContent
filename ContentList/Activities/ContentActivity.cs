// -----------------------------------------------------------------------
//  <copyright file="ContentActivity.cs" />
// -----------------------------------------------------------------------
using Android.App;
using Android.OS;
using Android.Net;
using Android.Content.PM;
using Android.Widget;
using Android.Content;
using AndroidX.AppCompat.App;

using Core.Interfaces;
using Core.Models;
using Core.Services;

using System.Collections.Generic;
using System.Linq;

namespace ContentList.Android.Activities
{
    [Activity(
        Label = "@string/app_name",
        Theme = "@style/AppTheme.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait,
        MainLauncher = true)]
    public class ContentActivity : AppCompatActivity, IDownloadingHandlers
    {
        AndroidX.AppCompat.App.AlertDialog dialog;
        ContentService contentService;
        ContentAdapter adapter;

        InternetChangedReceiver networkChangeReceiver;
        bool wasFirstLaunch = false;

        #region Activity callbacks
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.a_content);
            networkChangeReceiver = new InternetChangedReceiver();
            networkChangeReceiver.InternetStatusChanged += (se, args) => CheckUnreceivedContent();
            RegisterReceiver(networkChangeReceiver, new IntentFilter(ConnectivityManager.ConnectivityAction));

            var listView = FindViewById<ListView>(Resource.Id.gridAdapter);
            adapter = new ContentAdapter(this);
            listView.Adapter = adapter;
            listView.Clickable = true;
            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => OpenDetailsScreen(e.Position);

            contentService = new ContentService(this);
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (!wasFirstLaunch)
            {
                contentService.Start();
                wasFirstLaunch = true;
            }            
        }

        protected override void OnDestroy()
        {
            UnregisterReceiver(networkChangeReceiver);
            base.OnDestroy();
        }
        #endregion

        /// <summary>
        /// Handler function to open Details screen
        /// </summary>
        /// <param name="position">List Item position</param>
        private void OpenDetailsScreen(int position)
        {
            var intent = new Intent(this, typeof(DetailsActivity));
            var clickedItem = adapter[position];
            intent.PutExtra(DetailsActivity.TypeExtra, clickedItem.ModelType);
            intent.PutExtra(DetailsActivity.DetailsExtra, clickedItem.Details);
            StartActivity(intent);
        }

        /// <summary>
        /// Handler to reinitialize downloading content
        /// </summary>
        private void CheckUnreceivedContent()
        {
            if (contentService.IsDownloadingNeeded && !dialog.IsShowing)
            {
                AndroidX.AppCompat.App.AlertDialog dialog = null;
                AndroidX.AppCompat.App.AlertDialog.Builder builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this)
                    .SetTitle(Resource.String.UpdateDialogTitle)
                    .SetMessage(Resource.String.RedownloadContent)
                    .SetNegativeButton(GetString(Resource.String.NoOption).ToUpper(), (se, a) => RunOnUiThread(() => dialog?.Hide()))
                    .SetPositiveButton(GetString(Resource.String.YesOption).ToUpper(), (se, a) => 
                    {
                        dialog?.Hide();
                        contentService.Start();
                    })
                    .SetCancelable(false);

                dialog = builder.Create();
                dialog.Show();
            }
        }

        #region IDownloadingHandler interface implementation
        /// <summary>
        /// Callback to start downloading and update UI
        /// </summary>
        /// <param name="title">Title to be set for progress dialog</param>
        /// <param name="message">Message to be set for progress dialog</param>
        public void OnDownloadingStarted(string title, string message)
        {
            var dialogBuilder = new AndroidX.AppCompat.App.AlertDialog.Builder(this)
                .SetTitle(title)
                .SetPositiveButton(GetString(Resource.String.OkOption).ToUpper(), (se, a) =>
                {
                    RunOnUiThread(() => dialog.Hide());
                })
                .SetMessage(message)
                .SetCancelable(false);

            dialog = dialogBuilder.Create();
            dialog.Show();

            dialog.GetButton((int)DialogButtonType.Positive).Enabled = false;
        }

        /// <summary>
        /// Callback to stop downloading and update UI
        /// </summary>
        /// <param name="title">Title to be set for progress dialog</param>
        /// <param name="message">Message to be set for progress dialog. It's format. Need to pass really received models count</param>
        public void OnDownloadingFinised(string title, string message)
        {
            RunOnUiThread(() =>
            {
                dialog.SetTitle(title);
                dialog.SetMessage(string.Format(message, adapter.Count));
                dialog.GetButton((int)DialogButtonType.Positive).Enabled = true;
            });
        }

        /// <summary>
        /// Callback to notify, that some part is executed, and apply changes
        /// </summary>
        /// <param name="message">Message to be set for progress dialog</param>
        /// <param name="receivedModels">Execution result to update UI</param>
        public void OnProgressUpdate(string message, List<AdapterModel> receivedModels = null)
        {
            RunOnUiThread(() =>
            {
                if (receivedModels?.Any() ?? false)
                {
                    adapter.AddItems(receivedModels);
                    adapter.NotifyDataSetChanged();
                }                

                dialog.SetMessage(message);
            });
        }
        #endregion
    }
}
