using ClassLibrary.ClassesModels;
using ClassLibrary.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Schedule
{
    public class ScheduleModel
    {
        public int SchId { get; private set; } = -1;
        public EmployeeModel SchEmployeeModel {  get; private set; }
        public DateTime SchDate { get; private set; }
        public TimeSpan SchStart {  get; private set; }
        public TimeSpan SchEnd {  get; private set; }
        public string SchTimeString { get => $"{SchStart.ToString()} - {SchEnd.ToString()}"; }
        public string SchCountTimeString { get => $"{(SchEnd - SchStart).TotalHours} h"; }

        public ScheduleModel(int empId, DateTime schDate, TimeSpan schStart, TimeSpan schEnd)
        {
            SchDate = schDate;
            SchStart = schStart;
            SchEnd = schEnd;
            SchEmployeeModel = new EmployeeModel(empId);
        }
        public ScheduleModel(int schId, DateTime schDate, TimeSpan schStart, TimeSpan schEnd, EmployeeModel employeeModel)
        {
            SchId = schId;
            SchDate = schDate;
            SchStart = schStart;
            SchEnd = schEnd;
            SchEmployeeModel = employeeModel;
        }
        public ScheduleModel(int empId, DateTime schDate, TimeSpan schStart, TimeSpan schEnd, int schId)
        {
            SchEmployeeModel = new EmployeeModel(empId);
            SchId = schId;
            SchDate = schDate;
            SchStart = schStart;
            SchEnd = schEnd;
        }

        public void GetFullEmpInfo()
        {
            SchEmployeeModel = EmploymentDbConn.LoadFullEmployeeInfoOnId(SchEmployeeModel.EmpId);
        }
    }
}
