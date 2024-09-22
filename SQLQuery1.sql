CREATE OR ALTER PROCEDURE GetTicketSupportByID
    @TicketSupportID INT -- Required parameter to filter by TicketSupportID
AS
BEGIN
    -- Fetch TicketSupport data by TicketSupportID
    SELECT 
        TicketSupportID,
        PersonID,
        PackageID,
        StatusID,
        Email,
        PriorityID,
        DepartmentID,
        Subject,
        Description
    FROM TicketSupports
    WHERE TicketSupportID = @TicketSupportID;
END;



select * from TicketSupports

DROP PROCEDURE IF EXISTS GetPriorityByID