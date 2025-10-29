import React, { useEffect, useState } from 'react';
import { deleteBook, getAllBooks, getAllAuthors } from '../service/bookService';
import { useNavigate } from 'react-router-dom';
import './bookList.css';

const BookList = () => {
  const [books, setBooks] = useState([]);
    const [authors, setAuthors] = useState([]);
  const navigate = useNavigate();

  
    const loadBooks = async () => {
      try {
        const data = await getAllBooks();
        setBooks(data);
      } catch (err) {
        console.error("Kitaplar getirilemedi:", err);
      }
    };

    const loadAuthors = async () => {
        try {
     const data = await getAllAuthors();
     setAuthors(data);
        }
        catch(err) {
            console.error("Yazarlar getirilemedi:", err)
        }
    }
  
    useEffect(() => {
      loadBooks();
      loadAuthors();
    }, []);
  
    const handleDelete = async (id) => {
      if (!window.confirm("Bu kitabı silmek istediğine emin misin?")) return;
  
      try {
        await deleteBook(id);
        alert("Kitap başarıyla silindi ✅");
        loadBooks(); // tabloyu güncelle
      } catch (err) {
        alert("❌ Silme işlemi başarısız oldu.");
      }
    };
  

  useEffect(() => {
    getAllBooks()
      .then(setBooks)
      .catch(err => console.error("Kitaplar getirilemedi:", err));
  }, []);


  return (
    <div className="booklist-container">
      <div className="booklist-box">
        <div className="booklist-header">
          <h2 className="booklist-title">Kitap Listesi</h2>
          <button className="addbook-btn" onClick={() => navigate('/add-book')}>
            Yeni Kitap Ekle
          </button>
        </div>

        <table className="booklist-table">
          <thead>
            <tr>
              <th>ISBN</th>
              <th>Başlık</th>
              <th>Tür</th>
              
              <th>Yazarlar</th>
              <th>Yayınevi</th>
                      <th>İşlemler</th>
            </tr>
          </thead>
          <tbody>
            {books.length > 0 ? (
              books.map((b) => (
                <tr key={b.id}>
                  <td>{b.isbn}</td>
                  <td>{b.title}</td>
                  <td>{b.genre}</td>
         <td>
  {b.authors && b.authors.length > 0
    ? b.authors.join(", ")
    : "-"}
</td>
                <td>{b.publisherName ?? "-"}</td>
                                  <td>
                    <button
                      className="edit-btn"
                      onClick={() => navigate(`/update-book/${b.id}`)}
                    >
                      Güncelle
                    </button>
                    <button
                      className="delete-btn"
                      onClick={() => handleDelete(b.id)}
                    >
                      Sil
                    </button>
                  </td>

                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="4" className="empty-message">
                  Henüz kitap bulunamadı 📭
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default BookList;
