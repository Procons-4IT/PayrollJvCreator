namespace PayRollEntryApp
{
    partial class JVCreator
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
            this.ofdFileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ofdFileDialogue
            // 
            this.ofdFileDialogue.FileName = "ofdFileDialogue";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(478, 43);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(12, 44);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(460, 22);
            this.txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Name:";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 88);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(541, 29);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Execute";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 132);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(541, 161);
            this.txtOutput.TabIndex = 4;
            this.txtOutput.Text = "";
            // 
            // JVCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 312);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnBrowse);
            this.Name = "JVCreator";
            this.Text = "JV Importer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdFileDialogue;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.RichTextBox txtOutput;
    }
}

