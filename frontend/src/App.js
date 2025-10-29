import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import BookList from './containers/bookList';
import MemberList from './containers/memberList';
import AddMember from './containers/AddMember';
import AddPublisher from './containers/AddPublisher';
import PublisherList from './containers/publisherList';
import DashboardList from './containers/Dashboard';
import Login from './containers/Login';
import AddBook from './containers/AddBook';
import UpdatePublisher from './containers/UpdatePublisher';
import UpdateBook from './containers/UpdateBook';
import UpdateMember from './containers/UpdateMember';
import Home from './containers/Home';
import ProtectedRoute from './components/ProtectedRoutes';
import BookDetail from './containers/BookDetail';
import AuthorDetail from './containers/AuthorDetail';
import MemberProfile from './containers/MemberProfile';




function App() {
  return (
    <BrowserRouter>
      <div className="gradient__bg">
        <Routes>
          {/* 🔐 Login serbest */}
          <Route path="/login" element={<Login />} />

          {/* 🔒 Admin erişimi olan sayfalar */}
          <Route path="/dashboard" element={<ProtectedRoute element={<DashboardList />} allowedRoles={["Admin"]} />} />
          <Route path="/books" element={<ProtectedRoute element={<BookList />} allowedRoles={["Admin"]} />} />
                  <Route path="/members" element={<ProtectedRoute element={<MemberList />} allowedRoles={["Admin"]} />} />
            <Route path="/add-member" element={<ProtectedRoute element={<AddMember />} allowedRoles={["Admin"]} />} />
       <Route path="/update-member/:id" element={<ProtectedRoute element={<UpdateMember />} allowedRoles={["Admin"]} />} />

          <Route path="/add-book" element={<ProtectedRoute element={<AddBook />} allowedRoles={["Admin"]} />} />
          <Route path="/add-publisher" element={<ProtectedRoute element={<AddPublisher />} allowedRoles={["Admin"]} />} />
          <Route path="/update-publisher/:id" element={<ProtectedRoute element={<UpdatePublisher />} allowedRoles={["Admin"]} />} />
          <Route path="/update-book/:id" element={<ProtectedRoute element={<UpdateBook />} allowedRoles={["Admin"]} />} />
          <Route path="/publisher" element={<ProtectedRoute element={<PublisherList />} allowedRoles={["Admin"]} />} />

          {/* 👥 User erişimi olan sayfa */}
          <Route path="/home" element={<ProtectedRoute element={<Home />} allowedRoles={["User", "Admin"]} />} />
          <Route path="/book/:id" element={<BookDetail />} />
<Route path="/author/:id" element={<AuthorDetail />} />
<Route path="/member/:id" element={<MemberProfile />} />


          {/* Default yönlendirme */}
          <Route path="*" element={<Login />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
