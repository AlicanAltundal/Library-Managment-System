import React, { useState } from "react";
import api from "../service/api";
import "./addMember.css";

const AddMember = () => {
  const [form, setForm] = useState({
    memberCode: "",
    fullName: "",
    address: "",
    phoneNumber: "",
    email: "",
    password: "",
    role: "User",
  });

  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post("/Member/CreateMember", form);
      setMessage("✅ Üye başarıyla eklendi!");
      setForm({
        memberCode: "",
        fullName: "",
        address: "",
        phoneNumber: "",
        email: "",
        password: "",
        role: "User",
      });
    } catch (err) {
      console.error(err);
      setMessage("❌ Üye eklenemedi!");
    }
  };

  return (
    <div className="addmember-container">
      <div className="addmember-box">
        <h2 className="addmember-title">👤 Yeni Üye Ekle</h2>
        <form className="addmember-form" onSubmit={handleSubmit}>
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

          <input
            type="email"
            name="email"
            placeholder="E-posta"
            value={form.email}
            onChange={handleChange}
            required
          />

          <input
            type="password"
            name="password"
            placeholder="Şifre"
            value={form.password}
            onChange={handleChange}
            required
          />

          <select
            name="role"
            value={form.role}
            onChange={handleChange}
            className="addmember-select"
          >
            <option value="User">User</option>
            <option value="Admin">Admin</option>
          </select>

          <button type="submit">Üyeyi Ekle</button>
          {message && <p className="message">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default AddMember;
