--EXECUTE sp_PersonInfo_UpdateFullPersonInfo 101, 'Kacper', 'Lewandowski', 'M', '1987-05-04', 'Poland', 'Szczecin', 'Karola Wielkiego 55', '70-120', '554632011', 'kacper.kolano@interia.pl'

SELECT * FROM PersonBasicInfo
SELECT * FROM PersonAdressInfo
EXECUTE sp_PersonInfo_GetAllCon
EXECUTE sp_PersonInfo_GetAll
EXECUTE sp_PersonInfo_GetAllAdr
EXECUTE sp_PersonInfo_GetAllBas



SELECT * FROM Schedule WHERE YEAR(SchDate) = 2021
SELECT * FROM Schedule

EXECUTE sp_Schedule_SalaryMonthlyByYear 2020

SELECT NULL AS XD

CREATE PROCEDURE sp_Schedule_GetWholeYearSalary
	@year int
AS
BEGIN
SELECT FullName, 
	CASE WHEN [1] IS NULL THEN 0 ELSE [1] END AS [1],
	CASE WHEN [2] IS NULL THEN 0 ELSE [2] END AS [2],
	CASE WHEN [3] IS NULL THEN 0 ELSE [3] END AS [3],
	CASE WHEN [4] IS NULL THEN 0 ELSE [4] END AS [4],
	CASE WHEN [5] IS NULL THEN 0 ELSE [5] END AS [5],
	CASE WHEN [6] IS NULL THEN 0 ELSE [6] END AS [6],
	CASE WHEN [7] IS NULL THEN 0 ELSE [7] END AS [7],
	CASE WHEN [8] IS NULL THEN 0 ELSE [8] END AS [8],
	CASE WHEN [9] IS NULL THEN 0 ELSE [9] END AS [9],
	CASE WHEN [10] IS NULL THEN 0 ELSE [10] END AS [10],
	CASE WHEN [11] IS NULL THEN 0 ELSE [11] END AS [11],
	CASE WHEN [12] IS NULL THEN 0 ELSE [12] END AS [12]
	FROM(
	SELECT 
		FullName,
		CONVERT(VARCHAR, MONTH(SchDate)) AS DateMonth,
		FullDailySalary
	FROM (
		SELECT PerBasFirstName + ' ' + PerBasLastName AS FullName, SchDate, FLOOR(EmpProSalary * EmpManSalaryIncrease / 100 / EmpTime / 160 * (CONVERT(FLOAT, CONVERT(DATETIME, CONVERT(TIME, SchEnd))) * 24 - CONVERT(FLOAT, CONVERT(DATETIME, CONVERT(TIME, SchStart))) * 24)) AS FullDailySalary 
		FROM Schedule s
		LEFT JOIN FullEmployeeInfo fei ON s.Sch_EmpId = fei.EmpId
	WHERE YEAR(SchDate) = @year
	)t)c
PIVOT(
	SUM(FullDailySalary)
	FOR DateMonth IN (
	[1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12]
	)
) AS pt

END

EXECUTE sp_Schedule_GetWholeYearSalary 2020
DROP PROCEDURE sp_Schedule_GetWholeYearSalary