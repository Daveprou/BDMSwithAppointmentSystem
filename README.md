# BDwithAppointmentSystem
**Blood Donation Management System with Appointment Scheduling**


[📺 Watch the System Demo](https://github.com/user-attachments/assets/897194ee-a6e9-431e-a286-f998f80c59bf)

## 📌 Project Overview
This is a group project developed for educational purposes. I am responsible for the backend, using **C# Windows Forms Application (.NET Framework)** in **Visual Studio**.

The system is designed to modernize and streamline blood donation operations by enabling online appointment scheduling and efficient data management. It features **login/signup**, **admin and user dashboards**, and full **CRUD functionality** for managing donors, blood stock, transfers, and patients.

---

## 🔐 Authentication & Role-Based Access
- **Login System**: Validates user credentials against a database.
- **Role-Based Routing**: Automatically redirects to user or admin dashboard based on account type.
- **Sign Up**: Available for users; admin accounts are added manually for security.

---

## 🛠 Admin Dashboard
The admin interface uses **UserControls** for modular navigation. It includes the following features:

### 🏠 Home
- Summary panel with real-time stats:
  - Total Donors
  - Registered Users
  - Patients
  - Blood Transfers
  - Available Blood Units
  - Appointments Scheduled Today

### ➕ Add Donor
- For walk-in donor registration (not through signup)
- Captures all necessary donor information

### ✏️ Update Donor
- Modify existing donor details
- Deletes a user when necessary or with the user's consent. Deletion is not allowed if the user has an existing appointment record
- Features a searchable table (DataGridView) for quick access

### 👁 View Donor
- View and update appointment statuses
- Appointments marked as "Transferred" are locked
- Search by Donor ID or filter by status using the combo box

![View Donor Screenshot](https://github.com/user-attachments/assets/cf1d18e1-bf39-4476-911c-6a02dc5c1367)


### 🩸 Donate Blood
- Book, reschedule, or cancel appointments
- Prevents weekend bookings
- Dynamic time slots update based on existing appointments
- One active appointment per donor
- Built-in search bar and table for better management

### 🧪 Blood Stock
- Live tracker of blood availability by blood type
- Automatically deducts stock upon transfer

### 🔁 Blood Transfer
- Transfer available blood to patients
- Status: "Available" if patient’s blood type is in stock, otherwise "Not Available"
- Logs all transfers in a DataGridView

### 🧍‍♂️ Patients
- Register patients needing blood


![Patients Screenshot](https://github.com/user-attachments/assets/7323fd86-58ea-499d-800b-1f027afbf272)


### 📋 View Patients
- Edit patient records similar to the Update Donor section

---

## 🙋 User Dashboard
Each option is displayed as a separate form for clarity and usability:

### 🏠 Home
- Hospital information, mission, and contact details

### 👤 Profile
- View complete donor profile:
  - Upcoming appointments
  - Account information
  - Donation history with blood transfer tracking
  - Summary of donations (total and yearly)

### 🛠 Edit Profile
- Update personal details
- Password change requires confirmation
- Username is not editable

### 💉 Donate
- Schedule an appointment 
- No search or table — appointment is linked to the logged-in user
- Blood type can be set only once (initial registration)

### ❗ About
- Must-read instructions before setting an appointment

---

## 🎯 Purpose & Impact
This system aims to:
- Simplify the blood donation process for both users and hospitals
- Reduce wait times through efficient scheduling
- Ensure accurate record-keeping and improve transparency
- Encourage regular donations by offering a smooth experience
- Enable hospitals to better prepare for emergencies
- Provide donors with visibility on how their donations are used

By combining technology and healthcare, this project enhances community health and operational efficiency.

---

## 💻 Installation & Requirements

### Requirements:
- Visual Studio 2022
- .NET Framework 
- XAMPP MySql (for the database backend)
  
---

## 📌 Notes
- This project is for **educational purposes only**.
- Sensitive data and production deployment are not part of this build.
- Ensure proper security practices if adapting for real-world use.

---

