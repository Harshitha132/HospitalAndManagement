# Hospital Management System
Created HospitalAndManagement
Step1:Created folders
Step2:Create database,table and insert the data
Step 3: Install Package named Microsoft.Data.SqlClient
using 
		dotnet new console -n HospitalManagementSystem -f net10.0
		cd HospitalManagementSystem
		dotnet add package Microsoft.Data.SqlClient
Step 4: 
## Project structure
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
		