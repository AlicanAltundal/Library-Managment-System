import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../service/api";
import "./memberProfile.css";

const MemberProfile = () => {
  const { id } = useParams();
  const [member, setMember] = useState(null);

  useEffect(() => {
    api.get(`/Member/GetById/${id}`)
      .then(res => setMember(res.data))
      .catch(() => console.error("Üye bulunamadı"));
  }, [id]);

  if (!member) return <div>Yükleniyor...</div>;

  return (
    <div className="memberprofile-container">
      <div className="memberprofile-box">
        <h2>{member.fullName}</h2>
        <p><strong>E-posta:</strong> {member.email}</p>
        <p><strong>Adres:</strong> {member.address}</p>
        <p><strong>Telefon:</strong> {member.phoneNumber}</p>
      </div>
    </div>
  );
};

export default MemberProfile;
