import {
  forwardRef,
  useEffect,
  useImperativeHandle,
  useMemo,
  useState,
} from "react";

import { useApi } from "../../Api";
import { Filter } from "../Filter";
import SearchBar from "../SearchBar";
import Table, { Column } from "./Table";
import "./style.scss";

type DataResponse = {
  data?: unknown[];
  page: number;
  size: number;
};

type Props = {
  listURL: any;
  columns: Column[];
  bottom?: React.ReactNode;
  filter?: (row: any) => boolean;
  transform?: (data: any) => any;
};
export const ListView = forwardRef((props: Props, ref) => {
  const { listURL, columns, bottom, filter, transform } = props;

  const [data, setData] = useState<DataResponse | null>({
    data: [],
    page: 1,
    size: 1,
  });
  const [unFilterData, setUnFilterData] = useState<DataResponse["data"] | null>(
    []
  );
  const [loading, setLoading] = useState(true);

  useImperativeHandle(ref, () => ({
    reload() {
      fetchData();
    },
  }));

  const paging: number = useMemo(() => {
    return Math.ceil(data?.data?.length! / data?.size!);
  }, [data]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const res = await useApi.get(listURL);
      const _data = await res.data;
      const data = transform ? transform(_data) : _data;
      setData({
        data: filter ? data.filter(filter) : data, // Reverse the order of data here
        page: 1,
        size: 10,
      });
      setUnFilterData(data);
    } catch (error) {
      console.log(error);
    }
    setLoading(false);
  };


  useEffect(() => {
    fetchData();
  }, []);

  const filterCallBack = (query: string) => {
    const filter: DataResponse["data"] = unFilterData?.filter((e) => {
      const rowData: string[] = Object.values(e as object);
      return rowData.some((row) => {
        // console.log(String(row).includes(query));
        return String(row).toLowerCase().includes(query.toLowerCase());
      });
    });

    // @ts-ignore or @ts-expect-error
    setData({ ...data, data: filter });
  };

  return (
    <div className="listView">
      <div className="search flex">
        <div className="search">
          <SearchBar
            onSubmit={(query: string) => {
              filterCallBack(query);
            }}
          />
        </div>
        {/* <div className="filter flex">
          <Filter />
        </div> */}
      </div>
      <div className="listing">
        <Table
          data={data?.data?.slice((data?.page! - 1) * data?.size!, data?.page! * data?.size!)}
          currentPage={data?.page}
          loading={loading}
          pageCount={paging}
          columns={columns}
          onChange={(newPage) => data && setData({ ...data, page: newPage })}
        />
      </div>
      <div className="success-button-container">{bottom}</div>
    </div>
  );
});
