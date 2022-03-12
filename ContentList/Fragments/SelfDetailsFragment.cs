// -----------------------------------------------------------------------
//  <copyright file="SelfDetailsFragment.cs" />
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace ContentList
{
    public class SelfDetailsFragment : BaseFragment
    {
        /// <summary>
        /// Initialize view
        /// </summary>
        public override void InitializeView()
        {
            base.InitializeView();
            Dictionary<string, string> contentDictionary = new Dictionary<string, string>()
            {
                {GetString(Resource.String.NameTitle), "Ivan" },
                { GetString(Resource.String.AgeTitle), "23"},
                {GetString(Resource.String.WantWorking), bool.TrueString }
            };

            foreach (KeyValuePair<string,string> item in contentDictionary.AsEnumerable())
            {
                container.AddView(new DescritionView(ParentActivity, item.Key, item.Value).GetView());
            }
        }
    }
}