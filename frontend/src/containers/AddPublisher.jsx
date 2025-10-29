import React, { useState } from "react";
import { createPublisher } from "../service/publisherService";
import "./addPublisher.css";

const AddPublisher = () => {
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [message, setMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await createPublisher({ name, address });
      setMessage("✅ Yayınevi başarıyla eklendi!");
      setName("");
      setAddress("");
    } catch (err) {
      console.error(err);
      setMessage("❌ Yayınevi eklenirken hata oluştu!");
    }
  };

  return (
    <div className="addpublisher-container">
      <div className="addpublisher-box">
        <h2 className="addpublisher-title">🏢 Yeni Yayınevi Ekle</h2>

        <form className="addpublisher-form" onSubmit={handleSubmit}>
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

          <button type="submit">Yayınevini Kaydet</button>
          {message && <p className="message">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default AddPublisher;
