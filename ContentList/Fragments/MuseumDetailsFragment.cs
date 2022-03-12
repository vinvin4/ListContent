//-----------------------------------------------------------------------
//  <copyright file="MuseumDetailsFragment.cs" />
// -----------------------------------------------------------------------
using Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ContentList
{
    public class MuseumDetailsFragment : BaseFragment
    {
        /// <summary>
        /// Initialize view
        /// </summary>
        public override void InitializeView()
        {
            base.InitializeView();
            MuseumItemDetails facts = JsonConvert.DeserializeObject<MuseumItemDetails>(Details);
            Dictionary<string, string> factsDictionary = new Dictionary<string, string>()
            {
                {GetString(Resource.String.DisplayDateTitle), facts.DisplayDate },
                {GetString(Resource.String.OriginPlaceTitle), facts.OriginPlace }
            };

            foreach (KeyValuePair<string, string> item in factsDictionary.AsEnumerable())
            {
                container.AddView(new DescritionView(ParentActivity, item.Key, item.Value).GetView());
            }
        }
    }
}