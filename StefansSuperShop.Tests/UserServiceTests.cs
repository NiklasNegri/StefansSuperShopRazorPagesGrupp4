using Xunit.Abstractions;

namespace StefansSuperShop.Test;

public class UserServiceTests
{
    ITestOutputHelper _testOutputHelper;

    public UserServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    //[Fact(Skip = "Skipping since it is not yet ready")]
    [Fact]
    public void GetById_IdNotInDatabase_ThrowsError()
    {
        _testOutputHelper.WriteLine("This is a message");
        Assert.True(true);
    }
}
