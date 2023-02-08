namespace MyNewPaint
{
    partial class CanvasSizeForm
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
            this.components = new System.ComponentModel.Container();
            this.OKButton = new System.Windows.Forms.Button();
            this.RefuseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextboxWidth = new System.Windows.Forms.TextBox();
            this.TextboxHeight = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(192, 94);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 29);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "ОК";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // RefuseButton
            // 
            this.RefuseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.RefuseButton.Location = new System.Drawing.Point(298, 94);
            this.RefuseButton.Name = "RefuseButton";
            this.RefuseButton.Size = new System.Drawing.Size(100, 29);
            this.RefuseButton.TabIndex = 1;
            this.RefuseButton.Text = "Отмена";
            this.RefuseButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ширина:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Высота:";
            // 
            // TextboxWidth
            // 
            this.TextboxWidth.Location = new System.Drawing.Point(70, 34);
            this.TextboxWidth.Name = "TextboxWidth";
            this.TextboxWidth.Size = new System.Drawing.Size(100, 20);
            this.TextboxWidth.TabIndex = 4;
            this.TextboxWidth.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TextboxHeight
            // 
            this.TextboxHeight.Location = new System.Drawing.Point(263, 34);
            this.TextboxHeight.Name = "TextboxHeight";
            this.TextboxHeight.Size = new System.Drawing.Size(100, 20);
            this.TextboxHeight.TabIndex = 5;
            this.TextboxHeight.TextChanged += new System.EventHandler(this.TbHeight_TextChanged);
            // 
            // CanvasSizeForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.RefuseButton;
            this.ClientSize = new System.Drawing.Size(410, 135);
            this.Controls.Add(this.TextboxHeight);
            this.Controls.Add(this.TextboxWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RefuseButton);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CanvasSizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Размер холста";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button RefuseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextboxWidth;
        private System.Windows.Forms.TextBox TextboxHeight;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}