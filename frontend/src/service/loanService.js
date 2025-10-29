import api from "./api";

export const createLoan = async (loanData) => {
  const res = await api.post("/Loan/CreateLoan", loanData);
  return res.data;
};
