// -----------------------------------------------------------------------
//  <copyright file="IClonableModel.cs" />
// -----------------------------------------------------------------------
using Core.Models;

namespace Core.Interfaces
{
    internal interface IClonableModel
    {
        /// <summary>
        /// ToString overriding for model
        /// </summary>
        /// <returns>String-equivalent of model</returns>
        string ToString();

        /// <summary>
        /// Convert existing model to form, usable in platform implementation
        /// </summary>
        /// <returns>Adapter model, that is available for working on platform side</returns>
        AdapterModel ConvertToWorkingModel();
    }
}
