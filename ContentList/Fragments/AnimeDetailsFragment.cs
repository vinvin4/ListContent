// -----------------------------------------------------------------------
//  <copyright file="AnimeDetailsFragment.cs" />
// -----------------------------------------------------------------------
using Android.OS;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentList.Android.Fragments
{
    public class AnimeDetailsFragment : BaseFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            TAG = nameof(AnimeDetailsFragment);
        }

        /// <summary>
        /// Initialize view
        /// </summary>
        protected override void InitializeView()
        {
            var facts = JsonConvert.DeserializeObject<List<string>>(Details);
            for(int i = 0; i < facts.Count; i++)
            {
                container.AddView(new DescritionView(ParentActivity, Resource.Layout.t_list_container,
                    $"{i+1}.", facts[i]).GetView());
            }
        }
    }
}