import React, { useState } from 'react';
import { Form, Input, Button, Card, Typography, message } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { setToken } from '../../utils/auth';

const { Title } = Typography;

const LoginPage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const onFinish = (values) => {
    setLoading(true);
    // Simulate API call
    setTimeout(() => {
      if (values.username === 'admin' && values.password === 'admin') {
        setToken('demo-token');
        message.success('Đăng nhập thành công!');
        navigate('/dashboard');
      } else {
        message.error('Tài khoản hoặc mật khẩu không đúng! (Gợi ý: admin/admin)');
      }
      setLoading(false);
    }, 1000);
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', background: '#f0f2f5' }}>
      <Card style={{ width: 400, boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}>
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Title level={3}>Đăng Nhập Hệ Thống</Title>
          <p>Quản lý Hợp đồng & Hồ sơ</p>
        </div>
        <Form name="login" onFinish={onFinish} layout="vertical">
          <Form.Item
            name="username"
            rules={[{ required: true, message: 'Vui lòng nhập tên đăng nhập!' }]}
          >
            <Input prefix={<UserOutlined />} placeholder="Tên đăng nhập (admin)" size="large" />
          </Form.Item>
          <Form.Item
            name="password"
            rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
          >
            <Input.Password prefix={<LockOutlined />} placeholder="Mật khẩu (admin)" size="large" />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" size="large" block loading={loading}>
              Đăng nhập
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
};

export default LoginPage;
