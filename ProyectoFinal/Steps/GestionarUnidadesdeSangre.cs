using TechTalk.SpecFlow;
using System;
using System.Windows.Forms;

namespace Proyecto_Final_Blood_Bank.Steps
{
    [Binding]
    public sealed class GestionarUnidadesdeSangre
    {
        private REGISTRO_PACIENTE _registroPaciente;

        [BeforeScenario("@tag11")]
        public void BeforeScenarioWithTag()
        {
            _registroPaciente = new REGISTRO_PACIENTE();
            _registroPaciente.Show(); // Show the form before scenario starts
        }

        [Given(@"the user is on the patient registration screen")]
        public void GivenTheUserIsOnThePatientRegistrationScreen()
        {
            // No action needed here since the form is already shown
        }

        [When(@"they enter patient details (.+), (.+), (.+), (.+), (.+), (.+), (.+)")]
        public void WhenTheyEnterPatientDetails(string dni, string nombre, string apellido, string direccion, string telefono, string tipoSangre, string rh)
        {
            _registroPaciente.TxtDni.Text = dni;
            _registroPaciente.TxtNombre.Text = nombre;
            _registroPaciente.TxtApellido.Text = apellido;
            _registroPaciente.TxtDireccion.Text = direccion;
            _registroPaciente.TxtTelefono.Text = telefono;
            _registroPaciente.CmbTipoDeSangre.SelectedIndex = _registroPaciente.CmbTipoDeSangre.FindStringExact(tipoSangre);
            _registroPaciente.CmbRH.SelectedIndex = _registroPaciente.CmbRH.FindStringExact(rh);
        }

        [When(@"they click on the registrar button")]
        public void WhenTheyClickOnTheRegistrarButton()
        {
            _registroPaciente.btnregistrar_Click(null, EventArgs.Empty);
        }

        [Then(@"the patient should be successfully registered")]
        public void ThenThePatientShouldBeSuccessfullyRegistered()
        {
            // Implement verification steps if needed
            // For example: assert that the patient is added to the database or shown in the grid
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _registroPaciente.Close(); // Close the form after scenario ends
            _registroPaciente.Dispose();
        }
    }
}
