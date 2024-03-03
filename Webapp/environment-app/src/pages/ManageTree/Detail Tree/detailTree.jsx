import axios from 'axios';
import React, { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import img from '../../../Assets/imgcondau4.jpg';

import './detailTree.scss';

const detailTitle = {
    "streetId": "Tuyến dường",
    "cultivarId": "Giống cây",
    "bodyDiameter": "Đường kính thân",
    "leafLength": "Tán lá",
    "cutTime": "Thời điểm cắt tỉa gần nhất",
    "plantTime": "Thời điểm trồng",
    "note": "Ghi chú",
    "isExist": "Trạng thái"
}

function DetailTree() {
    const [data, setData] = useState({});
    const [active, setActive] = useState(false);

    const { state } = useLocation();
    const navigate = useNavigate();
    const { treeCode } = useParams()


    useEffect(() => {
        const fetchData = async () => {
            try {
                const res = await axios.get(`http://vesinhdanang.xyz/AmbatuGraduate_API/api/tree/GetByTreeCode/${treeCode}`);
                const data = await res.data;
                setData(data);
            } catch (error) {
                console.log(error)
            }
        }
        fetchData()}, []);

    return (
        <section className='detailTree'>
            <div className='container'>
                {
                    data && Object.keys(data).slice(0, 1).map((property, index) => {
                        if (property !== data.treeCode && property !== data.isExist)
                            console.log("property >>>>>>>>>>>>", property)
                        return (
                            <div key={index} class="card mb-3" >
                                <div class="row g-0">
                                    <div class="col-md-6">
                                        <img src={img} alt="img" />
                                    </div>
                                    <div class="col-md-6">
                                        <div className='flex flex-col  gap-5 items-center justify-between p-5'>
                                            <div className='grid gap-3 '>
                                                <h6 className='font-bold text-lg'>Tuyến đường:</h6>
                                                <h6 className='font-bold text-lg'>Giống cây:</h6>
                                                <h6 className='font-bold text-lg'>Đường kính thân:</h6>
                                                <h6 className='font-bold text-lg'>Tán lá:</h6>
                                                <h6 className='font-bold text-lg'>Thời điểm cắt tỉa gần nhất:</h6>
                                                <h6 className='font-bold text-lg'>Thời điểm trồng:</h6>
                                                <h6 className='font-bold text-lg'>Ghi chú:</h6>
                                            </div>

                                            <div className='grid gap-3 items-end'>
                                                <h6 className='font-bold text-lg'>{data["streetId"] ? data["streetId"] : 'Tuyến đường'}</h6>
                                                <h6 className='font-bold text-lg'>{data["GiongCay"] ? data["GiongCay"] : 'Giống cây'}</h6>
                                                <h6 className='font-bold text-lg'>{data["bodyDiameter"] ? data["bodyDiameter"] : 'Đường kính thân'}</h6>
                                                <h6 className='font-bold text-lg'>{data["leafLength"] ? data["leafLength"] : 'Tán lá'}</h6>
                                                <h6 className='font-bold text-lg'>{data["cutTime"] ? data["cutTime"] : 'Thời điểm cắt tỉa gần nhất'}</h6>
                                                <h6 className='font-bold text-lg'>{data["plantTime"] ? data["plantTime"] : 'Thời điểm trồng'}</h6>
                                                <h6 className='font-bold text-lg'>{data["note"] ? data["note"] : 'Ghi chú'}</h6>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        )
                    })
                }
            </div>
            <div className='button-group'>
                <div className='btn-success'>
                    <Button variant="success">Chỉnh sửa</Button>{' '}
                </div>
                <div className='btn-danger'>
                    <Button variant="danger">Xóa</Button>{' '}
                </div>
            </div>

        </section>
    )
}

export default DetailTree
