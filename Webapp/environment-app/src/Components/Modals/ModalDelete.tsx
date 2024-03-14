import axios from "axios";
import { useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import { RiDeleteBin5Fill } from "react-icons/ri";
import { TREE_DELETE, useApi } from "../../Api";
import { useParams } from "react-router-dom";

function ModalDelete() {
  const [show, setShow] = useState(false);
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const onDelete = async () => {
    try {
      const data = await useApi.get(TREE_DELETE.replace(":id", id));
      setData(data.data);
      console.log("Tree deleted successfully");
    } catch (error) {
      console.error("Error deleting item:", error);
    }
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
          <Modal.Title>Alert</Modal.Title>
        </Modal.Header>
        <Modal.Body>Bạn có chắc chắn muốn xóa?</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={onDelete}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ModalDelete;
