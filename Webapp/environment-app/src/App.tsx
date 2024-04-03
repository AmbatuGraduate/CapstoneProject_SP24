import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { Layout } from "./Components/Layout";
import { Login } from "./pages/Login";
import { ManageEmployee } from "./pages/ManageEmployee";
import { ManageTree } from "./pages/ManageTree";
import { CreateTree } from "./pages/ManageTree/Create";
import { DetailTree } from "./pages/ManageTree/Detail";
import { UpdateTree } from "./pages/ManageTree/Update";
import { ManageTreeTrimSchedule } from "./pages/ManageTreeTrimSchedule";
import { ManageCleaningSchedule } from "./pages/ManageCleaningSchedule";
import { ManageGarbageCollectionSchedule } from "./pages/ManageGarbageCollectionSchedule";
import { DetailEmployee } from "./pages/ManageEmployee/Detail";
import { DetailTreeTrimSchedule } from "./pages/ManageTreeTrimSchedule/Detail";
import { ManageReport } from "./pages/ManageReport";
import { CreateReport } from "./pages/ManageReport/Create";
import { DetailReport } from "./pages/ManageReport/Detail";
import { ResponseReport } from "./pages/ManageReport/Response";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <ManageReport />,
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
          path: "manage-employee/:email",
          element: <DetailEmployee />,
        },
        {
          path: "manage-treetrim-schedule",
          element: <ManageTreeTrimSchedule />,
        },
        {
          path: "manage-treetrim-schedule/:id",
          element: <DetailTreeTrimSchedule />,
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
          path: "/create",
          element: <CreateReport />,
        },
        {
          path: "manage-report/:id",
          element: <DetailReport />,
        },
        {
          path: "response",
          element: <ResponseReport />,
        }
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
