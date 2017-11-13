using MINSAL_Admin.UnidadesService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MINSAL_Admin
{
    public partial class FormNuevaUnidad : Form
    {
        // Servicio SOAP
        private UnidadesServiceSoapClient servicioUnidades;

        // Determina si se debe actualizar la tabla principal. 
        public bool resultado;

        public FormNuevaUnidad()
        {
            InitializeComponent();

            // Crea una instancia del servicio. 
            this.servicioUnidades = new UnidadesServiceSoapClient();
            // Valor inicial del campo 
            this.resultado = false;
        }

        // Botón para crear una unidad. 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que se haya escrito un nombre. 
            if (txtNombre.Text == "") 
            {
                MessageBox.Show("Ingrese el nombre!", "Error en el ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            // Validar que se haya seleccionado un departamento. 
            else if (cmbDepartamento.SelectedIndex == -1)
            {
                MessageBox.Show("Ingrese el departamento!", "Error en el ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            // Formulario válido. 
            else
            {
                // Construir el mensaje a mostrar al usuario.
                string mensaje = "Confirma la creación de una unidad con los datos: ";
                mensaje += "\nNombre: " + txtNombre.Text;
                mensaje += "\nDepartamento: " + cmbDepartamento.SelectedItem;
                mensaje += "\nTiene transporte: ";
                mensaje += ckbTransporte.Checked ? "Sí" : "No";

                // Mostrar el mensaje de confirmación y capturar el resultado. 
                DialogResult confirmar = MessageBox.Show(mensaje, "Confirmar creación", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                // Si el resultado de la confirmación es "Sí".
                if (confirmar == DialogResult.Yes)
                {
                    // Llama al servicio. 
                    string resultadoJson = this.servicioUnidades.crearUnidad(txtNombre.Text, (string) cmbDepartamento.SelectedItem, ckbTransporte.Checked);
                    bool guardado = JsonConvert.DeserializeObject<bool>(resultadoJson);

                    // Si el servicio fue un éxito. 
                    if (guardado)
                    {
                        // Para actualizar la tabla en el form principal
                        this.resultado = true;

                        //Cerrar la ventana de creación. 
                        this.Close();
                    }
                    // Si el servicio fue un fracaso
                    else
                    {
                        // Mostrar mensaje de error.
                        MessageBox.Show("Se ha producido un error al crear la unidad", "Error al crear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
