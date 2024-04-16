import { useEffect, useState, useRef } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { DETAIL_REPORT, EMPLOYEE_DETAIL, RESPONSE_REPORT, useApi } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { useCookies } from "react-cookie";

import { dayFormat } from "../../utils";
import "react-image-gallery/styles/scss/image-gallery.scss";
import ImageGallery, { ReactImageGalleryItem } from "react-image-gallery";
import Swal from "sweetalert2";
import SimpleMap from "../ManageTree/MapIntergration";

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
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);



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

  // ----------------------------------------------------------------
  const handleResponseClick = async () => {
    const { value } = await Swal.fire({
      title: 'Phản Hồi',
      html: `
      <textarea id="swal-input1" class="swal2-input" placeholder="Nhập nội dung phản hồi"></textarea>
      <div class="swal2-radio">
      <label style="color: red;  fontWeight: bold;">
      <input type="radio" name="status" value="0">
          Chưa được xử lý
        </label>
        <label style="color: darkgreen; fontWeight: bold;">
          <input type="radio" name="status" value="1">
          Đã xử lý
        </label>
      </div>
    `,
      focusConfirm: false,
      confirmButtonText: 'Gửi',
      preConfirm: () => {
        const response = (document.getElementById('swal-input1') as HTMLInputElement).value;
        const status = (document.querySelector('input[name="status"]:checked') as HTMLInputElement)?.value;
        if (!response) {
          Swal.showValidationMessage('Nhập phản hồi cho báo cáo');
        }
        return { response, status: Number(status) };
      },
    });

    if (value) {
      setIsLoading(true);
      try {
        await useApi.post(RESPONSE_REPORT, {
          ...value,
          reportID: id,
        });
        ref.current?.reload();
        navigate(-1);

        // Show success alert
        Swal.fire(
          'Thành công!',
          'Đã phản hồi báo cáo.',
          'success'
        );
      } catch (error) {
        console.error("Error submitting response:", error);

        // Show error alert
        Swal.fire(
          'Lỗi!',
          'Không thể phản hồi báo cáo. Vui lòng thử lại.',
          'error'
        );
      } finally {
        setIsLoading(false);
      }
    }
  };
  // ----------------------------------------------------------------
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
              <h6 style={{ fontWeight: 'bold', fontSize: '1.25rem' }}>{employeeDetail?.name}</h6>
            </div>
          </div>
        </div>
        <div className="detail-content-right impact" style={{ marginRight: '1rem' }}>
          <div style={{ backgroundColor: '#F0CD07', fontWeight: 'bold', padding: 6, borderRadius: 8 }}>
            Cần Giải Quyết Trước: {dayFormat(data.reportFormat?.expectedResolutionDate)}
          </div>

        </div>
      </div>

      <div className="report-form">
        <h2 style={{ padding: '1rem', fontWeight: 'bold', textDecoration: 'underline', color: '#2282F3' }}>Nội dung</h2>
        <div className="detail-cover-report">

          <div className="detail-content-right body">
            <div>
              <p>{data.reportFormat.reportBody?.split("\r\n")[1]}</p>
            </div>

          </div>
        </div>

        <hr className="line" style={{ marginTop: '3rem', opacity: '.1' }} />

        <div className="detail-cover-report">
          <div className="image-container">
            <ImageGallery
              items={images}
              showThumbnails={false}
              showFullscreenButton={false}
              showPlayButton={false}
              additionalClass='custom-image-gallery'
              renderItem={item => (
                <div className='image-gallery-image'>
                  <img
                    src={item.original}
                    alt={item.originalAlt}
                    srcSet={item.srcSet}
                    sizes={item.sizes}
                    title={item.originalTitle}
                    style={{
                      width: '70%',
                      height: '70%',
                      objectFit: 'contain',
                    }}
                  />
                  {
                    item.description &&
                    <span className='image-gallery-description' style={{ right: '0', left: 'initial' }}>
                      {item.description}
                    </span>
                  }
                </div>
              )}
            />
          </div>
        </div>

        <hr className="line" style={{ opacity: '.1' }} />
                     
          <h4 className="detail-content-right body">
            Địa Điểm: 
            {data.reportFormat?.issueLocation}
          </h4>
          <div className="map" style={{height: "350px" }}>
              <SimpleMap location={data.reportFormat?.issueLocation} />
          </div>

        <hr className="line" style={{ opacity: '.1' }} />

        <div className="detail-cover-report" >
          <div className="detail-content-right impact" style={{ display: 'grid', marginTop: '1rem', marginRight: '1rem', marginBottom: '1rem', width: '100%' }}>
            <h4 style={{ fontWeight: "bold", textDecoration: "underline", color: '#2282F3' }}>Phản Hồi:</h4>
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <div style={{ padding: '1rem' }}>{data.reportFormat?.reportResponse}</div>
              <div>
                {ReportStatus[data.reportFormat?.reportStatus] ==
                  ReportStatus.Resolved && (
                    <>
                      <div style={{ backgroundColor: '#CAF7B8' }} className="detail-cover-report" >
                        <div className="detail-content-right">
                          <b>Đã Giải Quyết:</b>{" "}
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
            <Button onClick={handleResponseClick} className="btnLink" variant="success">
              Phản Hồi
            </Button>
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
