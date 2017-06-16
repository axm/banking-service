CREATE PROCEDURE [Account].[spSetDirectDebit]
	@Id UNIQUEIDENTIFIER,
	@Amount MONEY,
	@FromAccountId UNIQUEIDENTIFIER,
	@ToAccountId UNIQUEIDENTIFIER,
	@StartDate DATETIMEOFFSET,
	@Frequency INT
AS
	BEGIN

		IF NOT EXISTS(SELECT NULL FROM Account.DirectDebit WHERE Id = @Id)
			INSERT INTO Account.DirectDebit(Id, Amount, FromAccountId, ToAccountId, StartDate, LastRunTimestamp, Frequency)
			VALUES(@Id, @Amount, @FromAccountId, @ToAccountId, @StartDate, NULL, @Frequency)
	END