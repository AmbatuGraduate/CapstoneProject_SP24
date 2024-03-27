import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { Layout } from "./Components/Layout";
import { Login } from "./pages/Login";
import { Manage } from "./pages/Manage";
import { ManageEmployee } from "./pages/ManageEmployee";
import { ManageTree } from "./pages/ManageTree";
import { CreateTree } from "./pages/ManageTree/Create";
import { DetailTree } from "./pages/ManageTree/Detail";
import { UpdateTree } from "./pages/ManageTree/Update";
import { ManageTreeTrimSchedule } from "./pages/ManageTreeTrimSchedule";
import { ManageGarbageCollectionSchedule } from "./pages/ManageGarbageCollectionSchedule";
import { ManageCleaningSchedule } from "./pages/ManageCleaningSchedule";
import { ManageReport } from "./pages/ManageReport";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <Manage />,
        },
        {
          path: "manage-tree",
          element: <ManageTree />,
        },
        {
          path: "manage-tree/create",
          element: <CreateTree />,
        },
        {
          path: "manage-tree/:id/update",
          element: <UpdateTree />,
        },
        {
          path: "manage-tree/:id",
          element: <DetailTree />,
        },
        {
          path: "manage-employee",
          element: <ManageEmployee />,
        },
        {
          path: "manage-treetrim-schedule",
          element: <ManageTreeTrimSchedule />,
        },
        {
          path: "manage-cleaning-schedule",
          element: <ManageCleaningSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule",
          element: <ManageGarbageCollectionSchedule />,
        },
        {
          path: "manage-report",
          element: <ManageReport />,
        },
        {
          path: "manage-report/create",
          element: <ManageReport />,
        },
      ],
    },
    {
      path: "login",
      element: <Login />,
    },
  ]);

  return <RouterProvider router={router} />;
}

export default App;