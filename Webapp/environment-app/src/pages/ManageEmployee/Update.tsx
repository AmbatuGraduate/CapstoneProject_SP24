import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { DEPARTMENT_LIST, EMPLOYEE_DETAIL, EMPLOYEE_UPDATE, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import Swal from "sweetalert2";

export const UpdateEmployee = () => {
  const navigate = useNavigate();
  const { email = "" } = useParams();
  const [data, setData] = useState<any>();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);

  const fetch = async () => {
    try {
      const data = await useApi.get(EMPLOYEE_DETAIL.replace(":email", "email=" + email));
      setData(data.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  const splitFullName = (fullName: string) => {
    const parts = fullName.split(" ");
    const lastName = parts[0]; // Lấy phần tử đầu tiên của chuỗi
    const firstName = parts.slice(1).join(" "); // Lấy các phần tử còn lại và ghép lại thành họ và tên
    return { firstName, lastName };
  };

  const { firstName, lastName } = splitFullName(data?.name || "");

  const fields: Field[] = [
    {
      label: "Họ",
      formType: "shortInput",
      keyName: "name",
      defaultValue: lastName,
    },
    {
      label: "Tên",
      formType: "shortInput",
      keyName: "familyName",
      defaultValue: firstName,
    },
    {
      label: "Email",
      formType: "input",
      keyName: "email",
      defaultValue: data?.email,
    },
    {
      label: "Số Điện Thoại",
      formType: "shortInput",
      keyName: "phone",
      defaultValue: data?.phoneNumber,
    },
    {
      label: "Bộ Phận",
      formType: "select",
      keyName: "departmentEmail",
      optionExtra: {
        url: DEPARTMENT_LIST,
        _key: "name",
        _value: "email",
      },
      defaultValue: data?.department,
    },
    {
      label: "Chức Vụ",
      formType: "select",
      keyName: "userRole",
      options: [
        {
          key: "Nhân Viên",
          value: 1,
        },
        {
          key: "Quản Lý",
          value: 2,
        },
      ],
      defaultValue: data?.role,
    },

    {
      label: "Địa Chỉ Thường Trú",
      formType: "input",
      keyName: "address",
      defaultValue: data?.address,
      // googleAddress: true,
      // value: address,
      // onChange: (e) => {
      //   setAddress(e.target.value);
      // },
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      Swal.fire({
        title: 'Đang cập nhật nhân viên...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });
      await useApi.put(EMPLOYEE_UPDATE, {
        ...data,
        password: ""
      });
      Swal.close();
      Swal.fire(
        'Thành công!',
        'Cập nhật nhân viên thành công!',
        'success'
      );
      ref.current?.reload();
      navigate(-1)
    } catch (error) {
      console.error("Error update employee:", error);
      Swal.fire(
        'Lỗi!',
        'Lỗi khi cập nhật nhân viên! Vui lòng thử lại sau.',
        'error'
      )
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="form-cover">
      <h4>Cập Nhật Thông Tin Nhân Viên</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate(-1)}
      />
    </div>
  );
};
