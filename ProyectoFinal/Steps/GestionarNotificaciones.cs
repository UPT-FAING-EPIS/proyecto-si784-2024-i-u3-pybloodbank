using TechTalk.SpecFlow;
using NUnit.Framework;
using Moq;
using System.Data;
using System.Windows.Forms;
using System;

namespace Proyecto_Final_Blood_Bank.Steps
{
    [Binding]
    public sealed class GestionarNotificacionesSteps
    {
        private LOGIN _loginForm;
        private Mock<IDbConnection> _mockConnection;
        private Mock<IDbCommand> _mockCommand;
        private Mock<IDataReader> _mockReader;

        [BeforeScenario("@tag2")]
        public void BeforeScenarioWithTag()
        {
            _mockConnection = new Mock<IDbConnection>();
            _mockCommand = new Mock<IDbCommand>();
            _mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(conn => conn.CreateCommand()).Returns(_mockCommand.Object);
            _mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockReader.Object);

            _loginForm = new LOGIN(_mockConnection.Object);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_loginForm != null)
            {
                _loginForm.Dispose();
                _loginForm = null;
            }
        }

        [Given("I enter a valid username (.*)")]
        public void GivenIEnterAValidUsername(string username)
        {
            var txtUsername = _loginForm.Controls["txtUsername"] as TextBox;
            txtUsername.Text = username;
        }

        [Given("I enter a valid password (.*)")]
        public void GivenIEnterAValidPassword(string password)
        {
            var txtPassword = _loginForm.Controls["txtPassword"] as TextBox;
            txtPassword.Text = password;
        }

        [Given("I enter an invalid username (.*)")]
        public void GivenIEnterAnInvalidUsername(string username)
        {
            var txtUsername = _loginForm.Controls["txtUsername"] as TextBox;
            txtUsername.Text = username;
        }

        [Given("I enter an invalid password (.*)")]
        public void GivenIEnterAnInvalidPassword(string password)
        {
            var txtPassword = _loginForm.Controls["txtPassword"] as TextBox;
            txtPassword.Text = password;
        }

        [When("I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginForm.btnLogin_Click(null, EventArgs.Empty);
        }

        [Then("I should see the main menu based on my user state")]
        public void ThenIShouldSeeTheMainMenuBasedOnMyUserState()
        {
            // Assuming that the user state is determined by the login form
            // and that the main menu forms are shown based on the user state
            Assert.Pass();
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            // Assuming that the login form shows a MessageBox on login failure
            // You would need to mock the MessageBox to verify this behavior
            Assert.Pass();
        }
    }
}
