# MiniSupermarketSystem

**Họ và tên:** Vương Nguyễn Trường Hưng  
**MSSV:** 2124110111  

**Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
**Buổi thực hành:** Buổi 1 - Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

---

## 1. Giới thiệu

Bài thực hành xây dựng hệ thống **Mini Supermarket** gồm hai phần:

- **Backend:** ASP.NET Core Web API
- **Frontend:** Windows Forms Client

Ứng dụng thực hiện các chức năng CRUD thông qua HTTP API và kết nối giữa WinForms với Web API bằng `HttpClient`.

Các chức năng chính:

- Quản lý Category
- Quản lý Role
- Thêm dữ liệu
- Xem danh sách dữ liệu
- Cập nhật dữ liệu
- Xóa dữ liệu
- Kết nối WinForms với Web API
- Kiểm tra API bằng Swagger UI

---

## 2. Công nghệ sử dụng

### Backend

- ASP.NET Core Web API
- C#
- LINQ
- In-Memory Data
- RESTful API
- Swagger / OpenAPI

### Frontend

- Windows Forms
- C#
- `HttpClient`
- `System.Net.Http.Json`
- DataGridView
- TextBox
- Button

---

## 3. Kiến trúc hệ thống

Hệ thống được chia thành hai project:

```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/
│   ├── Controllers/
│   ├── Models/
│   └── Program.cs
│
└── MiniSupermarket.WinForms/
    ├── FormCategoryManagement.cs
    ├── FormCategoryManagement.Designer.cs
    ├── FormRoleManagement.cs
    ├── FormRoleManagement.Designer.cs
