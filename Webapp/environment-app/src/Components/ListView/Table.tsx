import React from "react";
// import { BiSolidEdit } from "react-icons/bi";
import ReactPaginate from "react-paginate";
// import { Link } from "react-router-dom";
import ClipLoader from "react-spinners/ClipLoader";
// import ModalDelete from "../Modals/ModalDelete";
import "./table.scss";

export type Column = {
  header: string;
  accessorKey?: string;
  accessorFn?: (row: any) => React.ReactNode;
  align?: "left" | "center";
};

type Props = {
  data?: unknown[];
  currentPage?: number;
  pageCount?: number;
  loading?: boolean;
  columns: Column[];
  onChange?: (newPage: number) => void;
};

function Table(props: Props) {
  const {
    data,
    currentPage = 1,
    pageCount = 1,
    loading = false,
    columns,
    onChange,
  } = props;
  const handlePageClick = (event: any) => {
    onChange && onChange(event.selected + 1);
  };

  return loading ? (
    <ClipLoader
      className="spinner"
      color={"hsl(94, 59%, 35%)"}
      loading={loading}
      size={60}
    />
  ) : (
    <div className="bd-form">
      {
        <table className="table table-striped">
          <thead className="thread-primary ">
            <tr>
              {columns.map((c, index) => (
                <th key={index}>{c.header}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data?.map((d: any, index) => {
              return (
                <tr key={index}>
                  {columns.map((c, idx) => {
                    if (c.accessorKey)
                      return (
                        <td
                          key={idx}
                          className={c.align == "left" ? "text-left" : ""}
                        >
                          {d[c?.accessorKey]}
                        </td>
                      );
                    return (
                      <td
                        key={idx}
                        className={c.align == "left" ? "text-left" : ""}
                      >
                        {c.accessorFn && c.accessorFn(d)}
                      </td>
                    );
                  })}
                </tr>
              );
            })}
          </tbody>
        </table>
      }

      <ReactPaginate
        initialPage={currentPage - 1}
        breakLabel="..."
        nextLabel=">"
        onPageChange={handlePageClick}
        pageRangeDisplayed={2}
        marginPagesDisplayed={3}
        pageCount={pageCount}
        previousLabel="<"
        pageClassName="page-item"
        pageLinkClassName="page-link"
        previousClassName="page-item"
        previousLinkClassName="page-link"
        nextClassName="page-item"
        nextLinkClassName="page-link"
        breakClassName="page-item"
        breakLinkClassName="page-link"
        containerClassName="pagination "
        activeClassName="active"
        renderOnZeroPageCount={null}
      />
    </div>
  );
}

export default Table;
