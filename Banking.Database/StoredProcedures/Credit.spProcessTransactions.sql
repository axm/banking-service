CREATE PROCEDURE [Credit].[spProcessTransactions]
AS
	BEGIN
		INSERT INTO Credit.[Transaction](TransactionId, CreditId, [Timestamp], Amount)
		SELECT TransactionId, CreditId, [Timestamp], Amount
		FROM Credit.PendingTransaction
	END
