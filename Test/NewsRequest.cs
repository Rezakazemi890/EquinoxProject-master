using System;

namespace Equinox.Core.Test;

public class NewsRequest
{
    public string Subject { get; internal set; }
    public DateTime FromDate { get; internal set; }
    public string SortBy { get; internal set; }
}
