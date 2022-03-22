// -----------------------------------------------------------------------
//  <copyright file="IDownloadingHandlers.cs" />
// -----------------------------------------------------------------------s
using Core.Models;

using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IDownloadingHandlers
    {
        /// <summary>
        /// Callback to start downloading and update UI
        /// </summary>
        /// <param name="title">Title to be set for progress dialog</param>
        /// <param name="message">Message to be set for progress dialog</param>
        void OnDownloadingStarted(string title, string message);

        /// <summary>
        /// Callback to stop downloading and update UI
        /// </summary>
        /// <param name="title">Title to be set for progress dialog</param>
        /// <param name="message">Message to be set for progress dialog</param>
        void OnDownloadingFinised(string title, string message);

        /// <summary>
        /// Callback to notify, that some part is executed, and apply changes
        /// </summary>
        /// <param name="message">Message to be set for progress dialog</param>
        /// <param name="receivedModels">Execution result to update UI</param>
        void OnProgressUpdate(string message, List<AdapterModel> receivedModels = null);
    }
}
