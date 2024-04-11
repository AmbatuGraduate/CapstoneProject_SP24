import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { DEPARTMENT_LIST, EMPLOYEE_DETAIL, EMPLOYEE_UPDATE, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dayFormat } from "../../utils";
import { useCookies } from "react-cookie";

export const UpdateEmployee = () => {
  const navigate = useNavigate();
  const { email = "" } = useParams();
  const [data, setData] = useState<any>();
  const [token] = useCookies(["accessToken"]);
  const [address, setAddress] = useState<string | null>("");
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
      formType: "input",
      key: "name",
      defaultValue: lastName,
    },
    {
      label: "Tên",
      formType: "input",
      key: "familyName",
      defaultValue: firstName,
    },
    {
      label: "Email",
      formType: "input",
      key: "email",
      defaultValue: data?.email,
    },
    {
      label: "Số Điện Thoại",
      formType: "input",
      key: "phoneNumber",
      defaultValue: data?.phoneNumber,
    },
    {
      label: "Bộ Phận",
      formType: "select",
      key: "departmentEmail",
      optionExtra: {
        url: DEPARTMENT_LIST,
        _key: "name",
        _value: "email",
      },
      defaultValue: data?.department,
    },
    // {
    //   label: "Chức Vụ",
    //   formType: "input",
    //   key: "role",
    //   defaultValue: data?.role,
    // },

    {
      label: "Địa Chỉ Thường Trú",
      formType: "input",
      key: "address",
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
      await useApi.post(EMPLOYEE_UPDATE, {
        ...data,
      });
      ref.current?.reload();
      navigate(-1)
    } catch (error) {
      console.error("Error update employee:", error);
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
