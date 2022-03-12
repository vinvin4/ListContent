// -----------------------------------------------------------------------
//  <copyright file="AnimeDetailsFragment.cs" />
// -----------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentList
{
    public class AnimeDetailsFragment : BaseFragment
    {
        /// <summary>
        /// Initialize view
        /// </summary>
        public override void InitializeView()
        {
            base.InitializeView();
            List<string> facts = JsonConvert.DeserializeObject<List<string>>(Details);
            for(int i = 0; i < facts.Count; i++)
            {
                container.AddView(new DescritionView(ParentActivity, $"{i+1}.", facts[i]).GetView());
            }
        }
    }
}