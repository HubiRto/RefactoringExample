using System;
using JetBrains.Annotations;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_ReturnsTrue_WhenValidDataProvided()
    {
        var fakeClientRepository = new ClientRepository();
        var fakeUserCreditService = new UserCreditService();
        var userService = new UserService(fakeClientRepository, fakeUserCreditService);
        
        var result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 1);
        Assert.True(result);
    }

    [Fact]
    public void AddUser_ReturnsFalse_WhenInvalidDataProvided()
    {
        var fakeClientRepository = new ClientRepository();
        var fakeUserCreditService = new UserCreditService();
        var userService = new UserService(fakeClientRepository, fakeUserCreditService);
        
        var result = userService.AddUser("", "", "invalid-email", new DateTime(2010, 1, 1), 1);
        Assert.False(result);
    }
}