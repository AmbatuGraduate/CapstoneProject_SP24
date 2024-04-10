import { useCallback, useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { DETAIL_REPORT, useApi } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { useCookies } from "react-cookie";
import { dayFormat } from "../../utils";
import "react-image-gallery/styles/scss/image-gallery.scss";
import ImageGallery, { ReactImageGalleryItem } from "react-image-gallery";

export enum EReportImpact {
  LOW = 0,
  MEDIUM = 1,
  HIGH = 2,
}

export const DetailReport = () => {
  const [token] = useCookies(["accessToken"]);
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(false);

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
    navigate(-1);
  };

  const fetchData = useCallback(async () => {
    try {
      setLoading(true);
      const response = await useApi.get(DETAIL_REPORT.replace(":id", id));
      const data = response.data.value;
      setData(data);
      setLoading(false);
    } catch (error) {
      setLoading(false);
      console.error("Error fetching tree detail:", error);
    } finally {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    // console.log("id", id);

    // console.log(loading);
    fetchData();
  }, []);

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
  const images: ReactImageGalleryItem[] =
    data?.reportFormat?.reportImages?.map((img) => ({
      original: img,
      thumbnail: img
    })) || [];


  return data ? (
    <div className="main-layout row">
      <div className="detail-cover" style={{ marginBottom: "25px" }}>
        <div className="detail-content-left"></div>
        <div className="detail-content-right title">
          {data.reportFormat?.reportSubject.replace("[Report]", "")}
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
          {data.reportFormat?.issuerEmail}
        </div>
      </div>

      <div className="report-form">
        <div
          className="detail-cover-report"
          style={{
            marginBottom: "20px",
            borderBottom: "1px solid rgb(212, 212, 212)",
            paddingBottom: "20px",
          }}
        >
          <div className="detail-content-left"></div>
          <div className="detail-content-right body">
            {data.reportFormat.reportBody?.split("\r\n")[1]}
          </div>
        </div>

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right impact">
            <b>Trạng Thái:</b>{" "}
            <span
              className={
                data.reportFormat?.reportStatus == "Resolved"
                  ? "resolved"
                  : "unresolved"
              }
            >
              {ReportStatus[data.reportFormat?.reportStatus]}
            </span>
          </div>
        </div>

        <div
          className="detail-cover-report"
          style={{
            marginBottom: "20px",
            borderBottom: "1px solid rgb(212, 212, 212)",
            paddingBottom: "20px",
          }}
        >
          <div className="detail-content-left"></div>
          <div className="detail-content-right impact">
            <b>Mức Độ Ảnh Hưởng:</b>
            <span
              className={
                data.reportFormat?.reportImpact === EReportImpact.LOW
                  ? "low"
                  : data.reportFormat?.reportImpact === EReportImpact.MEDIUM
                  ? "medium"
                  : "HIGH"
              }
            >
              {ReportImpact[data.reportFormat?.reportImpact]}
            </span>
          </div>
        </div>

        <div className="detail-cover-report" style={{ marginBottom: "20px" }}>
          <div className="detail-content-left"></div>
          <div className="detail-content-right impact">
            <b>Cần Giải Quyết Trước:</b>{" "}
            {dayFormat(data.reportFormat?.expectedResolutionDate)}
          </div>
        </div>

        {ReportStatus[data.reportFormat?.reportStatus] ==
          ReportStatus.Resolved && (
          <>
            <div
              className="detail-cover-report"
              style={{
                marginBottom: "20px",
                borderBottom: "1px solid rgb(212, 212, 212)",
                paddingBottom: "20px",
              }}
            >
              <div className="detail-content-left"></div>
              <div className="detail-content-right impact">
                <b>Ngày Giải Quyết:</b>{" "}
                {dayFormat(data.reportFormat?.actualResolutionDate)}
              </div>
            </div>

            <div className="detail-cover-report">
              <div className="detail-content-left"></div>
              <div className="detail-content-right impact">
                <b>Phản Hồi:</b> {data.reportFormat?.reportResponse}
              </div>
            </div>
          </>
        )}

        {/* <div className="detail-cover-report">
          <div className="image-container">
            {data.reportFormat.reportImages.map((image, index) => (
              <img key={index} src={image} alt={`Image ${index + 1}`} />
            ))}
          </div>
        </div> */}

        <div className="detail-cover-report">
          <div className="image-container">
            <ImageGallery items={images} />;
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
          {JSON.parse(token.accessToken).role == "Admin" && (
            <Link to={`/response/${id}`}>
              <Button className="btnLink" variant="success">
                Phản Hồi
              </Button>
            </Link>
          )}
        </div>
      </div>
    </div>
  ) : (
    <ClipLoader
      className="spinner"
      color={"hsl(94, 59%, 35%)"}
      loading={loading}
      size={60}
    />
  );
};
