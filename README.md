# ID Replacement System

Welcome to the **ID Replacement System**, the ultimate solution for managing ID requests, appointments, and documentation in one streamlined application. Whether you're a student, admin, or someone just snooping around GitHub for cool projects, this app has something for everyone! 🎉

---

## 🧐 What is this?

The ID Replacement System is a C# application built with Windows Forms that uses SQL Server as its database backend. It provides a user-friendly way to:

- Manage student accounts.
- Handle ID replacement requests.
- Schedule and reschedule appointments.
- Log every transaction (because accountability is king!).
- **Admin Features**: Allow directors to review and approve/reject ID requests.

Whether your student ID was stolen by a raccoon 🦝 or it simply disintegrated in your washing machine, this system has your back!

---

## 💻 Tech Stack

- **Language:** C# (.NET Framework)
- **Frontend:** Windows Forms (WinForms)
- **Database:** Microsoft SQL Server
- **Architectural Pattern:** Repository-Service Pattern

---

## ✨ Features

### Student Module
- 🔐 **Login**: Students can log in using their credentials.
- 📜 **Profile**: View basic info like name, department, and year.

### ID Request Module
- 📝 **Submit Request**: Easily create new ID replacement requests.
- 🔄 **Status Updates**: Track the progress of your request (Pending, Approved, Rejected, or Completed).

### Appointment Management
- 🗓 **Schedule Appointments**: Book your slot for ID processing.
- 🔄 **Reschedule**: Life happens. Reschedule without hassle.

### Documentation
- 📂 **Document Upload**: Attach necessary files to your ID request.

### Admin Module (Director View)
- 📊 **Request Review**: View all pending ID requests.
- ✔️ **Approve/Reject Requests**: Take action on submitted ID requests.
- 🛠 **Admin Management**: Manage system settings and monitor logs.

### Transaction Logging
- 📋 **Logs**: Every single change is logged because transparency is cool. 

---

## 🚀 Getting Started

### Prerequisites

Before you dive in, make sure you have:

1. **Visual Studio 2022 or later** (because you're fancy).
2. **Microsoft SQL Server** (preferably on your local machine).
3. A warm cup of coffee ☕ (optional but recommended).

### Installation

1. Clone the repo:

   ```bash
   git clone https://github.com/NaCryptoKe/ID-Replacement-Project.git
   cd id-replacement-system
   ```

2. Open `ID_Replacement.sln` in Visual Studio.

3. Update the connection string in `DatabaseContext.cs` to point to your SQL Server:

   ```csharp
   _connectionString = "Your-Connection-String-Here";
   ```

4. Build and run the project. 🎉

---

## 🗂 Project Structure

```plaintext
ID_Replacement
├── Data
│   ├── Models             # Database models
│   ├── Repositories       # Interfaces & implementation for data access
├── Services               # Business logic
├── Forms                  # Windows Forms for UI
│   ├── LoginForm.cs       # Handles user login
│   ├── MainFrame.cs       # Main dashboard for students
│   ├── Admin.cs           # Dashboard for admin (directors)
├── DatabaseContext.cs     # Singleton for managing DB connections
├── bin/Debug              # Compiled binaries
└── README.md              # The coolest README ever
```

---

## 📋 Database Schema

Here are the main tables:

- **Students**: Stores student data.
- **IDRequests**: Tracks ID replacement requests.
- **Documents**: Links uploaded documents to requests.
- **Appointments**: Tracks appointment details.
- **TransactionLogs**: Logs every change for auditing.

For full schema details, check the `Database.sql` file.

---

## 🛠 How to Use

### Student View
1. **Login:** Enter your Student ID or email and password. (Default password is `PASSWORD123` for new students!)
2. **Request an ID:** Submit a new ID request and wait for updates.
3. **Schedule Appointments:** Pick a date that works for you.
4. **Upload Documents:** Attach any required files for smooth processing.

### Admin View
1. **Login as Admin:** Directors can log in using admin credentials.
2. **Review Requests:** View all submitted ID requests.
3. **Take Action:** Approve or reject requests based on eligibility.
4. **Monitor Logs:** Keep track of changes in the system.

---

## 🐛 Troubleshooting

- **Login Fails?** Double-check your credentials. If it still doesn’t work, yell at your screen. Then check the logs.
- **Database Issues?** Ensure SQL Server is running and your connection string is correct.
- **Weird UI Behavior?** Try turning it off and on again. Classic.

---

## 🤝 Contributing

Got ideas? Found bugs? Want to add a dancing cat animation? Open an issue or submit a pull request! Contributions are more than welcome.

---

## 🎉 Acknowledgments

- Shoutout to **Microsoft** for creating C# and SQL Server.
- Huge thanks to my caffeine addiction for getting this project done.
- And finally, thanks to **YOU** for checking out this project!

---

**Made with 💻, ☕, and a bit of panic.**
