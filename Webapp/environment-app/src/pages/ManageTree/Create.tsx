import { useNavigate } from "react-router-dom";
import {
  DEPARTMENT_LIST,
  TREE_ADD,
  TREE_TYPE_LIST,
  useApi,
} from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";
import Swal from "sweetalert2";

export const CreateTree = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [plantTime, setPlantTime] = useState<Date | null>(null);
  const [intervalCutTime, setIntervalCutTime] = useState<number>(1);
  const [address, setAddress] = useState<string | null>("");
  const [token] = useCookies(["accessToken"]);

  const cutTime = () => {
    const newCutTime = new Date(plantTime || new Date());
    newCutTime.setMonth(newCutTime.getMonth() + intervalCutTime * 3);
    return newCutTime;
  };

  const convertAddressToAbbreviation = () => {
    // Split the address into parts based on comma and space
    let parts = address?.split(", ");

    // Initialize an array to store the abbreviated parts
    let abbreviatedParts: any[] = [];

    // Iterate over each part of the address
    parts?.forEach((part, index) => {
      // Remove accents/diacritics from the part
      part = part.normalize("NFD").replace(/[\u0300-\u036f]/g, "");

      // Check if it's the first part (house number)
      let abbreviation;
      if (index === 0) {
        abbreviation = part.split(" ").join("");
      } else {
        // Abbreviate the part by taking the first letter of each word
        abbreviation = part
          .split(" ")
          .map((word) => word.charAt(0).toUpperCase())
          .join("");
      }

      // Push the abbreviated part to the array
      abbreviatedParts.push(abbreviation);
    });

    // Join the abbreviated parts together with underscores
    let abbreviation = abbreviatedParts.join("_");

    return abbreviation;
  };

  // --------------------------------------
  const handleTreeTypeClick = async () => {
    const { value } = await Swal.fire({
      title: "Thêm loại cây",
      html: `
      <textarea id="swal-input1" name"treeTypeName" class="swal2-input" placeholder="Nhập loại cây"></textarea>
    `,
      showCancelButton: true,
      focusConfirm: false,
      confirmButtonText: "Thêm loại cây",
      preConfirm: () => {
        const treeTypeName = (
          document.getElementById("swal-input1") as HTMLInputElement
        ).value;
        return { treeTypeName };
      },
    });

    if (value) {
      setIsLoading(true);
      try {
        await useApi.post(TREE_TYPE_LIST, {
          ...value,
        });
        ref.current?.reload();
        navigate("/manage-tree/create");

        // Show success alert
        Swal.fire("Thành công!", "Thêm giống cây thành công.", "success");
      } catch (error) {
        console.error("Error submitting response:", error);

        // Show error alert
        Swal.fire(
          "Lỗi!",
          "Không thể thêm giống cây. Vui lòng thử lại.",
          "error"
        );
      } finally {
        setIsLoading(false);
      }
    }
  };
  // --------------------------------------

  const fields: Field[] = [
    {
      label: "Mã Cây",
      formType: "shortInput",
      keyName: "treeCode",
      value: convertAddressToAbbreviation(),
      googleAddress: false,
      required: true,
      placeholder: "Ví dụ: 15_CD5_HX_CL",
    },
    {
      label: "Loại Cây",
      formType: "select",
      keyName: "treeTypeId",
      optionExtra: {
        url: TREE_TYPE_LIST,
        _key: "treeTypeName",
        _value: "treeTypeId",
      },
      googleAddress: false,
    },
    {
      label: "Địa Chỉ Cụ Thể",
      formType: "input",
      keyName: "treeLocation",
      googleAddress: true,
      // value: address,
      setAffectValue: setAddress,
      placeholder: "Nhập địa chỉ",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập địa chỉ",
    },
    {
      label: "Đường Kính Thân (cm)",
      formType: "number",
      keyName: "bodyDiameter",
      googleAddress: false,
      placeholder: "Ví dụ: 50",
    },
    {
      label: "Tán Lá (cm)",
      formType: "number",
      keyName: "leafLength",
      googleAddress: false,
      placeholder: "Ví dụ: 150",
    },
    {
      label: "Thời Điểm Trồng",
      formType: "date",
      keyName: "plantTime",
      defaultValue: new Date(),
      googleAddress: false,
      onChange: (date) => {
        setPlantTime(date);
      },
    },
    {
      label: "Thời Điểm Cắt",
      formType: "date",
      keyName: "cutTime",
      value: cutTime(),
      googleAddress: false,
    },
    {
      label: "Khoảng Thời Gian Cắt (Quý)",
      formType: "number",
      keyName: "intervalCutTime",
      defaultValue: intervalCutTime,
      googleAddress: false,
      onChange: (value) => setIntervalCutTime(Number(value || 0)),
    },
    {
      label: "Bộ Phận Phụ Trách",
      formType: "select",
      keyName: "email",
      optionExtra: {
        url: DEPARTMENT_LIST,
        _key: "name",
        _value: "email",
      },
      googleAddress: false,
      hiddenInput: true,
      display: JSON.parse(token.accessToken).role == "Manager" ? "None" : "",
    },

    {
      label: "Ghi Chú",
      formType: "textarea",
      keyName: "note",
      googleAddress: false,
      placeholder: "Ví dụ: Cần lưu ý...",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);
    try {
      Swal.fire({
        title: "Đang thêm cây...",
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });
      await useApi.post(TREE_ADD, {
        ...data,
        cutTime: data.cutTime,
        plantTime: data.plantTime,
        isExist: true,
        email:
          JSON.parse(token.accessToken).role == "Admin"
            ? data.email
            : JSON.parse(token.accessToken).departmentEmail,
      });
      Swal.close();
      Swal.fire("Thành công!", "Thêm cây mới thành công!", "success");
      ref.current?.reload();
      navigate("/manage-tree");
    } catch (error) {
      Swal.fire("Lỗi!", "Lỗi khi thêm cây! Vui lòng thử lại sau.", "error");
      console.error("Error creating tree:", error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="form-cover">
      <h4>Thêm cây</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-tree")}
      />
      <button
        className="btnAdd"
        onClick={handleTreeTypeClick}
        style={{ position: "absolute", bottom: "2rem", left: '17.5rem' }}
      >
        Thêm loại cây
      </button>
    </div>
  );
};
