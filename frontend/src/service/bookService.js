import api from './api';

export const getAllBooks = async () => {
  const res = await api.get('/Book/GetAllBooks');
  return res.data;
};



export const createBook = async (book) => {
  const res = await api.post('/Book/CreateBook', book);
  return res.data;
};

export const getBookById = async (id) => {
  const res = await api.get(`/Book/GetById/${id}`);
  return res.data;
};

export const updateBook = async (book) => {
  const res = await api.put(`/Book/UpdateBook/${book.id}`, book);
  return res.data;
};

export const getAllAuthors = async () => {
  const res = await api.get('/Author/GetAllAuthors');
  return res.data;
};




export const deleteBook = async (id) => {
  const res = await api.delete(`/Book/DeleteBook/${id}`);
  return res.data;
};
