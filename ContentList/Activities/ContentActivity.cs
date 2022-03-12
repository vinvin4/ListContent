// -----------------------------------------------------------------------
//  <copyright file="ContentActivity.cs" />
// -----------------------------------------------------------------------
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Interfaces;
using Android.Widget;
using Core.Models;
using Android.Content;
using System.Linq;
using Android.Net;

namespace ContentList
{
    [Activity(
        Label = "@string/app_name",
        Theme = "@style/AppTheme.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        MainLauncher = true)]
    public class ContentActivity : AppCompatActivity
    {
        AndroidX.AppCompat.App.AlertDialog.Builder dialogBuilder;
        AndroidX.AppCompat.App.AlertDialog dialog;
        List<IAbstractContentService> notExecutedServices;
        ContentAdapter adapter;

        private InternetChangedReceiver networkChangeReceiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.a_content);
            var networkReceiver = new InternetChangedReceiver();
            networkReceiver.InternetStatusChanged += (se, args) => CheckUnreceiedContent();
            RegisterReceiver(networkReceiver, new IntentFilter(ConnectivityManager.ConnectivityAction));

            var gridView = FindViewById<ListView>(Resource.Id.gridAdapter);
            adapter = new ContentAdapter(this);
            gridView.Adapter = adapter;
            gridView.Clickable = true;
            gridView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => OpenDetailsScreen(e.Position);

            notExecutedServices = new List<IAbstractContentService>()
            {
                new SelfService(),
                new ZooAnimalsService(),
                new AnimeService()
            };
            for (int i=1; i<10; i++)
            {
                notExecutedServices.Add(new MuseumService(i));
            }

            StartDownloadingContent();
        }

        /// <summary>
        /// Handler to create dialog message
        /// </summary>
        /// <param name="count">Count of downloaded items</param>
        /// <returns>Combined message</returns>
        private string GetDialogMessage(int count)
        {
            return string.Format(GetString(Resource.String.UpdateDialogMessage), count);
        }

        /// <summary>
        /// Handler function to open Details screen
        /// </summary>
        /// <param name="position">List Item position</param>
        private void OpenDetailsScreen(int position)
        {
            Intent intent = new Intent(this, typeof(DetailsActivity));
            var clickedItem = adapter[position];
            intent.PutExtra(DetailsActivity.TypeExtra, clickedItem.ModelType);
            intent.PutExtra(DetailsActivity.DetailsExtra, clickedItem.Details);
            StartActivity(intent);
        }

        /// <summary>
        /// Handler to initialize content
        /// </summary>
        private void StartDownloadingContent()
        {
            dialogBuilder = new AndroidX.AppCompat.App.AlertDialog.Builder(this)
                .SetTitle(Resource.String.UpdateDialogTitle)
                .SetPositiveButton("OK", (se, a) =>
                {
                    RunOnUiThread(() => dialog.Hide());
                })
                .SetMessage(GetDialogMessage(adapter.Count))
                .SetCancelable(false);

            dialog = dialogBuilder.Create();
            dialog.Show();

            dialog.GetButton((int)DialogButtonType.Positive).Enabled = false;

            Task.Run(async () =>
            {
                var corrupterExecutedServices = new List<IAbstractContentService>();
                foreach (var service in notExecutedServices)
                {
                    List<AdapterModel> receivedValues = await service.GetContentList();
                    if (receivedValues?.Any() ?? false)
                    {
                        RunOnUiThread(() =>
                        {
                            adapter.AddItems(receivedValues);
                            adapter.NotifyDataSetChanged();

                            dialog.SetMessage(GetDialogMessage(adapter.Count));
                        });
                        await Task.Delay(200);
                    }
                    else
                    {
                        corrupterExecutedServices.Add(service);
                    }                    
                }
                notExecutedServices = corrupterExecutedServices;
            }).ContinueWith((result) => 
            {
                RunOnUiThread(() => dialog.GetButton((int)DialogButtonType.Positive).Enabled = true);
            });
        }

        /// <summary>
        /// Handler to reinitialize downloading content
        /// </summary>
        private void CheckUnreceiedContent()
        {
            if ((notExecutedServices?.Any() ?? false) && !dialog.IsShowing)
            {
                AndroidX.AppCompat.App.AlertDialog dialog = null;
                AndroidX.AppCompat.App.AlertDialog.Builder builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this)
                    .SetTitle(Resource.String.UpdateDialogTitle)
                    .SetMessage(Resource.String.RedownloadContent)
                    .SetNegativeButton("NO", (se, a) => RunOnUiThread(() => dialog?.Hide()))
                    .SetPositiveButton("YES", (se, a) => StartDownloadingContent())
                    .SetCancelable(false);

                dialog = builder.Create();
                dialog.Show();
            }
        }

        protected override void OnDestroy()
        {
            UnregisterReceiver(networkChangeReceiver);
            base.OnDestroy();
        }
    }
}
