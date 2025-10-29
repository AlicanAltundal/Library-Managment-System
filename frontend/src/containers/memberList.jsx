import React, { useEffect, useState } from "react";
import api from "../service/api";
import { useNavigate } from "react-router-dom";
import "./memberList.css";
import { deleteMember, getAllMembers } from "../service/memberService";

const MemberList = () => {
  const [members, setMembers] = useState([]);
  const navigate = useNavigate();

  // 🔹 Üyeleri yükle
const loadMembers = async () => {
  try {
    const data = await getAllMembers();
    setMembers(Array.isArray(data) ? data : []);
  } catch (err) {
    console.error("Üyeler getirilemedi:", err);
    setMembers([]);
  }
};


  useEffect(() => {
    loadMembers();
  }, []);

  // 🔹 Üye sil
  const handleDelete = async (id) => {
    if (!window.confirm("Bu üyeyi silmek istediğine emin misin?")) return;
    try {
      await deleteMember(id);
      alert("Üye başarıyla silindi ✅");
      loadMembers();
    } catch (err) {
      alert("❌ Silme işlemi başarısız oldu.");
    }
  };

  return (
    <div className="memberlist-container">
      <div className="memberlist-box">
        <div className="memberlist-header">
          <h2 className="memberlist-title">Üye Listesi</h2>
          <button
            className="addmember-btn"
            onClick={() => navigate("/add-member")}
          >
            Yeni Üye Ekle
          </button>
        </div>

        <table className="memberlist-table">
          <thead>
            <tr>
              <th>Kod</th>
              <th>Ad Soyad</th>
              <th>E-posta</th>
              <th>Telefon</th>
              <th>Rol</th>
              <th>Adres</th>
              <th>İşlemler</th>
            </tr>
          </thead>
          <tbody>
            {members.length > 0 ? (
              members.map((m) => (
                <tr key={m.id}>
                  <td>{m.memberCode}</td>
                  <td>{m.fullName}</td>
                  <td>{m.email}</td>
                  <td>{m.phoneNumber ?? "-"}</td>
                  <td>
                    <span
                      className={
                        m.role === "Admin" ? "role-admin" : "role-user"
                      }
                    >
                      {m.role}
                    </span>
                  </td>
                  <td>{m.address}</td>
                  <td>
                    <button
                      className="edit-btn"
                      onClick={() => navigate(`/update-member/${m.id}`)}
                    >
                      Güncelle
                    </button>
                    <button
                      className="delete-btn"
                      onClick={() => handleDelete(m.id)}
                    >
                      Sil
                    </button>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="7" className="empty-message">
                  Henüz üye bulunamadı 📭
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default MemberList;
