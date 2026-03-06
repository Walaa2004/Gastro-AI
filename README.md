# GastroAI — AI-Powered Gastrointestinal Disease Detection System

 Bridging the gap between patients and specialist doctors through Artificial Intelligence — making healthcare accessible for everyone, everywhere.

---
## About The Project

**GastroAI** is an intelligent medical web platform that integrates a **Machine Learning model** for early detection of **8 gastrointestinal diseases** — built to bridge the gap between patients and specialist doctors, especially in underserved areas where access to specialists is limited.

The platform is **completely free** — doctors are volunteers who register, set their availability, and provide consultations based on AI-generated analysis reports.

---

## The Problem We're Solving

- **Limited access to specialists** — especially in remote areas and small cities
- **Long waiting times** — from booking to diagnosis to results
- **Delayed diagnosis** — due to full reliance on manual traditional methods
- **Heavy workload on doctors** — more patients than available specialists
- **Scattered medical records** — no centralized patient history

---

## How It Works

```
Patient uploads medical image
        ↓
AI Model analyzes the image
        ↓
Diagnosis report generated:
  • Disease name
  • Description
  • Severity level (Critical / Moderate / Mild)
        ↓
Patient consults a volunteer doctor
        ↓
Doctor provides treatment recommendations
```

---

## Key Features

### AI Disease Detection
- Detects **8 gastrointestinal diseases** from medical images
- Returns disease name, description & severity level
- ML model trained on a large medical image dataset

### Authentication & Authorization (My Contribution)
- Role-based system: **Patient / Doctor / Admin**
- High-level authentication & authorization
- Doctor registration with **Admin approval workflow**
- Access control with descriptive denial messages
- Full validation across all user types

### Admin Dashboard (My Contribution)
- Manage patients, doctors & appointments
- Doctor approval workflow (Pending / Confirmed / Rejected)
- Full system oversight & control

### Doctor Portal
- Volunteer registration & profile setup
- Set available days & consultation hours
- Receive AI-analyzed cases & write treatment recommendations

### Patient Portal
- Upload medical images for AI analysis
- Receive instant diagnosis report
- Book consultation with available volunteer doctor
- View medical history & doctor recommendations

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Web API |
| ORM | Entity Framework |
| Database | SQL Server |
| API Documentation | Swagger |
| AI Model | Python / Machine Learning |
| Frontend | HTML / CSS / JavaScript / React |

---

## Project Status

| Component | Status |
|-----------|--------|
| ✅ REST APIs | Complete |
| ✅ Authentication & Authorization | Complete |
| ✅ Admin Dashboard APIs | Complete |
| ✅ Doctor & Patient APIs | Complete |
| ✅ Swagger Documentation | Complete |
| 🔄 ML Model Integration | In Progress |
| 🔄 Frontend | In Progress |

---

## Team

Graduation Project — Team of **5 members**

| Contribution | Developer |
|-------------|-----------|
| Backend — Authentication & Admin APIs | Walaa Ahmed |
| ML Model | Team Members |
| Frontend | Team Members |

---

## How To Run (Backend)

1. Clone the repository
```bash
git clone https://github.com/Walaa2004/Gastro-AI.git
```

2. Open the solution in **Visual Studio**

3. Update the connection string in `appsettings.json`

4. Run migrations
```bash
Update-Database
```

5. Run the project and open Swagger UI at:
```
https://localhost:{port}/swagger
```

---

## Contact
Walaa Ahmed — [www.linkedin.com/in/walaa-ahmed2004]
