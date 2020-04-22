SELECT sch.SchId, sch.Sch_EmpId, sch.SchDate, SchEnd, SchStart, pbi.PerBasFirstName, pbi.PerBasLastName, emi.EmpManName, epi.EmpProName FROM  Schedule sch LEFT JOIN EmployeeInfo emp ON emp.EmpId = sch.Sch_EmpId LEFT JOIN PersonBasicInfo pbi ON emp.Emp_PerBasId = pbi.PerBasId LEFT JOIN EmploymentManagementInfo emi ON emi.EmpManId = emp.Emp_EmpManId LEFT JOIN EmploymentProfessionInfo epi ON emp.Emp_EmpProId = epi.EmpProId WHERE sch.SchDate = '2020-03-30';

SELECT * FROM EmployeeInfo LEFT JOIN PersonBasicInfo ON PerBasId = Emp_PerBasId;

--DELETE FROM Schedule WHERE SchId = 1;

SELECT * FROM Schedule WHERE Sch_EmpId = 1 AND SchDate >= '2020-03-01' AND SchDate < '2020-04-01';

SELECT ei.EmpId, pbi.PerBasFirstName, pbi.PerBasLastName, emi.EmpManName, emi.EmpManSalaryIncrease, epi.EmpProName, epi.EmpProSalary FROM (SELECT DISTINCT s.Sch_EmpId FROM Schedule s WHERE s.SchDate >= '2020-03-01' and s.SchDate < '2020-04-01') x LEFT JOIN EmployeeInfo ei ON ei.EmpId = x.Sch_EmpId LEFT JOIN PersonBasicInfo pbi ON pbi.PerBasId = ei.Emp_PerBasId LEFT JOIN EmploymentManagementInfo emi ON emi.EmpManId = ei.Emp_EmpManId LEFT JOIN EmploymentProfessionInfo epi ON epi.EmpProId = ei.Emp_EmpProId

SELECT AVG(EmpProSalary) AS XD FROM EmploymentProfessionInfo;

SELECT * FROM PersonBasicInfo WHERE PerBasId NOT IN (SELECT Emp_PerBasId FROM EmployeeInfo);

SELECT EmpId, EmpTime, EmpDateOfEmployment, PerBasId, PerBasFirstName, PerBasLastName, EmpManId, EmpManName, EmpManSalaryIncrease,EmpProId, EmpProName, EmpProSalary FROM EmployeeInfo INNER JOIN PersonBasicInfo ON PerBasId = Emp_PerBasId INNER JOIN EmploymentManagementInfo ON Emp_EmpManId = EmpManId INNER JOIN EmploymentProfessionInfo ON Emp_EmpProId = EmpProId WHERE EmpId NOT IN (SELECT Sch_EmpId FROM Schedule WHERE SchDate = '2020-04-04');

SELECT * FROM EmployeeInfo;

SELECT * FROM EmployeeAccount;