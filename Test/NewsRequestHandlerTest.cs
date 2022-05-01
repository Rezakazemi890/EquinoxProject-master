using System;
using Shouldly;
using Xunit;

namespace Equinox.Core.Test;

public class NewsRequestHandlerTest
{
    [Fact]
    public void Should_Return_News_Response_List_With_Request_Value()
    {
        //Arrange
        var NewsRequest = new NewsRequest
        {
            Subject = "Cryptocurrency",
            FromDate = DateTime.Now.Date,
            SortBy = "popularity"
        };

        var Handler = new NewsRequestHandler();
        //Act
        NewsServiceResult Result = Handler.GetNews(NewsRequest);

        //Assert
        Result.ShouldNotBeNull();
        Result.Subject.ShouldBe(NewsRequest.Subject);
        Result.FromDate.ShouldBe(NewsRequest.FromDate);
        Result.SortBy.ShouldBe(NewsRequest.SortBy);
        
    }
}
