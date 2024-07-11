using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [Binding]
    public class RegistroPacienteSteps
    {
        private REGISTRO_PACIENTE registroPaciente = new REGISTRO_PACIENTE();
        private string pacienteDNI;

        [Given(@"Estoy en la página de registro de pacientes")]
        public void GivenEstoyEnLaPaginaDeRegistroDePacientes()
        {
            // Puedes añadir aquí la lógica necesaria para abrir el formulario de registro de pacientes
            // Ejemplo: registroPaciente.Show();
        }

        [Given(@"tengo un paciente registrado con DNI ""(.*)""")]
        public void GivenTengoUnPacienteRegistradoConDNI(string dni)
        {
            // Lógica para verificar que el paciente con el DNI proporcionado está registrado
            // Puedes implementar una consulta SQL para verificar la existencia del paciente
            this.pacienteDNI = dni;

            // Ejemplo de consulta:
            /*
            string selectQuery = "SELECT COUNT(*) FROM Pacientes WHERE dni = @dni";
            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
            {
                con.Open();
                using (SqlCommand cmdSelect = new SqlCommand(selectQuery, con))
                {
                    cmdSelect.Parameters.AddWithValue("@dni", dni);
                    int count = (int)cmdSelect.ExecuteScalar();
                    Assert.IsTrue(count > 0, $"El paciente con DNI {dni} no está registrado.");
                }
            }
            */
        }

        [When(@"modifico el nombre a ""(.*)""")]
        public void WhenModificoElNombreA(string nombre)
        {
            registroPaciente.TxtNombre.Text = nombre;
        }

        [When(@"modifico el apellido a ""(.*)""")]
        public void WhenModificoElApellidoA(string apellido)
        {
            registroPaciente.TxtApellido.Text = apellido;
        }

        [When(@"modifico la dirección a ""(.*)""")]
        public void WhenModificoLaDireccionA(string direccion)
        {
            registroPaciente.TxtDireccion.Text = direccion;
        }

        [When(@"modifico el teléfono a ""(.*)""")]
        public void WhenModificoElTelefonoA(string telefono)
        {
            registroPaciente.TxtTelefono.Text = telefono;
        }

        [When(@"modifico el tipo de sangre a ""(.*)""")]
        public void WhenModificoElTipoDeSangreA(string tipoSangre)
        {
            registroPaciente.CmbTipoDeSangre.SelectedIndex = registroPaciente.CmbTipoDeSangre.FindStringExact(tipoSangre);
        }

        [When(@"modifico el RH a ""(.*)""")]
        public void WhenModificoElRHA(string rh)
        {
            registroPaciente.CmbRH.SelectedIndex = registroPaciente.CmbRH.FindStringExact(rh);
        }

        [When(@"presiono el botón modificar")]
        public void WhenPresionoElBotonModificar()
        {
            registroPaciente.btnModificar_Click(null, null);
        }

        [Then(@"debería ver un mensaje de confirmación de la modificación")]
        public void ThenDeberiaVerUnMensajeDeConfirmacionDeLaModificacion()
        {
            // Aquí puedes verificar que se muestre un mensaje de éxito después de la modificación
            // Ejemplo: Assert.IsTrue(registroPaciente.MensajeExitoVisible);
        }

        [Then(@"debería ver un mensaje de error sobre campos vacíos")]
        public void ThenDeberiaVerUnMensajeDeErrorSobreCamposVacios()
        {
            // Aquí puedes verificar que se muestre un mensaje de error por campos vacíos
            // Ejemplo: Assert.IsTrue(registroPaciente.MensajeErrorCamposVaciosVisible);
        }

        [Then(@"debería ver un mensaje de error sobre DNI incorrecto")]
        public void ThenDeberiaVerUnMensajeDeErrorSobreDniIncorrecto()
        {
            // Aquí puedes verificar que se muestre un mensaje de error por DNI incorrecto
            // Ejemplo: Assert.IsTrue(registroPaciente.MensajeErrorDniIncorrectoVisible);
        }

        // Añade más pasos según sea necesario para otros escenarios
    }
}
