import React from 'react';

const Admin = () => {
  // You can add state, API calls, etc. here later

  return (
    <div className="admin-container p-6 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-4">Admin Dashboard</h1>

      <section className="mb-6">
        <h2 className="text-xl font-semibold mb-2">User Management</h2>
        <p>Here you can manage users, assign roles, and more.</p>
      </section>

      <section className="mb-6">
        <h2 className="text-xl font-semibold mb-2">School Settings</h2>
        <p>Configure school settings, terms, sessions, etc.</p>
      </section>

      <section>
        <h2 className="text-xl font-semibold mb-2">Reports & Analytics</h2>
        <p>View reports and analytics related to students and classes.</p>
      </section>
    </div>
  );
};

export default Admin;
