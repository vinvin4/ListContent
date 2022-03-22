//-----------------------------------------------------------------------
//  <copyright file="MuseumDetailsFragment.cs" />
// -----------------------------------------------------------------------
using Android.OS;

namespace ContentList.Android.Fragments
{
    public class MuseumDetailsFragment : BaseFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            TAG = nameof(MuseumDetailsFragment);
        }
    }
}