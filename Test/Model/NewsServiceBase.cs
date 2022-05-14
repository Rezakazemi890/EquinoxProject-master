using System;

namespace Equinox.Core.Test.Model
{
    public class NewsServiceBase
    {
        public string Subject { get; internal set; }
        public DateTime FromDate { get; internal set; }
        public string SortBy { get; internal set; }
    }
}