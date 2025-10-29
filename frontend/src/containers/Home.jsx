import React, { useState } from "react";
import Navbar from "../components/Navbar";
import { searchLibrary } from "../service/searchService";
import "./home.css";

import { useNavigate } from "react-router-dom";

const Home = () => {
  const [keyword, setKeyword] = useState("");
  const [results, setResults] = useState({ books: [], authors: [], members: [] });
  const navigate = useNavigate();

  const handleSearch = async (text) => {
    setKeyword(text);
    if (text.length < 2) {
      setResults({ books: [], authors: [], members: [] });
      return;
    }

    try {
      const data = await searchLibrary(text);
      setResults(data);
    } catch (err) {
      console.error("Arama hatası:", err);
    }
  };

  const handleClick = (type, id) => {
    navigate(`/${type}/${id}`);
  };

  return (
    <div className="home-container">
      <Navbar onSearchChange={handleSearch} />
      <div className="home-content">
        <h2 className="home-heading">📚 Kütüphane Arama</h2>
        <p className="home-subtitle">
          Kitap, yazar veya üye arayabilirsin.
        </p>

        {keyword && (
          <div className="search-results">
            <h3>🔍 Arama Sonuçları</h3>

            <div className="result-section">
              <h4>📘 Kitaplar</h4>
              {results.books.length > 0 ? (
                <ul>
                  {results.books.map((b) => (
                    <li key={b.id} onClick={() => handleClick("book", b.id)} className="clickable">
                      {b.title}
                    </li>
                  ))}
                </ul>
              ) : <p>Sonuç yok.</p>}
            </div>

            <div className="result-section">
              <h4>✍️ Yazarlar</h4>
              {results.authors.length > 0 ? (
                <ul>
                  {results.authors.map((a) => (
                    <li key={a.id} onClick={() => handleClick("author", a.id)} className="clickable">
                   {a.firstName} {a.lastName}

                    </li>
                  ))}
                </ul>
              ) : <p>Sonuç yok.</p>}
            </div>

            <div className="result-section">
              <h4>👤 Üyeler</h4>
              {results.members.length > 0 ? (
                <ul>
                  {results.members.map((m) => (
                    <li key={m.id} onClick={() => handleClick("member", m.id)} className="clickable">
                      {m.fullName}
                    </li>
                  ))}
                </ul>
              ) : <p>Sonuç yok.</p>}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default Home;
