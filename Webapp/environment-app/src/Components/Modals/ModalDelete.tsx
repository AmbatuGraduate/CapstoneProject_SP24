import axios from "axios";
// import React from "react";
import { useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import { RiDeleteBin5Fill } from "react-icons/ri";

function ModalDelete() {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const onDelete = (treeCode) => {
    axios
      .delete(`your_api_endpoint/${treeCode}`)
      .then((/*response*/) => {
        console.log("Item deleted successfully");
        // loadListData();
      })
      .catch((error) => {
        console.error("Error deleting item:", error);
      });
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
