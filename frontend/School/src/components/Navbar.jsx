import { Link, useNavigate } from 'react-router-dom';

const Navbar = () => {
  const token = localStorage.getItem('token');
  const role = localStorage.getItem('role'); // assuming 'admin' or 'user'
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    navigate('/login');
  };

  return (
    <nav className="flex justify-between items-center px-6 py-4 bg-white shadow-md mb-6">
      <h1 className="text-xl font-bold text-blue-600">
        <Link to="/" className="hover:text-blue-700">School Management</Link>
      </h1> 
      <div className="space-x-6 text-sm font-medium">
        <Link to="/" className="hover:text-blue-500">Home</Link>

        {token ? (
          <>
            <Link to="/dashboard" className="hover:text-blue-500">Dashboard</Link>
            <Link to="/students" className="hover:text-blue-500">Students</Link>
            <Link to="/teachers" className="hover:text-blue-500">Teachers</Link>
            <Link to="/courses" className="hover:text-blue-500">Courses</Link>
            {role === 'admin' && (
              <Link to="/admin" className="hover:text-blue-500">Admin</Link>
            )}
            <button
              onClick={handleLogout}
              className="text-red-500 hover:underline"
            >
              Logout
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className="hover:text-blue-500">Login</Link>
            <Link to="/register" className="hover:text-blue-500">Register</Link>
          </>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
