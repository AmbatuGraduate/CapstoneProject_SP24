import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  EMPLOYEE_LIST,
  GROUP_DETAIL,
  GROUP_UPDATE,
  useApi,
} from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import Swal from "sweetalert2";

export const UpdateGroup = () => {
  const navigate = useNavigate();
  const { email = "" } = useParams();
  const ref = useRef<any>();
  const [data, setData] = useState<any>();
  const [, setIsLoading] = useState(false);

  const fetch = async () => {
    try {
      const data = await useApi.get(GROUP_DETAIL.replace(":email", email));
      setData(data.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  const fields: Field[] = [
    {
      label: "Email",
      formType: "input",
      keyName: "email",
      defaultValue: data?.value?.email,
      disabled: true,
    },
    {
      label: "Tên Bộ Phận",
      formType: "input",
      keyName: "name",
      defaultValue: data?.value?.name,
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập tên bộ phận",
    },
    {
      label: "Mô tả",
      formType: "input",
      keyName: "description",
      defaultValue: data?.value?.description,
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập mô tả cho bộ phận",
    },
    // {
    //   label: "Nhân viên",
    //   formType: "select",
    //   keyName: "members",
    //   defaultValue: data?.value?.members,
    //   optionExtra: {
    //     url: EMPLOYEE_LIST,
    //     _key: "email",
    //     _value: "email",
    //   },
    // },
    {
      label: "Quản lý",
      formType: "select",
      keyName: "owners",
      defaultValue: data?.value?.owners,
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {

    try {
      Swal.fire({
        title: 'Đang cập nhật bộ phận...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });
      await useApi.post(GROUP_UPDATE.replace(":email", email), {
        ...data,
        members: data?.owners,
      });
      Swal.close();
      Swal.fire(
        'Thành công!',
        'Cập nhật bộ phận thành công!',
        'success'
      );
      ref.current?.reload();
      navigate(-1)
    } catch (error) {
      console.error("Error update group:", error);
      Swal.fire(
        'Lỗi!',
        'Lỗi khi cập nhật bộ phận! Vui lòng thử lại sau.',
        'error'
      )
    } finally {
      setIsLoading(false);
    }
    navigate(-1);
  };

  return (
    <div className="form-cover">
      <h4>Cập Nhật Bộ Phận</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate(-1)}
      />
    </div>
  );
};
