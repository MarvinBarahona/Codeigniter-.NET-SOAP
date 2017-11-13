namespace MINSAL_Admin
{
    partial class FormModificarUnidadPadre
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblTransporte = new System.Windows.Forms.Label();
            this.lbxHijas = new System.Windows.Forms.ListBox();
            this.btnActivar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(38, 33);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(137, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "El nombre de la unidad";
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.Location = new System.Drawing.Point(38, 58);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(145, 13);
            this.lblDepartamento.TabIndex = 1;
            this.lblDepartamento.Text = "El departamento de la unidad";
            // 
            // lblTransporte
            // 
            this.lblTransporte.AutoSize = true;
            this.lblTransporte.Location = new System.Drawing.Point(11, 99);
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new System.Drawing.Size(296, 13);
            this.lblTransporte.TabIndex = 2;
            this.lblTransporte.Text = "Esta unidad posee transporte propio. Le provee transporte a: ";
            // 
            // lbxHijas
            // 
            this.lbxHijas.FormattingEnabled = true;
            this.lbxHijas.Location = new System.Drawing.Point(82, 119);
            this.lbxHijas.Name = "lbxHijas";
            this.lbxHijas.Size = new System.Drawing.Size(305, 95);
            this.lbxHijas.TabIndex = 3;
            // 
            // btnActivar
            // 
            this.btnActivar.Location = new System.Drawing.Point(369, 241);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(75, 23);
            this.btnActivar.TabIndex = 4;
            this.btnActivar.Text = "Desactivar";
            this.btnActivar.UseVisualStyleBackColor = true;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            // 
            // FormModificarUnidadHijo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 277);
            this.Controls.Add(this.btnActivar);
            this.Controls.Add(this.lbxHijas);
            this.Controls.Add(this.lblTransporte);
            this.Controls.Add(this.lblDepartamento);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModificarUnidadHijo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modificar";
            this.Load += new System.EventHandler(this.FormModificarUnidadPadre_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblTransporte;
        private System.Windows.Forms.ListBox lbxHijas;
        private System.Windows.Forms.Button btnActivar;
    }
}