import { useEffect, useState } from "react";
import { useApi } from "../../api";
import { Filter } from "../Filter";
import SearchBar from "../SearchBar";
import Table, { Column } from "./Table";
import "./style.scss";

type DataResponse = {
  data: unknown[];
  page: number;
  size: number;
};

type Props = {
  listURL: string;
  columns: Column[];
  bottom?: React.ReactNode;
};
export const ListView = (props: Props) => {
  const { listURL, columns, bottom } = props;

  const [data, setData] = useState<DataResponse | null>({
    data: [],
    page: 1,
    size: 1,
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    setLoading(true);
    try {
      const res = await useApi.get(listURL);
      const data = await res.data;
      setData({ data: data, page: 1, size: 10 });
    } catch (error) {
      console.log(error);
    }
    setLoading(false);
  };

  return (
    <div className="listView">
      <div className="search flex">
        <div className="search">
          <SearchBar onSubmit={() => {}} />
        </div>
        <div className="filter flex">
          <Filter />
        </div>
      </div>
      <div className="listing">
        <Table
          data={[...data?.data!].splice(data?.page! - 1, data?.size!)}
          currentPage={data?.page}
          loading={loading}
          pageCount={Math.ceil(data?.data.length! / data?.size!)}
          columns={columns}
          onChange={(newPage) => data && setData({ ...data, page: newPage })}
        />
      </div>
      <div className="success-button-container">{bottom}</div>
    </div>
  );
};
