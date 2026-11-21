using System.Windows.Forms;

namespace ZooShop
{
    partial class SupplierEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblDeliveryLeadTime;

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.NumericUpDown nudDeliveryLeadTime;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblName = new System.Windows.Forms.Label();
            lblPhoneNumber = new System.Windows.Forms.Label();
            lblDeliveryLeadTime = new System.Windows.Forms.Label();

            txtName = new System.Windows.Forms.TextBox();
            txtPhoneNumber = new System.Windows.Forms.TextBox();
            nudDeliveryLeadTime = new System.Windows.Forms.NumericUpDown();

            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)nudDeliveryLeadTime).BeginInit();

            SuspendLayout();

            // lblName
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(25, 65);
            lblName.Text = "Название:";

            // txtName
            txtName.Location = new System.Drawing.Point(190, 62);
            txtName.Size = new System.Drawing.Size(220, 23);

            // lblPhoneNumber
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new System.Drawing.Point(25, 105);
            lblPhoneNumber.Text = "Телефон:";

            // txtPhoneNumber
            txtPhoneNumber.Location = new System.Drawing.Point(190, 102);
            txtPhoneNumber.Size = new System.Drawing.Size(220, 23);

            // lblDeliveryLeadTime
            lblDeliveryLeadTime.AutoSize = true;
            lblDeliveryLeadTime.Location = new System.Drawing.Point(25, 145);
            lblDeliveryLeadTime.Text = "Срок доставки (дней):";

            // nudDeliveryLeadTime
            nudDeliveryLeadTime.Location = new System.Drawing.Point(190, 142);
            nudDeliveryLeadTime.Maximum = 365;
            nudDeliveryLeadTime.Minimum = 0;
            nudDeliveryLeadTime.Size = new System.Drawing.Size(220, 23);

            // btnSave
            btnSave.Location = new System.Drawing.Point(190, 195);
            btnSave.Size = new System.Drawing.Size(105, 32);
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.Location = new System.Drawing.Point(305, 195);
            btnCancel.Size = new System.Drawing.Size(105, 32);
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;

            // SupplierEditForm
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(445, 255);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblDeliveryLeadTime);
            Controls.Add(nudDeliveryLeadTime);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            ((System.ComponentModel.ISupportInitialize)nudDeliveryLeadTime).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}