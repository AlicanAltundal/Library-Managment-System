import React, { useEffect, useState } from "react";
import { getBookById, updateBook } from "../service/bookService";
import { getAllPublishers } from "../service/publisherService";
import "./updateBook.css";
import { useParams } from "react-router-dom";

const UpdateBook = () => {
  const { id } = useParams();
  const [isbn, setIsbn] = useState("");
  const [title, setTitle] = useState("");
  const [genre, setGenre] = useState("");
  const [publicationDate, setPublicationDate] = useState("");
  const [publisherId, setPublisherId] = useState("");
  const [publishers, setPublishers] = useState([]);
  const [message, setMessage] = useState("");

  useEffect(() => {
    // kitap bilgisi
    getBookById(id)
      .then((data) => {
        setIsbn(data.isbn);
        setTitle(data.title);
        setGenre(data.genre);
        setPublicationDate(data.publicationDate?.split("T")[0]);
        setPublisherId(data.publisherId);
      })
      .catch(() => setMessage("Kitap bilgileri alınamadı ❌"));

    // yayınevleri listesi
    getAllPublishers().then(setPublishers);
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateBook({
        id,
        isbn,
        title,
        genre,
        publicationDate,
        publisherId: parseInt(publisherId),
      });
      setMessage("✅ Kitap başarıyla güncellendi!");
    } catch (err) {
      console.error(err);
      setMessage("❌ Güncelleme sırasında hata oluştu.");
    }
  };

  return (
    <div className="updatebook-container">
      <div className="updatebook-box">
        <h2 className="updatebook-title">Kitap Güncelle</h2>

        <form className="updatebook-form" onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="ISBN"
            value={isbn}
            onChange={(e) => setIsbn(e.target.value)}
            required
          />

          <input
            type="text"
            placeholder="Kitap Başlığı"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />

          <input
            type="text"
            placeholder="Tür"
            value={genre}
            onChange={(e) => setGenre(e.target.value)}
            required
          />

          <input
            type="date"
            value={publicationDate}
            onChange={(e) => setPublicationDate(e.target.value)}
            required
          />

          <select
            value={publisherId}
            onChange={(e) => setPublisherId(e.target.value)}
            required
          >
            <option value="0" disabled selected>Yayınevi Seç</option>
            {publishers.map((p) => (
              <option key={p.id} value={p.id}>
                {p.name}
              </option>
            ))}
          </select>

          <button type="submit">Güncelle</button>
          {message && <p className="message">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default UpdateBook;
