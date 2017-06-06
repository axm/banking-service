CREATE PROCEDURE [Credit].[spCreateBillingPeriod]
	@Id UNIQUEIDENTIFIER,
	@StartDate DATE,
	@EndDate DATE
AS
	BEGIN

		INSERT INTO Credit.BillingPeriod(Id, StartDate, EndDate)
		VALUES(@Id, @StartDate, @EndDate)

	END
