use AdventureWorks2022;

-- Create tables with all male employees and female employees
begin transaction;

select emp.* into maleEmployees from HumanResources.Employee emp
where emp.Gender = 'M';

select emp.* into femaleEmployees from HumanResources.Employee emp
where emp.Gender = 'F';

alter table maleEmployees
drop column Gender;

alter table femaleEmployees
drop column Gender;

commit transaction;

-- Double Vacation Hours for male employees employed for over 4 years and female employees employed for over 3 years

begin transaction;

update maleEmployees
set VacationHours = VacationHours * 2
where year(HireDate) < year(getdate()) - 4;

update femaleEmployees
set VacationHours = VacationHours * 2
where year(HireDate) < year(getdate()) - 3;

commit transaction;

-- Remove all technicians from maleEmployees and femaleEmployees tables
begin transaction;

delete from maleEmployees
where JobTitle like '%Technician%';

delete from femaleEmployees
where JobTitle like '%Technician%';

commit transaction;

-- Combine both male and female employees table into employeesOfInterest table

begin transaction;

select * into employeesOfInterest
from maleEmployees
union all
select *
from femaleEmployees;

commit transaction;

-- Split employees based on experience: 
-- Less than 5 years -> juniorEmployees, 5+ years -> seniorEmployees
begin transaction;

select * into juniorEmployees 
from employeesOfInterest
where year(HireDate) >= year(getdate()) - 5;

select * into seniorEmployees 
from employeesOfInterest
where year(HireDate) < year(getdate()) - 5;

commit transaction;

-- Normalize sick leave hours (if < 40 then 40)
begin transaction;

update employeesOfInterest
set SickLeaveHours = 40
where SickLeaveHours < 40;

commit transaction;

-- Triple sick leave hours for seniors
begin transaction;

update seniorEmployees
set SickLeaveHours = SickLeaveHours * 3;

commit transaction;


-- Drop tables all that were created
begin transaction

drop table maleEmployees;
drop table femaleEmployees;
drop table employeesOfInterest;
drop table juniorEmployees;
drop table seniorEmployees;

commit transaction;