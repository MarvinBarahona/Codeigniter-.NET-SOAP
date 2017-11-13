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
    public partial class FormModificarUnidadHija : Form
    {
        // Servicio SOAP
        private UnidadesServiceSoapClient servicioUnidades;

        // Encapsular los datos mostrados. 
        private UnidadOrganizacional unidad;

        // Determina si se debe actualizar la tabla principal. 
        public bool resultado;

        // Candidatas a ser la que provee transporte a la unidad actual
        private UnidadOrganizacional[] candidatas;

        // Permitir nulos al deserealizar un JSON.
        private JsonSerializerSettings jsonAllowNull;

        public FormModificarUnidadHija(int id, UnidadOrganizacional[] candidatasPadre)
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

            // Asignar las candidatas a la variable del form. 
            this.candidatas = candidatasPadre;
        }


        // Al cargar la pantalla. 
        private void FormModificarUnidadHija_Load(object sender, EventArgs e)
        {
            // Mostrar los datos de la unidad. 
            lblNombre.Text = this.unidad.nombre;
            lblDepartamento.Text = this.unidad.departamento;

            // Mensaje en el botón. 
            btnActivar.Text = this.unidad.activa ? "Desactivar" : "Activar";

            // Elegir un padre solo si está activa. 
            cmbCandidatas.Enabled = this.unidad.activa;

            // Por cada candidata a ser padre
            foreach (UnidadOrganizacional candidata in this.candidatas)
            {
                // Agregar la opción del Combo box.
                cmbCandidatas.Items.Add(candidata);
            }

            // Configurar el combo box 
            cmbCandidatas.DisplayMember = "nombre";
            cmbCandidatas.ValueMember = "id";

            // Elegir la opción inicial del Combo box, si existe. 
            if (this.unidad.padre != null)
            {
                UnidadOrganizacional elegida = Array.Find(this.candidatas, u => u.id_unidad_organizacional == this.unidad.padre.id_unidad_organizacional);
                cmbCandidatas.SelectedItem = elegida;
            }
            
        }

        // Botón de activra / desactivar. 
        private void btnActivar_Click(object sender, EventArgs e)
        {
            // Construir el mensaje a mostrar al usuario. 
            string mensaje;
            if (this.unidad.activa)
            {
                mensaje = "¿Confirmar la desactivación de la unidad?";
                mensaje += "\nDesactivar la unidad eliminará la asociación con la unidad que le provee transporte";
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
                    cmbCandidatas.SelectedIndex = -1;
                    this.unidad.activa = !this.unidad.activa;
                    btnActivar.Text = this.unidad.activa ? "Desactivar" : "Activar";
                    cmbCandidatas.Enabled = this.unidad.activa;

                    // Mensaje de confirmación para el usuario. 
                    MessageBox.Show("Acción realizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Si el servicio fue un fracaso
                else
                {
                    // Mostrar mensaje de error. 
                    MessageBox.Show("Se ha producido un error al registrar la acción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Botón para guardar al padre de la unidad. 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un candidato. 
            if (cmbCandidatas.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una unidad!", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
             // Formulario válido. 
            else
            {
                // Llama al servicio. 
                string resultadoJson = this.servicioUnidades.asignarPadre(this.unidad.id_unidad_organizacional, (cmbCandidatas.SelectedItem as UnidadOrganizacional).id_unidad_organizacional);
                bool guardado = JsonConvert.DeserializeObject<bool>(resultadoJson);

                // Si el servicio fue un éxito. 
                if (guardado)
                {
                    // Para actualizar la tabla en el form principal
                    this.resultado = true;

                    // Mensaje de confirmación para el usuario. 
                    MessageBox.Show("Acción realizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Si el servicio fue un fracaso
                else
                {
                    // Mostrar mensaje de error.
                    MessageBox.Show("Se ha producido un error al registrar la acción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
