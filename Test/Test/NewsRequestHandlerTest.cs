using System;
using Equinox.Core.Test.Handler;
using Equinox.Core.Test.Model;
using Shouldly;
using Xunit;

using Equinox.Core.Test.DataService;
using Moq;
using Equinox.Core.Test.DataService.Domain;
using System.Collections.Generic;
using Equinox.Core.Test.Domain;
using System.Linq;

namespace Equinox.Core.Test.Test;

public class NewsRequestHandlerTest
{
    private readonly NewsRequest _newsRequest;
    private readonly NewsRequestHandler _handler;
    private readonly Mock<INewsService> _newsServiceMock;
    private List<New> _availableNews;

    public NewsRequestHandlerTest()
    {
        //Arrange
        _newsRequest = new NewsRequest
        {
            Subject = "Cryptocurrency",
            FromDate = DateTime.Now.Date,
            SortBy = "popularity"
        };

        _availableNews = new List<New>() { new New(){Id = 1} };
        _newsServiceMock = new Mock<INewsService>();
        _newsServiceMock.Setup(x => x.GetAvailableNew(_newsRequest.FromDate)).Returns(_availableNews);
        _handler = new NewsRequestHandler(_newsServiceMock.Object);
    }

    [Fact]
    public void Should_Return_News_Response_List_With_Request_Value()
    {
        //Act
        NewsServiceResult Result = _handler.GetNews(_newsRequest);

        //Assert
        Result.ShouldNotBeNull();
        Result.Subject.ShouldBe(_newsRequest.Subject);
        Result.FromDate.ShouldBe(_newsRequest.FromDate);
        Result.SortBy.ShouldBe(_newsRequest.SortBy);
    }

    [Fact]
    public void Should_throw_Exception_For_Null_Request()
    {
        var exception = Should.Throw<ArgumentException>(() => _handler.GetNews(null!));

        exception.ParamName.ShouldBe("newsRequest");
    }

    [Fact]
    public void Should_Save_News_Request()
    {
        News? news = null;
        _newsServiceMock.Setup(x => x.Save(It.IsAny<News>()))
        .Callback<News>(y => news = y);
        _handler.GetNews(_newsRequest);

        _newsServiceMock.Verify(x => x.Save(It.IsAny<News>()), Times.Once);

        news.ShouldNotBeNull();
        news.Subject.ShouldBe(_newsRequest.Subject);
        news.FromDate.ShouldBe(_newsRequest.FromDate);
        news.SortBy.ShouldBe(_newsRequest.SortBy);
        news.NewsID.ShouldBe(_availableNews[0].Id);
    }

    [Fact]
    public void Should_NotSave_News_If_Empty()
    {
        _availableNews.Clear();
        _handler.GetNews(_newsRequest);
        _newsServiceMock.Verify(x => x.Save(It.IsAny<News>()), Times.Never);
    }
}