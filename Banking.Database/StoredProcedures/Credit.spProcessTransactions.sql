CREATE PROCEDURE [Credit].[spProcessTransactions]
AS
	BEGIN
		SELECT TOP 1000 TransactionId, CreditId, [Timestamp], Amount
		INTO #TempTransactions
		FROM Credit.PendingTransaction

		INSERT INTO Credit.[Transaction](TransactionId, CreditId, [Timestamp], Amount)
		SELECT TransactionId, CreditId, [Timestamp], Amount
		FROM #TempTransactions

		DELETE Credit.PendingTransaction
		WHERE TransactionId IN (SELECT TransactionId FROM #TempTransactions)
	END
