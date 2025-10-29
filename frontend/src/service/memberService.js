import api from './api';

export const getAllMembers = async () => {
  const res = await api.get('/Member/GetAllMembers');
  return res.data;
};

export const getMemberById = async (id) => {
  const res = await api.get(`/Member/GetById/${id}`);
  return res.data;
};

export const updateMember = async (member) => {
  const res = await api.put(`/Member/UpdateMember/${member.id}`, member);
  return res.data;
};

export const deleteMember = async (id) => {
  const res = await api.delete(`/Member/DeleteMember/${id}`);
  return res.data;
};
