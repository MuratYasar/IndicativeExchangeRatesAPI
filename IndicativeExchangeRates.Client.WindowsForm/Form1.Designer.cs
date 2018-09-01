namespace IndicativeExchangeRates.Client.WindowsForm
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelFilteredAndSortedData = new System.Windows.Forms.Label();
            this.labelAllData = new System.Windows.Forms.Label();
            this.labelSortedData = new System.Windows.Forms.Label();
            this.labelFilteredData = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAllData = new System.Windows.Forms.TabPage();
            this.textboxAllData = new System.Windows.Forms.TextBox();
            this.tabPageFilteredData = new System.Windows.Forms.TabPage();
            this.textboxFilteredData = new System.Windows.Forms.TextBox();
            this.tabPageSortedData = new System.Windows.Forms.TabPage();
            this.textboxSortedData = new System.Windows.Forms.TextBox();
            this.tabPageFilteredAndSortedData = new System.Windows.Forms.TabPage();
            this.textboxFilteredAndSortedData = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageAllData.SuspendLayout();
            this.tabPageFilteredData.SuspendLayout();
            this.tabPageSortedData.SuspendLayout();
            this.tabPageFilteredAndSortedData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.labelFilteredAndSortedData);
            this.panel2.Controls.Add(this.labelAllData);
            this.panel2.Controls.Add(this.labelSortedData);
            this.panel2.Controls.Add(this.labelFilteredData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 352);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 98);
            this.panel2.TabIndex = 1;
            // 
            // labelFilteredAndSortedData
            // 
            this.labelFilteredAndSortedData.AutoSize = true;
            this.labelFilteredAndSortedData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilteredAndSortedData.ForeColor = System.Drawing.Color.DarkRed;
            this.labelFilteredAndSortedData.Location = new System.Drawing.Point(3, 78);
            this.labelFilteredAndSortedData.Name = "labelFilteredAndSortedData";
            this.labelFilteredAndSortedData.Size = new System.Drawing.Size(0, 13);
            this.labelFilteredAndSortedData.TabIndex = 3;
            // 
            // labelAllData
            // 
            this.labelAllData.AutoSize = true;
            this.labelAllData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAllData.ForeColor = System.Drawing.Color.DarkRed;
            this.labelAllData.Location = new System.Drawing.Point(3, 9);
            this.labelAllData.Name = "labelAllData";
            this.labelAllData.Size = new System.Drawing.Size(0, 13);
            this.labelAllData.TabIndex = 0;
            // 
            // labelSortedData
            // 
            this.labelSortedData.AutoSize = true;
            this.labelSortedData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSortedData.ForeColor = System.Drawing.Color.DarkRed;
            this.labelSortedData.Location = new System.Drawing.Point(3, 56);
            this.labelSortedData.Name = "labelSortedData";
            this.labelSortedData.Size = new System.Drawing.Size(0, 13);
            this.labelSortedData.TabIndex = 2;
            // 
            // labelFilteredData
            // 
            this.labelFilteredData.AutoSize = true;
            this.labelFilteredData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilteredData.ForeColor = System.Drawing.Color.DarkRed;
            this.labelFilteredData.Location = new System.Drawing.Point(3, 32);
            this.labelFilteredData.Name = "labelFilteredData";
            this.labelFilteredData.Size = new System.Drawing.Size(0, 13);
            this.labelFilteredData.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAllData);
            this.tabControl1.Controls.Add(this.tabPageFilteredData);
            this.tabControl1.Controls.Add(this.tabPageSortedData);
            this.tabControl1.Controls.Add(this.tabPageFilteredAndSortedData);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(646, 450);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageAllData
            // 
            this.tabPageAllData.Controls.Add(this.textboxAllData);
            this.tabPageAllData.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllData.Name = "tabPageAllData";
            this.tabPageAllData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllData.Size = new System.Drawing.Size(638, 424);
            this.tabPageAllData.TabIndex = 0;
            this.tabPageAllData.Text = "All Data";
            this.tabPageAllData.UseVisualStyleBackColor = true;
            // 
            // textboxAllData
            // 
            this.textboxAllData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxAllData.Location = new System.Drawing.Point(3, 3);
            this.textboxAllData.Multiline = true;
            this.textboxAllData.Name = "textboxAllData";
            this.textboxAllData.ReadOnly = true;
            this.textboxAllData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxAllData.Size = new System.Drawing.Size(632, 418);
            this.textboxAllData.TabIndex = 0;
            // 
            // tabPageFilteredData
            // 
            this.tabPageFilteredData.Controls.Add(this.textboxFilteredData);
            this.tabPageFilteredData.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilteredData.Name = "tabPageFilteredData";
            this.tabPageFilteredData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilteredData.Size = new System.Drawing.Size(638, 424);
            this.tabPageFilteredData.TabIndex = 1;
            this.tabPageFilteredData.Text = "Filtered Data";
            this.tabPageFilteredData.UseVisualStyleBackColor = true;
            // 
            // textboxFilteredData
            // 
            this.textboxFilteredData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxFilteredData.Location = new System.Drawing.Point(3, 3);
            this.textboxFilteredData.Multiline = true;
            this.textboxFilteredData.Name = "textboxFilteredData";
            this.textboxFilteredData.ReadOnly = true;
            this.textboxFilteredData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxFilteredData.Size = new System.Drawing.Size(632, 418);
            this.textboxFilteredData.TabIndex = 1;
            // 
            // tabPageSortedData
            // 
            this.tabPageSortedData.Controls.Add(this.textboxSortedData);
            this.tabPageSortedData.Location = new System.Drawing.Point(4, 22);
            this.tabPageSortedData.Name = "tabPageSortedData";
            this.tabPageSortedData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSortedData.Size = new System.Drawing.Size(638, 424);
            this.tabPageSortedData.TabIndex = 2;
            this.tabPageSortedData.Text = "Sorted Data";
            this.tabPageSortedData.UseVisualStyleBackColor = true;
            // 
            // textboxSortedData
            // 
            this.textboxSortedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxSortedData.Location = new System.Drawing.Point(3, 3);
            this.textboxSortedData.Multiline = true;
            this.textboxSortedData.Name = "textboxSortedData";
            this.textboxSortedData.ReadOnly = true;
            this.textboxSortedData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxSortedData.Size = new System.Drawing.Size(632, 418);
            this.textboxSortedData.TabIndex = 2;
            // 
            // tabPageFilteredAndSortedData
            // 
            this.tabPageFilteredAndSortedData.Controls.Add(this.textboxFilteredAndSortedData);
            this.tabPageFilteredAndSortedData.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilteredAndSortedData.Name = "tabPageFilteredAndSortedData";
            this.tabPageFilteredAndSortedData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilteredAndSortedData.Size = new System.Drawing.Size(638, 424);
            this.tabPageFilteredAndSortedData.TabIndex = 3;
            this.tabPageFilteredAndSortedData.Text = "Filtered And Sorted Data";
            this.tabPageFilteredAndSortedData.UseVisualStyleBackColor = true;
            // 
            // textboxFilteredAndSortedData
            // 
            this.textboxFilteredAndSortedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxFilteredAndSortedData.Location = new System.Drawing.Point(3, 3);
            this.textboxFilteredAndSortedData.Multiline = true;
            this.textboxFilteredAndSortedData.Name = "textboxFilteredAndSortedData";
            this.textboxFilteredAndSortedData.ReadOnly = true;
            this.textboxFilteredAndSortedData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxFilteredAndSortedData.Size = new System.Drawing.Size(632, 418);
            this.textboxFilteredAndSortedData.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageAllData.ResumeLayout(false);
            this.tabPageAllData.PerformLayout();
            this.tabPageFilteredData.ResumeLayout(false);
            this.tabPageFilteredData.PerformLayout();
            this.tabPageSortedData.ResumeLayout(false);
            this.tabPageSortedData.PerformLayout();
            this.tabPageFilteredAndSortedData.ResumeLayout(false);
            this.tabPageFilteredAndSortedData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textboxAllData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelSortedData;
        private System.Windows.Forms.Label labelFilteredData;
        private System.Windows.Forms.Label labelAllData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textboxSortedData;
        private System.Windows.Forms.TextBox textboxFilteredData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAllData;
        private System.Windows.Forms.TabPage tabPageFilteredData;
        private System.Windows.Forms.TabPage tabPageSortedData;
        private System.Windows.Forms.TabPage tabPageFilteredAndSortedData;
        private System.Windows.Forms.TextBox textboxFilteredAndSortedData;
        private System.Windows.Forms.Label labelFilteredAndSortedData;
    }
}

