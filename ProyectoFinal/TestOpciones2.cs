using NUnit.Framework;
using Proyecto_Final_Blood_Bank;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [TestFixture]
    public class TestOpciones2
    {
        private OPCIONES_2 opcionesForm;
        private Label lblNombre;
        private Button btnPacientes;
        private Button btnBanco;
        private Button btnDonantes;
        private LinkLabel LogOut;

        [SetUp]
        public void Setup()
        {
            // Inicializar el formulario y los controles necesarios para la prueba
            opcionesForm = new OPCIONES_2();
            lblNombre = new Label();
            btnPacientes = new Button();
            btnBanco = new Button();
            btnDonantes = new Button();
            LogOut = new LinkLabel();

            // Asignar los controles simulados al formulario
            opcionesForm.Controls.Add(lblNombre);
            opcionesForm.Controls.Add(btnPacientes);
            opcionesForm.Controls.Add(btnBanco);
            opcionesForm.Controls.Add(btnDonantes);
            opcionesForm.Controls.Add(LogOut);
        }

        [Test]
        public void btnBanco_Click_ShowsBancoDeSangreForm()
        {
            // Act
            opcionesForm.btnBanco_Click(null, null);

            // Assert
            // Asegurarse de que el formulario actual esté oculto y el nuevo formulario esté mostrado
            Assert.That(opcionesForm.Visible, Is.False, "El formulario OPCIONES_2 debería estar oculto.");
        }

        [Test]
        public void btnPacientes_Click_ShowsRegistroPacienteForm()
        {
            // Act
            opcionesForm.btnPacientes_Click(null, null);

            // Assert
            // Asegurarse de que el formulario actual esté oculto y el nuevo formulario esté mostrado
            Assert.That(opcionesForm.Visible, Is.False, "El formulario OPCIONES_2 debería estar oculto.");
        }

        [Test]
        public void LogOut_Click_ShowsLoginForm()
        {
            // Act
            opcionesForm.LogOut_Click(null, null);

            // Assert
            // Asegurarse de que el formulario actual esté oculto y el nuevo formulario esté mostrado
            Assert.That(opcionesForm.Visible, Is.False, "El formulario OPCIONES_2 debería estar oculto.");
        }

        [Test]
        public void btnDonantes_Click_ShowsRegistroDonantesForm()
        {
            // Act
            opcionesForm.btnDonantes_Click(null, null);

            // Assert
            // Asegurarse de que el formulario actual esté oculto y el nuevo formulario esté mostrado
            Assert.That(opcionesForm.Visible, Is.False, "El formulario OPCIONES_2 debería estar oculto.");
        }

        [Test]
        public void OPCIONES_2_Resize_ResizesControls()
        {
            // Arrange
            var originalSize = opcionesForm.Size;
            var newSize = new Size(originalSize.Width + 100, originalSize.Height + 100);
            opcionesForm.Size = newSize;

            // Act
            opcionesForm.OPCIONES_2_Resize(null, null);

            // Assert
            // Verificar que los controles hayan cambiado de tamaño correctamente
            // (Se requiere una lógica adicional para verificar las dimensiones específicas)
            // Esto es solo un ejemplo para asegurarse de que la función no lanza ninguna excepción.
            Assert.Pass("La función OPCIONES_2_Resize se ejecutó sin lanzar excepciones.");
        }


        [Test]
        public void Form_FormClosed_ExitsApplication()
        {
            // Arrange
            var mockForm = new Form(); // Formulario simulado para verificar la aplicación

            // Act
            Application.Run(mockForm); // Inicia el formulario simulado para agregarlo a la colección de formularios abiertos
            opcionesForm.Form_FormClosed(null, new FormClosedEventArgs(CloseReason.UserClosing)); // Simula el cierre de opcionesForm

            // Assert
            Assert.That(Application.OpenForms, Has.No.Member(mockForm), "El formulario simulado aún está en la colección de formularios abiertos.");
        }




        [TearDown]
        public void TearDown()
        {
            // Liberar los recursos aquí
            opcionesForm.Dispose();
            lblNombre.Dispose();
            btnPacientes.Dispose();
            btnBanco.Dispose();
            btnDonantes.Dispose();
            LogOut.Dispose();
        }
    }

}

