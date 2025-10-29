import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../service/api";
import "./bookDetail.css";

const BookDetail = () => {
  const { id } = useParams();
  const [book, setBook] = useState(null);
  const [message, setMessage] = useState("");

  useEffect(() => {
    api.get(`/Book/GetById/${id}`)
      .then(res => setBook(res.data))
      .catch(() => setMessage("Kitap bulunamadı"));
  }, [id]);

const handleLoan = async () => {
  try {
    const memberId = parseInt(localStorage.getItem("memberId"));
    const payload = {
      bookId: parseInt(id),
      memberId,
      librarianId: 1,
      dueDate: new Date(Date.now() + 14 * 24 * 60 * 60 * 1000).toISOString(),
    };

    console.log("Loan payload:", payload);
    await api.post(`/Loan/CreateLoan`, payload);

    setMessage("📖 Kitap başarıyla ödünç alındı!");
  } catch (err) {
    console.error("Loan hatası:", err);

    // ✅ Hata mesajını backend’den al
    if (err.response?.data?.message) {
      setMessage(`❌ ${err.response.data.message}`);
    } else {
      setMessage("❌ İşlem sırasında bir hata oluştu.");
    }
  }
};




  if (!book) return <div className="loading">Yükleniyor...</div>;

  return (
    <div className="bookdetail-container">
      <div className="bookdetail-box">
        <h2>{book.title}</h2>
        <p><strong>Yazarlar:</strong> {book.authors?.join(", ") || "-"}</p>
        <p><strong>Tür:</strong> {book.genre}</p>
        <p><strong>Yayınevi:</strong> {book.publisherName}</p>
        <button className="loan-btn" onClick={handleLoan}>📚 Kirala</button>
        {message && <p className="loan-message">{message}</p>}
      </div>
    </div>
  );
};

export default BookDetail;
