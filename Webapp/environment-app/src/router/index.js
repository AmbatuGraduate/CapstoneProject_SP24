import { Route, Routes } from "react-router"
import { Manage } from "../pages/Manage"
import { ManageTree } from "../pages/ManageTree/ManageTree"
import InputSizesExample from "../pages/UpdateTree/updateTree"

export const Router = () => {
    return (
        <Routes>
            <Route path="/" element={<Manage />} />
            <Route path="/manage-tree" element={<ManageTree />} />
            <Route path="/update-tree" element={<InputSizesExample />} />
        </Routes>
    )
}