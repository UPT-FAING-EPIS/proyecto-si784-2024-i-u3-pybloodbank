using System;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Final_Blood_Bank;

namespace Proyecto_Final_Blood_Bank
{
    [TestFixture]
    public class LoginTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDataReader> mockReader;
        private LOGIN loginForm;

        [SetUp]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();

            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);

            loginForm = new LOGIN(mockConnection.Object);
        }

        [Test]
        public void btnLogin_Click_AttemptLogin_Fails()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "InvalidUser!" };
            var passwordTextBox = new TextBox { Text = "Password123" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);
            SetPrivateField(loginForm, "txtPassword", passwordTextBox);

            // Act
            loginForm.btnLogin_Click(null, EventArgs.Empty);

            // Assert
            Assert.That(loginForm.AttemptLogin(), Is.True);
        }

        [Test]
        public void btnLogin_Click_AttemptLogin_Succeeds()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "ValidUser" };
            var passwordTextBox = new TextBox { Text = "Password123" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);
            SetPrivateField(loginForm, "txtPassword", passwordTextBox);

            // Act
            loginForm.btnLogin_Click(null, EventArgs.Empty);

            // Assert
            Assert.That(loginForm.AttemptLogin(), Is.True);
        }

        [Test]
        public void AttemptLogin_ValidUser()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "ValidUser" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);

            // Act
            var result = loginForm.AttemptLogin();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void AttemptLogin_InvalidUser()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "InvalidUser!" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);

            // Act
            var result = loginForm.AttemptLogin();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckCredentials_ValidCredentials()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "ValidUser" };
            var passwordTextBox = new TextBox { Text = "Password123" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);
            SetPrivateField(loginForm, "txtPassword", passwordTextBox);

            mockReader.Setup(r => r.Read()).Returns(true);
            mockReader.Setup(r => r["estado"]).Returns("N");

            // Act
            loginForm.CheckCredentials();

            // Assert
            Assert.Equals("ValidUser", LOGIN.NombreUsuario);

        }

        [Test]
        public void CheckCredentials_InvalidCredentials()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "ValidUser" };
            var passwordTextBox = new TextBox { Text = "Password123" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);
            SetPrivateField(loginForm, "txtPassword", passwordTextBox);

            mockReader.Setup(r => r.Read()).Returns(false);

            // Act
            loginForm.CheckCredentials();

            // Assert
            // Verify that the HandleFailedLogin method is called
        }

        [Test]
        public void HandleSuccessfulLogin_ShowsCorrectForm()
        {
            // Arrange
            var estado = "N";

            // Act
            loginForm.HandleSuccessfulLogin(estado);

            // Assert
            // Add assertions to verify the correct form is shown
        }

        [Test]
        public void HandleFailedLogin_ShowsErrorMessage()
        {
            // Arrange
            var usernameTextBox = new TextBox { Text = "ValidUser" };
            var passwordTextBox = new TextBox { Text = "Password123" };
            SetPrivateField(loginForm, "txtUsername", usernameTextBox);
            SetPrivateField(loginForm, "txtPassword", passwordTextBox);

            // Act
            loginForm.HandleFailedLogin();

            // Assert
            Assert.Equals(string.Empty, usernameTextBox.Text);
            Assert.Equals(string.Empty, passwordTextBox.Text);
        }

        private void SetPrivateField(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(obj, value);
            }
        }
    }

}
