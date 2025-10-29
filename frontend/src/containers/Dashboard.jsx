import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../service/api';
import './dashboard.css';

const Dashboard = () => {
  const [stats, setStats] = useState(null);
    const navigate = useNavigate();


  useEffect(() => {
    api.get('/Stat/GetDashboardData')
      .then(res => setStats(res.data))
      .catch(err => console.error("İstatistikler alınamadı:", err));
  }, []);

  if (!stats)
    return <div className="dashboard-loading">Yükleniyor...</div>;

  return (
    <div className="dashboard-container">
      <div className="dashboard-box">
        <h2 className="dashboard-title">Kütüphane İstatistikleri</h2>

        <div className="dashboard-grid">
          <div className="stat-card books">
            <h3>📚 Toplam Kitap</h3>
            <p>{stats.totalBooks}</p>
      <button className="addbook-btn" onClick={() => navigate('/books')}>
            Kitap Yönetimi
          </button>
          </div>

                     <div className="stat-card publishers">
            <h3>📰 Yayıncılar</h3>
                      <p>{stats.totalPublishers}</p>
        <button className="addbook-btn" onClick={() => navigate('/publisher')}>
            Yayınevi Yönetimi
          </button>
          </div>

          <div className="stat-card members">
            <h3>👥 Toplam Üye</h3>
            <p>{stats.totalMembers}</p>
                    <button className="addbook-btn" onClick={() => navigate('/members')}>
            Üye Yönetimi
          </button>

          </div>

          <div className="stat-card loans">
            <h3>📦 Toplam Ödünç</h3>
            <p>{stats.totalLoans}</p>
          </div>



          <div className="stat-card overdue">
            <h3>⏰ Geciken Kitaplar</h3>
            <p>{stats.overdueCount}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
