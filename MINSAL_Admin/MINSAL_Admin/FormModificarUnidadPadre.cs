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
    public partial class FormModificarUnidadPadre : Form
    {
        // Servicio SOAP
        private UnidadesServiceSoapClient servicioUnidades;

        // Encapsular los datos mostrados. 
        private UnidadOrganizacional unidad;

        // Determina si se debe actualizar la tabla principal. 
        public bool resultado;

        // Permitir nulos al deserealizar un JSON.
        private JsonSerializerSettings jsonAllowNull;

        public FormModificarUnidadPadre(int id)
        {
            InitializeComponent();

            // Crea una instancia del servicio. 
            this.servicioUnidades = new UnidadesServiceSoapClient();

            // Valor inicial del campo 
            this.resultado = false;

            // Evitar error de referencia nula al convertir objeto. 
            this.jsonAllowNull = new JsonSerializerSettings();
            this.jsonAllowNull.NullValueHandling = NullValueHandling.Ignore;

            // Obtener la unidad a mostrar. 
            string unidadJson = this.servicioUnidades.recuperarUnidad(id);
            this.unidad = JsonConvert.DeserializeObject<UnidadOrganizacional>(unidadJson, this.jsonAllowNull);
        }

        // Al cargar la pantalla. 
        private void FormModificarUnidadPadre_Load(object sender, EventArgs e)
        {
            // Mostrar los datos de la unidad. 
            lblNombre.Text = this.unidad.nombre;
            lblDepartamento.Text = this.unidad.departamento;

            // Mensaje en el botón. 
            btnActivar.Text = this.unidad.activa ? "Desactivar" : "Activar";

            // Por cada hija de la unidad. 
            foreach (UnidadOrganizacional unidadHija in this.unidad.hijos)
            {
                // Mostrar su nombre en el listbox del formulario
                lbxHijas.Items.Add(unidadHija.nombre);
            }
        }

        // Botón activar / desactivar
        private void btnActivar_Click(object sender, EventArgs e)
        {
            // Construir el mensaje de confirmación a mostrar al usuario. 
            string mensaje;
            if (this.unidad.activa)
            {
                mensaje = "¿Confirmar la desactivación de la unidad?";
                mensaje += "\nDesactivar la unidad eliminará la asociación con las unidades a las que les provee transporte";
            }
            else
            {
                mensaje = "¿Confirmar la activación de la unidad?";
            }

            // Mostrar el mensaje de confirmación y capturar el resultado. 
            DialogResult confirmar = MessageBox.Show(mensaje, "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            // Si el resultado de la confirmación es "Sí".
            if (confirmar == DialogResult.Yes)
            {
                // Llamar al servicio.
                string resultadoJson = this.servicioUnidades.asignarActiva(this.unidad.id_unidad_organizacional, !this.unidad.activa);
                bool guardado = JsonConvert.DeserializeObject<bool>(resultadoJson);

                // Si el servicio fue un éxito. 
                if (guardado)
                {
                    // Para actualizar la tabla en el form principal
                    this.resultado = true;

                    // Actualizar la vista. 
                    lbxHijas.Items.Clear();
                    this.unidad.activa = !this.unidad.activa;
                    btnActivar.Text = this.unidad.activa ? "Desactivar" : "Activar";

                    // Mensaje de confirmación para el usuario.
                    MessageBox.Show("Acción realizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Si el servicio fue un fracaso. 
                else
                {
                    MessageBox.Show("Se ha producido un error al registrar la acción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
