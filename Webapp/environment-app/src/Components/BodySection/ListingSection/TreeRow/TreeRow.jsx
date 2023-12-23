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
  const [data, setData] = useState()
  const fetchTrees = useCallback(async () => {
    try {
      const treeList = await treeApi.getAll();
      console.log(treeList);
      setData(treeList.data)
    }
    catch (error) {

    }
  }, [])
  useEffect(() => {
    fetchTrees()
  }, [fetchTrees]);
  return (
    data ?
      data.trees.map((tree) => {
        return (
          <tr className="tree-content">
          </tr>
        )
      })
      : 'loading'

  )
}

export default TreeRow;