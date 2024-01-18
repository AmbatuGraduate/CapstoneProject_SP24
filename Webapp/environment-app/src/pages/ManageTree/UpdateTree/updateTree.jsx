import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';

function SelectSizesExample() {
  const [active, setActive] = useState(false);
  const navigate = useNavigate();
  function closeItem() {
    setActive(true);
    navigate(-1)
  }

  return (
    <>
      <Form>
        <Form.Group>
          <Form.Label>Quận</Form.Label>
          <Form.Select>
            <option>Hải Châu</option>
            <option value="1">Liên Chiểu</option>
            <option value="2">Thanh Khê</option>
            <option value="3">Sơn Trà</option>
            <option value="4">Ngũ Hành Sơn</option>
            <option value="5">Cẩm Lệ</option>
          </Form.Select>
        </Form.Group>
        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
          <Form.Label>Tuyến đường</Form.Label>
          <Form.Control type="email" placeholder="" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
          <Form.Label>Giống cây</Form.Label>
          <Form.Control type="email" placeholder="" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
          <Form.Label>Thời điểm cắt tỉa gần nhất</Form.Label>
          <Form.Control type="email" placeholder="" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
          <Form.Label>Trạng thái</Form.Label>
          <Form.Control type="email" placeholder="" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
          <Form.Label>Thời điểm cắt tỉa</Form.Label>
          <Form.Control type="email" placeholder="" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
          <Form.Label>Ghi chú</Form.Label>
          <Form.Control as="textarea" rows={3} />
        </Form.Group>
      </Form>

      <Button variant="success">Lưu</Button>{' '}
      <Button variant="danger" onClick={closeItem}>Hủy</Button>{' '}
    </>
  );
}

export default SelectSizesExample;