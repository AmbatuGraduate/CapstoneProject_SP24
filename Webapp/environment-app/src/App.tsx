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
import { CreateEmployee } from "./pages/ManageEmployee/Create";
import { UpdateEmployee } from "./pages/ManageEmployee/Update";
import { CreateTreeTrimSchedule } from "./pages/ManageTreeTrimSchedule/Create";
import { UpdateTreeTrimSchedule } from "./pages/ManageTreeTrimSchedule/Update";
import { CreateGarbageCollectionSchedule } from "./pages/ManageGarbageCollectionSchedule/Create";
import { DetailGarbageCollectionSchedule } from "./pages/ManageGarbageCollectionSchedule/Detail";
import { UpdateGarbageCollectionSchedule } from "./pages/ManageGarbageCollectionSchedule/Update";
import { CreateCleaningSchedule } from "./pages/ManageCleaningSchedule/Create";
import { DetailCleaningSchedule } from "./pages/ManageCleaningSchedule/Detail";
import { UpdateCleaningSchedule } from "./pages/ManageCleaningSchedule/Update";
import { Manage } from "./pages/Manage";
import { ManageGroup } from "./pages/ManageGroup";
import { CreateGroup } from "./pages/ManageGroup/Create";
import { DetailGroup } from "./pages/ManageGroup/Detail";
import { Profile } from "./pages/Login/Profile";


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
          path: "manage-employee/create",
          element: <CreateEmployee />,
        },
        {
          path: "manage-employee/:email",
          element: <DetailEmployee />,
        },
        {
          path: "manage-employee/:email/update",
          element: <UpdateEmployee />,
        },
        {
          path: "manage-treetrim-schedule",
          element: <ManageTreeTrimSchedule />,
        },
        {
          path: "manage-treetrim-schedule/create",
          element: <CreateTreeTrimSchedule />,
        },
        {
          path: "manage-treetrim-schedule/:id",
          element: <DetailTreeTrimSchedule />,
        },
        {
          path: "manage-treetrim-schedule/:id/update",
          element: <UpdateTreeTrimSchedule />,
        },
        {
          path: "manage-cleaning-schedule",
          element: <ManageCleaningSchedule />,
        },
        {
          path: "manage-cleaning-schedule/create",
          element: <CreateCleaningSchedule />,
        },
        {
          path: "manage-cleaning-schedule/:id",
          element: <DetailCleaningSchedule />,
        },
        {
          path: "manage-cleaning-schedule/:id/update",
          element: <UpdateCleaningSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule",
          element: <ManageGarbageCollectionSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule/create",
          element: <CreateGarbageCollectionSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule/:id",
          element: <DetailGarbageCollectionSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule/:id/update",
          element: <UpdateGarbageCollectionSchedule />,
        },

        {
          path: "manage-garbagecollection-schedule/create",
          element: <CreateGarbageCollectionSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule/:id",
          element: <DetailGarbageCollectionSchedule />,
        },
        {
          path: "manage-garbagecollection-schedule/:id/update",
          element: <UpdateGarbageCollectionSchedule />,
        },
        {
          path: "manage-report",
          element: <ManageReport />,
        },
        {
          path: "/manage-report/create",
          element: <CreateReport />,
        },
        {
          path: "manage-report/:id",
          element: <DetailReport />,
        },
        {
          path: "response/:id",
          element: <ResponseReport />,
        },
        {
          path: "manage-group",
          element: <ManageGroup />,
        },
        {
          path: "/manage-group/create",
          element: <CreateGroup />,
        },
        {
          path: "/manage-group/:email",
          element: <DetailGroup />,
        },
        {
          path: "/myprofile",
          element: <Profile />,
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
