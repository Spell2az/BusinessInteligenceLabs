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
      this.btnGetDates = new System.Windows.Forms.Button();
      this.lstDates = new System.Windows.Forms.ListBox();
      this.sheet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.data_set_1DataSet = new BusinessInteligenceLabs.Data_set_1DataSet();
      this.sheet1TableAdapter = new BusinessInteligenceLabs.Data_set_1DataSetTableAdapters.Sheet1TableAdapter();
      this.btnTimeIds = new System.Windows.Forms.Button();
      this.lstDestinationTimes = new System.Windows.Forms.ListBox();
      ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.data_set_1DataSet)).BeginInit();
      this.SuspendLayout();
      // 
      // btnGetDates
      // 
      this.btnGetDates.Location = new System.Drawing.Point(198, 79);
      this.btnGetDates.Name = "btnGetDates";
      this.btnGetDates.Size = new System.Drawing.Size(75, 23);
      this.btnGetDates.TabIndex = 0;
      this.btnGetDates.Text = "GetTimes";
      this.btnGetDates.UseVisualStyleBackColor = true;
      this.btnGetDates.Click += new System.EventHandler(this.btnGetDates_ClickAsync);
      // 
      // lstDates
      // 
      this.lstDates.DataSource = this.sheet1BindingSource;
      this.lstDates.DisplayMember = "Order TimeDto";
      this.lstDates.FormattingEnabled = true;
      this.lstDates.HorizontalScrollbar = true;
      this.lstDates.Location = new System.Drawing.Point(198, 135);
      this.lstDates.Name = "lstDates";
      this.lstDates.Size = new System.Drawing.Size(192, 277);
      this.lstDates.TabIndex = 1;
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
      // btnTimeIds
      // 
      this.btnTimeIds.Location = new System.Drawing.Point(442, 79);
      this.btnTimeIds.Name = "btnTimeIds";
      this.btnTimeIds.Size = new System.Drawing.Size(234, 23);
      this.btnTimeIds.TabIndex = 2;
      this.btnTimeIds.Text = "Get from destination Times";
      this.btnTimeIds.UseVisualStyleBackColor = true;
      this.btnTimeIds.Click += new System.EventHandler(this.btnGetTimeFromDestination_Click);
      // 
      // lstDestinationTimes
      // 
      this.lstDestinationTimes.DataSource = this.sheet1BindingSource;
      this.lstDestinationTimes.DisplayMember = "Order TimeDto";
      this.lstDestinationTimes.FormattingEnabled = true;
      this.lstDestinationTimes.HorizontalScrollbar = true;
      this.lstDestinationTimes.Location = new System.Drawing.Point(442, 135);
      this.lstDestinationTimes.Name = "lstDestinationTimes";
      this.lstDestinationTimes.Size = new System.Drawing.Size(192, 277);
      this.lstDestinationTimes.TabIndex = 3;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.lstDestinationTimes);
      this.Controls.Add(this.btnTimeIds);
      this.Controls.Add(this.lstDates);
      this.Controls.Add(this.btnGetDates);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.data_set_1DataSet)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetDates;
        private System.Windows.Forms.ListBox lstDates;
        private Data_set_1DataSet data_set_1DataSet;
        private System.Windows.Forms.BindingSource sheet1BindingSource;
        private Data_set_1DataSetTableAdapters.Sheet1TableAdapter sheet1TableAdapter;
    private System.Windows.Forms.Button btnTimeIds;
    private System.Windows.Forms.ListBox lstDestinationTimes;
  }
}

