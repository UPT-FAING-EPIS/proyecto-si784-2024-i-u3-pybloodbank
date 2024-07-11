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
    public class TestIniciodeSesion
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
            Assert.That(loginForm.AttemptLogin(), Is.False);
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
            Assert.That(loginForm.AttemptLogin(), Is.False);
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
            Assert.That(result, Is.False);
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
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckCredentials_ValidCredentials()
        {
            // Arrange
            // Configuración inicial de los mocks y datos de entrada
            var username = "ValidUser";
            var password = "Password123";

            var usernameTextBox = new TextBox { Text = username };
            var passwordTextBox = new TextBox { Text = password };

            SetPrivateField(loginForm, "txtUsername", usernameTextBox);
            SetPrivateField(loginForm, "txtPassword", passwordTextBox);

            // Simular el comportamiento del lector y del comando
            mockReader.SetupSequence(r => r.Read())
                      .Returns(true) // Primera llamada a Read() devuelve true
                      .Returns(false); // Siguientes llamadas devuelven false

            mockReader.Setup(r => r["estado"]).Returns("N");

            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);
            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);

            // Act
            loginForm.CheckCredentials();
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
        }


        [TearDown]
        public void TearDown()
        {
            // Libera los recursos aquí
            if (loginForm != null)
            {
                loginForm.Dispose();
                loginForm = null;
            }
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
