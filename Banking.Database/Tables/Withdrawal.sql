CREATE TABLE [dbo].[Withdrawal]
(
	[WithdrawalId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[AccountId] UNIQUEIDENTIFIER NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[Amount] MONEY NOT NULL,

	CONSTRAINT CHK_Withdrawal_Amount_Positive CHECK(Amount > 0)
)
