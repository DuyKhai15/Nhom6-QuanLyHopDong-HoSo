import React, { useState } from 'react';
import { Table, Button, Space, Input, Tag, Card } from 'antd';
import { PlusOutlined, SearchOutlined } from '@ant-design/icons';

const ContractPage = () => {
  const [searchText, setSearchText] = useState('');

  const columns = [
    {
      title: 'Mã Hợp Đồng',
      dataIndex: 'code',
      key: 'code',
    },
    {
      title: 'Tên Hợp Đồng',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Khách Hàng',
      dataIndex: 'customer',
      key: 'customer',
    },
    {
      title: 'Trạng Thái',
      dataIndex: 'status',
      key: 'status',
      render: (status) => {
        let color = status === 'Hoạt động' ? 'green' : 'volcano';
        return <Tag color={color}>{status}</Tag>;
      },
    },
    {
      title: 'Ngày Tạo',
      dataIndex: 'createdAt',
      key: 'createdAt',
    },
    {
      title: 'Hành Động',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button type="link" style={{ padding: 0 }}>Sửa</Button>
          <Button type="link" danger style={{ padding: 0 }}>Xóa</Button>
        </Space>
      ),
    },
  ];

  const data = [
    {
      key: '1',
      code: 'HD-001',
      name: 'Hợp đồng tín dụng cá nhân',
      customer: 'Nguyễn Văn A',
      status: 'Hoạt động',
      createdAt: '2026-10-01',
    },
    {
      key: '2',
      code: 'HD-002',
      name: 'Hợp đồng vay vốn doanh nghiệp',
      customer: 'Công ty ABC',
      status: 'Chờ duyệt',
      createdAt: '2026-10-02',
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <h2>Quản Lý Hợp Đồng</h2>
        <Button type="primary" icon={<PlusOutlined />}>Thêm Hợp Đồng</Button>
      </div>
      
      <Card style={{ marginBottom: 16 }}>
        <Input 
          placeholder="Tìm kiếm theo mã, tên hợp đồng..." 
          prefix={<SearchOutlined />} 
          style={{ width: 300 }}
          value={searchText}
          onChange={(e) => setSearchText(e.target.value)}
        />
      </Card>

      <Table 
        columns={columns} 
        dataSource={data} 
        pagination={{ pageSize: 10 }} 
      />
    </div>
  );
};

export default ContractPage;
