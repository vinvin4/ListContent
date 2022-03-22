// -----------------------------------------------------------------------
//  <copyright file="IClonableModel.cs" />
// -----------------------------------------------------------------------
using Core.Interfaces;
using Core.Models;

using Newtonsoft.Json;

namespace Core.AbstractClasses
{
    public abstract class AbstractClonableModel : IClonableModel
    {
        /// <summary>
        /// Override <code>Object.ToString()</code> method for models
        /// </summary>
        /// <returns>JSON equivalent of received data</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Convert existing model to form, usable in platform implementation
        /// </summary>
        /// <returns>Adapter model, that is available for working on platform side</returns>
        public abstract AdapterModel ConvertToWorkingModel();
    }
}
