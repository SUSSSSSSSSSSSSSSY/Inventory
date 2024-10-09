# Inventory

Запросы в Microsoft SQL Server Management studio выглядят так:

CREATE DATABASE InventoryDB; 


CREATE TABLE InventoryItems
(
    ID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    Quantity INT,
    Status NVARCHAR(50)
);


CREATE PROCEDURE AddOrUpdateInventory
    @ObjectName NVARCHAR(100),
    @ObjectQuantity INT,
    @ObjectStatus NVARCHAR(50),
    @ObjectID INT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Inventory WHERE Name = @ObjectName)
    BEGIN
        UPDATE Inventory
        SET Quantity = @ObjectQuantity, Status = @ObjectStatus
        WHERE Name = @ObjectName;
        SELECT @ObjectID = ID
        FROM Inventory
        WHERE Name = @ObjectName;
    END
    ELSE
    BEGIN
        INSERT INTO Inventory (Name, Quantity, Status)
        VALUES (@ObjectName, @ObjectQuantity, @ObjectStatus);
        SELECT @ObjectID = SCOPE_IDENTITY();
    END
END;


В результате выдаёт "Новый/обновленный объект в инвентаре имеет ID: 1"
