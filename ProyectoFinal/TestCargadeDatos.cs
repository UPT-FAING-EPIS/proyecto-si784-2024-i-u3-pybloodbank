using NUnit.Framework;
using System;
using System.Data;

namespace Proyecto_Final_Blood_Bank.Tests
{
    [TestFixture]
    internal class TestCargadeDatos
    {
        private INFORME form;

        [SetUp]
        public void SetUp()
        {
            // Configurar el formulario y el contexto de la prueba
            form = new INFORME();
            form.Show(); // Mostrar el formulario para pruebas
        }

        [Test]
        public void TestCargarDatosChartStock()
        {
            // Llamar al método que carga datos en chartStock
            form.INFORME_Load(null, EventArgs.Empty);

            // Obtener el número de puntos en la serie "Stock" del chart
            int puntosStock = form.chartStock.Series["Stock"].Points.Count;

            // Verificar que se hayan agregado puntos al chart
            Assert.That(puntosStock > 0, "El chartStock no contiene datos.");
        }

        [Test]
        public void TestCargarDatosChartPedido()
        {
            // Llamar al método que carga datos en chartPedido
            form.INFORME_Load(null, EventArgs.Empty);

            // Obtener el número de puntos en la serie "Solicitada" del chart
            int puntosPedido = form.chartPedido.Series["Solicitada"].Points.Count;

            // Verificar que se hayan agregado puntos al chart
            Assert.That(puntosPedido > 0, "El chartPedido no contiene datos.");
        }

        [Test]
        public void TestRedimensionDeControles()
        {
            // Simular un cambio de tamaño en el formulario (podrías ajustar los valores según tu necesidad)
            form.Size = new System.Drawing.Size(800, 600);

            // Llamar al método de redimensión de controles
            form.INFORME_Resize_1(null, EventArgs.Empty);
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
