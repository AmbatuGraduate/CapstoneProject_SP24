import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { DETAIL_REPORT, useApi } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { useCookies } from "react-cookie";
import "./style.scss";
import { dayFormat } from "../../utils";

export const DetailReport = () => {
  const [token] = useCookies(["accessToken"]);
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);
  const ReportImpact = {
    0: "Thấp",
    1: "Trung Bình",
    2: "Cao",
  };
  const ReportStatus = {
    Resolved: "Đã xử lý",
    UnResolved: "Chưa được xử lý",
  };
  const handleNavigate = () => {
    navigate("/manage-report");
  };

  const fetchData = async () => {
    try {
      const response = await useApi.get(DETAIL_REPORT.replace(":id", id));
      const data = response.data;
      setData(data);
    } catch (error) {
      console.error("Error fetching tree detail:", error);
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    console.log("id", id);

    console.log(loading);
    fetchData();
  }, [id]);

  // useEffect(() => {
  //   if (data && data.value && data.value.reportFormat) {
  //     // const cleanedReportBody = data.value.reportFormat.reportBody.replace(
  //     //   /Report ID: .|Expected Resolution Date: .|Report Impact: .*/g,
  //     //   ""
  //     // );
  //     console.log("data,", data);
  //     setData((prevData) => ({
  //       ...prevData,
  //       value: {
  //         ...prevData.value,
  //         reportFormat: {
  //           ...prevData.value.reportFormat,
  //           reportBody: data.value.reportFormat.reportBody?.split("\r\n")[1],
  //         },
  //       },
  //     }));
  //   }
  // }, [JSON.stringify(data)]);

  return loading ? (
    <ClipLoader
      className="spinner"
      color={"hsl(94, 59%, 35%)"}
      loading={loading}
      size={60}
    />
  ) : (
    <div className="main-layout row">
      <div className="form-cover">
        <div
          className="detail-cover-report"
          style={{ marginBottom: "40px", textAlign: "right" }}
        >
          <div className="detail-content-left"></div>
          <div className="detail-content-right title">
            {data?.value?.reportFormat?.reportSubject.replace("[Report]", "")}
          </div>
        </div>
        <div className="detail-cover-report" style={{ marginBottom: "35px" }}>
          <div className="detail-content-left">
            <img
              style={{
                width: "50px",
                borderRadius: "50%",
              }}
              src={JSON.parse(token.accessToken).image}
              alt="Admin Image"
            />
          </div>
          <div className="detail-content-right email">
            {data?.value?.reportFormat?.issuerEmail}
          </div>
        </div>
        {/* <div className="detail-cover">
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            <b>Report ID:</b> {data?.value?.reportFormat?.id}
          </div>
        </div> */}

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            {data.value.reportFormat.reportBody?.split("\r\n")[1]}
          </div>
        </div>

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            <b>Trạng Thái:</b>{" "}
            <span
              className={
                data?.value?.reportFormat?.reportStatus == "Resolved"
                  ? "resolved"
                  : "unresolved"
              }
            >
              {ReportStatus[data?.value?.reportFormat?.reportStatus]}
            </span>
          </div>
        </div>

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            <b>Mức Độ Ảnh Hưởng:</b>
            <span
              className={
                data?.value?.reportFormat?.reportImpact == 0
                  ? "low"
                  : data?.value?.reportFormat?.reportImpact == 1
                  ? "medium"
                  : "HIGH"
              }
            >
              {ReportImpact[data?.value?.reportFormat?.reportImpact]}
            </span>
          </div>
        </div>

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            <b>Cần Giải Quyết Trước:</b>{" "}
            {dayFormat(data?.value?.reportFormat?.expectedResolutionDate)}
          </div>
        </div>

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            <b>Ngày Giải Quyết:</b>{" "}
            {dayFormat(data?.value?.reportFormat?.actualResolutionDate)}
          </div>
        </div>

        <div className="detail-cover-report">
          <div className="detail-content-left"></div>
          <div className="detail-content-right">
            <b>Phản Hồi:</b> {data?.value?.reportFormat?.reportResponse}
          </div>
        </div>

        <div className="button-cover grid">
          <Button
            className="btnCancel"
            variant="danger"
            onClick={handleNavigate}
          >
            Trở Về
          </Button>
          <Link to={`/response/${id}`}>
            <Button className="btnLink" variant="success">
              Phản Hồi
            </Button>
          </Link>
        </div>
      </div>
    </div>
  );
};
