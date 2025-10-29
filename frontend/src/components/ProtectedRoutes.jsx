import React from "react";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ element, allowedRoles }) => {
  const token = localStorage.getItem("token");
  const role = localStorage.getItem("role");

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  if (!allowedRoles.includes(role)) {
    // erişim izni yoksa yönlendir
    return <Navigate to={role === "Admin" ? "/dashboard" : "/home"} replace />;
  }

  return element;
};

export default ProtectedRoute;
