import api from "./api";

export const searchLibrary = async (keyword) => {
  const res = await api.get(`/Search/Search?keyword=${keyword}`);
  return res.data;
};
