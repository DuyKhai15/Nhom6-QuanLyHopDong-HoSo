# Quy ước đóng góp - Nhóm 6

## Quy trình
1. Fork repo, clone về máy, thêm remote `upstream`.
2. Luôn tách nhánh từ `develop` mới nhất.
3. Mỗi task WBS là một nhánh, một Pull Request.
4. PR gửi vào nhánh `develop` của repo gốc, gán Khải làm reviewer.

## Đặt tên nhánh
feature/<tên-sv>/<mã-WBS>-<tên-task>
Ví dụ: feature/an/1.3.2-login-api

## Commit message
<loại>(<phạm vi>): <mô tả> [WBS x.y.z]
Loại: feat, fix, docs, test, chore
Ví dụ: feat(auth): thêm API đăng nhập JWT [WBS 1.3.2]

## Bảo mật
Không commit mật khẩu, chuỗi kết nối, JWT secret, file .env.
Dùng appsettings.Example.json và .env.example với giá trị giả.