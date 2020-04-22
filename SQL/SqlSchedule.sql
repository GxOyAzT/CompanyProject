use Company

CREATE TABLE Schedule(
	SchId int primary key identity not null,
	Sch_EmpId int foreign key references EmployeeInfo(EmpId) not null,
	SchStart datetime not null,
	SchEnd datetime not null
);