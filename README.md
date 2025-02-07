# 🆔 ID Replacement System

🔑 **Secure. Fast. Hassle-Free ID Management.**

Welcome to the **ID Replacement System** – your one-stop solution for handling student ID requests, document uploads, and appointment scheduling with ease! 🎓💼

## 📌 Features

✅ **User Authentication** – Login securely using your student credentials. 🔐  
✅ **Request New IDs** – Submit ID replacement requests in a few clicks. 📄  
✅ **Track Your Status** – Get real-time updates on request approval. 🚦  
✅ **Manage Appointments** – Book, update, or reschedule your ID pickup. 📅  
✅ **Admin Dashboard** – Full control for admins to approve or reject requests. 🏢  

---

## 🏗 Tech Stack

🖥 **Backend:** C# (.NET 8.0)  
🗄 **Database:** Microsoft SQL Server  
🌐 **Frontend:** Windows Forms (.NET Windows Application)  
🔄 **APIs & Procedures:** SQL Stored Procedures for secure data handling  

---

## 🚀 Installation Guide

📌 **Prerequisites:**  
- .NET SDK 8.0+  
- SQL Server  
- Visual Studio  

🔧 **Setup Steps:**  

1️⃣ Clone the repository:  
```sh
git clone https://github.com/NaCryptoKe/ID-Replacement-Project.git
cd ID_Replacement
```

2️⃣ Set up the database in SQL Server:  
   - Run `Database.sql` to create necessary tables & procedures.  
   - Update `DatabaseContext.cs` with your SQL connection string.  

3️⃣ Run the application:  
```sh
dotnet run
```
That's it! You're all set! 🎉  

---

## 🎯 How to Use

### 🏫 For Students:
- Login using your student email & password.  
- Submit an **ID Replacement Request**.  
- Upload required documents. 📂  
- Schedule an appointment. 📅  
- Track your request status. 📊  

### 🔧 For Admins:
- View pending requests.  
- Approve or reject requests with a single click. ✅❌  
- Assign appointment slots for students.  
- Generate **Transaction Reports**. 📜  

---

## 🛠 Project Structure

📂 **ID_Replacement/**  
├── `Database.sql` - SQL scripts for database setup  
├── `Program.cs` - Application entry point 🚀  
├── `AdminForm.cs` - Admin dashboard  
├── `LoginForm.cs` - User login page  
├── `MainFrame.cs` - Main application frame  
├── `Repositories/` - Data access layer  
├── `Services/` - Business logic layer  
└── `README.md` - You’re reading it! 📖  

---

## 📢 Contributing

💡 Found a bug? Have a cool feature idea?  
Fork the repo, make changes, and submit a PR! 🛠🔥  

---

## 🔥 Need Help?

🤝 Reach out via:  
📧 Email: nahomcryptoketema@gmail.com  
🐛 Report Issues: [GitHub Issues](https://github.com/NaCryptoKe/ID-Replacement-Project.git)  

🎉 **Happy Coding & ID Managing!** 🚀