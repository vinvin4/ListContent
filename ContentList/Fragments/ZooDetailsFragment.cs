//-----------------------------------------------------------------------
//  <copyright file="ZooFragmentDetails.cs" />
// -----------------------------------------------------------------------
using Android.OS;

namespace ContentList.Android.Fragments
{
    public class ZooFragmentDetails : BaseFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            TAG = nameof(ZooFragmentDetails);
        }
    }
}