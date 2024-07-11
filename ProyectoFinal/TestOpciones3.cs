using NUnit.Framework;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [TestFixture]
    internal class TestOpciones3
    {
        private OPCIONES form;

        [SetUp]
        public void SetUp()
        {
            // Configurar el formulario y el contexto de la prueba
            form = new OPCIONES();
            form.Show(); // Mostrar el formulario para pruebas
        }

        [Test]
        public void TestOPCIONES_Load()
        {
            // Llamar al método OPCIONES_Load
            form.OPCIONES_Load(null, EventArgs.Empty);
        }

        [Test]
        public void TestOnMouseEnter()
        {
            // Simular el evento MouseEnter
            form.OnMouseEnter(null, EventArgs.Empty);

            // Verificar que la fuente de LogOut haya cambiado
            Assert.That(form.LogOut.Font, Is.EqualTo(new Font("Nirmala UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0)));
        }

        [Test]
        public void TestOnMouseLeave()
        {
            // Simular el evento MouseLeave
            form.OnMouseLeave(null, EventArgs.Empty);

            // Verificar que la fuente de LogOut haya cambiado
            Assert.That(form.LogOut.Font, Is.EqualTo(new Font("Nirmala UI", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0)));
        }

        [Test]
        public void TestResize()
        {
            // Simular un cambio de tamaño en el formulario
            form.Size = new Size(1024, 768);
            form.OPCIONES_Resize(null, EventArgs.Empty);
        }

        [Test]
        public void TestBtnBanco_Click()
        {
            // Simular el evento click del botón Banco
            form.btnBanco_Click(null, EventArgs.Empty);

            // Verificar que se haya ocultado el formulario actual
            Assert.That(form.Visible, Is.False, "El formulario OPCIONES no se ha ocultado correctamente después de hacer clic en btnBanco.");
        }

        [Test]
        public void TestBtnPersonal_Click()
        {
            // Simular el evento click del botón Personal
            form.btnPersonal_Click(null, EventArgs.Empty);

            // Verificar que se haya ocultado el formulario actual
            Assert.That(form.Visible, Is.False, "El formulario OPCIONES no se ha ocultado correctamente después de hacer clic en btnPersonal.");
        }

        [Test]
        public void TestBtnPacientes_Click()
        {
            // Simular el evento click del botón Pacientes
            form.btnPacientes_Click(null, EventArgs.Empty);

            // Verificar que se haya ocultado el formulario actual
            Assert.That(form.Visible, Is.False, "El formulario OPCIONES no se ha ocultado correctamente después de hacer clic en btnPacientes.");
        }

        [Test]
        public void TestBtnInforme_Click()
        {
            // Simular el evento click del botón Informe
            form.btnInforme_Click(null, EventArgs.Empty);

            // Verificar que se haya ocultado el formulario actual
            Assert.That(form.Visible, Is.False, "El formulario OPCIONES no se ha ocultado correctamente después de hacer clic en btnInforme.");
        }

        [Test]
        public void TestBtnDonantes_Click()
        {
            // Simular el evento click del botón Donantes
            form.btnDonantes_Click(null, EventArgs.Empty);

            // Verificar que se haya ocultado el formulario actual
            Assert.That(form.Visible, Is.False, "El formulario OPCIONES no se ha ocultado correctamente después de hacer clic en btnDonantes.");
        }

        [Test]
        public void TestLogOut_Click()
        {
            // Simular el evento click del botón LogOut
            form.LogOut_Click(null, EventArgs.Empty);

            // Verificar que se haya ocultado el formulario actual
            Assert.That(form.Visible, Is.False, "El formulario OPCIONES no se ha ocultado correctamente después de hacer clic en LogOut.");
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
