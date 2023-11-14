using NSubstitute;
using NUnit.Framework;
using Telemetry;
using Utilities;

namespace Login.EditModeTests
{
    public class LoginControllerTests
    {
        private ITelemetrySender _telemetrySender;

        [SetUp]
        public void SetUp()
        {
            _telemetrySender = Substitute.For<ITelemetrySender>();
            ServiceLocator.Instance.RegisterService<ITelemetrySender>(_telemetrySender);
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Instance.Clear();
        }

        [Test]
        public void Send_LoggedIn_SendsUserID()
        {
            // Arrange
            var humbleLoginController = new LogInAnonymousController();
        
            // Action
            humbleLoginController.LogIn();
        
            // Assert
            _telemetrySender.Received(1).Send(Arg.Is<string>(s => !string.IsNullOrEmpty(s)));
        }
    }
}
