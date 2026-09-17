namespace MiniSupermarket.WinForms
{
    partial class FormRoleManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblTitle = new Label();

            this.grpRoleList = new GroupBox();
            this.dgvRoles = new DataGridView();

            this.grpRoleInfo = new GroupBox();

            this.lblId = new Label();
            this.txtId = new TextBox();

            this.lblRoleName = new Label();
            this.txtRoleName = new TextBox();

            this.lblDescription = new Label();
            this.txtDescription = new TextBox();

            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnLoad = new Button();

            this.grpRoleList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();

            this.grpRoleInfo.SuspendLayout();

            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font(
                "Segoe UI",
                18F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            this.lblTitle.Location = new Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(260, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ ROLE";


            // 
            // grpRoleList
            // 
            this.grpRoleList.Controls.Add(this.dgvRoles);
            this.grpRoleList.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            this.grpRoleList.Location = new Point(25, 70);
            this.grpRoleList.Name = "grpRoleList";
            this.grpRoleList.Size = new Size(650, 470);
            this.grpRoleList.TabIndex = 1;
            this.grpRoleList.TabStop = false;
            this.grpRoleList.Text = "Danh sách Role";

            // 
            // dgvRoles
            // 
            this.dgvRoles.AllowUserToAddRows = false;
            this.dgvRoles.AllowUserToDeleteRows = false;
            this.dgvRoles.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvRoles.BackgroundColor = SystemColors.Window;
            this.dgvRoles.BorderStyle = BorderStyle.Fixed3D;

            this.dgvRoles.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvRoles.Dock = DockStyle.Fill;

            this.dgvRoles.Location = new Point(3, 26);
            this.dgvRoles.MultiSelect = false;
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.ReadOnly = true;
            this.dgvRoles.RowHeadersVisible = false;

            this.dgvRoles.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            this.dgvRoles.Size = new Size(644, 441);
            this.dgvRoles.TabIndex = 0;

            // 
            // grpRoleInfo
            // 
            this.grpRoleInfo.Controls.Add(this.lblId);
            this.grpRoleInfo.Controls.Add(this.txtId);

            this.grpRoleInfo.Controls.Add(this.lblRoleName);
            this.grpRoleInfo.Controls.Add(this.txtRoleName);

            this.grpRoleInfo.Controls.Add(this.lblDescription);
            this.grpRoleInfo.Controls.Add(this.txtDescription);

            this.grpRoleInfo.Controls.Add(this.btnAdd);
            this.grpRoleInfo.Controls.Add(this.btnUpdate);
            this.grpRoleInfo.Controls.Add(this.btnDelete);
            this.grpRoleInfo.Controls.Add(this.btnLoad);

            this.grpRoleInfo.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            this.grpRoleInfo.Location = new Point(700, 70);
            this.grpRoleInfo.Name = "grpRoleInfo";
            this.grpRoleInfo.Size = new Size(400, 470);
            this.grpRoleInfo.TabIndex = 2;
            this.grpRoleInfo.TabStop = false;
            this.grpRoleInfo.Text = "Thông tin Role";

            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.lblId.Location = new Point(25, 45);
            this.lblId.Name = "lblId";
            this.lblId.Size = new Size(45, 19);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Mã ID";

            // 
            // txtId
            // 
            this.txtId.Location = new Point(25, 70);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new Size(350, 25);
            this.txtId.TabIndex = 1;

            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.lblRoleName.Location = new Point(25, 115);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new Size(76, 19);
            this.lblRoleName.TabIndex = 2;
            this.lblRoleName.Text = "Tên Role";

            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new Point(25, 140);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new Size(350, 25);
            this.txtRoleName.TabIndex = 3;

            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.lblDescription.Location = new Point(25, 185);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(47, 19);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Mô tả";

            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Point(25, 210);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars =
                ScrollBars.Vertical;

            this.txtDescription.Size = new Size(350, 100);
            this.txtDescription.TabIndex = 5;

            // 
            // btnAdd
            // 
            this.btnAdd.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.btnAdd.Location = new Point(25, 335);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(105, 40);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;

            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.btnUpdate.Location = new Point(145, 335);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(105, 40);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;

            // 
            // btnDelete
            // 
            this.btnDelete.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.btnDelete.Location = new Point(265, 335);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(110, 40);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;

            // 
            // btnLoad
            // 
            this.btnLoad.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            this.btnLoad.Location = new Point(25, 395);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(350, 40);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "Tải lại danh sách";
            this.btnLoad.UseVisualStyleBackColor = true;

            // 
            // FormRoleManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1125, 570);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpRoleList);
            this.Controls.Add(this.grpRoleInfo);

            this.Name = "FormRoleManagement";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý Role";

            // Events
            this.Load += new EventHandler(
                this.FormRoleManagement_Load);

            this.btnLoad.Click += new EventHandler(
                this.btnLoad_Click);

            this.btnAdd.Click += new EventHandler(
                this.btnAdd_Click);

            this.btnUpdate.Click += new EventHandler(
                this.btnUpdate_Click);

            this.btnDelete.Click += new EventHandler(
                this.btnDelete_Click);

            this.dgvRoles.CellClick += new DataGridViewCellEventHandler(
                this.dgvRoles_CellClick);

            this.grpRoleList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvRoles)).EndInit();

            this.grpRoleInfo.ResumeLayout(false);
            this.grpRoleInfo.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitle;

        private GroupBox grpRoleList;
        private DataGridView dgvRoles;

        private GroupBox grpRoleInfo;

        private Label lblId;
        private TextBox txtId;

        private Label lblRoleName;
        private TextBox txtRoleName;

        private Label lblDescription;
        private TextBox txtDescription;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnLoad;
    }
}