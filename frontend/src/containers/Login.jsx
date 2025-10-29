import React, { useState } from "react";
import api from "../service/api";
import "./login.css";
import { jwtDecode } from "jwt-decode";


export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");



const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    const res = await api.post("/Auth/Login", { email, password });
    const { token, fullName, role } = res.data;

    const decoded = jwtDecode(token);
    const memberId = parseInt(decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]);

    localStorage.setItem("token", token);
    localStorage.setItem("role", role);
    localStorage.setItem("memberId", memberId);

    alert(`Hoş geldin, ${fullName}!`);
    window.location.href = role === "Admin" ? "/dashboard" : "/home";
  } catch (err) {
    setError("Giriş başarısız! E-posta veya şifre hatalı.");
  }
};



  return (
    <div className="login-page">
      <div className="login-card scale-up-center">
        <h2>Üye Girişi</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="email"
            placeholder="E-posta adresi"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <input
            type="password"
            placeholder="Şifre"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <button type="submit">Giriş Yap</button>
          {error && <p className="error">{error}</p>}
        </form>
      </div>
    </div>
  );
}
