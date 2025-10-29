import React, { useEffect, useState } from 'react';
import { getAllPublishers, deletePublisher } from '../service/publisherService';
import './publisherList.css';
import { useNavigate } from 'react-router-dom';

const PublisherList = () => {
  const [publishers, setPublishers] = useState([]);
  const navigate = useNavigate();

  const loadPublishers = async () => {
    try {
      const data = await getAllPublishers();
      setPublishers(data);
    } catch (err) {
      console.error("Yayınevleri getirilemedi:", err);
    }
  };

  useEffect(() => {
    loadPublishers();
  }, []);

  const handleDelete = async (id) => {
    if (!window.confirm("Bu yayınevini silmek istediğine emin misin?")) return;

    try {
      await deletePublisher(id);
      alert("Yayınevi başarıyla silindi ✅");
      loadPublishers(); // tabloyu güncelle
    } catch (err) {
      alert("❌ Silme işlemi başarısız oldu.");
    }
  };

  return (
    <div className="publisherlist-container">
      <div className="publisherlist-box">
        <div className="publisherlist-header">
          <h2 className="publisherlist-title">Yayınevi Listesi</h2>
          <button
            className="addpublisher-btn"
            onClick={() => navigate('/add-publisher')}
          >
            + Yeni Ekle
          </button>
        </div>

        <table className="publisherlist-table">
          <thead>
            <tr>
              <th>Adı</th>
              <th>Adres</th>
              <th>Toplam Kitap</th>
              <th>İşlemler</th>
            </tr>
          </thead>
          <tbody>
            {publishers.length > 0 ? (
              publishers.map((p) => (
                <tr key={p.id}>
                  <td>{p.name}</td>
                  <td>{p.address}</td>
                  <td>{p.bookCount}</td>
                  <td>
                    <button
                      className="edit-btn"
                      onClick={() => navigate(`/update-publisher/${p.id}`)}
                    >
                      Güncelle
                    </button>
                    <button
                      className="delete-btn"
                      onClick={() => handleDelete(p.id)}
                    >
                      Sil
                    </button>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="4" className="empty-message">
                  Henüz yayınevi bulunamadı 🏙️
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PublisherList;
