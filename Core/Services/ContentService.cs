// -----------------------------------------------------------------------
//  <copyright file="ContentService.cs" />
// -----------------------------------------------------------------------

using Core.AbstractClasses;
using Core.Interfaces;
using Core.Models;
using Core.Utilities;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ContentService
    {
        #region Private fields
        private List<AbstractContentService> notExecutedServices;
        private readonly IDownloadingHandlers callbacks;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="ContentService"/>
        /// </summary>
        /// <param name="callbacks">Callbacks to handle Dialog behaviour</param>
        public ContentService(IDownloadingHandlers callbacks)
        {
            this.callbacks = callbacks;
            notExecutedServices = new List<AbstractContentService>()
            {
                new SelfService(),
                new ZooAnimalsService(),
                new AnimeService()
            };
            for (int i = 1; i < 10; i++)
            {
                notExecutedServices.Add(new MuseumService(i));
            }
        }
        #endregion
        /// <summary>
        /// Property to get information, are not executed services available
        /// </summary>
        public bool IsDownloadingNeeded => notExecutedServices?.Any() ?? true;

        /// <summary>
        /// Start service execution.
        /// Downloading content and update UI via callbacks
        /// </summary>
        public void Start()
        {
            int tasksCounter = 0;
            int modelsCounter = 0;
            callbacks.OnDownloadingStarted(DetailsResources.DownloadingProgressTitle, CreateDialogMessage(tasksCounter, modelsCounter));
            var executedServices = new List<AbstractContentService>();
            Task.Run(async () =>
            {
                foreach (var service in notExecutedServices)
                {
                    List<AdapterModel> receivedValues = await service.GetContentList();
                    tasksCounter++;
                    
                    if (receivedValues?.Any() ?? false)
                    {
                        modelsCounter += receivedValues.Count;
                        callbacks?.OnProgressUpdate(CreateDialogMessage(tasksCounter, modelsCounter), receivedValues);
                        executedServices.Add(service);
                    }
                    else
                    {
                        callbacks?.OnProgressUpdate(CreateDialogMessage(tasksCounter, modelsCounter));
                    }
                }
            }).ContinueWith((result) =>
            {
                Utilities.Utilities.LogMessage($"ContentService State. Completed {result.IsCompleted}, Cancelled: {result.IsCanceled}");
                callbacks.OnDownloadingFinised(DetailsResources.DownloadingFinishTitle,
                    DetailsResources.DownloadingFinishContent);
                var executedServicesIds = executedServices
                    .Select(service => service.SessionId)
                    .ToList();
                notExecutedServices = notExecutedServices
                    .Where(service => !executedServicesIds.Contains(service.SessionId))
                    .ToList();
            });
        }

        /// <summary>
        /// Support function to get content for progress dialog
        /// </summary>
        /// <param name="tasksCounter">Already executed tasks count</param>
        /// <param name="modelsCounter">Already downloaded models count</param>
        /// <returns>String dialog message</returns>
        private string CreateDialogMessage(int tasksCounter, int modelsCounter)
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format(DetailsResources.DownloadingProgressContent, tasksCounter, notExecutedServices.Count));
            if (modelsCounter > 4)
            {
                builder.Append(string.Format(DetailsResources.DownloadingProgressContentPostfix, modelsCounter));
            }
            return builder.ToString();
        }
            
    }
}
