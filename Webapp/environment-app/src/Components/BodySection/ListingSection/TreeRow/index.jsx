import React, { useCallback, useEffect, useState } from 'react';
// import treeApi from '../../../../Api/treeApi';
import './style.scss'
import axios from 'axios';
import { BiAlignLeft, BiSolidEdit } from "react-icons/bi";
import Delete from '../../../Modals/ModelDelete';
import { Link } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import ClipLoader from "react-spinners/ClipLoader";
import ReactPaginate from 'react-paginate';

function TreeRow() {
  const [totalTree, setTotalTree] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [currentPages, setCurrentPages] = useState(1);
  const [data, setData] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:8000/tree')
      .then(res => setData(res.data))
      .catch(err => console.log(err))
      .catch(res => setTotalTree(res.data))
      .catch(res => setTotalPages(res.total_pages))
  }, []);

  const [loading, setLoading] = useState(true);
  useEffect(() => {
    setLoading(true)
    setTimeout(() => {
      setLoading(false)
    }, 1000)
  }, []);

  const handlePageClick = (event) => {
    console.log("log event", event.selected);
    setCurrentPages(event.selected + 1);
  }


  return (
    loading ?
      <ClipLoader
        className='spinner'
        color={'#0fb34b'}
        loading={loading}
        size={60}
      />
      :
      <div className='bd-form'>
        {
          <table className='table table-striped'>
            <thead className='thread-primary '>
              <tr>
                <th >Chỉnh sửa</th>
                <th >Mã số cây</th>
                <th >Quận</th>
                <th >Tuyến đường</th>
                <th >Giống cây</th>
                <th>Thời điểm cắt tỉa gần nhất</th>
                <th>Trạng thái</th>
              </tr>
            </thead>
            <tbody>

              {data.map((d, index) => {
                if (index < (currentPages * 10) && index > ((currentPages - 1) * 10 - 1))
                  return <tr key={index}>
                    <td>
                      <Link to="/update-tree">
                        <button type="button" class="btn btn-click" ><BiSolidEdit /></button>
                      </Link>
                      <Delete />
                    </td>
                    <td>
                      <Link to="/detail-tree">
                        <button type="button" class="btn btn-click">{d.MaCay}</button>
                      </Link>
                    </td>
                    <td className='text-left'>{d.Quan}</td>
                    <td className='text-left'>{d.DuongId}</td>
                    <td className='text-left'>{d.GiongCay}</td>
                    <td>{d.ThoiDiemCatTiaGanNhat}</td>
                    <td className={"font-bold " + (d.TrangThai === "Đã cắt" ? "green-text" : "red-text")}>{d.TrangThai === "Đã cắt" ? "Đã cắt" : "Cần Cắt"}</td>
                  </tr>
              })
              }
            </tbody>
          </table>
        }
        <ReactPaginate
          breakLabel="..."
          nextLabel=">"
          onPageChange={handlePageClick}
          pageRangeDisplayed={2}
          marginPagesDisplayed={1}
          pageCount={10}
          previousLabel="<"


          pageClassName='page-item'
          pageLinkClassName='page-link'
          previousClassName='page-item'
          previousLinkClassName='page-link'
          nextClassName='page-item'
          nextLinkClassName='page-link'
          breakClassName='page-item'
          breakLinkClassName='page-link'
          containerClassName="pagination "
          activeClassName='active'
          renderOnZeroPageCount={null}

        />
      </div>
  )
}

export default TreeRow