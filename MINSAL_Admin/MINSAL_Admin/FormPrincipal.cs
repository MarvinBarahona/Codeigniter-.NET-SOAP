using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.IO;
using System.ServiceModel;
using Newtonsoft.Json;
using MINSAL_Admin.UnidadesService;

namespace MINSAL_Admin
{
    public partial class FormPrincipal : Form
    {
        // Servicio SOAP. 
        private UnidadesServiceSoapClient servicioUnidades;

        // Datatable con los datos como una tabla. 
        private DataTable dtUnidades;

        // Variable que almacena los datos como objetos.
        private UnidadOrganizacional[] unidades;
    
        // Permitir nulos al deserealizar un JSON.
        private JsonSerializerSettings jsonAllowNull;

        public FormPrincipal()
        {
            InitializeComponent();

            // Crear nueva instancia del servicio. 
            this.servicioUnidades = new UnidadesServiceSoapClient();

            // Crear esqueleto de la variable. 
            this.dtUnidades = new DataTable("unidades");
            this.dtUnidades.Columns.Add("id", typeof(int));
            this.dtUnidades.Columns.Add("Nombre", typeof(string));
            this.dtUnidades.Columns.Add("Departamento", typeof(string));
            this.dtUnidades.Columns.Add("¿Transporte?", typeof(string));
            this.dtUnidades.Columns.Add("¿Activa?", typeof(string));
            this.dtUnidades.Columns.Add("transporte", typeof(bool));

            // Asignar tabla como fuente de datos. 
            this.dgvUnidades.DataSource = this.dtUnidades;            

            // Permitir nulos al deserealizar objetos. 
            this.jsonAllowNull = new JsonSerializerSettings();
            this.jsonAllowNull.NullValueHandling = NullValueHandling.Ignore;
        }

        // Al cargar la pantalla. 
        private void Form1_Load(object sender, EventArgs e)
        {
            // Cargar las unidades almacenadas. 
            this.obtenerUnidades();            
        }

        // Buscador por nombre. 
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Si hay algo escrito en el buscador. 
            if (txtBuscar.TextLength != 0)
            {
                // Filtrar por nombre. 
                this.dtUnidades.DefaultView.RowFilter = string.Format("Nombre LIKE '%{0}%'", txtBuscar.Text);
            }
            // Si no hay anda escrito en el buscador. 
            else
            {
                // No filtrar. 
                this.dtUnidades.DefaultView.RowFilter = "";
            }            
        }

        // Botón Nuevo. 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Usando una instancia del form para crear. 
            using (FormNuevaUnidad formNuevaUnidad = new FormNuevaUnidad())
            {
                // Mostrar form. 
                formNuevaUnidad.ShowDialog();

                // Recargar tabla si es necesario. 
                if (formNuevaUnidad.resultado) this.obtenerUnidades();
            }
        }

        private void dgvUnidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que la celda no sea el encabezado. 
            if (e.RowIndex > -1)
            {
                // Determinar la fila de la celda. 
                DataGridViewRow unidad = dgvUnidades.Rows[e.RowIndex];

                // Si la celda es de un padre. 
                if ((bool)unidad.Cells["transporte"].Value)
                {
                    // Usando una instancia del form para modificar padres. 
                    using (FormModificarUnidadPadre formModificar = new FormModificarUnidadPadre((int)unidad.Cells["id"].Value))
                    {
                        // Mostrar form.
                        formModificar.ShowDialog();

                        // Recargar tabla si es necesario.
                        if (formModificar.resultado) this.obtenerUnidades();
                    }
                }
                // Si la celda es de una hija. 
                else
                {
                    // Obtener las candidatas filtrando todas las unidades. 
                    string departamento = (string)unidad.Cells["Departamento"].Value;
                    UnidadOrganizacional[] candidatas = Array.FindAll(this.unidades, u => u.tiene_transporte && u.activa && u.departamento == departamento);

                    // Usando una instancia del form para modificar hijas.
                    using (FormModificarUnidadHija formModificar = new FormModificarUnidadHija((int)unidad.Cells["id"].Value, candidatas))
                    {
                        // Mostrar form.
                        formModificar.ShowDialog();

                        // Recargar tabla si es necesario.
                        if (formModificar.resultado) this.obtenerUnidades();
                    }
                }
            }
        }

        // Cargar las unidades almacenadas. 
        private void obtenerUnidades()
        {
            try
            {
                // Llamar al servicio. 
                string unidadesJson = servicioUnidades.recuperarUnidades();
                this.unidades = JsonConvert.DeserializeObject<UnidadOrganizacional[]>(unidadesJson, this.jsonAllowNull);

                // Cargar a la variable. 
                this.dtUnidades.Clear();
                foreach (UnidadOrganizacional unidad in this.unidades)
                {
                    object[] unidadArray = { unidad.id_unidad_organizacional, unidad.nombre, unidad.departamento, unidad.tiene_transporte ? "Sí" : "No", unidad.activa ? "Sí" : "No", unidad.tiene_transporte };
                    this.dtUnidades.Rows.Add(unidadArray);
                }

                // Configurar tabla: Campos no visibles
                this.dgvUnidades.Columns["id"].Visible = false;
                this.dgvUnidades.Columns["transporte"].Visible = false;
                // Configurar tabla: Sorting
                this.dgvUnidades.Sort(this.dgvUnidades.Columns["Departamento"], ListSortDirection.Ascending);
                // Configurar tabla: Ancho de columnas. 
                this.dgvUnidades.Columns["Nombre"].MinimumWidth = 300;
                this.dgvUnidades.Columns["Departamento"].MinimumWidth = 100;
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Encienda el servidor!", "No se encuentra el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
