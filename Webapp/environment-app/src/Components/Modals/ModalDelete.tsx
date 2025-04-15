import { useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import { RiDeleteBin5Fill } from "react-icons/ri";

type Props = {
  handleDelete: () => void;
  title?: string;
  description?: string;
};

function ModalDelete(props: Props) {
  const { title, description, handleDelete } = props;
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const onDelete = async () => {
    handleDelete && handleDelete();
    handleClose();
  };

  return (
    <>
      <button type="button" className="btn btn-click" onClick={handleShow}>
        <RiDeleteBin5Fill />
      </button>

      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title>{title || "Cảnh báo"}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{description || "Bạn có chắc chắn muốn xóa?"}</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Hủy
          </Button>
          <Button variant="primary" onClick={onDelete}>
            Xóa
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ModalDelete;
