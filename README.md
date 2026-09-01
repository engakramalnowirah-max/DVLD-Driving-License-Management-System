# ?? DVLD — Driving & Vehicle License Department Management System

A complete **Driving & Vehicle License Department (DVLD) Management System** built from scratch using **C#**, **.NET**, **Windows Forms**, **ADO.NET**, and **Microsoft SQL Server**.

This project was developed as a practical application of the programming concepts and software development principles I learned throughout my programming journey.

> **Built from scratch over approximately 3 months without relying on ready-made source code or copying an existing implementation.**

The main purpose of this project was not only to create a functional system, but to understand how to design, build, debug, and maintain a complete software application using a structured architecture.

---

# ?? Project Overview

The **DVLD System** simulates the operations of a Driving & Vehicle License Department.

It manages people, drivers, applications, driving tests, licenses, international licenses, detained licenses, and different types of driving license services.

The system is designed around real business workflows rather than simple data entry.

For example:

```text
Person
   ?
Application
   ?
Local Driving License Application
   ?
Test Appointments
   ?
Tests
   ?
Driver
   ?
Driving License
```

This structure allows the system to track an applicant throughout the complete licensing process.

---

# ?? Project Objectives

The project was developed to apply programming concepts in a real-world application and gain practical experience with:

* Object-Oriented Programming
* Three-Tier Architecture
* Separation of Concerns
* Database Design
* SQL Server
* ADO.NET
* CRUD Operations
* Business Logic
* Data Access
* Validation
* Exception Handling
* Debugging
* Problem Solving
* Reusable Components
* Multi-layer application design
* Relational database relationships
* Complex business workflows

---

# ??? Technologies Used

| Technology               | Purpose                   |
| ------------------------ | ------------------------- |
| **C#**                   | Main programming language |
| **.NET**                 | Application development   |
| **Windows Forms**        | Desktop User Interface    |
| **ADO.NET**              | Database access           |
| **Microsoft SQL Server** | Relational database       |
| **Visual Studio**        | Development environment   |
| **Git / GitHub**         | Version control           |

---

# ??? Architecture

The project follows a **Three-Tier Architecture** to separate responsibilities and make the system easier to maintain and extend.

```text
???????????????????????????????????????
?         Presentation Layer          ?
?          Windows Forms UI           ?
?                                     ?
?  Forms / Controls / User Interaction?
???????????????????????????????????????
                   ?
                   ?
???????????????????????????????????????
?           Business Layer            ?
?                                     ?
?  Business Rules / Validation /      ?
?  Application & License Processing   ?
???????????????????????????????????????
                   ?
                   ?
???????????????????????????????????????
?          Data Access Layer          ?
?                                     ?
?       ADO.NET / SQL Operations      ?
???????????????????????????????????????
                   ?
                   ?
???????????????????????????????????????
?            SQL Server               ?
?              Database               ?
???????????????????????????????????????
```

### Presentation Layer

Responsible for:

* User Interface
* Windows Forms
* User interaction
* Displaying information
* Collecting user input
* Calling business operations

### Business Layer

Responsible for:

* Business rules
* Validation
* Processing applications
* License operations
* Test operations
* Connecting the Presentation Layer with the Data Access Layer

### Data Access Layer

Responsible for:

* Database communication
* SQL queries
* CRUD operations
* Reading and updating records
* Handling database operations through ADO.NET

---

# ?? Main Services

The system supports the following major driving license services.

## ?? 1. New Local Driving License Service

Allows applicants to apply for a new local driving license.

The process is connected to:

* Applicant information
* Application type
* License class
* Test appointments
* Test results
* Driver creation
* License issuance

---

## ?? 2. Renew Driving License Service

Allows an existing driving license to be renewed while maintaining the driver's existing information and license history.

---

## ?? 3. Replacement for a Lost Driving License

Allows a driver to request a replacement license after losing the original license.

---

## ??? 4. Replacement for a Damaged Driving License

Allows a driver to request a replacement when the existing driving license is damaged.

---

## ?? 5. Release Detained Driving License

Manages the process of releasing a detained driving license.

The system maintains information about:

* Detained license
* Related license
* Release application
* User who created the record
* User who released the license

---

## ?? 6. New International License

Allows eligible drivers to apply for an international driving license.

The international license is linked to:

* Driver
* Local driving license
* Application
* User who created the record

---

## ?? 7. Retake Test

Allows an applicant to retake a test when the previous attempt was unsuccessful.

The system supports linking a retake application with the corresponding test appointment.

---

# ?? Testing System

Testing is an important part of the DVLD workflow.

The system manages:

* Test Types
* Test Appointments
* Test Attempts
* Test Results
* Retake Tests
* Test History

The test system is connected through the following relationship:

```text
Application
     ?
     ?
Local Driving License Application
     ?
     ?
Test Appointment
     ?
     ?
Test
     ?
     ?
Test Result
```

This design allows multiple test attempts to be associated with the licensing process.

---

# ??? Driving Tests

The system supports different test types through the `TestTypes` entity.

Examples include:

* Vision Test
* Theory Test
* Practical Driving Test

The actual test types are stored in the database and connected to `TestAppointments`.

---

# ?? People & Drivers

The system separates **People** from **Drivers**.

A person contains personal information and can be associated with:

* Nationality
* Applications
* User accounts
* Driver records

A driver is associated with a person and can have:

* Driving licenses
* International licenses
* Driving history

Relationship:

```text
People
   ?
   ???????????????? Applications
   ?
   ???????????????? Users
   ?
   ???????????????? Drivers
                         ?
                         ???? Licenses
                         ?
                         ???? International Licenses
```

---

# ?? Application Management

Applications are the central part of many DVLD operations.

The `Applications` entity is connected to:

* `ApplicationTypes`
* `People`
* `Users`

This allows the system to identify:

* Who submitted the application
* What type of service was requested
* Which user created the application

Local driving license applications extend this workflow through:

```text
Applications
      ?
      ?
LocalDrivingLicenseApplications
      ?
      ?
LicenseClasses
```

---

# ?? License Management

The `Licenses` entity connects:

* Applications
* Drivers
* License Classes
* Users

This allows the system to maintain the relationship between a driver's license and the process that created it.

```text
Application
     ?
     ?
License
     ?
     ??? Driver
     ??? License Class
     ??? Created By User
```

---

# ?? International License Management

International licenses are connected to:

```text
Driver
   ?
   ???? International License
             ?
             ??? Application
             ??? Local License
             ??? Created By User
```

This ensures that an international license is associated with the driver's local license and the application used to issue it.

---

# ?? Detained License Management

The system includes a dedicated `DetainedLicenses` entity.

A detained license is associated with:

* A driving license
* A release application
* The user who created the detention record
* The user who released the license

Relationship:

```text
License
   ?
   ?
Detained License
   ?
   ??? Release Application
   ??? Created By User
   ??? Released By User
```

---

# ??? Database Design

The system uses **Microsoft SQL Server** as its relational database.

The database is designed around related entities rather than storing all information in a single table.

### Main Entities

```text
People
Countries
Users
Drivers

Applications
ApplicationTypes
LocalDrivingLicenseApplications

LicenseClasses
Licenses
InternationalLicenses

TestTypes
TestAppointments
Tests

DetainedLicenses
```

---

# ?? Database Relationships

Some of the main relationships implemented in the database are:

### People

```text
Countries
    ?
    ?
People
    ?
    ???? Applications
    ???? Drivers
    ???? Users
```

### Applications

```text
ApplicationTypes
       ?
       ?
Applications ????? People
       ?
       ???????????? Users
```

### Local Driving License

```text
Applications
      ?
      ?
LocalDrivingLicenseApplications
      ?
      ?
LicenseClasses
```

### Tests

```text
Applications
      ?
      ?
TestAppointments ????? TestTypes
      ?
      ?
Tests
```

### Licenses

```text
Applications ???????
                   ?
Driver ?????????? Licenses ????? LicenseClasses
                   ?
                   ?
                 User
```

These relationships are enforced using **Foreign Keys** to maintain referential integrity.

---

# ?? Core Database Relationships

The project implements relationships such as:

* `Applications ? ApplicationTypes`
* `Applications ? People`
* `Applications ? Users`
* `Drivers ? People`
* `Drivers ? Users`
* `Users ? People`
* `Licenses ? Applications`
* `Licenses ? Drivers`
* `Licenses ? LicenseClasses`
* `Licenses ? Users`
* `InternationalLicenses ? Applications`
* `InternationalLicenses ? Drivers`
* `InternationalLicenses ? Licenses`
* `InternationalLicenses ? Users`
* `DetainedLicenses ? Licenses`
* `DetainedLicenses ? Applications`
* `DetainedLicenses ? Users`
* `LocalDrivingLicenseApplications ? Applications`
* `LocalDrivingLicenseApplications ? LicenseClasses`
* `TestAppointments ? Applications`
* `TestAppointments ? LocalDrivingLicenseApplications`
* `TestAppointments ? TestTypes`
* `TestAppointments ? Users`
* `Tests ? TestAppointments`
* `Tests ? Users`

This structure provides a strong relational foundation for the application's business workflows.

---

# ?? Programming Concepts Applied

One of the main goals of this project was to apply what I learned rather than studying concepts independently.

The project allowed me to practice:

### Object-Oriented Programming

* Classes
* Objects
* Encapsulation
* Inheritance
* Polymorphism
* Abstraction
* Interfaces
* Static members
* Properties
* Methods

### C# Concepts

* Nullable Types
* Exception Handling
* Validation
* Collections
* Enums
* Generics
* Delegates and Events
* Custom Attributes
* Reflection
* XML Documentation
* Debug / Release concepts
* Conditional compilation
* Reusable components

---

# ?? Debugging & Problem Solving

One of the most valuable parts of developing this project was learning how to **debug and solve problems independently**.

Throughout development, I encountered different types of problems involving:

* C# code
* Object references
* Database operations
* SQL queries
* Foreign Keys
* Data validation
* Business logic
* Layer communication
* Application workflow

Instead of simply searching for a solution, I focused on understanding **why the problem happened**.

My debugging process became:

```text
Error
  ?
Read the Error Message
  ?
Locate the Problem
  ?
Trace the Execution
  ?
Inspect Variables / Objects
  ?
Understand the Root Cause
  ?
Implement a Solution
  ?
Test
  ?
Verify the Result
```

This experience significantly improved my ability to read errors, analyze problems, and understand the behavior of my own code.

---

# ?? What This Project Taught Me

This project was an important transition from **learning programming concepts individually to applying them together in a complete software system**.

I learned how:

* A database connects to an application.
* Different application layers communicate with each other.
* Business rules should be separated from the UI.
* Database operations should be isolated from business logic.
* Foreign Keys maintain data integrity.
* Complex workflows can be represented through related entities.
* Errors can be investigated systematically.
* Debugging is a learning process, not just an error-fixing process.
* Good architecture makes applications easier to maintain.
* A working application requires more than writing code — it requires analysis, design, testing, debugging, and continuous improvement.

---

# ?? Development Journey

### Development Time

**Approximately 3 months**

During this period, I continuously:

```text
Learn
  ?
Understand
  ?
Implement
  ?
Encounter Problems
  ?
Debug
  ?
Solve
  ?
Improve
  ?
Learn More
```

The project evolved alongside my understanding of programming.

Every new concept I learned became an opportunity to improve the project or solve a new problem.

---

# ?? Why I Built This Project

The main objective was not to create a project simply for completion.

I wanted to answer a more important question:

> **Can I take what I learn and use it to build a complete software system from scratch?**

This project became the answer.

It gave me practical experience in:

**Programming ? Architecture ? Database ? Business Logic ? Debugging ? Problem Solving**

all within one application.

---

# ?? Project Structure

The solution is organized according to the Three-Tier Architecture:

```text
DVLD
?
??? Presentation Layer
?   ??? Forms
?   ??? User Controls
?   ??? UI Components
?
??? Business Layer
?   ??? Business Classes
?   ??? Business Rules
?   ??? Validation
?
??? Data Access Layer
?   ??? Data Access Classes
?   ??? SQL Queries
?   ??? Database Operations
?
??? SQL Server Database
    ??? Tables
    ??? Relationships
    ??? Constraints
    ??? Data
```

---

# ?? Future Improvements

Possible future improvements include:

* Modern UI redesign
* Improved user experience
* Better logging
* Automated testing
* More advanced reporting
* Performance optimization
* Authentication improvements
* Role-based authorization
* API integration
* Web-based version
* Mobile application
* Additional analytics and dashboards

---

# ?? Final Note

This project represents more than a software application for me.

It represents approximately **three months of learning, experimenting, debugging, problem solving, and continuous development**.

I built the system from scratch and used it as a practical environment to apply the concepts I was learning.

The most valuable lesson was not a specific programming language or technology.

It was learning how to approach a problem:

**Understand ? Analyze ? Implement ? Debug ? Solve ? Improve.**

This project helped me become more comfortable with writing code, reading code, understanding errors, debugging applications, working with databases, designing application layers, and thinking about software as a complete system.

---

# ??ž?? Developer

**Akram Alnowirah**

IT Student | Software Developer

### Built With

**C# · .NET · Windows Forms · ADO.NET · SQL Server**

---

? **This project is part of my learning journey and demonstrates my practical application of programming, database, architecture, debugging, and problem-solving skills.**
