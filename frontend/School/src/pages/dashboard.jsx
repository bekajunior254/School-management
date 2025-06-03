import { useEffect, useState } from 'react';

export default function Dashboard() {
  const [stats, setStats] = useState({
    students: 0,
    teachers: 0,
    courses: 0,
  });

  // Simulate fetching dashboard stats
  useEffect(() => {
    // Replace with real API call later
    setTimeout(() => {
      setStats({
        students: 120,
        teachers: 15,
        courses: 20,
      });
    }, 500);
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-4xl font-bold mb-6">Dashboard</h1>

      <div className="grid grid-cols-3 gap-6">
        <div className="bg-white shadow-md rounded p-4 text-center">
          <h2 className="text-2xl font-semibold">{stats.students}</h2>
          <p className="text-gray-600">Students</p>
        </div>
        <div className="bg-white shadow-md rounded p-4 text-center">
          <h2 className="text-2xl font-semibold">{stats.teachers}</h2>
          <p className="text-gray-600">Teachers</p>
        </div>
        <div className="bg-white shadow-md rounded p-4 text-center">
          <h2 className="text-2xl font-semibold">{stats.courses}</h2>
          <p className="text-gray-600">Courses</p>
        </div>
      </div>

      <section className="mt-10">
        <h3 className="text-xl font-semibold mb-3">Announcements</h3>
        <ul className="list-disc pl-5 space-y-2 text-gray-700">
          <li>School holiday starts next Friday.</li>
          <li>New grading policy effective next semester.</li>
          <li>Parent-teacher meetings scheduled for June 15th.</li>
        </ul>
      </section>
    </div>
  );
}
