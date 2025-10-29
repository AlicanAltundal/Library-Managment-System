import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../service/api";
import "./authorDetail.css";

const AuthorDetail = () => {
  const { id } = useParams();
  const [author, setAuthor] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    api.get(`/Author/GetById/${id}`)
      .then(res => setAuthor(res.data))
      .catch(() => console.error("Yazar bulunamadı"));
  }, [id]);

  if (!author) return <div>Yükleniyor...</div>;

  return (
    <div className="authordetail-container">
      <div className="authordetail-box">
        <h2>{author.firstName} {author.lastName}</h2>
        <p>{author.biography ?? "Bu yazar hakkında bilgi bulunmamaktadır."}</p>

        <h3>Eserleri:</h3>
        <ul>
          {author.books?.map((b) => (
            <li key={b.id} onClick={() => navigate(`/book/${b.id}`)} className="clickable">
              {b.title}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default AuthorDetail;
