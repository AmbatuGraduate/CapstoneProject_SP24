import { useNavigate } from "react-router-dom";
import { EMPLOYEE_LIST, TREE_ADD, TREE_TYPE_LIST, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useEffect, useRef, useState } from "react";
import { useCookies } from "react-cookie";
import Swal from "sweetalert2";

export const CreateTree = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [plantTime, setPlantTime] = useState<Date | null>(null);
  const [intervalCutTime, setIntervalCutTime] = useState<number>(1);

  const cutTime = () => {
    const newCutTime = new Date(plantTime || new Date());
    newCutTime.setMonth(newCutTime.getMonth() + intervalCutTime * 3);
    return newCutTime;
  };

  // --------------------------------------
  const handleTreeTypeClick = async () => {
    const { value } = await Swal.fire({
      title: 'Thêm giống cây',
      html: `
      <textarea id="swal-input1" name"treeTypeName" class="swal2-input" placeholder="Nhập giống cây"></textarea>
    `,
      showCancelButton: true,
      focusConfirm: false,
      confirmButtonText: 'Thêm giống cây',
      preConfirm: () => {
        const treeTypeName = (document.getElementById('swal-input1') as HTMLInputElement).value;
        return { treeTypeName };
      },
    });

    if (value) {
      setIsLoading(true);
      try {
        await useApi.post(TREE_TYPE_LIST, {
          ...value,
        });
        navigate("/manage-tree/create")

        // Show success alert
        Swal.fire(
          'Thành công!',
          'Thêm giống cây thành công.',
          'success'
        );
      } catch (error) {
        console.error("Error submitting response:", error);

        // Show error alert
        Swal.fire(
          'Lỗi!',
          'Không thể thêm giống cây. Vui lòng thử lại.',
          'error'
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
      // onChange: (e) => {
      //   setAddress(e.target.value);
      // },
      placeholder: "Nhập địa chỉ",
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
      label: "Người Phụ Trách",
      formType: "select",
      keyName: "email",
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
      googleAddress: false,
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
        title: 'Đang thêm cây...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });
      await useApi.post(TREE_ADD, {
        ...data,
        cutTime: data.cutTime,
        plantTime: data.plantTime,
        isExist: true,
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

  <div>
    <button className="btnAdd" onClick={handleTreeTypeClick}>Thêm giống cây</button>
  </div>

  return (
    <div className="form-cover">
      <h4>Thêm cây</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-tree")}
      />
      <button className="btnAdd" onClick={handleTreeTypeClick}>Thêm giống cây</button>
    </div>
  );
};
