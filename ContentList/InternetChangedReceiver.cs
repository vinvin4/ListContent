//-----------------------------------------------------------------------
//  <copyright file="InternetChangedReceiver.cs" />
// -----------------------------------------------------------------------
using Android.Content;
using Android.Net;
using System;

namespace ContentList.Android
{
    [BroadcastReceiver]
    public class InternetChangedReceiver : BroadcastReceiver
    {
        /// <summary>
        /// handler to execute some action, when Internet connection become availablee
        /// </summary>
        public event EventHandler<bool> InternetStatusChanged;

        /// <summary>
        /// Initialize new instance of <see cref="InternetChangedReceiver"/>
        /// </summary>
        public InternetChangedReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            
            if (networkInfo != null)
            {
                InternetStatusChanged?.Invoke(this, true);
            }
        }
    }
}