import api from './api';

// ✅ Tüm yayınevlerini getir
export const getAllPublishers = async () => {
  const res = await api.get('/Publisher/GetAllPublishers');
  return res.data;
};

// ✅ Yayınevi ID ile getir

// ✅ Yeni yayınevi oluştur
export const createPublisher = async (publisher) => {
  const res = await api.post('/Publisher/CreatePublisher', publisher);
  return res.data;
};

// ✅ Güncelle
export const getPublisherById = async (id) => {
  const res = await api.get(`/Publisher/GetById/${id}`);
  return res.data;
};

export const updatePublisher = async ({ id, name, address }) => {
  const res = await api.put(`/Publisher/UpdatePublisher/${id}`, { name, address });
  return res.data;
};



export const deletePublisher = async (id) => {
  const res = await api.delete(`/Publisher/DeletePublisher/${id}`);
  return res.data;
};

