using NUnit.Framework;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [TestFixture]
    internal class TestBancodeSangre
    {
        private BANCO_DE_SANGRE form;

        [SetUp]
        public void SetUp()
        {
            // Configurar el formulario y el contexto de la prueba
            form = new BANCO_DE_SANGRE();
            form.Show(); // Mostrar el formulario para pruebas
        }

        [Test]
        public void TestBANCO_DE_SANGRE_Load()
        {
            // Llamar al método BANCO_DE_SANGRE_Load
            form.BANCO_DE_SANGRE_Load(null, EventArgs.Empty);
        }

        [Test]
        public void TestCargarGrilla()
        {
            // Llamar al método que carga la grilla
            form.CargarGrilla();

            // Verificar que el DataGridView se haya llenado con datos
            DataTable dt = form.dgvBancoSangre.DataSource as DataTable;
            Assert.That(dt, Is.Not.Null, "El DataGridView no se ha llenado correctamente.");
            Assert.That(dt.Rows.Count, Is.GreaterThan(0), "El DataGridView no contiene datos.");
        }

        [Test]
        public void TestCargarGrilla2()
        {
            // Llamar al método que carga la segunda grilla
            form.CargarGrilla2();

            // Verificar que el DataGridView se haya llenado con datos
            DataTable dt = form.dgvPacientes.DataSource as DataTable;
            Assert.That(dt, Is.Not.Null, "El DataGridView de pacientes no se ha llenado correctamente.");
            Assert.That(dt.Rows.Count, Is.GreaterThan(0), "El DataGridView de pacientes no contiene datos.");
        }

        [Test]
        public void TestButton1Click()
        {
            // Simular el texto de búsqueda
            form.txtBuscar.Text = "test";

            // Ejecutar el evento click del botón Buscar
            form.button1_Click(null, EventArgs.Empty);
        }

        [Test]
        public void TestLabel3Click()
        {
            // Simular la acción de clic en el label3 y verificar el resultado
            form.lblUsuario.Text = "existingUser"; // Simula un usuario válido
            form.label3_Click(null, EventArgs.Empty);

            // Aquí puedes agregar verificaciones para asegurar que se abrió el formulario correcto
            // Dependiendo de la implementación, podrías necesitar verificar el estado del formulario
        }

        [Test]
        public void TestOnMouseEnter()
        {
            // Simular el evento MouseEnter
            form.OnMouseEnter(null, EventArgs.Empty);

            // Verificar que la fuente de lblVolver haya cambiado
            Assert.That(form.lblVolver.Font, Is.EqualTo(new Font("Nirmala UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0)));
        }

        [Test]
        public void TestOnMouseLeave()
        {
            // Simular el evento MouseLeave
            form.OnMouseLeave(null, EventArgs.Empty);

            // Verificar que la fuente de lblVolver haya cambiado
            Assert.That(form.lblVolver.Font, Is.EqualTo(new Font("Nirmala UI", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0)));
        }

        [Test]
        public void TestResize()
        {
            // Simular un cambio de tamaño en el formulario
            form.Size = new Size(1024, 768);
            form.BANCO_DE_SANGRE_Resize(null, EventArgs.Empty);
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
