use AdventureWorks2022;

-- All products that have a lower rating than 3
select ProductID, [Name]
from Production.[Product]
where ProductID in
	(select ProductID 
	from Production.ProductReview
	where Rating < 3);

-- Most expensive products
select * 
from Production.[Product]
where ListPrice =
	(select MAX(ListPrice)
	from Production.[Product])

-- Customers who have placed at least 1 order
select BusinessEntityID, FirstName, LastName
from Person.Person
where BusinessEntityID in
	(select CustomerID
	from Sales.Customer);

-- Orders with above average total price
select * 
from Sales.SalesOrderDetail
where LineTotal >
	(select AVG(LineTotal)
	from Sales.SalesOrderDetail);

-- Products that have never been ordered
select *
from Production.Product
where ProductID not in
	(select ProductID 
	from Sales.SalesOrderDetail);

-- Customers IDs with orderqty more than the average
select distinct header.CustomerID
from Sales.SalesOrderHeader header
where header.SalesOrderID in
	(select detail.SalesOrderID
	from Sales.SalesOrderDetail detail
	where detail.OrderQty > 
		(select AVG(OrderQty)
		from Sales.SalesOrderDetail));

-- Suppliers with most expensive product
select s.BusinessEntityID, s.Name
from Purchasing.Vendor s
where s.BusinessEntityID IN
    (select pv.BusinessEntityID
     from Purchasing.ProductVendor pv
     where pv.ProductID = 
         (select top 1 p.ProductID
          from Production.Product p
		  where p.ProductID in
			(select ProductID from Purchasing.ProductVendor)
          order by p.ListPrice desc));