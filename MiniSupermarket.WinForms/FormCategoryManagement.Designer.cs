namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.GroupBox groupBoxCategories;
        private System.Windows.Forms.DataGridView dgvCategories;

        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.Label lblDescription;

        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.TextBox txtDescription;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            groupBoxSearch = new GroupBox();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            groupBoxCategories = new GroupBox();
            dgvCategories = new DataGridView();
            groupBoxInfo = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblCategoryName = new Label();
            txtCategoryName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            groupBoxSearch.SuspendLayout();
            groupBoxCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            groupBoxInfo.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxSearch
            // 
            groupBoxSearch.Controls.Add(txtKeyword);
            groupBoxSearch.Controls.Add(btnSearch);
            groupBoxSearch.Controls.Add(btnLoad);
            groupBoxSearch.Location = new Point(12, 12);
            groupBoxSearch.Name = "groupBoxSearch";
            groupBoxSearch.Size = new Size(760, 65);
            groupBoxSearch.TabIndex = 0;
            groupBoxSearch.TabStop = false;
            groupBoxSearch.Text = "Tìm kiếm";
            // 
            // txtKeyword
            // 
            txtKeyword.BorderStyle = BorderStyle.FixedSingle;
            txtKeyword.Location = new Point(15, 25);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(450, 23);
            txtKeyword.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(480, 23);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(90, 27);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(580, 23);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(90, 27);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // groupBoxCategories
            // 
            groupBoxCategories.Controls.Add(dgvCategories);
            groupBoxCategories.Location = new Point(12, 90);
            groupBoxCategories.Name = "groupBoxCategories";
            groupBoxCategories.Size = new Size(470, 360);
            groupBoxCategories.TabIndex = 1;
            groupBoxCategories.TabStop = false;
            groupBoxCategories.Text = "Danh sách Nhóm hàng";
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = SystemColors.Window;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(14, 22);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersWidth = 30;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(450, 320);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            dgvCategories.CellContentClick += dgvCategories_CellContentClick;
            // 
            // groupBoxInfo
            // 
            groupBoxInfo.Controls.Add(lblId);
            groupBoxInfo.Controls.Add(txtId);
            groupBoxInfo.Controls.Add(lblCategoryName);
            groupBoxInfo.Controls.Add(txtCategoryName);
            groupBoxInfo.Controls.Add(lblDescription);
            groupBoxInfo.Controls.Add(txtDescription);
            groupBoxInfo.Controls.Add(btnAdd);
            groupBoxInfo.Controls.Add(btnUpdate);
            groupBoxInfo.Controls.Add(btnDelete);
            groupBoxInfo.Location = new Point(495, 90);
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.Size = new Size(277, 360);
            groupBoxInfo.TabIndex = 2;
            groupBoxInfo.TabStop = false;
            groupBoxInfo.Text = "Thông tin Nhóm hàng";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(15, 30);
            lblId.Name = "lblId";
            lblId.Size = new Size(38, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Mã ID";
            // 
            // txtId
            // 
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.Location = new Point(15, 50);
            txtId.Name = "txtId";
            txtId.ReadOnly = false;
            txtId.Size = new Size(245, 23);
            txtId.TabIndex = 1;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(15, 85);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(92, 15);
            lblCategoryName.TabIndex = 2;
            lblCategoryName.Text = "Tên Nhóm hàng";
            // 
            // txtCategoryName
            // 
            txtCategoryName.BorderStyle = BorderStyle.FixedSingle;
            txtCategoryName.Location = new Point(15, 105);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(245, 23);
            txtCategoryName.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(15, 140);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(40, 15);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô Tả";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Location = new Point(15, 160);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(245, 80);
            txtDescription.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 260);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(70, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(95, 260);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(180, 260);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(70, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 471);
            Controls.Add(groupBoxSearch);
            Controls.Add(groupBoxCategories);
            Controls.Add(groupBoxInfo);
            Name = "FormCategoryManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Danh mục Nhóm hàng";
            Load += FormCategoryManagement_Load;
            groupBoxSearch.ResumeLayout(false);
            groupBoxSearch.PerformLayout();
            groupBoxCategories.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            groupBoxInfo.ResumeLayout(false);
            groupBoxInfo.PerformLayout();
            ResumeLayout(false);
        }
    }
}