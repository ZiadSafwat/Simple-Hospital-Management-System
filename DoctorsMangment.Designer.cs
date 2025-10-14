namespace HospitalManagementSystem.Models
{
    partial class DoctorsMangment
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
        // Ensure that there is only one definition of InitializeComponent in the DoctorsMangment partial class.
        // If you have another file (such as DoctorsMangment.cs) that also defines InitializeComponent, remove or rename that method there.
        // The InitializeComponent method should only exist in the Designer file (DoctorsMangment.Designer.cs).

        // No code changes are needed in this file if this is the only definition of InitializeComponent.
        // Please check your other partial class files for DoctorsMangment and remove any duplicate InitializeComponent methods.
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            txtSpecialty = new TextBox();
            txtName = new TextBox();
            btnUpdate = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tabPage2 = new TabPage();
            dtEnd = new DateTimePicker();
            numSlot = new NumericUpDown();
            dtStart = new DateTimePicker();
            cbDayOfWeek = new ComboBox();
            dgvSchedule = new DataGridView();
            btnDeleteSchedule = new Button();
            btnAddSchedule = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            dgvDoctors = new DataGridView();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSlot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDoctors).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(463, 17);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(357, 535);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtEmail);
            tabPage1.Controls.Add(txtPhone);
            tabPage1.Controls.Add(txtSpecialty);
            tabPage1.Controls.Add(txtName);
            tabPage1.Controls.Add(btnUpdate);
            tabPage1.Controls.Add(btnClear);
            tabPage1.Controls.Add(btnDelete);
            tabPage1.Controls.Add(btnAdd);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(349, 502);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Info";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(102, 184);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 13;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(102, 130);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(125, 27);
            txtPhone.TabIndex = 12;
            // 
            // txtSpecialty
            // 
            txtSpecialty.Location = new Point(102, 78);
            txtSpecialty.Name = "txtSpecialty";
            txtSpecialty.Size = new Size(125, 27);
            txtSpecialty.TabIndex = 11;
            // 
            // txtName
            // 
            txtName.Location = new Point(102, 23);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 10;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(150, 257);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(150, 324);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 8;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(6, 324);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(6, 257);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 184);
            label4.Name = "label4";
            label4.Size = new Size(46, 20);
            label4.TabIndex = 5;
            label4.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 130);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 4;
            label3.Text = "Phone";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 81);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 3;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 23);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 2;
            label1.Text = "Name";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dtEnd);
            tabPage2.Controls.Add(numSlot);
            tabPage2.Controls.Add(dtStart);
            tabPage2.Controls.Add(cbDayOfWeek);
            tabPage2.Controls.Add(dgvSchedule);
            tabPage2.Controls.Add(btnDeleteSchedule);
            tabPage2.Controls.Add(btnAddSchedule);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(349, 502);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Schedule";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtEnd
            // 
            dtEnd.Format = DateTimePickerFormat.Time;
            dtEnd.Location = new Point(235, 156);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new Size(76, 27);
            dtEnd.TabIndex = 12;
            // 
            // numSlot
            // 
            numSlot.Location = new Point(161, 85);
            numSlot.Name = "numSlot";
            numSlot.Size = new Size(150, 27);
            numSlot.TabIndex = 11;
            // 
            // dtStart
            // 
            dtStart.Format = DateTimePickerFormat.Time;
            dtStart.Location = new Point(70, 156);
            dtStart.Name = "dtStart";
            dtStart.Size = new Size(79, 27);
            dtStart.TabIndex = 10;
            // 
            // cbDayOfWeek
            // 
            cbDayOfWeek.FormattingEnabled = true;
            cbDayOfWeek.Location = new Point(175, 27);
            cbDayOfWeek.Name = "cbDayOfWeek";
            cbDayOfWeek.Size = new Size(151, 28);
            cbDayOfWeek.TabIndex = 9;
            // 
            // dgvSchedule
            // 
            dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSchedule.Location = new Point(6, 239);
            dgvSchedule.Name = "dgvSchedule";
            dgvSchedule.RowHeadersWidth = 51;
            dgvSchedule.Size = new Size(337, 257);
            dgvSchedule.TabIndex = 2;
            // 
            // btnDeleteSchedule
            // 
            btnDeleteSchedule.Location = new Point(212, 204);
            btnDeleteSchedule.Name = "btnDeleteSchedule";
            btnDeleteSchedule.Size = new Size(131, 29);
            btnDeleteSchedule.TabIndex = 8;
            btnDeleteSchedule.Text = "Delete Schedule";
            btnDeleteSchedule.UseVisualStyleBackColor = true;
            // 
            // btnAddSchedule
            // 
            btnAddSchedule.Location = new Point(10, 204);
            btnAddSchedule.Name = "btnAddSchedule";
            btnAddSchedule.Size = new Size(118, 29);
            btnAddSchedule.TabIndex = 7;
            btnAddSchedule.Text = "Add Schedule";
            btnAddSchedule.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(186, 156);
            label8.Name = "label8";
            label8.Size = new Size(34, 20);
            label8.TabIndex = 6;
            label8.Text = "End";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 156);
            label7.Name = "label7";
            label7.Size = new Size(40, 20);
            label7.TabIndex = 5;
            label7.Text = "Start";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 92);
            label6.Name = "label6";
            label6.Size = new Size(142, 20);
            label6.TabIndex = 4;
            label6.Text = "Sesseion Time  -min";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 27);
            label5.Name = "label5";
            label5.Size = new Size(116, 20);
            label5.TabIndex = 3;
            label5.Text = "Day of the week";
            // 
            // dgvDoctors
            // 
            dgvDoctors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoctors.Location = new Point(12, 26);
            dgvDoctors.Name = "dgvDoctors";
            dgvDoctors.RowHeadersWidth = 51;
            dgvDoctors.Size = new Size(432, 526);
            dgvDoctors.TabIndex = 1;
            // 
            // DoctorsMangment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(841, 564);
            Controls.Add(dgvDoctors);
            Controls.Add(tabControl1);
            Name = "DoctorsMangment";
            Text = "Form1";
            Load += DoctorsMangment_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSlot).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDoctors).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvDoctors;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnDelete;
        private Button btnAdd;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private DateTimePicker dtStart;
        private ComboBox cbDayOfWeek;
        private DataGridView dgvSchedule;
        private Button btnDeleteSchedule;
        private Button btnAddSchedule;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtSpecialty;
        private TextBox txtName;
        private NumericUpDown numSlot;
        private DateTimePicker dtEnd;
    }
}