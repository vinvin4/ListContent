// -----------------------------------------------------------------------
//  <copyright file="IContentService.cs" />
// -----------------------------------------------------------------------
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IContentService
    {
        /// <summary>
        /// Get Content API for all services
        /// </summary>
        /// <returns>Content list</returns>
        Task<List<AdapterModel>> GetContentList();
    }
}
