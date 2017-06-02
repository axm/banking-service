CREATE PROCEDURE [Credit].[spProcessPayments]
AS
	BEGIN
		
		INSERT INTO Credit.[Payment](PaymentId, CreditId, FromAccountId, [Timestamp], Amount)
		SELECT PaymentId, CreditId, FromAccountId, [Timestamp], Amount
		FROM Credit.PendingPayment
	END