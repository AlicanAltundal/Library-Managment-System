import React, { useEffect, useState } from "react";
import { getPublisherById, updatePublisher } from "../service/publisherService";
import "./updatePublisher.css";
import { useParams } from "react-router-dom";

const UpdatePublisher = () => {
  const { id } = useParams();
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [message, setMessage] = useState("");

  useEffect(() => {
    getPublisherById(id)
      .then((data) => {
        setName(data.name);
        setAddress(data.address);
      })
      .catch(() => setMessage("Yayınevi bilgileri alınamadı ❌"));
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updatePublisher({ id, name, address });
      setMessage("✅ Yayınevi başarıyla güncellendi!");
    } catch (err) {
      console.error(err);
      setMessage("❌ Güncelleme sırasında hata oluştu.");
    }
  };

  return (
    <div className="updatepublisher-container">
      <div className="updatepublisher-box">
        <h2 className="updatepublisher-title">Yayınevi Güncelle</h2>

        <form className="updatepublisher-form" onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="Yayınevi Adı"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />

          <input
            type="text"
            placeholder="Adres"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
            required
          />

          <button type="submit">Güncelle</button>
          {message && <p className="message">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default UpdatePublisher;
