import { useState, useEffect } from 'react';

export default function Courses() {
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState(null);

  useEffect(() => {
    const mockCourses = [
      { id: 1, title: 'Algebra 101', credits: 3, description: 'Basic algebra course covering equations and inequalities.' },
      { id: 2, title: 'English Literature', credits: 2, description: 'Introduction to classic and modern literature.' },
      { id: 3, title: 'Physics', credits: 4, description: 'Fundamentals of mechanics and thermodynamics.' },
    ];
    setCourses(mockCourses);
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-4xl font-bold mb-6">Courses</h1>

      <div className="flex gap-6">
        <ul className="w-1/3 border rounded p-4 max-h-[300px] overflow-y-auto">
          {courses.map((course) => (
            <li
              key={course.id}
              className={`p-2 cursor-pointer rounded ${
                selectedCourse?.id === course.id ? 'bg-green-100' : 'hover:bg-gray-100'
              }`}
              onClick={() => setSelectedCourse(course)}
            >
              {course.title}
            </li>
          ))}
        </ul>

        <div className="w-2/3 border rounded p-6 bg-white shadow">
          {selectedCourse ? (
            <>
              <h2 className="text-2xl font-semibold mb-4">{selectedCourse.title}</h2>
              <p><strong>Credits:</strong> {selectedCourse.credits}</p>
              <p className="mt-4">{selectedCourse.description}</p>
            </>
          ) : (
            <p className="text-gray-500">Select a course to see details.</p>
          )}
        </div>
      </div>
    </div>
  );
}
