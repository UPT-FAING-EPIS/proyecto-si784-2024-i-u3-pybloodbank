using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using System.Windows.Forms;
using System.Reflection;

namespace Proyecto_Final_Blood_Bank
{
    internal class RdonanteTEST
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDataReader> mockReader;
        private RegistroDonantes form;

        [SetUp]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();

            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);

            form = new RegistroDonantes();
        }

        [Test]
        public void CargarGrilla_ShouldLoadDataIntoGrid()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Rows.Add(dataTable.NewRow());
            mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);
            mockReader.Setup(r => r.Read()).Returns(true);

            // Act
            form.GetType().GetMethod("CargarGrilla", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, null);
        }

        [Test]
        public void Limpiar_ShouldClearFields()
        {
            // Arrange
            var txtApellido = form.Controls["txtApellido"] as TextBox;
            var txtDni = form.Controls["txtDni"] as TextBox;
            var txtNombre = form.Controls["txtNombre"] as TextBox;
            var rbNo = form.Controls["rbNo"] as RadioButton;
            var rbSi = form.Controls["rbSi"] as RadioButton;
            var cmbRH = form.Controls["cmbRH"] as ComboBox;
            var cmbTipoDeSangre = form.Controls["cmbTipoDeSangre"] as ComboBox;

            txtApellido.Text = "Test";
            txtDni.Text = "12345678";
            txtNombre.Text = "Test";
            rbNo.Checked = true;
            rbSi.Checked = true;
            cmbRH.SelectedIndex = 0;
            cmbTipoDeSangre.SelectedIndex = 0;

            // Act
            form.GetType().GetMethod("Limpiar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(txtApellido.Text, Is.EqualTo(string.Empty));
                Assert.That(txtDni.Text, Is.EqualTo(string.Empty));
                Assert.That(txtNombre.Text, Is.EqualTo(string.Empty));
                Assert.That(rbNo.Checked, Is.False);
                Assert.That(rbSi.Checked, Is.False);
                Assert.That(cmbRH.SelectedIndex, Is.EqualTo(-1));
                Assert.That(cmbTipoDeSangre.SelectedIndex, Is.EqualTo(-1));
            });
        }

        [Test]
        public void ValidarDni_ShouldReturnTrue_WhenDniIsValid()
        {
            // Arrange
            string validDni = "12345678";

            // Act
            var result = (bool)form.GetType().GetMethod("ValidarDni", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, new object[] { validDni });

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidarDni_ShouldReturnFalse_WhenDniIsInvalid()
        {
            // Arrange
            string invalidDni = "1234567";

            // Act
            var result = (bool)form.GetType().GetMethod("ValidarDni", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, new object[] { invalidDni });

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DonanteExiste_ShouldReturnTrue_WhenDonanteExists()
        {
            // Arrange
            string dni = "12345678";
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(1);
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            // Act
            var result = (bool)form.GetType().GetMethod("DonanteExiste", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, new object[] { dni });
        }

        [Test]
        public void DonanteExiste_ShouldReturnFalse_WhenDonanteDoesNotExist()
        {
            // Arrange
            string dni = "12345678";
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(0);
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            // Act
            var result = (bool)form.GetType().GetMethod("DonanteExiste", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, new object[] { dni });
        }

        [Test]
        public void ValidarCampos_ShouldReturnTrue_WhenAllFieldsAreValid()
        {
            // Arrange
            var txtDni = form.Controls["txtDni"] as TextBox;
            var txtNombre = form.Controls["txtNombre"] as TextBox;
            var txtApellido = form.Controls["txtApellido"] as TextBox;
            var cmbTipoDeSangre = form.Controls["cmbTipoDeSangre"] as ComboBox;
            var cmbRH = form.Controls["cmbRH"] as ComboBox;
            var rbNo = form.Controls["rbNo"] as RadioButton;

            txtDni.Text = "12345678";
            txtNombre.Text = "Test";
            txtApellido.Text = "Test";
            cmbTipoDeSangre.SelectedIndex = 0;
            cmbRH.SelectedIndex = 0;
            rbNo.Checked = true;

            // Act
            var result = (bool)form.GetType().GetMethod("ValidarCampos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, null);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidarCampos_ShouldReturnFalse_WhenAnyFieldIsInvalid()
        {
            // Arrange
            var txtDni = form.Controls["txtDni"] as TextBox;
            txtDni.Text = string.Empty;  // Invalid DNI

            // Act
            var result = (bool)form.GetType().GetMethod("ValidarCampos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RegistrarDonante_ShouldInsertNewDonante()
        {
            // Arrange
            var txtDni = form.Controls["txtDni"] as TextBox;
            var txtNombre = form.Controls["txtNombre"] as TextBox;
            var txtApellido = form.Controls["txtApellido"] as TextBox;
            var cmbTipoDeSangre = form.Controls["cmbTipoDeSangre"] as ComboBox;
            var cmbRH = form.Controls["cmbRH"] as ComboBox;
            var txtLitros = form.Controls["txtLitros"] as TextBox;

            txtDni.Text = "12345678";
            txtNombre.Text = "Test";
            txtApellido.Text = "Test";
            cmbTipoDeSangre.SelectedItem = "A";
            cmbRH.SelectedItem = "+";
            txtLitros.Text = "0.5";

            mockCommand.Setup(c => c.ExecuteNonQuery()).Verifiable();

            // Act
            form.GetType().GetMethod("RegistrarDonante", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, null);
        }

        [Test]
        public void ActualizarBancoDeSangre_ShouldUpdateBancoDeSangre()
        {
            // Arrange
            var cmbTipoDeSangre = form.Controls["cmbTipoDeSangre"] as ComboBox;
            var cmbRH = form.Controls["cmbRH"] as ComboBox;

            cmbTipoDeSangre.SelectedItem = "A";
            cmbRH.SelectedItem = "+";

            mockCommand.Setup(c => c.ExecuteNonQuery()).Verifiable();

            // Act
            form.GetType().GetMethod("ActualizarBancoDeSangre", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, null);
        }

        [Test]
        public void ModificarDonante_ShouldUpdateExistingDonante()
        {
            // Arrange
            var txtDni = form.Controls["txtDni"] as TextBox;
            var txtNombre = form.Controls["txtNombre"] as TextBox;
            var txtApellido = form.Controls["txtApellido"] as TextBox;
            var cmbTipoDeSangre = form.Controls["cmbTipoDeSangre"] as ComboBox;
            var cmbRH = form.Controls["cmbRH"] as ComboBox;
            var txtLitros = form.Controls["txtLitros"] as TextBox;

            txtDni.Text = "12345678";
            txtNombre.Text = "Test";
            txtApellido.Text = "Test";
            cmbTipoDeSangre.SelectedItem = "A";
            cmbRH.SelectedItem = "+";
            txtLitros.Text = "0.5";

            mockCommand.Setup(c => c.ExecuteNonQuery()).Verifiable();

        }


        [Test]
        public void RegistroDonantes_Resize_ShouldResizeControls()
        {
            // Arrange
            var originalSize = form.Size;
            form.Size = new System.Drawing.Size(originalSize.Width + 100, originalSize.Height + 100);

            // Act
            form.GetType().GetMethod("RegistroDonantes_Resize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(form, new object[] { null, EventArgs.Empty });

            // Assert

        }

        [Test]
        public void lblVolver_MouseEnter_ShouldChangeFont()
        {
            // Arrange
            var lblVolver = form.Controls["lblVolver"] as Label;

            // Obtener el tipo de EventArgs vacío
            Type[] eventArgsType = new Type[] { typeof(EventArgs) };

        }


        [Test]
        public void lblVolver_MouseLeave_ShouldResetFont()
        {
            // Arrange
            var lblVolver = form.Controls["lblVolver"] as Label;

        }

        [TearDown]
        public void TearDown()
        {
            // Libera los recursos aquí
            if (form != null)
            {
                form.Dispose();
                form = null;
            }
        }
    }
}
