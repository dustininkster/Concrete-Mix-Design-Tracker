
namespace Concrete_Mix_Design_Tracker
{
    partial class MaterialSelect
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
            this.cklstMaterialSelect = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblSelectMaterial = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cklstMaterialSelect
            // 
            this.cklstMaterialSelect.CheckOnClick = true;
            this.cklstMaterialSelect.FormattingEnabled = true;
            this.cklstMaterialSelect.Items.AddRange(new object[] {
            "Cement",
            "Supplementary Cementitous Material",
            "Coarse Aggregate",
            "Fine Aggregate",
            "Water",
            "Admixture"});
            this.cklstMaterialSelect.Location = new System.Drawing.Point(65, 60);
            this.cklstMaterialSelect.Name = "cklstMaterialSelect";
            this.cklstMaterialSelect.Size = new System.Drawing.Size(249, 109);
            this.cklstMaterialSelect.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(65, 193);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(249, 33);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblSelectMaterial
            // 
            this.lblSelectMaterial.AutoSize = true;
            this.lblSelectMaterial.Location = new System.Drawing.Point(125, 44);
            this.lblSelectMaterial.Name = "lblSelectMaterial";
            this.lblSelectMaterial.Size = new System.Drawing.Size(125, 13);
            this.lblSelectMaterial.TabIndex = 2;
            this.lblSelectMaterial.Text = "Select new material type:";
            // 
            // MaterialSelect
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 253);
            this.ControlBox = false;
            this.Controls.Add(this.lblSelectMaterial);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cklstMaterialSelect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MaterialSelect";
            this.Text = "Material Select";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cklstMaterialSelect;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSelectMaterial;
    }
}