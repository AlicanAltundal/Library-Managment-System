import React, { useState, useEffect } from "react";
import { createBook } from "../service/bookService";
import api from "../service/api";
import "./addBook.css";

const AddBook = () => {
  const [form, setForm] = useState({
    isbn: "",
    title: "",
    genre: "",
    publicationDate: "",
    publisherId: "",
  });

  const [publishers, setPublishers] = useState([]);
  const [authors, setAuthors] = useState([]);
  const [authorIds, setAuthorIds] = useState([]); // ✅ Çoklu yazar seçimi
  const [message, setMessage] = useState("");

  // 📚 Yayınevleri + Yazarları getir
  useEffect(() => {
    api.get("/Publisher/GetAllPublishers")
      .then((res) => setPublishers(res.data))
      .catch((err) => console.error("Yayınevleri alınamadı:", err));

    api.get("/Author/GetAllAuthors")
      .then((res) => setAuthors(res.data))
      .catch((err) => console.error("Yazarlar alınamadı:", err));
  }, []);

  // 📘 Form değişimi
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  // 💾 Kitap ekleme
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createBook({
        ...form,
        publisherId: parseInt(form.publisherId),
        publicationDate: new Date(form.publicationDate),
        authorIds: authorIds.map((id) => parseInt(id)), // ✅ backend’e gönder
      });

      setMessage("✅ Kitap başarıyla eklendi!");
      setForm({ isbn: "", title: "", genre: "", publicationDate: "", publisherId: "" });
      setAuthorIds([]);
    } catch (error) {
      console.error(error);
      setMessage("❌ Kitap eklenemedi!");
    }
  };

  return (
    <div className="addbook-container">
      <div className="addbook-box">
        <h2 className="addbook-title">📘 Yeni Kitap Ekle</h2>
        <form className="addbook-form" onSubmit={handleSubmit}>
          <input
            type="text"
            name="isbn"
            placeholder="ISBN"
            value={form.isbn}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="title"
            placeholder="Başlık"
            value={form.title}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="genre"
            placeholder="Tür"
            value={form.genre}
            onChange={handleChange}
            required
          />
          <input
            type="date"
            name="publicationDate"
            value={form.publicationDate}
            onChange={handleChange}
            required
          />

          {/* 🎨 Çoklu yazar seçimi */}
          <select
            multiple
            value={authorIds}
            onChange={(e) =>
              setAuthorIds(Array.from(e.target.selectedOptions, (opt) => opt.value))
            }
            className="addbook-select"
            required
          >
            <option disabled>Yazar Seç</option>
            {authors.map((a) => (
              <option key={a.id} value={a.id}>
                {a.firstName} {a.lastName}
              </option>
            ))}
          </select>

          {/* 🏢 Yayınevi seçimi */}
          <select
            name="publisherId"
            value={form.publisherId}
            onChange={handleChange}
            required
          >
            <option value="">Yayınevi Seç</option>
            {publishers.map((p) => (
              <option key={p.id} value={p.id}>
                {p.name}
              </option>
            ))}
          </select>

          <button type="submit">Kitabı Ekle</button>
          {message && <p className="message">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default AddBook;
