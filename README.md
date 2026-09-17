# MiniSupermarket.API# MiniSupermarket

Ứng dụng quản lý nhóm hàng gồm:

-   `MiniSupermarket.API`: ASP.NET Core Web API
-   `MiniSupermarket.WinForms`: Windows Forms Client
-   Giao tiếp giữa WinForms và API bằng HTTP/JSON
-   Chức năng: xem danh sách, tìm kiếm, thêm, cập nhật, xóa nhóm hàng.

------------------------------------------------------------------------

# 1. Cấu trúc project

``` text
Solution 'MiniSupermarket.API'
│
├── MiniSupermarket.API
│   ├── Connected Services
│   ├── Dependencies
│   ├── Properties
│   │   └── launchSettings.json
│   ├── Controllers
│   │   ├── CategoriesController.cs
│   │   └── WeatherForecastController.cs
│   ├── Models
│   │   └── Category.cs
│   ├── appsettings.json
│   ├── MiniSupermarket.API.http
│   ├── Program.cs
│   └── WeatherForecast.cs
│
└── MiniSupermarket.WinForms
    ├── Dependencies
    ├── FormCategoryManagement.cs
    ├── FormCategoryManagement.Designer.cs
    └── Program.cs
```

## Vai trò từng file

  --------------------------------------------------------------------------
  File                                   Chức năng
  -------------------------------------- -----------------------------------
  `Program.cs` trong API                 Khởi động Web API, cấu hình service
                                         và middleware

  `CategoriesController.cs`              Xử lý API CRUD nhóm hàng

  `Category.cs`                          Model dữ liệu nhóm hàng

  `appsettings.json`                     Cấu hình ứng dụng, ví dụ connection
                                         string

  `launchSettings.json`                  Cấu hình URL/port khi chạy API

  `FormCategoryManagement.cs`            Code xử lý chức năng của Form

  `FormCategoryManagement.Designer.cs`   Code tạo giao diện WinForms

  `Program.cs` trong WinForms            Khởi động ứng dụng WinForms
  --------------------------------------------------------------------------

------------------------------------------------------------------------

# 2. Kiến trúc hoạt động

``` text
SQL Server / Database
        ↑
        │
        │ Entity Framework / Data Access
        │
MiniSupermarket.API
        │
        │ HTTP + JSON
        ↓
MiniSupermarket.WinForms
        │
        ↓
FormCategoryManagement
        │
        ├── txtKeyword
        ├── dgvCategories
        ├── txtId
        ├── txtCategoryName
        ├── txtDescription
        ├── btnSearch
        ├── btnLoad
        ├── btnAdd
        ├── btnUpdate
        └── btnDelete
```

WinForms không thao tác trực tiếp với database. Form gửi HTTP request
đến Web API. API xử lý dữ liệu và trả JSON về cho WinForms.

------------------------------------------------------------------------

# 3. MiniSupermarket.API

## 3.1 `Program.cs`

`Program.cs` là điểm khởi động của ASP.NET Core Web API.

Thông thường file này có nhiệm vụ:

``` csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### Giải thích

``` csharp
var builder = WebApplication.CreateBuilder(args);
```

Tạo đối tượng builder để cấu hình Web API.

``` csharp
builder.Services.AddControllers();
```

Đăng ký hệ thống Controller.

Nhờ đó `CategoriesController.cs` có thể nhận request:

``` text
GET    /api/categories
POST   /api/categories
PUT    /api/categories/1
DELETE /api/categories/1
```

``` csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

Đăng ký Swagger/OpenAPI để kiểm tra API trên trình duyệt.

``` csharp
app.UseHttpsRedirection();
```

Chuyển hướng request HTTP sang HTTPS nếu cấu hình yêu cầu.

``` csharp
app.UseAuthorization();
```

Bật middleware authorization.

``` csharp
app.MapControllers();
```

Ánh xạ các Controller thành endpoint API.

``` csharp
app.Run();
```

Khởi động Web API.

------------------------------------------------------------------------

# 4. `Models/Category.cs`

`Category.cs` là model đại diện cho nhóm hàng.

Ví dụ:

``` csharp
public class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }
}
```

## Ý nghĩa từng thuộc tính

``` csharp
public int CategoryId { get; set; }
```

Mã định danh của nhóm hàng.

Ví dụ:

``` text
1
2
3
```

``` csharp
public string CategoryName { get; set; } = string.Empty;
```

Tên nhóm hàng.

Ví dụ:

``` text
Bánh kẹo & đồ ăn vặt
Nước giải khát & Trà
Sữa & Sản phẩm từ Sữa
```

``` csharp
public string? Description { get; set; }
```

Mô tả nhóm hàng.

Dấu `?` cho phép giá trị `null`.

------------------------------------------------------------------------

# 5. `Controllers/CategoriesController.cs`

Đây là file quan trọng nhất ở Backend đối với chức năng quản lý nhóm
hàng.

Controller nhận request từ WinForms, xử lý dữ liệu và trả response.

Một Controller CRUD thường có các nhóm hàm:

``` text
GET       → lấy dữ liệu
POST      → thêm
PUT       → cập nhật
DELETE    → xóa
```

------------------------------------------------------------------------

## 5.1 GET danh sách

Endpoint:

``` http
GET /api/categories
```

Ví dụ:

``` csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
{
    var categories = await _context.Categories.ToListAsync();

    return Ok(categories);
}
```

### Giải thích

``` csharp
[HttpGet]
```

Cho biết hàm này xử lý HTTP GET.

``` csharp
_context.Categories
```

Truy cập tập dữ liệu Category.

``` csharp
.ToListAsync()
```

Lấy toàn bộ danh sách.

``` csharp
return Ok(categories);
```

Trả HTTP 200 và dữ liệu JSON.

WinForms nhận kết quả này bằng:

``` csharp
_client.GetFromJsonAsync<List<CategoryDto>>("categories");
```

------------------------------------------------------------------------

# 6. POST - Thêm nhóm hàng

Endpoint:

``` http
POST /api/categories
```

Ví dụ request:

``` json
{
    "categoryName": "Bánh kẹo",
    "description": "Các loại bánh và kẹo"
}
```

Controller có thể xử lý:

``` csharp
[HttpPost]
public async Task<ActionResult<Category>> PostCategory(Category category)
{
    _context.Categories.Add(category);

    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(GetCategories),
        new { id = category.CategoryId },
        category
    );
}
```

### Giải thích

``` csharp
_context.Categories.Add(category);
```

Đưa category mới vào DbContext.

``` csharp
await _context.SaveChangesAsync();
```

Lưu thay đổi xuống database.

``` csharp
return CreatedAtAction(...);
```

Trả response thành công, thường là HTTP 201.

------------------------------------------------------------------------

# 7. PUT - Cập nhật nhóm hàng

Endpoint:

``` http
PUT /api/categories/{id}
```

Ví dụ:

``` http
PUT /api/categories/1
```

Request:

``` json
{
    "categoryId": 1,
    "categoryName": "Bánh kẹo và đồ ăn vặt",
    "description": "Bánh, kẹo, snack"
}
```

Ví dụ Controller:

``` csharp
[HttpPut("{id}")]
public async Task<IActionResult> PutCategory(
    int id,
    Category category)
{
    if (id != category.CategoryId)
    {
        return BadRequest();
    }

    _context.Entry(category).State =
        EntityState.Modified;

    await _context.SaveChangesAsync();

    return NoContent();
}
```

### Giải thích

``` csharp
int id
```

ID lấy từ URL.

``` csharp
category.CategoryId
```

ID nằm trong dữ liệu JSON.

``` csharp
if (id != category.CategoryId)
```

Kiểm tra ID URL và ID dữ liệu có khớp không.

``` csharp
EntityState.Modified
```

Đánh dấu đối tượng là dữ liệu cần cập nhật.

``` csharp
SaveChangesAsync()
```

Lưu thay đổi.

------------------------------------------------------------------------

# 8. DELETE - Xóa nhóm hàng

Endpoint:

``` http
DELETE /api/categories/{id}
```

Ví dụ:

``` http
DELETE /api/categories/1
```

Controller có thể xử lý:

``` csharp
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteCategory(int id)
{
    var category = await _context.Categories.FindAsync(id);

    if (category == null)
    {
        return NotFound();
    }

    _context.Categories.Remove(category);

    await _context.SaveChangesAsync();

    return NoContent();
}
```

### Giải thích

``` csharp
FindAsync(id)
```

Tìm category theo ID.

``` csharp
if (category == null)
```

Không tìm thấy thì trả 404.

``` csharp
_context.Categories.Remove(category);
```

Đánh dấu bản ghi cần xóa.

``` csharp
SaveChangesAsync()
```

Thực hiện xóa trong database.

------------------------------------------------------------------------

# 9. API tìm kiếm

Endpoint được WinForms sử dụng:

``` http
GET /api/categories/search?keyword=Bánh
```

Ví dụ Controller:

``` csharp
[HttpGet("search")]
public async Task<ActionResult<IEnumerable<Category>>> Search(
    string keyword)
{
    var result = await _context.Categories
        .Where(x =>
            x.CategoryName.Contains(keyword) ||
            (x.Description != null &&
             x.Description.Contains(keyword)))
        .ToListAsync();

    return Ok(result);
}
```

### Giải thích

``` csharp
[HttpGet("search")]
```

Tạo endpoint:

``` text
/api/categories/search
```

``` csharp
string keyword
```

Nhận từ khóa từ query string.

Ví dụ:

``` text
?keyword=Bánh
```

``` csharp
Contains(keyword)
```

Kiểm tra tên hoặc mô tả có chứa từ khóa.

------------------------------------------------------------------------

# 10. MiniSupermarket.WinForms

Project WinForms chịu trách nhiệm hiển thị giao diện và gửi request đến
API.

File chính:

``` text
FormCategoryManagement.cs
FormCategoryManagement.Designer.cs
```

------------------------------------------------------------------------

# 11. `FormCategoryManagement.Designer.cs`

File Designer chứa code tạo giao diện.

Không nên viết logic API vào Designer.

Các control chính:

  Control        Name                Chức năng
  -------------- ------------------- ---------------------
  TextBox        `txtKeyword`        Nhập từ khóa
  Button         `btnSearch`         Tìm kiếm
  Button         `btnLoad`           Tải lại
  DataGridView   `dgvCategories`     Danh sách nhóm hàng
  TextBox        `txtId`             Mã nhóm hàng
  TextBox        `txtCategoryName`   Tên nhóm hàng
  TextBox        `txtDescription`    Mô tả
  Button         `btnAdd`            Thêm
  Button         `btnUpdate`         Cập nhật
  Button         `btnDelete`         Xóa

------------------------------------------------------------------------

# 12. `FormCategoryManagement.cs`

Đây là file chứa logic của Form.

Code chính sử dụng:

``` csharp
using System.Net.Http.Json;
using System.Windows.Forms;
```

`System.Net.Http.Json` cho phép gửi và nhận JSON trực tiếp bằng
`HttpClient`.

------------------------------------------------------------------------

# 13. HttpClient

Code:

``` csharp
private static readonly HttpClient _client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7147/api/")
};
```

### Chức năng

Tạo HTTP client để WinForms giao tiếp với Web API.

`BaseAddress` là địa chỉ gốc.

``` text
https://localhost:7147/api/
```

Do đó:

``` csharp
_client.GetFromJsonAsync(...)
```

với:

``` text
"categories"
```

sẽ thành:

``` text
https://localhost:7147/api/categories
```

------------------------------------------------------------------------

# 14. Constructor

Code:

``` csharp
public FormCategoryManagement()
{
    InitializeComponent();
}
```

### Chức năng

Khởi tạo Form.

``` csharp
InitializeComponent();
```

được tạo trong Designer và dùng để tạo toàn bộ control:

-   TextBox
-   Button
-   DataGridView
-   GroupBox
-   Label

------------------------------------------------------------------------

# 15. Khi Form mở

Code:

``` csharp
private async void FormCategoryManagement_Load(
    object sender,
    EventArgs e)
{
    await LoadDataAsync();
}
```

### Chức năng

Khi Form được mở, gọi:

``` csharp
LoadDataAsync();
```

để lấy danh sách nhóm hàng từ API.

Luồng:

``` text
Mở Form
   ↓
Form Load
   ↓
LoadDataAsync()
   ↓
GET /api/categories
   ↓
API trả JSON
   ↓
DataGridView hiển thị
```

------------------------------------------------------------------------

# 16. Hàm `LoadDataAsync`

Code:

``` csharp
private async Task LoadDataAsync()
{
    try
    {
        var categories =
            await _client.GetFromJsonAsync<List<CategoryDto>>(
                "categories");

        dgvCategories.DataSource = categories;
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
```

### Chức năng

Dùng chung cho:

-   mở Form
-   tải lại
-   sau khi thêm
-   sau khi cập nhật
-   sau khi xóa

### Dòng quan trọng

``` csharp
GetFromJsonAsync<List<CategoryDto>>("categories")
```

Gửi:

``` http
GET https://localhost:7147/api/categories
```

và chuyển JSON thành:

``` text
List<CategoryDto>
```

Sau đó:

``` csharp
dgvCategories.DataSource = categories;
```

đưa danh sách vào DataGridView.

------------------------------------------------------------------------

# 17. CategoryDto

Code:

``` csharp
public class CategoryDto
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
        = string.Empty;

    public string? Description { get; set; }
}
```

### Chức năng

DTO là lớp dùng để nhận dữ liệu JSON từ API.

Ví dụ API trả:

``` json
[
    {
        "categoryId": 1,
        "categoryName": "Bánh kẹo",
        "description": "Bánh và kẹo"
    }
]
```

JSON được chuyển thành:

``` text
CategoryDto
    CategoryId = 1
    CategoryName = "Bánh kẹo"
    Description = "Bánh và kẹo"
```

------------------------------------------------------------------------

# 18. DataGridView

Control:

``` text
dgvCategories
```

Code:

``` csharp
dgvCategories.DataSource = categories;
```

### Chức năng

Hiển thị danh sách Category.

Các property của DTO có thể trở thành cột:

``` text
CategoryId
CategoryName
Description
```

Trong Designer có thể đổi HeaderText để hiển thị:

``` text
Mã ID
Tên Nhóm hàng
Mô Tả
```

------------------------------------------------------------------------

# 19. Click vào một dòng

Code:

``` csharp
private void dgvCategories_CellClick(
    object sender,
    DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row =
            dgvCategories.Rows[e.RowIndex];

        txtId.Text =
            row.Cells["CategoryId"].Value?.ToString() ?? "";

        txtCategoryName.Text =
            row.Cells["CategoryName"].Value?.ToString() ?? "";

        txtDescription.Text =
            row.Cells["Description"].Value?.ToString() ?? "";
    }
}
```

### Chức năng

Khi click một dòng:

``` text
DataGridView
      ↓
Lấy dữ liệu dòng
      ↓
Đưa sang TextBox
```

Ví dụ:

``` text
DataGridView:

1 | Bánh kẹo | Bánh và kẹo
```

sẽ đưa sang:

``` text
txtId           = 1
txtCategoryName = Bánh kẹo
txtDescription  = Bánh và kẹo
```

Mục đích là để người dùng có thể sửa hoặc xóa dòng đó.

------------------------------------------------------------------------

# 20. Nút Tải lại

Code:

``` csharp
private async void btnLoad_Click(
    object sender,
    EventArgs e)
{
    await LoadDataAsync();
}
```

### Chức năng

Gọi lại:

``` http
GET /api/categories
```

để lấy dữ liệu mới nhất.

------------------------------------------------------------------------

# 21. Nút Thêm

Code:

``` csharp
private async void btnAdd_Click(
    object sender,
    EventArgs e)
{
    var newCat = new
    {
        CategoryName = txtCategoryName.Text,
        Description = txtDescription.Text
    };

    var response =
        await _client.PostAsJsonAsync(
            "categories",
            newCat);

    if (response.IsSuccessStatusCode)
    {
        MessageBox.Show(
            "Thêm mới thành công!",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        await LoadDataAsync();
        ClearInputs();
    }
    else
    {
        MessageBox.Show(
            "Thêm mới thất bại!",
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }
}
```

### Chức năng

Lấy dữ liệu từ:

``` text
txtCategoryName
txtDescription
```

tạo object:

``` csharp
var newCat = new
{
    CategoryName = txtCategoryName.Text,
    Description = txtDescription.Text
};
```

Sau đó gửi POST:

``` csharp
PostAsJsonAsync("categories", newCat);
```

Request thực tế:

``` text
POST https://localhost:7147/api/categories
```

Body:

``` json
{
    "categoryName": "...",
    "description": "..."
}
```

Nếu thành công:

``` csharp
await LoadDataAsync();
```

tải lại bảng.

``` csharp
ClearInputs();
```

xóa nội dung TextBox.

------------------------------------------------------------------------

# 22. Nút Cập nhật

Code:

``` csharp
private async void btnUpdate_Click(
    object sender,
    EventArgs e)
{
    if (string.IsNullOrEmpty(txtId.Text))
    {
        MessageBox.Show(
            "Vui lòng chọn nhóm hàng cần sửa!",
            "Cảnh báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
    }

    int id = int.Parse(txtId.Text);

    var updateCat = new
    {
        CategoryId = id,
        CategoryName = txtCategoryName.Text,
        Description = txtDescription.Text
    };

    var response =
        await _client.PutAsJsonAsync(
            $"categories/{id}",
            updateCat);

    if (response.IsSuccessStatusCode)
    {
        MessageBox.Show(
            "Cập nhật thành công!",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        await LoadDataAsync();
        ClearInputs();
    }
    else
    {
        MessageBox.Show(
            "Cập nhật thất bại!",
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }
}
```

### Luồng

``` text
Click dòng
   ↓
txtId nhận ID
   ↓
Sửa tên / mô tả
   ↓
Click Cập nhật
   ↓
PUT /api/categories/{id}
   ↓
API cập nhật database
   ↓
LoadDataAsync()
```

### `int.Parse`

``` csharp
int id = int.Parse(txtId.Text);
```

Chuyển ID từ chuỗi sang số nguyên.

Ví dụ:

``` text
"5" → 5
```

Vì vậy `txtId` không nên chứa:

``` text
qwe
abc
hello
```

Nếu ID do database tự tạo, nên đặt:

``` csharp
txtId.ReadOnly = true;
```

và lấy ID bằng cách click dòng trong DataGridView.

------------------------------------------------------------------------

# 23. Nút Xóa

Code:

``` csharp
private async void btnDelete_Click(
    object sender,
    EventArgs e)
{
    if (string.IsNullOrEmpty(txtId.Text))
    {
        MessageBox.Show(
            "Vui lòng chọn nhóm hàng cần xóa!",
            "Cảnh báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
    }

    int id = int.Parse(txtId.Text);

    var confirm = MessageBox.Show(
        $"Bạn có chắc muốn xóa nhóm hàng ID = {id}?",
        "Xác nhận",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

    if (confirm == DialogResult.Yes)
    {
        var response =
            await _client.DeleteAsync(
                $"categories/{id}");

        if (response.IsSuccessStatusCode)
        {
            MessageBox.Show(
                "Xóa thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            await LoadDataAsync();
            ClearInputs();
        }
        else
        {
            MessageBox.Show(
                "Xóa thất bại!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
```

### Chức năng

Kiểm tra đã chọn ID chưa.

Sau đó hỏi xác nhận.

Nếu chọn Yes:

``` csharp
DeleteAsync($"categories/{id}");
```

Ví dụ ID = 3:

``` http
DELETE https://localhost:7147/api/categories/3
```

------------------------------------------------------------------------

# 24. Nút Tìm kiếm

Code:

``` csharp
private async void btnSearch_Click(
    object sender,
    EventArgs e)
{
    string keyword = txtKeyword.Text.Trim();

    if (string.IsNullOrEmpty(keyword))
    {
        await LoadDataAsync();
        return;
    }

    try
    {
        var result =
            await _client.GetFromJsonAsync<List<CategoryDto>>(
                $"categories/search?keyword={keyword}");

        dgvCategories.DataSource = result;
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Không tìm thấy kết quả phù hợp!",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}
```

### Chức năng

Lấy từ khóa:

``` csharp
string keyword = txtKeyword.Text.Trim();
```

Ví dụ người dùng nhập:

``` text
Bánh
```

Request:

``` text
GET /api/categories/search?keyword=Bánh
```

Kết quả được đưa vào:

``` csharp
dgvCategories.DataSource = result;
```

Nếu ô tìm kiếm trống:

``` csharp
await LoadDataAsync();
```

→ hiển thị lại toàn bộ danh sách.

------------------------------------------------------------------------

# 25. `ClearInputs()`

Code:

``` csharp
private void ClearInputs()
{
    txtId.Text = "";
    txtCategoryName.Text = "";
    txtDescription.Text = "";
}
```

### Chức năng

Xóa dữ liệu trên các ô nhập sau khi:

-   thêm thành công
-   cập nhật thành công
-   xóa thành công

------------------------------------------------------------------------

# 26. Tổng hợp các HTTP request

  Chức năng   Method   Endpoint                               Control
  ----------- -------- -------------------------------------- -----------------
  Hiển thị    GET      `/api/categories`                      `dgvCategories`
  Tải lại     GET      `/api/categories`                      `btnLoad`
  Tìm kiếm    GET      `/api/categories/search?keyword=...`   `btnSearch`
  Thêm        POST     `/api/categories`                      `btnAdd`
  Cập nhật    PUT      `/api/categories/{id}`                 `btnUpdate`
  Xóa         DELETE   `/api/categories/{id}`                 `btnDelete`

------------------------------------------------------------------------

# 27. Luồng CRUD hoàn chỉnh

## Create

``` text
txtCategoryName
txtDescription
       ↓
    btnAdd
       ↓
POST /api/categories
       ↓
API
       ↓
Database
       ↓
LoadDataAsync()
       ↓
DataGridView
```

## Read

``` text
Form Load / btnLoad
       ↓
GET /api/categories
       ↓
API
       ↓
JSON
       ↓
List<CategoryDto>
       ↓
dgvCategories
```

## Update

``` text
Click DataGridView
       ↓
txtId
txtCategoryName
txtDescription
       ↓
btnUpdate
       ↓
PUT /api/categories/{id}
       ↓
API
       ↓
Database
       ↓
LoadDataAsync()
```

## Delete

``` text
Click DataGridView
       ↓
txtId
       ↓
btnDelete
       ↓
Confirm
       ↓
DELETE /api/categories/{id}
       ↓
API
       ↓
Database
       ↓
LoadDataAsync()
```

------------------------------------------------------------------------

# 28. Cấu hình port

API hiện chạy tại:

``` text
https://localhost:7147
```

Swagger:

``` text
https://localhost:7147/swagger/index.html
```

WinForms phải sử dụng:

``` csharp
BaseAddress = new Uri(
    "https://localhost:7147/api/");
```

## Vì sao phải khớp port?

Nếu WinForms sử dụng:

``` text
https://localhost:7123/api/
```

nhưng API chạy:

``` text
https://localhost:7147/
```

thì WinForms không thể kết nối.

Lỗi thường gặp:

``` text
No connection could be made because the target machine
actively refused it.
```

------------------------------------------------------------------------

# 29. Cách test API bằng Swagger

Chạy:

``` text
MiniSupermarket.API
```

Sau đó mở:

``` text
https://localhost:7147/swagger/index.html
```

## Test GET

Chọn:

``` text
GET /api/categories
```

→ `Try it out`

→ `Execute`

Nếu API hoạt động, Response trả về danh sách JSON.

------------------------------------------------------------------------

# 30. Cách test WinForms

## Bước 1

Chạy:

``` text
MiniSupermarket.API
```

để Swagger hoạt động.

## Bước 2

Chạy:

``` text
MiniSupermarket.WinForms
```

## Bước 3

Form mở → tự động gọi:

``` text
GET /api/categories
```

## Bước 4

Kiểm tra:

``` text
Tải lại
Tìm kiếm
Chọn dòng
Thêm
Cập nhật
Xóa
```

------------------------------------------------------------------------

# 31. Các event cần nối trong Designer

Form cần có các event:

``` text
FormCategoryManagement_Load
btnLoad_Click
btnSearch_Click
btnAdd_Click
btnUpdate_Click
btnDelete_Click
dgvCategories_CellClick
```

Ví dụ:

``` csharp
this.Load +=
    new System.EventHandler(
        this.FormCategoryManagement_Load);

this.btnLoad.Click +=
    new System.EventHandler(
        this.btnLoad_Click);

this.btnSearch.Click +=
    new System.EventHandler(
        this.btnSearch_Click);

this.btnAdd.Click +=
    new System.EventHandler(
        this.btnAdd_Click);

this.btnUpdate.Click +=
    new System.EventHandler(
        this.btnUpdate_Click);

this.btnDelete.Click +=
    new System.EventHandler(
        this.btnDelete_Click);

this.dgvCategories.CellClick +=
    new DataGridViewCellEventHandler(
        this.dgvCategories_CellClick);
```

Thông thường Visual Studio sẽ tự tạo các dòng này khi event được gán
trong Properties.

------------------------------------------------------------------------

# 32. Các lỗi thường gặp

## Lỗi 1: Connection refused

``` text
No connection could be made because the target machine
actively refused it.
```

Kiểm tra:

``` text
API có đang chạy không?
Port API có phải 7147 không?
BaseAddress có phải 7147 không?
```

Đúng:

``` csharp
https://localhost:7147/api/
```

------------------------------------------------------------------------

## Lỗi 2: `FormatException`

Nếu có:

``` csharp
int id = int.Parse(txtId.Text);
```

mà `txtId` chứa:

``` text
qwe
```

sẽ lỗi.

ID phải là số:

``` text
1
2
3
```

Nên để:

``` csharp
txtId.ReadOnly = true;
```

nếu ID được lấy từ DataGridView/database.

------------------------------------------------------------------------

## Lỗi 3: DataGridView không có dữ liệu

Kiểm tra trực tiếp:

``` text
https://localhost:7147/api/categories
```

Nếu API không trả dữ liệu → kiểm tra Backend.

Nếu API trả JSON nhưng WinForms không hiển thị → kiểm tra:

``` csharp
dgvCategories.DataSource = categories;
```

và `CategoryDto`.

------------------------------------------------------------------------

## Lỗi 4: Click dòng không điền TextBox

Kiểm tra event:

``` text
dgvCategories_CellClick
```

đã được nối chưa.

Kiểm tra tên cột:

``` text
CategoryId
CategoryName
Description
```

phải khớp với tên property/column đang có.

------------------------------------------------------------------------

# 33. Tóm tắt code cần nhớ

## Kết nối API

``` csharp
private static readonly HttpClient _client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7147/api/")
};
```

## GET

``` csharp
await _client.GetFromJsonAsync<List<CategoryDto>>(
    "categories");
```

## POST

``` csharp
await _client.PostAsJsonAsync(
    "categories",
    newCat);
```

## PUT

``` csharp
await _client.PutAsJsonAsync(
    $"categories/{id}",
    updateCat);
```

## DELETE

``` csharp
await _client.DeleteAsync(
    $"categories/{id}");
```

## Hiển thị DataGridView

``` csharp
dgvCategories.DataSource = categories;
```

## Lấy dữ liệu từ TextBox

``` csharp
txtCategoryName.Text
txtDescription.Text
```

## Lấy ID

``` csharp
int id = int.Parse(txtId.Text);
```

------------------------------------------------------------------------

# 34. Chức năng của từng thành phần

``` text
FormCategoryManagement.cs
        │
        ├── HttpClient
        │     └── Kết nối API
        │
        ├── LoadDataAsync()
        │     └── GET danh sách
        │
        ├── btnLoad_Click()
        │     └── Tải lại
        │
        ├── btnSearch_Click()
        │     └── Tìm kiếm
        │
        ├── btnAdd_Click()
        │     └── POST
        │
        ├── btnUpdate_Click()
        │     └── PUT
        │
        ├── btnDelete_Click()
        │     └── DELETE
        │
        ├── dgvCategories_CellClick()
        │     └── Đưa dòng được chọn vào TextBox
        │
        └── ClearInputs()
              └── Xóa ô nhập
```

------------------------------------------------------------------------

# 35. Kết quả mong đợi

Giao diện gồm:

``` text
┌──────────────────────────────────────────────────────────────┐
│ Tìm kiếm                                                     │
│ [ txtKeyword                       ] [Tìm kiếm] [Tải lại]    │
├───────────────────────────────┬──────────────────────────────┤
│ Danh sách Nhóm hàng           │ Thông tin Nhóm hàng          │
│                               │                              │
│ Mã ID | Tên Nhóm | Mô Tả     │ Mã ID                       │
│ ──────────────────────────── │ [ txtId ]                    │
│ 1     | Bánh kẹo | ...       │                              │
│ 2     | Nước ... | ...       │ Tên Nhóm hàng               │
│ 3     | Sữa ...  | ...       │ [ txtCategoryName ]          │
│                               │                              │
│       dgvCategories            │ Mô Tả                        │
│                               │ [ txtDescription ]           │
│                               │                              │
│                               │ [Thêm] [Cập nhật] [Xóa]     │
└───────────────────────────────┴──────────────────────────────┘
```

------------------------------------------------------------------------

# 36. Thứ tự chạy toàn bộ hệ thống

``` text
1. Database
      ↓
2. MiniSupermarket.API
      ↓
3. https://localhost:7147
      ↓
4. Swagger kiểm tra API
      ↓
5. MiniSupermarket.WinForms
      ↓
6. FormCategoryManagement
      ↓
7. HttpClient
      ↓
8. CategoriesController
      ↓
9. Database
      ↓
10. JSON trả về WinForms
      ↓
11. DataGridView hiển thị
```

------------------------------------------------------------------------

# 37. Ghi chú

`FormCategoryManagement.Designer.cs` dùng cho phần giao diện.

`FormCategoryManagement.cs` dùng cho phần xử lý.

Không nên đặt logic CRUD vào `Designer.cs`, vì Visual Studio có thể tự
thay đổi file Designer khi chỉnh giao diện.

API và WinForms là hai project riêng nhưng nằm trong cùng một Solution
và giao tiếp qua HTTP.

------------------------------------------------------------------------

# 38. Version

``` text
MiniSupermarket
Version 1.0
```
