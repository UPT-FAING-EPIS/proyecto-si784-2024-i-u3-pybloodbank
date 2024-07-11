using System;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Proyecto_Final_Blood_Bank.Steps
{
    [Binding]
    public class LoginSteps
    {
        private LOGIN _loginForm;
        private string _errorMessage;

        [BeforeScenario("@tag4")]
        public void BeforeScenarioWithTag()
        {
            _loginForm = new LOGIN();
            _loginForm.Show(); // Mostrar el formulario para cada escenario
        }

        [AfterScenario("@tag4")]
        public void AfterScenarioWithTag()
        {
            _loginForm.Close(); // Cerrar el formulario después de cada escenario
            _loginForm.Dispose();
        }

        [Given("I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            // Ya se inicializa el formulario en BeforeScenarioWithTag
        }

        [Given("I enter my username (.*)")]
        public void GivenIEnterMyUsername(string username)
        {
            _loginForm.Username = username;
        }

        [Given("I enter my password (.*)")]
        public void GivenIEnterMyPassword(string password)
        {
            _loginForm.Password = password;
        }

        [Given("I enter an invalid username (.*)")]
        public void GivenIEnterAnInvalidUsername(string username)
        {
            _loginForm.Username = username;
        }

        [Given("I enter an invalid password (.*)")]
        public void GivenIEnterAnInvalidPassword(string password)
        {
            _loginForm.Password = password;
        }

        [Given("I leave the username field empty")]
        public void GivenILeaveTheUsernameFieldEmpty()
        {
            _loginForm.Username = "";
        }

        [Given("I leave the password field empty")]
        public void GivenILeaveThePasswordFieldEmpty()
        {
            _loginForm.Password = "";
        }

        [When("I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginForm.btnLogin_Click(null, EventArgs.Empty); // Simular clic en el botón de login
        }

        [Then("I should be redirected to the options page")]
        public void ThenIShouldBeRedirectedToOptionsPage()
        {
            // Verificar que se redirija a la página de opciones esperada
            Assert.That(_loginForm.Visible, Is.True); // Assert.That con condición Is.True
        }

        [Then("I should see an error message about invalid credentials")]
        public void ThenIShouldSeeAnErrorMessageAboutInvalidCredentials()
        {
            // Verificar mensaje de error por credenciales inválidas
            _errorMessage = "Error de inicio de sesión";
            Assert.That(ErrorMessageIsDisplayed(_errorMessage), Is.True); // Assert.That con condición Is.True
        }

        [Then("I should see an error message about empty fields")]
        public void ThenIShouldSeeAnErrorMessageAboutEmptyFields()
        {
            // Verificar mensaje de error por campos vacíos
            _errorMessage = "Error de inicio de sesión";
            Assert.That(ErrorMessageIsDisplayed(_errorMessage), Is.True); // Assert.That con condición Is.True
        }

        private bool ErrorMessageIsDisplayed(string errorMessage)
        {
            Form errorForm = Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Text == "Error");
            if (errorForm != null)
            {
                Label errorLabel = errorForm.Controls.OfType<Label>().FirstOrDefault(l => l.Text == errorMessage);
                return errorLabel != null;
            }
            return false;
        }
    }
}
