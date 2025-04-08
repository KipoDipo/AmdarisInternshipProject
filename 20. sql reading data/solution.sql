use AdventureWorks2022;

select FirstName, LastName, BusinessEntityID as Employee_id 
from Person.Person
order by LastName;

select PersonPhone.BusinessEntityID, FirstName, LastName, PhoneNumber
from Person.PersonPhone
inner join Person.Person on Person.BusinessEntityID = PersonPhone.BusinessEntityID
where FirstName like 'L%'
order by LastName, FirstName;

select ROW_NUMBER() over (order by PostalCode, LastName, SalesYTD) as RowNumber, LastName, SalesYTD, PostalCode
from Sales.SalesPerson
inner join Person.[Address] on TerritoryID = AddressId
inner join Person.Person on Person.BusinessEntityID = SalesPerson.BusinessEntityID
where SalesYTD > 0
order by SalesYTD desc, PostalCode asc;

select SalesOrderID, SUM(LineTotal) as Total from Sales.SalesOrderDetail
group by SalesOrderID
having SUM(LineTotal) > 100000
