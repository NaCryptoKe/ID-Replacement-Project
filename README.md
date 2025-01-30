# 🎉 Welcome to the ID Replacement Project! 🎉

## 🚀 Project Overview
This is a **super simple** C# project that connects to a **SQL Server** database to allow users to authenticate with either their **Student ID** or **Email** and retrieve their details. It's a basic, yet powerful tool to practice and understand how **C#** and **SQL Server** work together. 

### Features
- **Login with Student ID or Email**
- **Secure query structure** (well, as secure as it gets with plain SQL queries 😉)
- **Get student data** based on credentials (ID or Email)
- **Basic implementation of SQL queries** to fetch data

## 🧑‍💻 How It Works
1. You enter your **Student ID** or **Email** and your **Password**.
2. The program runs a SQL query to check if your credentials match any record in the **Students** table.
3. If you're a match, it grabs your info! Otherwise, prepare to face the error message of your dreams. 😬

## 🔨 Getting Started
### Prerequisites
To run this project, you'll need:
- **C#** (of course!) 💻
- **SQL Server** (You can use SQL Server Express if you're just getting started!)
- A simple SQL database setup with a **Students** table containing `StudentID`, `Email`, and `Password` fields.

### Steps to Run It:
1. Clone this repo. Or, if you're feeling extra fancy, click that shiny **Download** button above. 📥
   
   ```bash
   https://github.com/NaCryptoKe/ID-Replacement-Project.git
2. Open the project in your favorite C# editor (VS Code? Visual Studio? Whichever one you think is cool 🖥️).
3. Connect to your SQL Server instance and create the tables
4. Run the project and enter your Student ID or Email and Password to test the magic!
It checks whether the username is a valid Student ID or Email and makes sure that the Password is correct. If so, 🎉 – you're in!

## 📢 Contributing
Want to make this project even cooler? Feel free to fork the repo, make some changes, and create a pull request! Here are some ideas:

- **Add** parameterized queries to make it secure (Yes, we know this could be improved 😉)

- **Add** some awesome features like user registration or password hashing.

- **Refactor** the code to handle multiple SQL queries or improve the database structure.

## 🎈 Enjoy!
That's it! You now have a working, basic project that allows you to authenticate with a Student ID or Email. Keep it simple, keep it fun, and always remember: Code like nobody's watching! 😎

Good luck, and happy coding! 👾
