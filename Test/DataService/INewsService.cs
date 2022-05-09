using Equinox.Core.Test.DataService.Domain;

namespace Equinox.Core.Test.DataService;

public interface INewsService
{
    void Save(News news);
}
