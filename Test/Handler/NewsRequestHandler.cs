using System;
using Equinox.Core.Test.DataService;
using Equinox.Core.Test.DataService.Domain;
using Equinox.Core.Test.Model;

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

            _newsService.Save(CreateNewsObject<News>(newsRequest));

            return CreateNewsObject<NewsServiceResult>(newsRequest);
        }

        private static NewsServiceResult CreateNewsObject<NewsServiceResult>(NewsRequest newsRequest)
        where NewsServiceResult : NewsServiceBase, new()
        {
            return new NewsServiceResult
            {
                Subject = newsRequest.Subject,
                FromDate = newsRequest.FromDate,
                SortBy = newsRequest.SortBy
            };
        }
    }
}