use AdventureWorks2022;

-- Gender distribution in HumanResources.Employee
select Gender, COUNT(*) as Count 
from HumanResources.Employee
group by Gender

-- Average rate per gender
select Gender, AVG(Rate) as AverageSalary
from HumanResources.Employee
join HumanResources.EmployeePayHistory on EmployeePayHistory.BusinessEntityID = Employee.BusinessEntityID
group by Gender

-- Average rate per title in desc order + Average leave hours
select JobTitle, AVG(Rate) as AverageSalary, AVG(SickLeaveHours) as LeaveHours
from HumanResources.Employee
join HumanResources.EmployeePayHistory on EmployeePayHistory.BusinessEntityID = Employee.BusinessEntityID
group by JobTitle
order by AverageSalary desc

-- Expenses per year
select YEAR(poh.OrderDate), COUNT(*) as Count, SUM(TotalDue) as Total
from Purchasing.PurchaseOrderHeader poh
group by YEAR(poh.OrderDate)
order by YEAR(poh.OrderDate)

-- Expenses and purchase count per Job title for the year 2011
select e.JobTitle, COUNT(*) as Count, SUM(TotalDue) as Total
from HumanResources.Employee e
join Purchasing.PurchaseOrderHeader poh on poh.EmployeeID = e.BusinessEntityID
where YEAR(poh.OrderDate) = 2011
group by e.JobTitle

-- Expenses and purchase count per Job title for the year 2011
select e.JobTitle, COUNT(*) as Count, SUM(TotalDue) as Total
from HumanResources.Employee e
join Purchasing.PurchaseOrderHeader poh on poh.EmployeeID = e.BusinessEntityID
where YEAR(poh.OrderDate) = 2011
group by e.JobTitle

-- Average order quantity per order by gender
select e.Gender, AVG(pod.OrderQty) as Average
from HumanResources.Employee e
join Purchasing.PurchaseOrderHeader poh on e.BusinessEntityID = poh.EmployeeID
join Purchasing.PurchaseOrderDetail pod on poh.PurchaseOrderID = pod.PurchaseOrderID
group by e.Gender

-- Each person's name + credit card type
select p.BusinessEntityID, p.FirstName, p.LastName, cc.CardType
from Person.Person p
join Sales.PersonCreditCard pcc on p.BusinessEntityID = pcc.BusinessEntityID
join Sales.CreditCard cc on pcc.CreditCardID = cc.CreditCardID
order by p.FirstName, p.LastName