import React from "react";
import ReactPaginate from "react-paginate";
import ClipLoader from "react-spinners/ClipLoader";
import "./table.scss";

import { GrFormPrevious } from "react-icons/gr";
import { GrFormNext } from "react-icons/gr";

export type Column = {
  header: string;
  accessorKey?: string;
  accessorFn?: (row: any) => React.ReactNode;
  width?: string;
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
                          style={{ width: c.width }}
                        >
                          {d[c?.accessorKey]}
                        </td>
                      );
                    return (
                      <td
                        key={idx}
                        style={{ width: c.width }}
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

      {pageCount > 1 && (
        <ReactPaginate
          initialPage={currentPage - 1}
          breakLabel="..."
          nextLabel={<GrFormNext />}
          onPageChange={handlePageClick}
          pageRangeDisplayed={2}
          marginPagesDisplayed={3}
          pageCount={pageCount}
          previousLabel={<GrFormPrevious />}
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
      )}

    </div>
  );
}

export default Table;
