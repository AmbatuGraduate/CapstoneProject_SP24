import { Route, Routes } from "react-router"
import { Manage } from "../pages/Manage"
import { ManageTree } from "../pages/ManageTree"
import InputSizesExample from "../pages/ManageTree/UpdateTree"
import { ManageRoute } from "../pages/ManageRoute"
import { ManageGarbageTruck } from "../pages/ManageGabageTruck"
import { ManageEmployee } from "../pages/ManageEmployee"
import { Login } from "../pages/Login"
import DetailTree from "../pages/ManageTree/Detail Tree/detailTree"

export const Router = () => {
    return (
        <Routes>
            {/* add login */}
            <Route path="/login" element={<Login />} />
            <Route path="/" element={<Manage />} />
            <Route path="/manage-tree" element={<ManageTree />} />
            <Route path="/detail-tree/:treeCode" element={<DetailTree />}/>
            <Route path="/update-tree" element={<InputSizesExample />} />
            <Route path="/manage-route" element={<ManageRoute />} />
            <Route path="/manage-garbage-truck" element={<ManageGarbageTruck />} />
            <Route path="/manage-employee" element={<ManageEmployee />} />


        </Routes>
    )
}