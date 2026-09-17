using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormRoleManagement : Form
    {
        // HttpClient dùng để kết nối đến Web API
        // Port 7147 phải đúng với port API đang chạy
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7147/api/")
        };

        // Constructor của Form
        public FormRoleManagement()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // Khi Form vừa mở, tự động tải danh sách Role
        // =========================================================
        private async void FormRoleManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadDataAsync();
        }

        // =========================================================
        // LOAD DATA
        // Gọi API GET /api/roles
        // Sau đó đưa dữ liệu vào DataGridView
        // =========================================================
        private async Task LoadDataAsync()
        {
            try
            {
                var roles =
                    await _client.GetFromJsonAsync<List<RoleDto>>(
                        "roles");

                dgvRoles.DataSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // BUTTON LOAD
        // Nút Tải lại
        // =========================================================
        private async void btnLoad_Click(
            object sender,
            EventArgs e)
        {
            await LoadDataAsync();
        }

        // =========================================================
        // DATAGRIDVIEW CELL CLICK
        // Khi chọn một dòng Role:
        // Đưa dữ liệu từ DataGridView lên các TextBox
        // =========================================================
        private void dgvRoles_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // Không xử lý nếu click vào phần header
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvRoles.Rows[e.RowIndex];

            // Lấy ID
            txtId.Text =
                row.Cells["RoleId"].Value?.ToString()
                ?? string.Empty;

            // Lấy tên Role
            txtRoleName.Text =
                row.Cells["RoleName"].Value?.ToString()
                ?? string.Empty;

            // Lấy mô tả
            txtDescription.Text =
                row.Cells["Description"].Value?.ToString()
                ?? string.Empty;
        }

        // =========================================================
        // BUTTON ADD
        // Thêm Role mới
        //
        // POST /api/roles
        // =========================================================
        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            // Kiểm tra tên Role
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên Role!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRoleName.Focus();
                return;
            }

            // Tạo dữ liệu gửi lên API
            var newRole = new
            {
                RoleName = txtRoleName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            try
            {
                // Gửi POST /api/roles
                var response =
                    await _client.PostAsJsonAsync(
                        "roles",
                        newRole);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm Role thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Tải lại danh sách
                    await LoadDataAsync();

                    // Xóa dữ liệu trong TextBox
                    ClearInputs();
                }
                else
                {
                    // Lấy nội dung lỗi từ API nếu có
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thêm Role thất bại!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi thêm Role:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // BUTTON UPDATE
        // Cập nhật Role
        //
        // PUT /api/roles/{id}
        // =========================================================
        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            // Kiểm tra đã chọn Role chưa
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn Role cần sửa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Kiểm tra ID có phải số không
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // Kiểm tra tên Role
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên Role!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRoleName.Focus();
                return;
            }

            // Tạo dữ liệu cập nhật
            var updateRole = new
            {
                RoleId = id,
                RoleName = txtRoleName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            try
            {
                // PUT /api/roles/{id}
                var response =
                    await _client.PutAsJsonAsync(
                        $"roles/{id}",
                        updateRole);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật Role thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Tải lại danh sách
                    await LoadDataAsync();

                    // Xóa TextBox
                    ClearInputs();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Cập nhật Role thất bại!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cập nhật Role:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // BUTTON DELETE
        // Xóa Role
        //
        // DELETE /api/roles/{id}
        // =========================================================
        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            // Kiểm tra đã chọn Role chưa
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn Role cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Kiểm tra ID
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // Hỏi xác nhận
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa Role ID = {id}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                // DELETE /api/roles/{id}
                var response =
                    await _client.DeleteAsync(
                        $"roles/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa Role thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Tải lại danh sách
                    await LoadDataAsync();

                    // Xóa TextBox
                    ClearInputs();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Xóa Role thất bại!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi xóa Role:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CLEAR INPUTS
        // Xóa dữ liệu trên các TextBox
        // =========================================================
        private void ClearInputs()
        {
            txtId.Text = string.Empty;
            txtRoleName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            txtRoleName.Focus();
        }
    }

    // =============================================================
    // ROLE DTO
    // Dùng để nhận dữ liệu JSON từ API
    // =============================================================
    public class RoleDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
            = string.Empty;

        public string? Description { get; set; }
    }
}