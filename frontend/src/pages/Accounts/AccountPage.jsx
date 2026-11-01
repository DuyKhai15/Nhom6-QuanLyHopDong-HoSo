import React from 'react';
import { Table, Button, Space, Tag } from 'antd';
import { PlusOutlined } from '@ant-design/icons';

const AccountPage = () => {
  const columns = [
    {
      title: 'Tên Đăng Nhập',
      dataIndex: 'username',
      key: 'username',
    },
    {
      title: 'Họ và Tên',
      dataIndex: 'fullName',
      key: 'fullName',
    },
    {
      title: 'Email',
      dataIndex: 'email',
      key: 'email',
    },
    {
      title: 'Vai Trò',
      dataIndex: 'role',
      key: 'role',
      render: (role) => (
        <Tag color={role === 'Admin' ? 'purple' : 'blue'}>{role}</Tag>
      ),
    },
    {
      title: 'Trạng Thái',
      dataIndex: 'isActive',
      key: 'isActive',
      render: (isActive) => (
        <Tag color={isActive ? 'green' : 'red'}>
          {isActive ? 'Hoạt động' : 'Đã khóa'}
        </Tag>
      ),
    },
    {
      title: 'Hành Động',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button type="link" style={{ padding: 0 }}>Sửa</Button>
          <Button type="link" danger style={{ padding: 0 }}>Khóa</Button>
        </Space>
      ),
    },
  ];

  const data = [
    {
      key: '1',
      username: 'admin',
      fullName: 'Quản trị viên',
      email: 'admin@qlda.com',
      role: 'Admin',
      isActive: true,
    },
    {
      key: '2',
      username: 'user1',
      fullName: 'Nhân viên A',
      email: 'usera@qlda.com',
      role: 'Nhân Viên',
      isActive: true,
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <h2>Quản Lý Tài Khoản</h2>
        <Button type="primary" icon={<PlusOutlined />}>Thêm Tài Khoản</Button>
      </div>

      <Table 
        columns={columns} 
        dataSource={data} 
        pagination={{ pageSize: 10 }} 
      />
    </div>
  );
};

export default AccountPage;
