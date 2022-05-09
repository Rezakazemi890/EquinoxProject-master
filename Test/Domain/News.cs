using System;

namespace Equinox.Core.Test.DataService.Domain
{
    public class News
    {
        public string Subject { get; internal set; }
        public DateTime FromDate { get; internal set; }
        public string SortBy { get; internal set; }
    }
}