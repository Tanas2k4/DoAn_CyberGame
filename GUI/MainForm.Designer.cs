namespace GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.comboBoxZones = new System.Windows.Forms.ComboBox();
            this.btnDatmay = new System.Windows.Forms.Button();
            this.flpDatmay = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lsbOrders = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnThanhtoan = new System.Windows.Forms.Button();
            this.lblServiceInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMachineInfo2 = new System.Windows.Forms.Label();
            this.lblMachineInfo = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cbbPhanLoai = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.flpService = new System.Windows.Forms.FlowLayoutPanel();
            this.lsbDichvu = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Report = new Microsoft.Reporting.WinForms.ReportViewer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.qLCGDataSet4 = new GUI.QLCGDataSet4();
            this.ordersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ordersTableAdapter = new GUI.QLCGDataSet4TableAdapters.OrdersTableAdapter();
            this.foodItemsTableAdapter1 = new GUI.QLCGDataSet2TableAdapters.FoodItemsTableAdapter();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qLCGDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxZones
            // 
            this.comboBoxZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxZones.FormattingEnabled = true;
            this.comboBoxZones.Location = new System.Drawing.Point(8, 34);
            this.comboBoxZones.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxZones.Name = "comboBoxZones";
            this.comboBoxZones.Size = new System.Drawing.Size(262, 33);
            this.comboBoxZones.TabIndex = 3;
            this.comboBoxZones.SelectionChangeCommitted += new System.EventHandler(this.comboBoxZones_SelectedIndexChanged);
            // 
            // btnDatmay
            // 
            this.btnDatmay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDatmay.BackgroundImage")));
            this.btnDatmay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDatmay.Location = new System.Drawing.Point(895, 557);
            this.btnDatmay.Margin = new System.Windows.Forms.Padding(4);
            this.btnDatmay.Name = "btnDatmay";
            this.btnDatmay.Size = new System.Drawing.Size(123, 49);
            this.btnDatmay.TabIndex = 4;
            this.btnDatmay.UseVisualStyleBackColor = true;
            this.btnDatmay.Click += new System.EventHandler(this.btnDatmay_Click);
            // 
            // flpDatmay
            // 
            this.flpDatmay.AllowDrop = true;
            this.flpDatmay.AutoScroll = true;
            this.flpDatmay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpDatmay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flpDatmay.BackgroundImage")));
            this.flpDatmay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpDatmay.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.flpDatmay.Location = new System.Drawing.Point(8, 82);
            this.flpDatmay.Margin = new System.Windows.Forms.Padding(4);
            this.flpDatmay.Name = "flpDatmay";
            this.flpDatmay.Size = new System.Drawing.Size(868, 524);
            this.flpDatmay.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1332, 645);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage1.BackgroundImage")));
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.btnLogout);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lsbOrders);
            this.tabPage1.Controls.Add(this.btnThanhtoan);
            this.tabPage1.Controls.Add(this.lblServiceInfo);
            this.tabPage1.Controls.Add(this.comboBoxZones);
            this.tabPage1.Controls.Add(this.btnDatmay);
            this.tabPage1.Controls.Add(this.flpDatmay);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1324, 616);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Trang chủ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(371, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 53);
            this.label1.TabIndex = 11;
            this.label1.Text = "QUẢN LÝ MÁY TRẠM";
            // 
            // lsbOrders
            // 
            this.lsbOrders.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lsbOrders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lsbOrders.BackgroundImage")));
            this.lsbOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsbOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsbOrders.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbOrders.FullRowSelect = true;
            this.lsbOrders.HideSelection = false;
            this.lsbOrders.Location = new System.Drawing.Point(895, 248);
            this.lsbOrders.Margin = new System.Windows.Forms.Padding(4);
            this.lsbOrders.Name = "lsbOrders";
            this.lsbOrders.Size = new System.Drawing.Size(413, 301);
            this.lsbOrders.TabIndex = 9;
            this.lsbOrders.UseCompatibleStateImageBehavior = false;
            this.lsbOrders.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên món ";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Giá ";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Số lượng";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Thành tiền";
            this.columnHeader4.Width = 100;
            // 
            // btnThanhtoan
            // 
            this.btnThanhtoan.Location = new System.Drawing.Point(1026, 557);
            this.btnThanhtoan.Margin = new System.Windows.Forms.Padding(4);
            this.btnThanhtoan.Name = "btnThanhtoan";
            this.btnThanhtoan.Size = new System.Drawing.Size(280, 49);
            this.btnThanhtoan.TabIndex = 8;
            this.btnThanhtoan.Text = "Thanh toán";
            this.btnThanhtoan.UseVisualStyleBackColor = true;
            this.btnThanhtoan.Click += new System.EventHandler(this.btnThanhtoan_Click);
            this.btnThanhtoan.MouseEnter += new System.EventHandler(this.btnThanhtoan_MouseEnter);
            this.btnThanhtoan.MouseLeave += new System.EventHandler(this.btnThanhtoan_MouseLeave);
            // 
            // lblServiceInfo
            // 
            this.lblServiceInfo.AutoSize = true;
            this.lblServiceInfo.Location = new System.Drawing.Point(876, 277);
            this.lblServiceInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServiceInfo.Name = "lblServiceInfo";
            this.lblServiceInfo.Size = new System.Drawing.Size(0, 24);
            this.lblServiceInfo.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.Controls.Add(this.lblMachineInfo2);
            this.groupBox1.Controls.Add(this.lblMachineInfo);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Black", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(895, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 234);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "THÔNG TIN MÁY TRẠM";
            // 
            // lblMachineInfo2
            // 
            this.lblMachineInfo2.AutoSize = true;
            this.lblMachineInfo2.Location = new System.Drawing.Point(68, 104);
            this.lblMachineInfo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMachineInfo2.Name = "lblMachineInfo2";
            this.lblMachineInfo2.Size = new System.Drawing.Size(0, 23);
            this.lblMachineInfo2.TabIndex = 6;
            // 
            // lblMachineInfo
            // 
            this.lblMachineInfo.AutoSize = true;
            this.lblMachineInfo.Location = new System.Drawing.Point(43, 34);
            this.lblMachineInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMachineInfo.Name = "lblMachineInfo";
            this.lblMachineInfo.Size = new System.Drawing.Size(0, 23);
            this.lblMachineInfo.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage2.BackgroundImage")));
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.btnEdit);
            this.tabPage2.Controls.Add(this.cbbPhanLoai);
            this.tabPage2.Controls.Add(this.txtTimKiem);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.lblTotalAmount);
            this.tabPage2.Controls.Add(this.btnOrder);
            this.tabPage2.Controls.Add(this.flpService);
            this.tabPage2.Controls.Add(this.lsbDichvu);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage2.Size = new System.Drawing.Size(1324, 616);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dịch vụ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(77, 396);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 170);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEdit.Location = new System.Drawing.Point(322, 320);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(53, 43);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // cbbPhanLoai
            // 
            this.cbbPhanLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPhanLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbbPhanLoai.FormattingEnabled = true;
            this.cbbPhanLoai.Items.AddRange(new object[] {
            "Tất cả",
            "Thức ăn",
            "Giải khát",
            "Thuốc ",
            "Ăn vặt",
            "Combo"});
            this.cbbPhanLoai.Location = new System.Drawing.Point(807, 30);
            this.cbbPhanLoai.Name = "cbbPhanLoai";
            this.cbbPhanLoai.Size = new System.Drawing.Size(214, 33);
            this.cbbPhanLoai.TabIndex = 9;
            this.cbbPhanLoai.SelectedIndexChanged += new System.EventHandler(this.cbbPhanLoai_SelectedIndexChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Location = new System.Drawing.Point(1037, 30);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(269, 33);
            this.txtTimKiem.TabIndex = 8;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(184, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(443, 53);
            this.label2.TabIndex = 7;
            this.label2.Text = "QUẢN LÝ DỊCH VỤ";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblTotalAmount.Location = new System.Drawing.Point(127, 283);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(0, 16);
            this.lblTotalAmount.TabIndex = 6;
            // 
            // btnOrder
            // 
            this.btnOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOrder.BackgroundImage")));
            this.btnOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOrder.Location = new System.Drawing.Point(9, 320);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(306, 43);
            this.btnOrder.TabIndex = 1;
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // flpService
            // 
            this.flpService.AllowDrop = true;
            this.flpService.AutoScroll = true;
            this.flpService.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpService.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flpService.BackgroundImage")));
            this.flpService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpService.Location = new System.Drawing.Point(382, 70);
            this.flpService.Margin = new System.Windows.Forms.Padding(4);
            this.flpService.Name = "flpService";
            this.flpService.Size = new System.Drawing.Size(924, 522);
            this.flpService.TabIndex = 0;
            // 
            // lsbDichvu
            // 
            this.lsbDichvu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lsbDichvu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lsbDichvu.FormattingEnabled = true;
            this.lsbDichvu.ItemHeight = 20;
            this.lsbDichvu.Location = new System.Drawing.Point(10, 71);
            this.lsbDichvu.Margin = new System.Windows.Forms.Padding(4);
            this.lsbDichvu.MultiColumn = true;
            this.lsbDichvu.Name = "lsbDichvu";
            this.lsbDichvu.Size = new System.Drawing.Size(365, 244);
            this.lsbDichvu.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Report);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1324, 616);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Báo cáo";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Report
            // 
            this.Report.BackColor = System.Drawing.Color.LightGray;
            this.Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Report.Location = new System.Drawing.Point(3, 3);
            this.Report.Name = "Report";
            this.Report.ServerReport.BearerToken = null;
            this.Report.Size = new System.Drawing.Size(1318, 610);
            this.Report.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // qLCGDataSet4
            // 
            this.qLCGDataSet4.DataSetName = "QLCGDataSet4";
            this.qLCGDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ordersBindingSource
            // 
            this.ordersBindingSource.DataMember = "Orders";
            this.ordersBindingSource.DataSource = this.qLCGDataSet4;
            // 
            // ordersTableAdapter
            // 
            this.ordersTableAdapter.ClearBeforeFill = true;
            // 
            // foodItemsTableAdapter1
            // 
            this.foodItemsTableAdapter1.ClearBeforeFill = true;
            // 
            // btnLogout
            // 
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogout.BackgroundImage")));
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogout.Location = new System.Drawing.Point(291, 34);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(35, 33);
            this.btnLogout.TabIndex = 12;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 645);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Nét cỏ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qLCGDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxZones;
        private System.Windows.Forms.Button btnDatmay;
        private System.Windows.Forms.FlowLayoutPanel flpDatmay;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.FlowLayoutPanel flpService;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.ListBox lsbDichvu;
        private System.Windows.Forms.Label lblMachineInfo;
        private System.Windows.Forms.Label lblServiceInfo;
        private System.Windows.Forms.Button btnThanhtoan;
        private QLCGDataSet4 qLCGDataSet4;
        private System.Windows.Forms.BindingSource ordersBindingSource;
        private QLCGDataSet4TableAdapters.OrdersTableAdapter ordersTableAdapter;
        private System.Windows.Forms.ListView lsbOrders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMachineInfo2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ComboBox cbbPhanLoai;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer Report;
        private System.Windows.Forms.PictureBox pictureBox1;
        private QLCGDataSet2TableAdapters.FoodItemsTableAdapter foodItemsTableAdapter1;
        private System.Windows.Forms.Button btnLogout;
    }
}