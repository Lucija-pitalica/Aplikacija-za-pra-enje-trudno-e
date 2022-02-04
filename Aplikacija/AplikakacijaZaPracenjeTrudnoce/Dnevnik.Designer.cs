namespace AplikakacijaZaPracenjeTrudnoce
{
    partial class Dnevnik
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
            this.buttonProsliIzvjesce = new System.Windows.Forms.Button();
            this.dataGridViewZapisiIzvjesca = new System.Windows.Forms.DataGridView();
            this.buttonDodaj = new System.Windows.Forms.Button();
            this.buttonNoviIzvjestaj = new System.Windows.Forms.Button();
            this.labelBroj = new System.Windows.Forms.Label();
            this.labelOpis = new System.Windows.Forms.Label();
            this.groupBoxDnevnik = new System.Windows.Forms.GroupBox();
            this.comboBoxBroj = new System.Windows.Forms.ComboBox();
            this.textBoxPrehrana = new System.Windows.Forms.TextBox();
            this.textBoxSimptomi = new System.Windows.Forms.TextBox();
            this.labelOstali = new System.Windows.Forms.Label();
            this.checkBoxBol = new System.Windows.Forms.CheckBox();
            this.checkBoxZgaravica = new System.Windows.Forms.CheckBox();
            this.checkBoxSlabost = new System.Windows.Forms.CheckBox();
            this.checkBoxMucnina = new System.Windows.Forms.CheckBox();
            this.buttonProsliDnevnik = new System.Windows.Forms.Button();
            this.groupBoxIzvjesce = new System.Windows.Forms.GroupBox();
            this.labelKonacni = new System.Windows.Forms.Label();
            this.labelPocetni = new System.Windows.Forms.Label();
            this.dateTimePickerKonacni = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerPocetni = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZapisiIzvjesca)).BeginInit();
            this.groupBoxDnevnik.SuspendLayout();
            this.groupBoxIzvjesce.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonProsliIzvjesce
            // 
            this.buttonProsliIzvjesce.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonProsliIzvjesce.Location = new System.Drawing.Point(1102, 340);
            this.buttonProsliIzvjesce.Name = "buttonProsliIzvjesce";
            this.buttonProsliIzvjesce.Size = new System.Drawing.Size(175, 69);
            this.buttonProsliIzvjesce.TabIndex = 0;
            this.buttonProsliIzvjesce.Text = "Prošli izvještaji";
            this.buttonProsliIzvjesce.UseVisualStyleBackColor = true;
            this.buttonProsliIzvjesce.Click += new System.EventHandler(this.buttonProsliIzvjesce_Click);
            // 
            // dataGridViewZapisiIzvjesca
            // 
            this.dataGridViewZapisiIzvjesca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZapisiIzvjesca.Location = new System.Drawing.Point(718, 24);
            this.dataGridViewZapisiIzvjesca.Name = "dataGridViewZapisiIzvjesca";
            this.dataGridViewZapisiIzvjesca.RowHeadersWidth = 51;
            this.dataGridViewZapisiIzvjesca.RowTemplate.Height = 24;
            this.dataGridViewZapisiIzvjesca.Size = new System.Drawing.Size(894, 298);
            this.dataGridViewZapisiIzvjesca.TabIndex = 0;
            // 
            // buttonDodaj
            // 
            this.buttonDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonDodaj.Location = new System.Drawing.Point(153, 340);
            this.buttonDodaj.Name = "buttonDodaj";
            this.buttonDodaj.Size = new System.Drawing.Size(175, 69);
            this.buttonDodaj.TabIndex = 2;
            this.buttonDodaj.Text = "Dodaj novi zapis u dnevnik";
            this.buttonDodaj.UseVisualStyleBackColor = true;
            this.buttonDodaj.Click += new System.EventHandler(this.buttonDodaj_Click);
            // 
            // buttonNoviIzvjestaj
            // 
            this.buttonNoviIzvjestaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonNoviIzvjestaj.Location = new System.Drawing.Point(484, 340);
            this.buttonNoviIzvjestaj.Name = "buttonNoviIzvjestaj";
            this.buttonNoviIzvjestaj.Size = new System.Drawing.Size(175, 69);
            this.buttonNoviIzvjestaj.TabIndex = 3;
            this.buttonNoviIzvjestaj.Text = "Napravi novi izvještaj";
            this.buttonNoviIzvjestaj.UseVisualStyleBackColor = true;
            this.buttonNoviIzvjestaj.Click += new System.EventHandler(this.buttonNoviIzvjestaj_Click);
            // 
            // labelBroj
            // 
            this.labelBroj.AutoSize = true;
            this.labelBroj.Location = new System.Drawing.Point(136, 43);
            this.labelBroj.Name = "labelBroj";
            this.labelBroj.Size = new System.Drawing.Size(168, 20);
            this.labelBroj.TabIndex = 5;
            this.labelBroj.Text = "Broj puta povraćanja:";
            this.labelBroj.Visible = false;
            // 
            // labelOpis
            // 
            this.labelOpis.AutoSize = true;
            this.labelOpis.Location = new System.Drawing.Point(6, 246);
            this.labelOpis.Name = "labelOpis";
            this.labelOpis.Size = new System.Drawing.Size(120, 20);
            this.labelOpis.TabIndex = 6;
            this.labelOpis.Text = "Opis prehrane:";
            // 
            // groupBoxDnevnik
            // 
            this.groupBoxDnevnik.Controls.Add(this.comboBoxBroj);
            this.groupBoxDnevnik.Controls.Add(this.textBoxPrehrana);
            this.groupBoxDnevnik.Controls.Add(this.textBoxSimptomi);
            this.groupBoxDnevnik.Controls.Add(this.labelOstali);
            this.groupBoxDnevnik.Controls.Add(this.checkBoxBol);
            this.groupBoxDnevnik.Controls.Add(this.checkBoxZgaravica);
            this.groupBoxDnevnik.Controls.Add(this.labelBroj);
            this.groupBoxDnevnik.Controls.Add(this.checkBoxSlabost);
            this.groupBoxDnevnik.Controls.Add(this.checkBoxMucnina);
            this.groupBoxDnevnik.Controls.Add(this.labelOpis);
            this.groupBoxDnevnik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxDnevnik.Location = new System.Drawing.Point(12, 12);
            this.groupBoxDnevnik.Name = "groupBoxDnevnik";
            this.groupBoxDnevnik.Size = new System.Drawing.Size(410, 310);
            this.groupBoxDnevnik.TabIndex = 7;
            this.groupBoxDnevnik.TabStop = false;
            this.groupBoxDnevnik.Text = "Dnevnik";
            // 
            // comboBoxBroj
            // 
            this.comboBoxBroj.FormattingEnabled = true;
            this.comboBoxBroj.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBoxBroj.Location = new System.Drawing.Point(315, 40);
            this.comboBoxBroj.Name = "comboBoxBroj";
            this.comboBoxBroj.Size = new System.Drawing.Size(83, 28);
            this.comboBoxBroj.TabIndex = 15;
            this.comboBoxBroj.Visible = false;
            // 
            // textBoxPrehrana
            // 
            this.textBoxPrehrana.Location = new System.Drawing.Point(173, 243);
            this.textBoxPrehrana.Multiline = true;
            this.textBoxPrehrana.Name = "textBoxPrehrana";
            this.textBoxPrehrana.Size = new System.Drawing.Size(225, 58);
            this.textBoxPrehrana.TabIndex = 11;
            // 
            // textBoxSimptomi
            // 
            this.textBoxSimptomi.Location = new System.Drawing.Point(173, 160);
            this.textBoxSimptomi.Multiline = true;
            this.textBoxSimptomi.Name = "textBoxSimptomi";
            this.textBoxSimptomi.Size = new System.Drawing.Size(225, 54);
            this.textBoxSimptomi.TabIndex = 13;
            // 
            // labelOstali
            // 
            this.labelOstali.AutoSize = true;
            this.labelOstali.Location = new System.Drawing.Point(6, 160);
            this.labelOstali.Name = "labelOstali";
            this.labelOstali.Size = new System.Drawing.Size(131, 20);
            this.labelOstali.TabIndex = 12;
            this.labelOstali.Text = "Ostali simptomi:";
            // 
            // checkBoxBol
            // 
            this.checkBoxBol.AutoSize = true;
            this.checkBoxBol.Location = new System.Drawing.Point(6, 129);
            this.checkBoxBol.Name = "checkBoxBol";
            this.checkBoxBol.Size = new System.Drawing.Size(159, 24);
            this.checkBoxBol.TabIndex = 10;
            this.checkBoxBol.Text = "Bol u zglobovima";
            this.checkBoxBol.UseVisualStyleBackColor = true;
            // 
            // checkBoxZgaravica
            // 
            this.checkBoxZgaravica.AutoSize = true;
            this.checkBoxZgaravica.Location = new System.Drawing.Point(6, 99);
            this.checkBoxZgaravica.Name = "checkBoxZgaravica";
            this.checkBoxZgaravica.Size = new System.Drawing.Size(103, 24);
            this.checkBoxZgaravica.TabIndex = 9;
            this.checkBoxZgaravica.Text = "Žgaravica";
            this.checkBoxZgaravica.UseVisualStyleBackColor = true;
            // 
            // checkBoxSlabost
            // 
            this.checkBoxSlabost.AutoSize = true;
            this.checkBoxSlabost.Location = new System.Drawing.Point(6, 69);
            this.checkBoxSlabost.Name = "checkBoxSlabost";
            this.checkBoxSlabost.Size = new System.Drawing.Size(87, 24);
            this.checkBoxSlabost.TabIndex = 8;
            this.checkBoxSlabost.Text = "Slabost";
            this.checkBoxSlabost.UseVisualStyleBackColor = true;
            // 
            // checkBoxMucnina
            // 
            this.checkBoxMucnina.AutoSize = true;
            this.checkBoxMucnina.Location = new System.Drawing.Point(6, 39);
            this.checkBoxMucnina.Name = "checkBoxMucnina";
            this.checkBoxMucnina.Size = new System.Drawing.Size(94, 24);
            this.checkBoxMucnina.TabIndex = 7;
            this.checkBoxMucnina.Text = "Mučnina";
            this.checkBoxMucnina.UseVisualStyleBackColor = true;
            this.checkBoxMucnina.Click += new System.EventHandler(this.checkBoxMucnina_Click);
            // 
            // buttonProsliDnevnik
            // 
            this.buttonProsliDnevnik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonProsliDnevnik.Location = new System.Drawing.Point(921, 340);
            this.buttonProsliDnevnik.Name = "buttonProsliDnevnik";
            this.buttonProsliDnevnik.Size = new System.Drawing.Size(175, 69);
            this.buttonProsliDnevnik.TabIndex = 8;
            this.buttonProsliDnevnik.Text = "Prošli zapisi";
            this.buttonProsliDnevnik.UseVisualStyleBackColor = true;
            this.buttonProsliDnevnik.Click += new System.EventHandler(this.buttonProsliDnevnik_Click);
            // 
            // groupBoxIzvjesce
            // 
            this.groupBoxIzvjesce.Controls.Add(this.labelKonacni);
            this.groupBoxIzvjesce.Controls.Add(this.labelPocetni);
            this.groupBoxIzvjesce.Controls.Add(this.dateTimePickerKonacni);
            this.groupBoxIzvjesce.Controls.Add(this.dateTimePickerPocetni);
            this.groupBoxIzvjesce.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxIzvjesce.Location = new System.Drawing.Point(439, 12);
            this.groupBoxIzvjesce.Name = "groupBoxIzvjesce";
            this.groupBoxIzvjesce.Size = new System.Drawing.Size(260, 310);
            this.groupBoxIzvjesce.TabIndex = 9;
            this.groupBoxIzvjesce.TabStop = false;
            this.groupBoxIzvjesce.Text = "Izvješće";
            // 
            // labelKonacni
            // 
            this.labelKonacni.AutoSize = true;
            this.labelKonacni.Location = new System.Drawing.Point(20, 178);
            this.labelKonacni.Name = "labelKonacni";
            this.labelKonacni.Size = new System.Drawing.Size(125, 20);
            this.labelKonacni.TabIndex = 3;
            this.labelKonacni.Text = "Konačni datum:";
            // 
            // labelPocetni
            // 
            this.labelPocetni.AutoSize = true;
            this.labelPocetni.Location = new System.Drawing.Point(20, 73);
            this.labelPocetni.Name = "labelPocetni";
            this.labelPocetni.Size = new System.Drawing.Size(121, 20);
            this.labelPocetni.TabIndex = 2;
            this.labelPocetni.Text = "Početni datum:";
            // 
            // dateTimePickerKonacni
            // 
            this.dateTimePickerKonacni.Location = new System.Drawing.Point(24, 201);
            this.dateTimePickerKonacni.Name = "dateTimePickerKonacni";
            this.dateTimePickerKonacni.Size = new System.Drawing.Size(200, 27);
            this.dateTimePickerKonacni.TabIndex = 1;
            // 
            // dateTimePickerPocetni
            // 
            this.dateTimePickerPocetni.Location = new System.Drawing.Point(24, 99);
            this.dateTimePickerPocetni.Name = "dateTimePickerPocetni";
            this.dateTimePickerPocetni.Size = new System.Drawing.Size(200, 27);
            this.dateTimePickerPocetni.TabIndex = 0;
            // 
            // Dnevnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1644, 496);
            this.Controls.Add(this.groupBoxIzvjesce);
            this.Controls.Add(this.buttonProsliDnevnik);
            this.Controls.Add(this.buttonNoviIzvjestaj);
            this.Controls.Add(this.buttonDodaj);
            this.Controls.Add(this.dataGridViewZapisiIzvjesca);
            this.Controls.Add(this.buttonProsliIzvjesce);
            this.Controls.Add(this.groupBoxDnevnik);
            this.Name = "Dnevnik";
            this.Text = "Dnevnik";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZapisiIzvjesca)).EndInit();
            this.groupBoxDnevnik.ResumeLayout(false);
            this.groupBoxDnevnik.PerformLayout();
            this.groupBoxIzvjesce.ResumeLayout(false);
            this.groupBoxIzvjesce.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonProsliIzvjesce;
        private System.Windows.Forms.DataGridView dataGridViewZapisiIzvjesca;
        private System.Windows.Forms.Button buttonDodaj;
        private System.Windows.Forms.Button buttonNoviIzvjestaj;
        private System.Windows.Forms.Label labelBroj;
        private System.Windows.Forms.Label labelOpis;
        private System.Windows.Forms.GroupBox groupBoxDnevnik;
        private System.Windows.Forms.TextBox textBoxPrehrana;
        private System.Windows.Forms.TextBox textBoxSimptomi;
        private System.Windows.Forms.Label labelOstali;
        private System.Windows.Forms.CheckBox checkBoxBol;
        private System.Windows.Forms.CheckBox checkBoxZgaravica;
        private System.Windows.Forms.CheckBox checkBoxSlabost;
        private System.Windows.Forms.CheckBox checkBoxMucnina;
        private System.Windows.Forms.ComboBox comboBoxBroj;
        private System.Windows.Forms.Button buttonProsliDnevnik;
        private System.Windows.Forms.GroupBox groupBoxIzvjesce;
        private System.Windows.Forms.Label labelKonacni;
        private System.Windows.Forms.Label labelPocetni;
        private System.Windows.Forms.DateTimePicker dateTimePickerKonacni;
        private System.Windows.Forms.DateTimePicker dateTimePickerPocetni;
    }
}