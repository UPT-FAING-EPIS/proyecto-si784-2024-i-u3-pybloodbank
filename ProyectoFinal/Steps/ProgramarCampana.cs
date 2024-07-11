using TechTalk.SpecFlow;

namespace Proyecto_Final_Blood_Bank.Steps
{
    [Binding]
    public sealed class ProgramarCampana
    {
        [BeforeScenario("@tag10")]
        public void BeforeScenarioWithTag()
        {
            // Lógica para inicializar datos específicos antes de escenarios con @tag10
            InicializarDatos();
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // Lógica para configurar datos generales antes de cualquier escenario
            ConfigurarDatosGenerales();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Lógica para limpieza de datos después de cada escenario
            LimpiarDatos();
        }

        private void InicializarDatos()
        {
            // Ejemplo de inicialización de datos para escenarios con @tag10
            // Aquí puedes agregar código para configurar datos específicos necesarios para estos escenarios
        }

        private void ConfigurarDatosGenerales()
        {
            // Ejemplo de configuración de datos generales antes de cualquier escenario
            // Puedes agregar código aquí para asegurarte de que ciertos datos estén en un estado predefinido antes de los escenarios
        }

        private void LimpiarDatos()
        {
            // Ejemplo de limpieza de datos después de cada escenario
            // Aquí puedes agregar código para revertir cualquier cambio o limpiar datos temporales después de ejecutar un escenario
        }
    }
}

