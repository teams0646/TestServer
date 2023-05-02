using System.Windows.Forms;

namespace TestServer
{
    partial class MainWindowForm
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
        /// 
        private DataGridView dgvUsers;
        private DataGridView dgvGroupTests;


        private void InitializeComponent()
        {
            this.dgvGroupTests = new System.Windows.Forms.DataGridView();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.clientsListBox = new System.Windows.Forms.ListBox();
            this.lblGroupTestName = new System.Windows.Forms.Label();
            this.lblGroupTestGroup = new System.Windows.Forms.Label();
            this.lstGroups = new System.Windows.Forms.ListBox();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.cmbTests = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGroupTestsEmpty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupTests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGroupTests
            // 
            this.dgvGroupTests.Location = new System.Drawing.Point(0, 0);
            this.dgvGroupTests.Name = "dgvGroupTests";
            this.dgvGroupTests.Size = new System.Drawing.Size(240, 150);
            this.dgvGroupTests.TabIndex = 0;
            // 
            // dgvUsers
            // 
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(240, 150);
            this.dgvUsers.TabIndex = 0;
            // 
            // clientsListBox
            // 
            this.clientsListBox.FormattingEnabled = true;
            this.clientsListBox.Location = new System.Drawing.Point(0, 234);
            this.clientsListBox.Name = "clientsListBox";
            this.clientsListBox.Size = new System.Drawing.Size(696, 212);
            this.clientsListBox.TabIndex = 0;
            // 
            // lblGroupTestName
            // 
            this.lblGroupTestName.AutoSize = true;
            this.lblGroupTestName.Location = new System.Drawing.Point(619, 9);
            this.lblGroupTestName.Name = "lblGroupTestName";
            this.lblGroupTestName.Size = new System.Drawing.Size(95, 13);
            this.lblGroupTestName.TabIndex = 1;
            this.lblGroupTestName.Text = "lblGroupTestName";
            // 
            // lblGroupTestGroup
            // 
            this.lblGroupTestGroup.AutoSize = true;
            this.lblGroupTestGroup.Location = new System.Drawing.Point(618, 51);
            this.lblGroupTestGroup.Name = "lblGroupTestGroup";
            this.lblGroupTestGroup.Size = new System.Drawing.Size(96, 13);
            this.lblGroupTestGroup.TabIndex = 2;
            this.lblGroupTestGroup.Text = "lblGroupTestGroup";
            // 
            // lstGroups
            // 
            this.lstGroups.FormattingEnabled = true;
            this.lstGroups.Location = new System.Drawing.Point(297, 0);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new System.Drawing.Size(291, 108);
            this.lstGroups.TabIndex = 3;
            // 
            // cmbGroups
            // 
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(323, 140);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(121, 21);
            this.cmbGroups.TabIndex = 4;
            // 
            // cmbTests
            // 
            this.cmbTests.FormattingEnabled = true;
            this.cmbTests.Location = new System.Drawing.Point(467, 140);
            this.cmbTests.Name = "cmbTests";
            this.cmbTests.Size = new System.Drawing.Size(121, 21);
            this.cmbTests.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Group:";
            // 
            // lblGroupTestsEmpty
            // 
            this.lblGroupTestsEmpty.AutoSize = true;
            this.lblGroupTestsEmpty.Location = new System.Drawing.Point(619, 95);
            this.lblGroupTestsEmpty.Name = "lblGroupTestsEmpty";
            this.lblGroupTestsEmpty.Size = new System.Drawing.Size(101, 13);
            this.lblGroupTestsEmpty.TabIndex = 9;
            this.lblGroupTestsEmpty.Text = "lblGroupTestsEmpty";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tests:";
            // 
            // cmbUsers
            // 
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(323, 200);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(121, 21);
            this.cmbUsers.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "User:";
            // 
            // MainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 761);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbUsers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblGroupTestsEmpty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTests);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lstGroups);
            this.Controls.Add(this.lblGroupTestGroup);
            this.Controls.Add(this.lblGroupTestName);
            this.Controls.Add(this.clientsListBox);
            this.Name = "MainWindowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin panel";
            this.Load += new System.EventHandler(this.MainWindowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupTests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox clientsListBox;
        private System.Windows.Forms.Label lblGroupTestName;
        private System.Windows.Forms.Label lblGroupTestGroup;
        private System.Windows.Forms.ListBox lstGroups;
        private ComboBox cmbGroups;
        private ComboBox cmbTests;
        private Label label1;
        private Label lblGroupTestsEmpty;
        private Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ComboBox cmbUsers;
        private Label label2;
    }
}

