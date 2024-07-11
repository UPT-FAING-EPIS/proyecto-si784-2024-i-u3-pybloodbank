using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using Proyecto_Final_Blood_Bank;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace ProyectoFinalBloodBankTests
{
    [TestFixture]
    internal class RegistroPacienteTests
    {
        private const string ConnectionString = "Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True";
        private const string V = "123456789";
        private REGISTRO_PACIENTE form;

        [SetUp]
        public void SetUp()
        {
            // Configurar el formulario y el contexto de la prueba
            form = new REGISTRO_PACIENTE();

            // Acceder al campo lblVolver usando reflexión
            var fieldInfo = typeof(REGISTRO_PACIENTE).GetField("lblVolver", BindingFlags.NonPublic | BindingFlags.Instance);
            var lblVolver = (Label)fieldInfo.GetValue(form);
            lblVolver.Font = new Font("Nirmala UI", 12F, FontStyle.Bold | FontStyle.Underline);

            form.Show(); // Mostrar el formulario para pruebas
        }

        [Test]
        public void TestBtnRegistrarClick()
        {
            // Simulación de datos de entrada utilizando las propiedades públicas
            form.TxtDni.Text = "12345678";
            form.TxtNombre.Text = "Juan";
            form.TxtApellido.Text = "Perez";
            form.TxtDireccion.Text = "Calle Falsa 123";
            form.TxtTelefono.Text = V;
            form.CmbTipoDeSangre.SelectedIndex = 0; // Ejemplo: "A"
            form.CmbRH.SelectedIndex = 0; // Ejemplo: "+"

            try
            {
                // Ejecutar el evento click del botón Registrar
                form.btnregistrar_Click(null, EventArgs.Empty);

                // Verificar si se ha insertado correctamente en la base de datos
                string selectQuery = "SELECT COUNT(*) FROM Pacientes WHERE dni = @dni";
                int count = 0;

                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (var cmdSelect = new SqlCommand(selectQuery, con))
                    {
                        cmdSelect.Parameters.AddWithValue("@dni", form.TxtDni.Text.Trim());
                        count = (int)cmdSelect.ExecuteScalar();
                    }
                }
            }
            finally
            {
                // Cerrar el formulario al finalizar las pruebas
                form.Close();
            }
        }

        [Test]
        public void TestBtnEliminarClick()
        {
            // Simulación de datos de entrada utilizando las propiedades públicas
            form.TxtDni.Text = "12345678"; // Suponemos que este DNI existe en la base de datos para poder eliminarlo

            try
            {
                // Ejecutar el evento click del botón Eliminar
                form.btnEliminar_Click(null, EventArgs.Empty);

                // Verificar si se ha eliminado correctamente en la base de datos
                string selectQuery = "SELECT COUNT(*) FROM Pacientes WHERE dni = @dni";
                int count = 0;

                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (var cmdSelect = new SqlCommand(selectQuery, con))
                    {
                        cmdSelect.Parameters.AddWithValue("@dni", form.TxtDni.Text.Trim());
                        count = (int)cmdSelect.ExecuteScalar();
                    }
                }

                // Verificar que no existan registros con el DNI especificado
                Assert.That(count, Is.EqualTo(0));
            }
            finally
            {
                // Cerrar el formulario al finalizar las pruebas
                form.Close();
            }
        }

        [Test]
        public void TestBtnModificarClick()
        {
            // Simulación de datos de entrada utilizando las propiedades públicas
            form.TxtDni.Text = "12345678"; // Suponemos que este DNI existe en la base de datos para poder modificarlo
            form.TxtNombre.Text = "NuevoNombre";
            form.TxtApellido.Text = "NuevoApellido";
            form.TxtDireccion.Text = "NuevaDireccion";
            form.TxtTelefono.Text = "987654321";
            form.CmbTipoDeSangre.SelectedIndex = 1; // Ejemplo: "B"
            form.CmbRH.SelectedIndex = 1; // Ejemplo: "-"

            try
            {
                // Obtener el estado actual del registro antes de modificarlo
                string selectQueryBefore = "SELECT COUNT(*) FROM Pacientes WHERE dni = @dni";
                int countBefore = 0;

                using (var conBefore = new SqlConnection(ConnectionString))
                {
                    conBefore.Open();
                    using (var cmdSelectBefore = new SqlCommand(selectQueryBefore, conBefore))
                    {
                        cmdSelectBefore.Parameters.AddWithValue("@dni", form.TxtDni.Text.Trim());

                        countBefore = (int)cmdSelectBefore.ExecuteScalar();
                    }
                }

                // Ejecutar el evento click del botón Modificar
                form.btnModificar_Click(null, EventArgs.Empty);

                // Obtener el estado después de modificar el registro
                int countAfter = 0;

                using (var conAfter = new SqlConnection(ConnectionString))
                {
                    conAfter.Open();
                    using (var cmdSelectAfter = new SqlCommand(selectQueryBefore, conAfter))
                    {
                        cmdSelectAfter.Parameters.AddWithValue("@dni", form.TxtDni.Text.Trim());

                        countAfter = (int)cmdSelectAfter.ExecuteScalar();
                    }
                }
            }
            finally
            {
                // Cerrar el formulario al finalizar las pruebas
                form.Close();
            }
        }


        [Test]
        public void TestBtnLimpiarClick()
        {
            // Ejecutar el evento click del botón Limpiar
            form.btnlimpiar_Click(null, EventArgs.Empty);

            // Verificar que todos los campos estén limpios después de hacer clic en Limpiar
            Assert.That(form.TxtDni.Text, Is.EqualTo(""));
            Assert.That(form.TxtNombre.Text, Is.EqualTo(""));
            Assert.That(form.TxtApellido.Text, Is.EqualTo(""));
            Assert.That(form.TxtDireccion.Text, Is.EqualTo(""));
            Assert.That(form.TxtTelefono.Text, Is.EqualTo(""));
            Assert.That(form.CmbTipoDeSangre.SelectedIndex, Is.EqualTo(-1));
            Assert.That(form.CmbRH.SelectedIndex, Is.EqualTo(-1));
        }

        [Test]
        public void TestHoraTick()
        {
            // Simular el evento Tick del temporizador de hora
            form.Hora_Tick(null, EventArgs.Empty);

            // Verificar que el campo txtFechayHora contenga una fecha y hora válida
            DateTime resultDateTime;
            Assert.That(DateTime.TryParseExact(form.TxtFechayHora.Text, "dd/MM/yy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out resultDateTime), Is.True);
        }


        [Test]
        public void TestLabel3Click()
        {
            // Simular el evento Click del label lblUsuario
            form.label3_Click(null, EventArgs.Empty);

            // Aquí puedes agregar aserciones para verificar el comportamiento esperado al hacer clic en lblUsuario
            // Dependiendo de la lógica dentro del método label3_Click
        }

        [Test]
        public void TestOnMouseEnter()
        {
            // Simulate the MouseEnter event of lblVolver
            form.OnMouseEnter(null, EventArgs.Empty);

            // Verify that the font name, size, and style match the expected values
            var fieldInfo = typeof(REGISTRO_PACIENTE).GetField("lblVolver", BindingFlags.NonPublic | BindingFlags.Instance);
            var lblVolver = (Label)fieldInfo.GetValue(form);

            Assert.That(lblVolver.Font.Name, Is.EqualTo("Nirmala UI"));
            Assert.That(lblVolver.Font.Size, Is.EqualTo(12F));
            Assert.That(lblVolver.Font.Style, Is.EqualTo(FontStyle.Bold | FontStyle.Underline));
        }


        [Test]
        public void TestOnMouseLeave()
        {
            // Simulate the MouseLeave event of lblVolver
            form.OnMouseLeave(null, EventArgs.Empty);

            // Verify that the font name, size, and style match the expected values
            var fieldInfo = typeof(REGISTRO_PACIENTE).GetField("lblVolver", BindingFlags.NonPublic | BindingFlags.Instance);
            var lblVolver = (Label)fieldInfo.GetValue(form);

            Assert.That(lblVolver.Font.Name, Is.EqualTo("Nirmala UI"));
            Assert.That(lblVolver.Font.Size, Is.EqualTo(9.75F));
            Assert.That(lblVolver.Font.Style, Is.EqualTo(FontStyle.Bold | FontStyle.Underline));
        }



        [Test]
        public void TestDgvregistrosCellContentClick()
        {
            // Simular el evento CellContentClick de dgvPacientes
            form.dgvregistros_CellContentClick(null, new DataGridViewCellEventArgs(0, 0));

            // Aquí puedes agregar aserciones para verificar que los datos seleccionados se carguen correctamente en los controles del formulario
            // Dependiendo de la lógica dentro del método dgvregistros_CellContentClick
        }



        [TearDown]
        public void TearDown()
        {
            // Cerrar el formulario al finalizar las pruebas si no se ha cerrado
            if (form != null && !form.IsDisposed)
            {
                form.Close();
            }
        }
    }
}
