CREATE PROCEDURE [Account].[spNewAccount]
	@AccountId UNIQUEIDENTIFIER,
	@SortCode VARCHAR(8),
	@Overdraft MONEY,
	@Balance MONEY
AS
	BEGIN

		IF NOT EXISTS(SELECT NULL FROM dbo.AccountData WHERE AccountId = @AccountId)
			INSERT INTO dbo.AccountData(AccountId, SortCode, Overdraft, Balance)
			VALUES(@AccountId, @SortCode, @Overdraft, @Balance)

	END
