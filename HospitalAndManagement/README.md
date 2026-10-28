# Hospital Management System

A C# console application developed using .NET, ADO.NET, and SQL Server to manage patients, doctors, admissions, and hospital fees.

## Project Development Steps

### Step 1: Create the Project and Folders

Created the Hospital Management System project and organized the code into separate folders.

### Step 2: Create the Database and Tables

- Created the `HospitalManagementDB` database using SQL Server Management Studio (SSMS).
- Created the required tables:
  - Patients
  - Doctors
  - Admissions
  - HospitalFees
- Inserted sample data into the tables.
- Established relationships using primary keys and foreign keys.

### Step 3: Install the Required Package

Installed the `Microsoft.Data.SqlClient` package to connect the C# application to SQL Server.

Commands used:

```bash
dotnet new console -n HospitalManagementSystem -f net10.0
cd HospitalManagementSystem
dotnet add package Microsoft.Data.SqlClient
```

### Step 4: Project Structure

```text
HospitalManagementSystem/
├── Program.cs
├── Helpers/
│   └── DatabaseHelper.cs
├── Models/
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Admission.cs
│   └── HospitalFee.cs
├── Repositories/
│   ├── PatientRepository.cs
│   ├── DoctorRepository.cs
│   ├── AdmissionRepository.cs
│   └── HospitalFeeRepository.cs
└── Services/
    ├── PatientService.cs
    ├── DoctorService.cs
    ├── AdmissionService.cs
    └── HospitalFeeService.cs
```

### Step 5: Create DatabaseHelper

Created `DatabaseHelper.cs` inside the `Helpers` folder.

This class contains the connection string and a `CreateConnection()` method used to connect the application to SQL Server.

Connection string:

```csharp
@"Server=.\SQLEXPRESS;Database=HospitalManagementDB;" +
@"Trusted_Connection=True;TrustServerCertificate=True;";
```

The `CreateConnection()` method creates and returns a `SqlConnection` object.

**Note:** The SQL Server instance name must match the instance installed on the computer.

### Step 6: Create Model Classes

Created the following model classes inside the `Models` folder:

- `Patient.cs`
- `Doctor.cs`
- `Admission.cs`
- `HospitalFee.cs`

Model classes contain properties representing the data required by the application. These properties are used by the service and repository layers.

### Step 7: Create Repository Classes

Created repository classes to perform database operations using ADO.NET.

- `PatientRepository.cs`
- `DoctorRepository.cs`
- `AdmissionRepository.cs`
- `HospitalFeeRepository.cs`

Each repository is responsible for communicating with SQL Server using SQL commands and parameterized queries.

The planned database operations are:

- **Add:** Insert a new record.
- **Display:** Retrieve and display records.
- **Update:** Modify an existing record.
- **Delete:** Remove a record when permitted by database constraints.

### Step 8: Create Service Classes

Created service classes to implement business logic and validate user input.

- `PatientService.cs`
- `DoctorService.cs`
- `AdmissionService.cs`
- `HospitalFeeService.cs`

The service layer validates the data before passing it to the repository layer.

### Step 9: Implement CRUD Operations

Implement and test the following operations for each module:

- Add a patient, doctor, admission, and hospital fee.
- Display all records.
- Update existing records.
- Delete records when permitted.

### Step 10: Implement Date Functionality

Add and manage relevant dates, including:

- Patient registration date.
- Doctor joining date.
- Admission date.
- Discharge date.
- Hospital fee payment date.

Allow date-based searches and validate that the discharge date is not earlier than the admission date.

### Step 11: Implement Transaction Handling

Use ADO.NET transactions when related database operations must succeed or fail together.

For example, when creating an admission and its hospital fee:

1. Begin a transaction.
2. Insert the admission record.
3. Insert the corresponding hospital fee record.
4. Commit if both operations succeed.
5. Roll back if either operation fails.

### Step 12: Test the Application

Test the following:

- Database connectivity.
- Add, display, update, and delete operations.
- Input validation.
- Date functionality.
- Transaction commit and rollback.
- Database relationships and constraints.

### Step 13: Maintain Git and GitHub

Commit each feature separately to maintain a clear development history.

Example commit messages:

```bash
git add README.md
git commit -m "docs: add project README"

git add Models/Patient.cs
git commit -m "feat(models): add patient model"

git add Repositories/PatientRepository.cs
git commit -m "feat(patient): implement patient repository CRUD"

git add Services/PatientService.cs
git commit -m "feat(patient): add patient service validation"
```

Before each commit, run `git status` to verify which files have changed. Push your commits to GitHub when ready.

## Technologies Used

- C#
- .NET 10
- ADO.NET
- Microsoft.Data.SqlClient
- SQL Server
- SQL Server Management Studio (SSMS)
- Git and GitHub

## Project Architecture

The application follows a layered architecture:

**Program.cs → Service → Repository → DatabaseHelper → SQL Server**

This separation makes the application easier to maintain, test, and extend.

## Current Progress

Update this section as you complete and test each feature.

- [x] Project and folder structure
- [x] Database and initial tables
- [x] Required package installation
- [x] DatabaseHelper
- [x] Model classes
- [ ] Repository CRUD operations
- [ ] Service validation
- [ ] Console menu
- [ ] Date functionality
- [ ] Transaction handling
- [ ] Complete testing
- [ ] Final GitHub documentation
