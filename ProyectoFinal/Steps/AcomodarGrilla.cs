using NUnit.Framework;
using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto_Final_Blood_Bank;
using TechTalk.SpecFlow;

namespace Proyecto_Final_Blood_Bank.Steps
{
    [Binding]
    public sealed class AcomodarGrilla
    {
        private BANCO_DE_SANGRE _loginForm;
        private Mock<IDbConnection> _mockConnection;
        private Mock<IDbCommand> _mockCommand;
        private Mock<IDataReader> _mockReader;

        [BeforeScenario("@tag9")]
        public void BeforeScenarioWithTag()
        {
            _mockConnection = new Mock<IDbConnection>();
            _mockCommand = new Mock<IDbCommand>();
            _mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(conn => conn.CreateCommand()).Returns(_mockCommand.Object);
            _mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockReader.Object);

            // Inicialización de la ventana de login con la conexión mockeada
            _loginForm = new BANCO_DE_SANGRE();
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // Configuración adicional antes del escenario
            ConfigurarSistema();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Limpieza después del escenario
            LimpiarDatos();
        }

        private void ConfigurarSistema()
        {
            // Cargar datos en las grillas al inicio del escenario
            _loginForm.BANCO_DE_SANGRE_Load(this, EventArgs.Empty);
        }

        private void LimpiarDatos()
        {

            _loginForm.Dispose();
        }
    }
}
