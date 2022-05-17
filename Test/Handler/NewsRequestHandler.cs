using System;
using Equinox.Core.Test.DataService;
using Equinox.Core.Test.DataService.Domain;
using Equinox.Core.Test.Model;
using System.Collections.Generic;
using Equinox.Core.Test.Domain;
using System.Linq;

namespace Equinox.Core.Test.Handler
{
    public class NewsRequestHandler
    {
        private readonly INewsService _newsService;

        public NewsRequestHandler(INewsService newsService)
        {
            this._newsService = newsService;
        }

        public NewsServiceResult GetNews(NewsRequest newsRequest)
        {
            if (newsRequest is null)
            {
                throw new ArgumentNullException(nameof(newsRequest));
            }

            IEnumerable<New> availableNew = _newsService.GetAvailableNew(newsRequest.FromDate);
            if (availableNew.Any())
            {
                var _new = availableNew.First();
                var _newsRes = CreateNewsObject<News>(newsRequest);
                _newsRes.NewsID = _new.Id;
                _newsService.Save(_newsRes);
            }

            return CreateNewsObject<NewsServiceResult>(newsRequest);
        }

        private static TNewsService CreateNewsObject<TNewsService>(NewsRequest newsRequest)
        where TNewsService : NewsServiceBase, new()
        {
            return new TNewsService
            {
                Subject = newsRequest.Subject,
                FromDate = newsRequest.FromDate,
                SortBy = newsRequest.SortBy
            };
        }
    }
}