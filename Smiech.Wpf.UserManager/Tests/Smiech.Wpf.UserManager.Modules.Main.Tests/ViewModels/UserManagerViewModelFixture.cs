using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Smiech.Wpf.UserManager.Modules.Main.ViewModels;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;
using Xunit;

namespace Smiech.Wpf.UserManager.Modules.Main.Tests.ViewModels
{
    public class UserManagerViewModelFixture
    {
        private readonly IFixture _fixture;
        private readonly Mock<IGoRestApiService> _goRestApiMock;
        private const int DefaultPageNumber = 1;

        public UserManagerViewModelFixture()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            _goRestApiMock = _fixture.Freeze<Mock<IGoRestApiService>>();
            _goRestApiMock.Setup(x => x.GetUserDataAsync(It.IsAny<int>())).ReturnsUsingFixture(_fixture);
            _goRestApiMock.Setup(x => x.GetUserDataByQuery(It.IsAny<UserQuery>(), It.IsAny<int>())).ReturnsUsingFixture(_fixture);
        }

        [Fact]
        public void LoadsCorrectDataAtStartup()
        {
            var expectedUserResponse = _fixture.Freeze<UserResponse>();

            var userManagerViewModel = _fixture.Create<UserManagerViewModel>();

            _goRestApiMock.Verify(x => x.GetUserDataAsync(DefaultPageNumber), Times.Once);
            userManagerViewModel.UserViewModels.Should().BeEquivalentTo(expectedUserResponse.Data);
            userManagerViewModel.Pagination.Should().BeEquivalentTo(expectedUserResponse.Meta.Pagination);
        }

        [Fact]
        public void LoadsCorrectDataWhenSearchIsActive()
        {
            var expectedUserResponse = _fixture.Freeze<UserResponse>();
            var expectedParameters = new UserQuery()
            {
                Email = "EMAIL",
            };

            var userManagerViewModel = _fixture.Create<UserManagerViewModel>();
            userManagerViewModel.UserQueryViewModel.IsActive = true;
            userManagerViewModel.UserQueryViewModel.Email = "EMAIL";
            userManagerViewModel.GetUsersByQueryCommand.Execute(null);


            _goRestApiMock.Verify(x =>
                x.GetUserDataByQuery(It.Is<UserQuery>((f) => f.Email == expectedParameters.Email), DefaultPageNumber), Times.Once);
        }
    }
}
