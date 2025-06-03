import { useState, useEffect } from 'react';

export default function Teachers() {
  const [teachers, setTeachers] = useState([]);
  const [selectedTeacher, setSelectedTeacher] = useState(null);

  useEffect(() => {
    // Mock data for teachers
    const mockTeachers = [
      { id: 1, name: 'Mr. Anderson', subject: 'Mathematics', email: 'anderson@example.com' },
      { id: 2, name: 'Ms. Taylor', subject: 'English', email: 'taylor@example.com' },
      { id: 3, name: 'Mrs. Lee', subject: 'Physics', email: 'lee@example.com' },
    ];
    setTeachers(mockTeachers);
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-4xl font-bold mb-6">Teachers</h1>

      <div className="flex gap-6">
        <ul className="w-1/3 border rounded p-4 max-h-[300px] overflow-y-auto">
          {teachers.map((teacher) => (
            <li
              key={teacher.id}
              className={`p-2 cursor-pointer rounded ${
                selectedTeacher?.id === teacher.id ? 'bg-blue-100' : 'hover:bg-gray-100'
              }`}
              onClick={() => setSelectedTeacher(teacher)}
            >
              {teacher.name}
            </li>
          ))}
        </ul>

        <div className="w-2/3 border rounded p-6 bg-white shadow">
          {selectedTeacher ? (
            <>
              <h2 className="text-2xl font-semibold mb-4">{selectedTeacher.name}</h2>
              <p><strong>Subject:</strong> {selectedTeacher.subject}</p>
              <p><strong>Email:</strong> {selectedTeacher.email}</p>
              <p className="mt-4 italic text-gray-600">More details and actions can go here.</p>
            </>
          ) : (
            <p className="text-gray-500">Select a teacher to view details.</p>
          )}
        </div>
      </div>
    </div>
  );
}
