import React from 'react'
import { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Delete from '../../../Components/Modals/ModelDelete';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import Image from 'react-bootstrap/Image';
import Row from 'react-bootstrap/Row';


function DetailTree() {
    const [data, setData] = useState([]);
    const [active, setActive] = useState(false);
    const navigate = useNavigate();
    useEffect(() => {
        axios.get('http://localhost:8000/tree')
            .then(res => setData(res.data))
            .catch(err => console.log(err))
    }, [])
    return (
        <section className='detailTree'>
            <div className='container'>
                <div className='row'>
                    {data.map((d, i) => {
                        return <tr key={i}>
                            <Container>
                                <Row>
                                    <Col xs={6} md={4}>
                                        {/* <Image src={} thumbnail /> */}
                                    </Col>
                                </Row>
                            </Container>
                            <Button variant="success">Chỉnh sửa</Button>{' '}
                            <Button variant="danger" >Xóa
                                <Delete />
                            </Button>{' '}
                        </tr>
                    })}
                </div>
            </div>
        </section>
    )
}

export default DetailTree