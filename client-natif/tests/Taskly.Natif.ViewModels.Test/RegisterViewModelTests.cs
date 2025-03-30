using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Client.Domain.DTO;
using Taskly.Natif.Application.Services;
using Taskly.Natif.Application.Services.Interface;
using Taskly.Natif.Application.ViewModels;

namespace Taskly.Natif.ViewModels.Test
{
    [TestClass]
    public sealed class RegisterViewModelTests
    {
        private RegisterViewModel viewModel;
        private Mock<IToastService> toastSerivceMock;
        private Mock<INavigationService> navigationServiceMock;
        private Mock<IAuthenticationService> authenticationServiceMock;
        private Mock<ILogger<RegisterViewModel>> loggerMock;

        [TestInitialize]
        public void SetUp()
        {
            // Arrange
            toastSerivceMock = new Mock<IToastService>();
            navigationServiceMock = new Mock<INavigationService>();
            authenticationServiceMock = new Mock<IAuthenticationService>();
            loggerMock = new Mock<ILogger<RegisterViewModel>>();

            viewModel = new(authenticationServiceMock.Object, toastSerivceMock.Object, loggerMock.Object, navigationServiceMock.Object);
        }

        [TestMethod]
        public void When_RegisterIsCalledAndValidatorHasValidData_Then_ServiceIsCalledWithRigthArgs()
        {
            // Arrange
            authenticationServiceMock.Setup(asm => asm.RegisterUser(It.IsAny<RegisterModel>()))
                .ReturnsAsync(true);
            viewModel.EmailValidator.Value = "test@test.com";
            viewModel.UsernameValidator.Value = "username";
            viewModel.PasswordValidator.Value = "Password123$";
            viewModel.LastnameValidator.Value = "Lastname";
            viewModel.FirstnameValidator.Value = "Firstname";
            viewModel.BirthdateValidator.Value = DateTime.Now.AddYears(-15);

            // Act
            viewModel.RegisterCommand.Execute(null);

            // Assert
            authenticationServiceMock.Verify(
                asm => asm.RegisterUser(It.IsAny<RegisterModel>()), 
                Times.Once);
            navigationServiceMock.Verify(
                nsm => nsm.NavigateTo(It.IsAny<string>()),
                Times.Once);
        }

        [TestMethod]
        public void When_RegisterIsCalledAndValidatorHasNoValidData_Then_ServiceIsNotCalled()
        {
            // Arrange
            viewModel.EmailValidator.Value = "";
            viewModel.UsernameValidator.Value = "username";
            viewModel.PasswordValidator.Value = "Password123$";
            viewModel.LastnameValidator.Value = "Lastname";
            viewModel.FirstnameValidator.Value = "Firstname";
            viewModel.BirthdateValidator.Value = DateTime.Now.AddYears(-15);

            // Act
            viewModel.RegisterCommand.Execute(null);

            // Assert
            authenticationServiceMock.Verify(
                asm => asm.RegisterUser(It.IsAny<RegisterModel>()),
                Times.Never);
        }
    }
}
