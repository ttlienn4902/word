
namespace quan_ly_thu_vien
{
    partial class frmbaocao9
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
            this.btnIn = new System.Windows.Forms.Button();
            this.btnLamLai = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMaTheMuonBC = new System.Windows.Forms.ComboBox();
            this.btnTimKiemBC = new System.Windows.Forms.Button();
            this.dataGridViewHSMtheoTM = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHSMtheoTM)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(589, 118);
            this.btnIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(82, 26);
            this.btnIn.TabIndex = 23;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnLamLai
            // 
            this.btnLamLai.Location = new System.Drawing.Point(466, 118);
            this.btnLamLai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLamLai.Name = "btnLamLai";
            this.btnLamLai.Size = new System.Drawing.Size(89, 26);
            this.btnLamLai.TabIndex = 22;
            this.btnLamLai.Text = "Làm Lại";
            this.btnLamLai.UseVisualStyleBackColor = true;
            this.btnLamLai.Click += new System.EventHandler(this.btnLamLai_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(258, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(279, 31);
            this.label2.TabIndex = 21;
            this.label2.Text = "Tìm Kiếm Hồ Sơ Mượn ";
            // 
            // cbMaTheMuonBC
            // 
            this.cbMaTheMuonBC.FormattingEnabled = true;
            this.cbMaTheMuonBC.Location = new System.Drawing.Point(177, 118);
            this.cbMaTheMuonBC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbMaTheMuonBC.Name = "cbMaTheMuonBC";
            this.cbMaTheMuonBC.Size = new System.Drawing.Size(108, 24);
            this.cbMaTheMuonBC.TabIndex = 20;
            // 
            // btnTimKiemBC
            // 
            this.btnTimKiemBC.Location = new System.Drawing.Point(354, 118);
            this.btnTimKiemBC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTimKiemBC.Name = "btnTimKiemBC";
            this.btnTimKiemBC.Size = new System.Drawing.Size(78, 26);
            this.btnTimKiemBC.TabIndex = 19;
            this.btnTimKiemBC.Text = "Tìm kiếm ";
            this.btnTimKiemBC.UseVisualStyleBackColor = true;
            this.btnTimKiemBC.Click += new System.EventHandler(this.btnTimKiemBC_Click);
            // 
            // dataGridViewHSMtheoTM
            // 
            this.dataGridViewHSMtheoTM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHSMtheoTM.Location = new System.Drawing.Point(46, 185);
            this.dataGridViewHSMtheoTM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewHSMtheoTM.Name = "dataGridViewHSMtheoTM";
            this.dataGridViewHSMtheoTM.RowHeadersWidth = 62;
            this.dataGridViewHSMtheoTM.RowTemplate.Height = 28;
            this.dataGridViewHSMtheoTM.Size = new System.Drawing.Size(708, 210);
            this.dataGridViewHSMtheoTM.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Mã thẻ mượn ";
            // 
            // frmbaocao9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnLamLai);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMaTheMuonBC);
            this.Controls.Add(this.btnTimKiemBC);
            this.Controls.Add(this.dataGridViewHSMtheoTM);
            this.Controls.Add(this.label1);
            this.Name = "frmbaocao9";
            this.Text = "FrmBaoCao9";
            this.Load += new System.EventHandler(this.frmbaocao9_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHSMtheoTM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnLamLai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMaTheMuonBC;
        private System.Windows.Forms.Button btnTimKiemBC;
        private System.Windows.Forms.DataGridView dataGridViewHSMtheoTM;
        private System.Windows.Forms.Label label1;
    }
}