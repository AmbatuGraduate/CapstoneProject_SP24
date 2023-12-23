import React, { useCallback, useEffect, useState } from 'react'
import { MdDelete } from "react-icons/md";
import treeApi from '../../../../Api/treeApi';

// function renderTreesList() {
//   useEffect(()=>{
//     fetchTrees()
//   }, [fetchTrees]);
// }
// const fetchTrees = useCallback(async() => {
//   try{
//     const treeList = await treeApi.getAll();
//     console.log(treeList);
//   }
//   catch(error){

//   }
// }, [])

function TreeRow() {
  var properties = {
    'Mã số cây': 'id',
    'Quận': 'district',
    'Tuyến đường': 'street',
    'Loại cây': 'rootType',
    'Giống cây': 'type',
    'Đường kính thân': 'bodyDiameter',
    'Tán lá': 'leafLength',
    'Thời điểm trồng': 'plantTime',
    'Thời điểm cắt': 'cutTime',
    'Thời hạn cắt': 'intervalCutTime',
    'Ghi chú': 'note'
  };
  const [data, setData] = useState();

  const fetchTrees = useCallback(async () => {
    try {
      const treeList = await treeApi.getAll();
      setData(treeList);
    } catch (error) {
      // Handle the error
    }
  }, []);

  useEffect(() => {
    fetchTrees();
  }, [fetchTrees]);

  return (
    data ? (
      <div className="trees-area-wrapper tableView">

        <div className="trees-header">
          <div className="tree-cell function"><i class="fas fa-band-aid"></i>Chỉnh sửa</div>
          <div className="tree-cell id">Mã số cây</div>
          <div className="tree-cell county">Quận</div>
          <div className="tree-cell street">Tuyến đường</div>
          <div className="tree-cell types">Loại cây</div>
          <div className="tree-cell cultivar">Giống cây</div>
          <div className="tree-cell diameter">Đường kính thân(cm)</div>
          <div className="tree-cell foliage-width">Tán lá(cm)</div>
          <div className="tree-cell plating">Thời điểm trồng</div>
          <div className="tree-cell status-cell">Thời điểm cắt tỉa gần nhất</div>
          <div className="tree-cell cutting">Thời hạn cắt tỉa</div>
          <div className="tree-cell notes">Ghi chú</div>
        </div>

        {data.map((tree) => (

          <div className="trees-row">
            <div className="tree-cell function">
              <span className="cell-label">Chỉnh sửa:</span>
              <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editTreeModal">Sửa</button>
              <button data-treeid="' + tree.id + '" type="button" className="btn btn-primary delete-btn" data-bs-toggle="modal" data-bs-target="#deleteModal">Xóa</button>

              {Object.entries(properties).map(([label, property]) => (
                <div key={label} className={"tree-cell " + property.toLowerCase()}>
                  <span className="cell-label">{label}:</span> {tree[property]}
                </div>
              ))}

            </div>
          </div>
        ))}

      </div>

    ) : 'loading'
  );
}

export default TreeRow