namespace BusinessInteligenceLabs
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
            this.components = new System.ComponentModel.Container();
            this.sheet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.data_set_1DataSet = new BusinessInteligenceLabs.Data_set_1DataSet();
            this.sheet1TableAdapter = new BusinessInteligenceLabs.Data_set_1DataSetTableAdapters.Sheet1TableAdapter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstTimeDimension = new System.Windows.Forms.ListBox();
            this.lstProductsDimension = new System.Windows.Forms.ListBox();
            this.lstCustomersDimension = new System.Windows.Forms.ListBox();
            this.lstTimeSource = new System.Windows.Forms.ListBox();
            this.lstProductsSource = new System.Windows.Forms.ListBox();
            this.lstFacts = new System.Windows.Forms.ListBox();
            this.lstCustomersSource = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_set_1DataSet)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sheet1BindingSource
            // 
            this.sheet1BindingSource.DataMember = "Sheet1";
            this.sheet1BindingSource.DataSource = this.data_set_1DataSet;
            // 
            // data_set_1DataSet
            // 
            this.data_set_1DataSet.DataSetName = "Data_set_1DataSet";
            this.data_set_1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sheet1TableAdapter
            // 
            this.sheet1TableAdapter.ClearBeforeFill = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(50, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1322, 695);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.lstTimeDimension);
            this.tabPage1.Controls.Add(this.lstProductsDimension);
            this.tabPage1.Controls.Add(this.lstCustomersDimension);
            this.tabPage1.Controls.Add(this.lstTimeSource);
            this.tabPage1.Controls.Add(this.lstProductsSource);
            this.tabPage1.Controls.Add(this.lstFacts);
            this.tabPage1.Controls.Add(this.lstCustomersSource);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1314, 669);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(487, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(256, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Fill all from dimension tables";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.GetFromDestinationButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Fill all from source";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.FillAllFromSource);
            // 
            // lstTimeDimension
            // 
            this.lstTimeDimension.FormattingEnabled = true;
            this.lstTimeDimension.HorizontalScrollbar = true;
            this.lstTimeDimension.Location = new System.Drawing.Point(434, 273);
            this.lstTimeDimension.Name = "lstTimeDimension";
            this.lstTimeDimension.Size = new System.Drawing.Size(355, 160);
            this.lstTimeDimension.TabIndex = 11;
            // 
            // lstProductsDimension
            // 
            this.lstProductsDimension.FormattingEnabled = true;
            this.lstProductsDimension.HorizontalScrollbar = true;
            this.lstProductsDimension.Location = new System.Drawing.Point(434, 489);
            this.lstProductsDimension.Name = "lstProductsDimension";
            this.lstProductsDimension.Size = new System.Drawing.Size(355, 160);
            this.lstProductsDimension.TabIndex = 10;
            // 
            // lstCustomersDimension
            // 
            this.lstCustomersDimension.FormattingEnabled = true;
            this.lstCustomersDimension.HorizontalScrollbar = true;
            this.lstCustomersDimension.Location = new System.Drawing.Point(434, 84);
            this.lstCustomersDimension.Name = "lstCustomersDimension";
            this.lstCustomersDimension.Size = new System.Drawing.Size(355, 160);
            this.lstCustomersDimension.TabIndex = 9;
            // 
            // lstTimeSource
            // 
            this.lstTimeSource.FormattingEnabled = true;
            this.lstTimeSource.HorizontalScrollbar = true;
            this.lstTimeSource.Location = new System.Drawing.Point(49, 273);
            this.lstTimeSource.Name = "lstTimeSource";
            this.lstTimeSource.Size = new System.Drawing.Size(355, 160);
            this.lstTimeSource.TabIndex = 8;
            // 
            // lstProductsSource
            // 
            this.lstProductsSource.FormattingEnabled = true;
            this.lstProductsSource.HorizontalScrollbar = true;
            this.lstProductsSource.Location = new System.Drawing.Point(49, 489);
            this.lstProductsSource.Name = "lstProductsSource";
            this.lstProductsSource.Size = new System.Drawing.Size(355, 160);
            this.lstProductsSource.TabIndex = 7;
            // 
            // lstFacts
            // 
            this.lstFacts.FormattingEnabled = true;
            this.lstFacts.HorizontalScrollbar = true;
            this.lstFacts.Location = new System.Drawing.Point(880, 173);
            this.lstFacts.Name = "lstFacts";
            this.lstFacts.Size = new System.Drawing.Size(352, 277);
            this.lstFacts.TabIndex = 6;
            // 
            // lstCustomersSource
            // 
            this.lstCustomersSource.FormattingEnabled = true;
            this.lstCustomersSource.HorizontalScrollbar = true;
            this.lstCustomersSource.Location = new System.Drawing.Point(49, 84);
            this.lstCustomersSource.Name = "lstCustomersSource";
            this.lstCustomersSource.Size = new System.Drawing.Size(355, 160);
            this.lstCustomersSource.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1314, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Customer Data from Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(431, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Customer Data from Dimension table";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Time Data from Dimension table";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(431, 473);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Product Data from Dimension table";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 473);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Product Data from Source";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Time Data from Source";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(877, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Fact Table Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 719);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_set_1DataSet)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Data_set_1DataSet data_set_1DataSet;
        private System.Windows.Forms.BindingSource sheet1BindingSource;
        private Data_set_1DataSetTableAdapters.Sheet1TableAdapter sheet1TableAdapter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox lstTimeDimension;
        private System.Windows.Forms.ListBox lstProductsDimension;
        private System.Windows.Forms.ListBox lstCustomersDimension;
        private System.Windows.Forms.ListBox lstTimeSource;
        private System.Windows.Forms.ListBox lstProductsSource;
        private System.Windows.Forms.ListBox lstFacts;
        private System.Windows.Forms.ListBox lstCustomersSource;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

