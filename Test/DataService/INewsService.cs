using System;
using System.Collections.Generic;
using Equinox.Core.Test.DataService.Domain;
using Equinox.Core.Test.Domain;

namespace Equinox.Core.Test.DataService;

public interface INewsService
{
    void Save(News news);
    IEnumerable<New> GetAvailableNew(DateTime date);
}
