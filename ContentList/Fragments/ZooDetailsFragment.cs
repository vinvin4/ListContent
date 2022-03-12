//-----------------------------------------------------------------------
//  <copyright file="ZooFragmentDetails.cs" />
// -----------------------------------------------------------------------
using Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ContentList
{
    public class ZooFragmentDetails : BaseFragment
    {
        /// <summary>
        /// Initialize view
        /// </summary>
        public override void InitializeView()
        {
            base.InitializeView();
            ZooAnimalDetails facts = JsonConvert.DeserializeObject<ZooAnimalDetails>(Details);
            Dictionary<string, string> factsDictionary = new Dictionary<string, string>()
            {
                {GetString(Resource.String.NameTitle), facts.Name },
                {GetString(Resource.String.AnimalLenghtTitle), facts.Lenght },
                {GetString(Resource.String.AnimalWeightTitle), facts.Weight },
                {GetString(Resource.String.LifespanTitle), facts.Lifespan.ToString()}
            };
            factsDictionary[GetString(Resource.String.DurationTitle)] = facts.IsDiurnal ?
                GetString(Resource.String.DiurnalDuration) : GetString(Resource.String.NocturnalDuration);

            foreach (KeyValuePair<string, string> item in factsDictionary.AsEnumerable())
            {
                container.AddView(new DescritionView(ParentActivity, item.Key, item.Value).GetView());
            }
        }
    }
}