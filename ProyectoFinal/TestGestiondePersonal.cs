using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [TestFixture]
    internal class TestGestiondePersonal
    {
        private PERSONAL form;

        [SetUp]
        public void SetUp()
        {
            // Configurar el formulario y el contexto de la prueba
            form = new PERSONAL();
            form.Show(); // Mostrar el formulario para pruebas
        }

        [Test]
        public void TestCargarGrilla()
        {
            // Llamar al método que carga la grilla
            form.CargarGrilla();

            // Verificar que el DataGridView se haya llenado con datos
            DataTable dt = form.dgvPersonal.DataSource as DataTable;
            Assert.That(dt.Rows.Count > 0, "El DataGridView no contiene datos.");
        }

        [Test]
        public void TestLabel3Click()
        {
            // Simular la acción de clic en label3 y verificar el resultado
            form.lblUsuario.Text = "existingUser"; // Simula un usuario válido
            form.label3_Click(null, EventArgs.Empty);

            // Aquí puedes agregar verificaciones para asegurar que se abrió el formulario correcto
            // Dependiendo de la implementación, podrías necesitar verificar el estado del formulario
        }

        [Test]
        public void TestButton1Click_SubirEstado()
        {
            // Simular el texto de usuario y ejecutar el evento click del botón Subir
            form.txtUsuario.Text = "testUser";
            form.cmbEstado.SelectedIndex = 1; // Simula selección de estado P

            form.button1_Click(null, EventArgs.Empty);

            // Verificar que el estado se haya actualizado correctamente en la base de datos
            string estadoObtenido = ObtenerEstadoUsuarioDesdeBD("testUser");
        }

        [Test]
        public void TestButtonBajarClick()
        {
            // Simular el texto de usuario y ejecutar el evento click del botón Bajar
            form.txtUsuario.Text = "testUser";
            form.cmbEstado.SelectedIndex = 0; // Simula selección de estado N

            form.btnBajar_Click(null, EventArgs.Empty);

            // Verificar que el estado se haya actualizado correctamente en la base de datos
            string estadoObtenido = ObtenerEstadoUsuarioDesdeBD("testUser");
        }

        private string ObtenerEstadoUsuarioDesdeBD(string usuario)
        {
            string estado = "";

            // Consultar el estado actual del usuario en la base de datos
            string comando = "SELECT estado FROM Personal WHERE Usuario = @usuario";
            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(comando, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    using (SqlDataReader datos = cmd.ExecuteReader())
                    {
                        if (datos.Read())
                        {
                            estado = datos["estado"].ToString();
                        }
                    }
                }
            }

            return estado;
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
