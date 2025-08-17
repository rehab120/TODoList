# 📌 PlanMate  
**PlanMate** is a smart To-Do List web application built with **ASP.NET Core MVC**.  
It helps users organize their tasks, get reminders before deadlines, and manage their time effectively.  

---

## 🚀 Features  

### 👥 Roles  
- **User**  
  - Register & Login (with a welcome email sent via **Brevo**).  
  - Create, edit, delete, and mark tasks.  
  - Task statuses: **Pending**, **Completed**, and automatic **TimeOver** when deadlines are missed.  
  - Receive **Task Reminder emails** (e.g., 2 hours before a deadline).  
  - Contact support via **Contact Us**, with confirmation emails.  

- **Admin**  
  - Manage user and admin accounts.  
  - View and handle issues reported by users.  

---

### ⚙️ Background Jobs  
- Integrated **Hangfire** to run periodic checks (every minute) for:  
  - Updating task statuses.  
  - Sending reminder emails automatically.  

---

### 💻 Tech Stack  
- **ASP.NET Core MVC**  
- **Entity Framework**  
- **SQL Server**  
- **Brevo (SMTP)** for email notifications  
- **Hangfire** for background job scheduling  

---

### 🌐 Deployment  
- Hosted on **MonsterASP** → [Live Demo](https://todolistwebsite.runasp.net/)  
- Fully **Responsive Design** – works seamlessly on desktop & mobile.  



---
