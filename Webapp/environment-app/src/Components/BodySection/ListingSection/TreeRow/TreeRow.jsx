import React, { useCallback, useEffect, useState } from 'react';
import { MdDelete } from "react-icons/md";
import treeApi from '../../../../Api/treeApi';
import './treeRow.scss';

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

        <div className="table-responsive">
          <table className="table table-striped">
            <thead>
              <tr>
                <th className="tree-cell function"><i className="fas fa-band-aid"></i>Chỉnh sửa</th>
                {Object.keys(properties).map(label => (
                  <th key={label} className={"tree-cell " + properties[label].toLowerCase()}>{label}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {data.map((tree) => (
                <tr key={tree.id} className="trees-row">
                  <td className="tree-cell function">
                    {/* <span className="cell-label">Chỉnh sửa:</span> */}
                    <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editTreeModal">Sửa</button>
                    <button data-treeid={tree.id} type="button" className="btn btn-primary delete-btn" data-bs-toggle="modal" data-bs-target="#deleteModal">Xóa</button>
                  </td>
                  {Object.values(properties).map(property => (
                    <td key={property} className={"tree-cell " + property.toLowerCase()}>{tree[property]}</td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>

      </div>
    ) : 'loading'
  );
}

export default TreeRow;
