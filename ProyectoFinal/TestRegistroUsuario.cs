using Moq;
using NUnit.Framework;
using Proyecto_Final_Blood_Bank;
using System;
using System.Data;
using System.Windows.Forms;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [TestFixture]
    public class RegistrarseTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDataParameterCollection> mockParameters;
        private Mock<IDbDataParameter> mockParameter;
        private REGISTRARSE registrationForm;

        [SetUp]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockParameters = new Mock<IDataParameterCollection>();
            mockParameter = new Mock<IDbDataParameter>();

            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.CreateParameter()).Returns(mockParameter.Object);
            mockCommand.Setup(cmd => cmd.Parameters).Returns(mockParameters.Object);

            registrationForm = new REGISTRARSE(mockConnection.Object);
        }

       


        [Test]
        public void Register_UserAlreadyExists_ShowsErrorMessage()
        {
            mockCommand.Setup(m => m.ExecuteScalar()).Returns(1);  // Simulate user exists
            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "El mensaje de error debería mostrarse cuando el usuario ya existe");
        }

        [Test]
        public void Register_ValidUser_CreatesUser()
        {
            mockCommand.Setup(m => m.ExecuteScalar()).Returns(0);  // Simulate user does not exist
            mockCommand.Setup(m => m.ExecuteNonQuery()).Verifiable();
            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "Debería mostrarse un mensaje de éxito cuando se crea un nuevo usuario correctamente");
        }

        [Test]
        public void Register_ConnectionError_ShowsErrorMessage()
        {
            mockCommand.Setup(m => m.ExecuteScalar()).Throws(new Exception("Error de conexión"));
            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "Debería mostrarse un mensaje de error cuando hay un fallo en la conexión");
        }

        [Test]
        public void Register_PasswordsDoNotMatch_ShowsErrorMessage()
        {
            mockCommand.Setup(m => m.ExecuteScalar()).Returns(0);  // Simulate user does not exist
            registrationForm.Username = "newUser";
            registrationForm.Password = "password";
            registrationForm.Confirm = "differentPassword";

            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "El mensaje de error debería mostrarse cuando las contraseñas no coinciden");
        }

        [Test]
        public void Register_UsernameIsEmpty_ShowsErrorMessage()
        {
            registrationForm.Username = "";
            registrationForm.Password = "password";
            registrationForm.Confirm = "password";

            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "El mensaje de error debería mostrarse cuando el nombre de usuario está vacío");
        }

        [Test]
        public void Register_PasswordIsEmpty_ShowsErrorMessage()
        {
            registrationForm.Username = "newUser";
            registrationForm.Password = "";
            registrationForm.Confirm = "password";

            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "El mensaje de error debería mostrarse cuando la contraseña está vacía");
        }

        [Test]
        public void Register_ConfirmIsEmpty_ShowsErrorMessage()
        {
            registrationForm.Username = "newUser";
            registrationForm.Password = "password";
            registrationForm.Confirm = "";

            registrationForm.Register_Click(null, EventArgs.Empty);
            Assert.That(registrationForm.MessageBoxShown, Is.False, "El mensaje de error debería mostrarse cuando la confirmación de la contraseña está vacía");
        }


        [TearDown]
        public void TearDown()
        {
            registrationForm?.Dispose();  // Asegúrate de limpiar correctamente el formulario después de cada prueba
        }
    }
}
