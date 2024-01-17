import React, { useCallback, useEffect, useState } from 'react';
// import treeApi from '../../../../Api/treeApi';
import './treeRow.scss';
import axios from 'axios';
import { BiSolidEdit } from "react-icons/bi";
import Delete from '../../../Modals/ModelDelete';
import { Link } from 'react-router-dom';

function TreeRow() {
  const [data, setData] = useState([]);
  useEffect(() => {
    axios.get('http://localhost:8000/tree')
      .then(res => setData(res.data))
      .catch(err => console.log(err))
  }, [])
  return (
    <div className='bd-form'>
      <table className='table'>
        <thead className='table'>
          <tr>
            <th>Chỉnh sửa</th>
            <th>Mã số cây</th>
            <th>Tuyến đường</th>
            <th>Đường kính thân</th>
            <th>Tán lá</th>
            <th>Thời điểm trồng</th>
            <th>Thời điểm cắt tỉa</th>
            <th>Lịch cắt tỉa</th>
            <th>Giống cây</th>
            <th>Ghi chú</th>
          </tr>
        </thead>
        <tbody>
          {data.map((d, i) => {
            return <tr key={i}>
              <td>
                <Link to="/update-tree" activeClassname="active" extract={true}>
                  <button type="button" class="btn btn-click" ><BiSolidEdit /></button>
                </Link>
                <Delete />
              </td>
              <td><button type="button" class="btn btn-click">{d.MaCay}</button></td>
              <td>{d.DuongId}</td>
              <td>{d.DuongKinhThan}</td>
              <td>{d.TanLa}</td>
              <td>{d.ThoiDiemTrong}</td>
              <td>{d.ThoiDiemCatTiepTheo}</td>
              <td>{d.LichCatTia}</td>
              <td>{d.GiongCayId}</td>
              <td>{d.Note}</td>
            </tr>
          })
          }
        </tbody>
      </table>
    </div>
  )
}

export default TreeRow


