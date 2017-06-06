CREATE PROCEDURE [Account].[spSetOverdraft]
	@Id UNIQUEIDENTIFIER,
	@Amount MONEY
AS
	UPDATE dbo.AccountData
	SET Overdraft = @Amount
	WHERE AccountId = @Id