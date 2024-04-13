import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { DETAIL_REPORT, EMPLOYEE_DETAIL, useApi } from "../../Api";
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
  const [employeeDetail, setEmployeeDetail] = useState<any>();
  const [employeeDetailLoaded, setEmployeeDetailLoaded] = useState<boolean>(false);

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

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const response = await useApi.get(DETAIL_REPORT.replace(":id", id));
        const data = response.data.value;
        setData(data);
        if (data) {
          await fetchEmployeeDetail(data.reportFormat?.issuerEmail);
        }
      } catch (error) {
        setLoading(false);
        console.error("Error fetching tree detail:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  const fetchEmployeeDetail = async (email: string) => {
    try {
      const response = await useApi.get(EMPLOYEE_DETAIL.replace(":email", "email=" + email));
      const employeeDetail = response.data;
      setEmployeeDetail(employeeDetail);
      setEmployeeDetailLoaded(true); // Đánh dấu rằng dữ liệu đã được tải thành công
    } catch (error) {
      console.error("Error fetching employee detail:", error);
    }
  };

  const images: ReactImageGalleryItem[] =
    data?.reportFormat?.reportImages?.map((img) => ({
      original: img,
      thumbnail: img
    })) || [];


  return data ? (
    <div className="main-layout row">
      <div className="detail-cover">
        <div style={{ display: 'flex', justifyContent: "space-between" }}>
          <h4 className="title">
            Tiêu Đề:
            {data.reportFormat?.reportSubject.replace("[Report]", "")}
          </h4>
          <div className="detail-content-right impact" style={{ margin: 'auto 1rem' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <b>Trạng Thái:</b>
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
            <div>
              <b>Mức Độ Ảnh Hưởng:</b>{" "}
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
        </div>
        <hr className="line" style={{ margin: '0' }} />
      </div>

      <div className="detail-cover-report" style={{ display: 'flex', justifyContent: "space-between" }}>
        <div className="detail-content-right">
          <div style={{ display: 'flex' }}>
            <img
              style={{
                width: "50px",
                borderRadius: "50%",
                margin: '0 1rem'
              }}
              src={employeeDetailLoaded ? employeeDetail?.picture : '../assets/imgs/avatar.jpg'}
              alt="issuer"
            />
            <div>
              <h5>{data.reportFormat?.issuerEmail}</h5>
              <h6>{employeeDetail?.name}</h6>
            </div>
          </div>
        </div>
        <div className="detail-content-right impact" style={{ marginRight: '1rem' }}>
          <b>Cần Giải Quyết Trước:</b>{" "}
          {dayFormat(data.reportFormat?.expectedResolutionDate)}
        </div>
      </div>

      <div className="report-form">
        <div className="detail-cover-report">
          <div className="detail-content-right body">
            {data.reportFormat.reportBody?.split("\r\n")[1]}
          </div>
        </div>

        <hr className="line" style={{ marginTop: '3rem', opacity: '.1' }} />

        <div className="detail-cover-report">
          <div className="image-container">
            <ImageGallery items={images} showThumbnails={false} showFullscreenButton={false} showPlayButton={false}

            />
          </div>
        </div>

        <hr className="line" style={{ opacity: '.1' }} />

        <div className="detail-cover-report" >
          <div className="detail-content-right impact" style={{ display: 'grid', marginLeft: '3rem', marginTop: '1rem', marginRight: '1rem', marginBottom: '1rem', width: '100%' }}>
            <h5>Phản Hồi:</h5>
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <div>{data.reportFormat?.reportResponse}</div>
              <div>
                {ReportStatus[data.reportFormat?.reportStatus] ==
                  ReportStatus.Resolved && (
                    <>
                      <div className="detail-cover-report" >
                        <div className="detail-content-left"></div>
                        <div className="detail-content-right">
                          <b>Ngày Giải Quyết:</b>{" "}
                          {dayFormat(data.reportFormat?.actualResolutionDate)}
                        </div>
                      </div>
                    </>
                  )}
              </div>
            </div>
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
          {(JSON.parse(token.accessToken).role == "Admin" && data.reportFormat?.reportStatus == "UnResolved") && (
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
