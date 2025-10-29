import React, { useEffect, useState } from "react";
import api from "../service/api";
import { useParams } from "react-router-dom";
import "./updateMember.css";
import { updateMember } from "../service/memberService";

const UpdateMember = () => {
  const { id } = useParams();
  const [form, setForm] = useState({
    memberCode: "",
    fullName: "",
    email: "",
    address: "",
    phoneNumber: "",
    role: "User",
  });

  const [message, setMessage] = useState("");

  // 🧠 Üye bilgilerini çek
  useEffect(() => {
    console.log("🟢 useEffect çalıştı, ID:", id);
    api
      .get(`/Member/GetById/${id}`)
      .then((res) => {
        console.log("✅ Üye bilgileri geldi:", res.data);
        setForm(res.data);
      })
      .catch((err) => {
        console.error("❌ Üye bilgileri alınamadı:", err);
        setMessage("❌ Üye bilgileri alınamadı.");
      });
  }, [id]);

  // 🖊 Form değişimi
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  // 💾 Güncelleme işlemi
  const handleSubmit = async (e) => {
    e.preventDefault();

    console.log("🚀 Güncelle butonuna tıklandı!");
    console.log("🧩 Gönderilecek form verisi:", form);

    try {
      await updateMember({
        id: parseInt(id),
        memberCode: form.memberCode,
        fullName: form.fullName,
        email: form.email,
        address: form.address,
        phoneNumber: form.phoneNumber,
        role: form.role,
      });

      console.log("✅ Güncelleme isteği başarılı!");
      setMessage("✅ Üye başarıyla güncellendi!");
    } catch (err) {
      console.error("🔥 Güncelleme hatası:", err);
      if (err.response) {
        console.error("📦 Backend yanıtı:", err.response.data);
        console.error("📡 Status:", err.response.status);
      }
      setMessage("❌ Güncelleme sırasında bir hata oluştu.");
    }
  };

  return (
    <div className="updatemember-container">
      <div className="updatemember-box">
        <h2 className="updatemember-title">Üye Bilgilerini Güncelle</h2>

        <form className="updatemember-form" onSubmit={handleSubmit}>
          <input
            type="text"
            name="memberCode"
            placeholder="Üye Kodu"
            value={form.memberCode}
            onChange={handleChange}
            required
          />

          <input
            type="text"
            name="fullName"
            placeholder="Ad Soyad"
            value={form.fullName}
            onChange={handleChange}
            required
          />

          <input
            type="email"
            name="email"
            placeholder="E-posta"
            value={form.email}
            onChange={handleChange}
            required
          />

          <input
            type="text"
            name="address"
            placeholder="Adres"
            value={form.address}
            onChange={handleChange}
            required
          />

          <input
            type="text"
            name="phoneNumber"
            placeholder="Telefon Numarası"
            value={form.phoneNumber}
            onChange={handleChange}
            required
          />

          <select name="role" value={form.role} onChange={handleChange} required>
            <option value="User">User</option>
            <option value="Admin">Admin</option>
          </select>

          <button type="submit">Güncelle</button>
          {message && <p className="message">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default UpdateMember;
