CREATE PROCEDURE [Credit].[spGetCurrentBillingPeriod]
AS
	SELECT Id, StartDate, EndDate
	FROM Credit.BillingPeriod bp
	WHERE GETDATE() >= bp.StartDate AND GETDATE() <= bp.EndDate
