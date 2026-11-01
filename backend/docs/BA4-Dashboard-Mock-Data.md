# BA4 - Kịch bản dữ liệu mô phỏng Dashboard

## 1. Mục tiêu

Chuẩn bị dữ liệu mô phỏng phục vụ thiết kế và kiểm thử Dashboard.

## 2. Dữ liệu hiện tại

### Hợp đồng

| ContractId | ContractCode | Status |
|---:|---|---|
| 1 | HD001 | Đang hiệu lực |
| 2 | HD002 | Đang hiệu lực |
| 6 | HD004 | Đang hiệu lực |

### Tài liệu

| DocumentId | ContractId | FileName |
|---:|---:|---|
| 1 | 1 | hopdong_HD001.pdf |
| 2 | 1 | phuluc_HD001.pdf |
| 3 | 2 | hopdong_HD002.pdf |

## 3. Các chỉ số Dashboard

- Tổng số hợp đồng: 3
- Hợp đồng đang hiệu lực: 3
- Tổng số tài liệu: 3

## 4. Tài liệu theo hợp đồng

| ContractCode | Số tài liệu |
|---|---:|
| HD001 | 2 |
| HD002 | 1 |
| HD004 | 0 |

## 5. Kết quả mong đợi

Dashboard phải hiển thị đúng các số liệu trên.

## 6. Ghi chú

Dữ liệu sử dụng dữ liệu hiện có trong database
ContractManagement, không cần INSERT thêm dữ liệu.