namespace MINSAL_Admin
{
    partial class FormModificarUnidadHija
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnActivar = new System.Windows.Forms.Button();
            this.lblTransporte = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.cmbCandidatas = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnActivar
            // 
            this.btnActivar.Location = new System.Drawing.Point(368, 241);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(75, 23);
            this.btnActivar.TabIndex = 8;
            this.btnActivar.Text = "Desactivar";
            this.btnActivar.UseVisualStyleBackColor = true;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            // 
            // lblTransporte
            // 
            this.lblTransporte.AutoSize = true;
            this.lblTransporte.Location = new System.Drawing.Point(10, 99);
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new System.Drawing.Size(308, 13);
            this.lblTransporte.TabIndex = 7;
            this.lblTransporte.Text = "Esta unidad no posee transporte propio. Utiliza el transporte de: ";
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.Location = new System.Drawing.Point(37, 58);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(145, 13);
            this.lblDepartamento.TabIndex = 6;
            this.lblDepartamento.Text = "El departamento de la unidad";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(37, 33);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(137, 13);
            this.lblNombre.TabIndex = 5;
            this.lblNombre.Text = "El nombre de la unidad";
            // 
            // cmbCandidatas
            // 
            this.cmbCandidatas.FormattingEnabled = true;
            this.cmbCandidatas.Location = new System.Drawing.Point(151, 131);
            this.cmbCandidatas.Name = "cmbCandidatas";
            this.cmbCandidatas.Size = new System.Drawing.Size(188, 21);
            this.cmbCandidatas.TabIndex = 9;
            this.cmbCandidatas.Text = "Seleccionar...";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(212, 174);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormModificarUnidadHija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 277);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cmbCandidatas);
            this.Controls.Add(this.btnActivar);
            this.Controls.Add(this.lblTransporte);
            this.Controls.Add(this.lblDepartamento);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModificarUnidadHija";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modificar";
            this.Load += new System.EventHandler(this.FormModificarUnidadHija_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Label lblTransporte;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ComboBox cmbCandidatas;
        private System.Windows.Forms.Button btnGuardar;
    }
}