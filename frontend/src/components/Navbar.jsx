import React from "react";
import "./navbar.css";
import { useNavigate } from "react-router-dom";

const Navbar = ({ onSearchChange }) => {
  const navigate = useNavigate();

  return (
    <nav className="navbar">
      <div className="navbar-left" onClick={() => navigate("/home")}>
        <span className="navbar-title">Kütüphane Yönetim Sistemi</span>
      </div>

      <div className="navbar-center">
        <input
          type="text"
          className="navbar-search"
          placeholder="Kitap, yazar veya üye ara..."
          onChange={(e) => onSearchChange(e.target.value)}
        />
      </div>

      <div className="navbar-right">
        <button className="navbar-btn" onClick={() => navigate("/login")}>
          Çıkış Yap
        </button>
      </div>
    </nav>
  );
};

export default Navbar;
