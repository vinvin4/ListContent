// -----------------------------------------------------------------------
//  <copyright file="SelfDetailsFragment.cs" />
// -----------------------------------------------------------------------
using Android.OS;

namespace ContentList.Android.Fragments
{
    public class SelfDetailsFragment : BaseFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            TAG = nameof(SelfDetailsFragment);
        }
    }
}