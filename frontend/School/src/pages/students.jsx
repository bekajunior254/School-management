import { useState, useEffect } from 'react';

export default function Students() {
  const [students, setStudents] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredStudents, setFilteredStudents] = useState([]);

  useEffect(() => {
    // Mock fetching students from API
    const mockStudents = [
      { id: 1, name: 'John Doe', email: 'john@example.com', course: 'Algebra 101' },
      { id: 2, name: 'Jane Smith', email: 'jane@example.com', course: 'English Literature' },
      { id: 3, name: 'Michael Johnson', email: 'mikej@example.com', course: 'Physics' },
      { id: 4, name: 'Emily Davis', email: 'emilyd@example.com', course: 'Chemistry' },
    ];
    setStudents(mockStudents);
    setFilteredStudents(mockStudents);
  }, []);

  // Filter students by name or course on search input
  useEffect(() => {
    const filtered = students.filter(
      (student) =>
        student.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
        student.course.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredStudents(filtered);
  }, [searchTerm, students]);

  return (
    <div className="p-6">
      <h1 className="text-4xl font-bold mb-6">Students</h1>

      <input
        type="text"
        placeholder="Search students by name or course"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        className="border rounded p-2 mb-6 w-full max-w-md"
      />

      <table className="w-full max-w-4xl border-collapse">
        <thead>
          <tr className="bg-gray-200">
            <th className="border px-4 py-2 text-left">Name</th>
            <th className="border px-4 py-2 text-left">Email</th>
            <th className="border px-4 py-2 text-left">Course</th>
          </tr>
        </thead>
        <tbody>
          {filteredStudents.length > 0 ? (
            filteredStudents.map((student) => (
              <tr key={student.id} className="hover:bg-gray-100">
                <td className="border px-4 py-2">{student.name}</td>
                <td className="border px-4 py-2">{student.email}</td>
                <td className="border px-4 py-2">{student.course}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="3" className="text-center p-4 text-gray-500">
                No students found.
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
