# Web Bán Hàng .NET MVC

Đây là một ứng dụng web bán hàng xây dựng bằng **ASP.NET MVC**, cung cấp các tính năng quản lý sản phẩm, danh mục, giỏ hàng, lịch sử giao dịch và phân quyền người dùng. Dự án này sử dụng **Entity Framework** và **SQL Server** để lưu trữ dữ liệu, đồng thời hỗ trợ các tính năng API RESTful.

## Tính Năng

### 1. **Quản Lý Sản Phẩm**
- Thêm, sửa, xóa sản phẩm.
- Hiển thị danh sách sản phẩm với thông tin chi tiết như tên, mô tả, giá cả và hình ảnh.

### 2. **Quản Lý Danh Mục**
- Thêm, sửa, xóa danh mục sản phẩm.
- Cho phép phân loại sản phẩm theo danh mục để dễ dàng tìm kiếm.

### 3. **Giỏ Hàng**
- Người dùng có thể thêm sản phẩm vào giỏ hàng, thay đổi số lượng và tính toán tổng giá.
- Hỗ trợ thanh toán qua giỏ hàng và theo dõi đơn hàng.

### 4. **Tính Năng Phân Quyền**
- **Admin**: Quản lý toàn bộ sản phẩm, danh mục, người dùng và đơn hàng.
- **User**: Đăng ký, đăng nhập, xem sản phẩm, thêm vào giỏ hàng và thanh toán.
- **Phân quyền an toàn và bảo mật thông qua rồi** (Role-Based Access Control).

### 5. **Đăng Ký và Đăng Nhập**
- Hệ thống đăng ký người dùng mới và đăng nhập người dùng đã có tài khoản.
- Sử dụng mã hóa mật khẩu và bảo mật thông tin người dùng.

### 6. **Chi Tiết Sản Phẩm**
- Cung cấp trang chi tiết sản phẩm với mô tả đầy đủ, hình ảnh và các thông tin liên quan.

### 7. **Lịch Sử Giao Dịch Khách Hàng**
- Người dùng có thể theo dõi lịch sử giao dịch của mình, bao gồm thông tin đơn hàng và trạng thái thanh toán.

### 8. **API RESTful**
- Cung cấp các API RESTful để quản lý sản phẩm, danh mục và đơn hàng.
- Được sử dụng với **Postman** để kiểm tra và gửi yêu cầu API.

### 9. **Postman**
- Bộ yêu cầu Postman có sẵn để kiểm tra các API, hỗ trợ phát triển và kiểm tra nhanh.

## Các Công Nghệ Được Sử Dụng
- **ASP.NET MVC**: Framework chính để phát triển ứng dụng.
- **Entity Framework**: ORM để quản lý cơ sở dữ liệu.
- **SQL Server**: Cơ sở dữ liệu.
- **Bootstrap**: Để tạo giao diện responsive và đẹp mắt.
- **jQuery**: Để xử lý tương tác động và các thao tác không đồng bộ.
- **Postman**: Dùng để kiểm tra các API.

## Cài Đặt

### Yêu Cầu Hệ Thống
- **.NET Core SDK 3.1 trở lên**
- **SQL Server** (hoặc sử dụng SQL Server Express)
- **Visual Studio 2022**

### Cài Đặt và Chạy Ứng Dụng
1. Clone repository:
   ```bash
   git clone https://github.com/username/repository.git
   ```
2. Mở dự án trong **Visual Studio**.
3. Chạy **SQL Server** và tạo cơ sở dữ liệu mới.
4. Cập nhật chuỗi kết nối cơ sở dữ liệu trong **appsettings.json**.
5. Chạy ứng dụng.

## Cấu Trúc Dự Án

- **Controllers**: Chứa các lớp điều khiển cho các tính năng của ứng dụng.
- **Models**: Chứa các mô hình dữ liệu cho các thực thể như sản phẩm, danh mục, người dùng.
- **Views**: Chứa các trang Razor để hiển thị giao diện người dùng.
- **API**: Các controller API để quản lý các tài nguyên qua RESTful.

## Hướng Dẫn API

Ứng dụng cung cấp các API RESTful cho các thao tác CRUD trên sản phẩm và danh mục. Dưới đây là một số endpoint ví dụ:

- **GET /api/products**: Lấy danh sách sản phẩm.
- **POST /api/products**: Thêm sản phẩm mới.
- **PUT /api/products/{id}**: Cập nhật sản phẩm theo ID.
- **DELETE /api/products/{id}**: Xóa sản phẩm theo ID.

## Liên Hệ
- Tác giả: Hồ Duy Vũ
- Email: duyvu11092004@gmail.com
