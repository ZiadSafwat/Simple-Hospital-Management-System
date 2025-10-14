namespace HospitalManagementSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DoctorCB = new ComboBox();
            PatientCB = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            monthCalendar1 = new MonthCalendar();
            cmbTimes = new ComboBox();
            lblStatus = new Label();
            btnBook = new Button();
            groupBox4 = new GroupBox();
            txtNotes = new RichTextBox();
            Notetextarea = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            Notetextarea.SuspendLayout();
            SuspendLayout();
            // 
            // DoctorCB
            // 
            DoctorCB.FormattingEnabled = true;
            DoctorCB.Location = new Point(37, 35);
            DoctorCB.Name = "DoctorCB";
            DoctorCB.Size = new Size(151, 28);
            DoctorCB.TabIndex = 0;
            DoctorCB.SelectedIndexChanged += DoctorCB_SelectedIndexChanged;
            // 
            // PatientCB
            // 
            PatientCB.FormattingEnabled = true;
            PatientCB.Location = new Point(37, 43);
            PatientCB.Name = "PatientCB";
            PatientCB.Size = new Size(151, 28);
            PatientCB.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(DoctorCB);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(236, 73);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Doctor";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(PatientCB);
            groupBox2.Location = new Point(12, 104);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(236, 113);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Patient";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(monthCalendar1);
            groupBox3.Location = new Point(12, 237);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(272, 250);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Date";
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(6, 32);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 10;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // cmbTimes
            // 
            cmbTimes.FormattingEnabled = true;
            cmbTimes.Location = new Point(39, 35);
            cmbTimes.Name = "cmbTimes";
            cmbTimes.Size = new Size(151, 28);
            cmbTimes.TabIndex = 10;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(85, 76);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(50, 20);
            lblStatus.TabIndex = 11;
            lblStatus.Text = "label1";
            // 
            // btnBook
            // 
            btnBook.Location = new Point(403, 447);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(94, 29);
            btnBook.TabIndex = 12;
            btnBook.Text = "Book";
            btnBook.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(cmbTimes);
            groupBox4.Controls.Add(lblStatus);
            groupBox4.Location = new Point(307, 284);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(250, 125);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            groupBox4.Text = "Time";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(19, 25);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(282, 211);
            txtNotes.TabIndex = 14;
            txtNotes.Text = "";
            // 
            // Notetextarea
            // 
            Notetextarea.Controls.Add(txtNotes);
            Notetextarea.Location = new Point(307, 12);
            Notetextarea.Name = "Notetextarea";
            Notetextarea.Size = new Size(315, 257);
            Notetextarea.TabIndex = 15;
            Notetextarea.TabStop = false;
            Notetextarea.Text = "Note";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 531);
            Controls.Add(Notetextarea);
            Controls.Add(groupBox4);
            Controls.Add(btnBook);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            Notetextarea.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private ComboBox DoctorCB;
        private ComboBox PatientCB;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private MonthCalendar monthCalendar1;
        private ComboBox cmbTimes;
        private Label lblStatus;
        private Button btnBook;
        private GroupBox groupBox4;
        private RichTextBox txtNotes;
        private GroupBox Notetextarea;
    }
}
