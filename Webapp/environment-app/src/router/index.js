import { Route, Routes } from "react-router"
import { Manage } from "../pages/Manage"
import { ManageTree } from "../pages/ManageTree/ManageTree"

export const Router = () => {
    return (
        <Routes>
            <Route path="/" element={<Manage />} />
            <Route path="/manage-tree" element={<ManageTree />} />
        </Routes>
    )
}