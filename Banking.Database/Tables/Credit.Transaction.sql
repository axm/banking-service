﻿CREATE TABLE [Credit].[Transaction]
(
	[TransactionId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[CreditId] UNIQUEIDENTIFIER NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[Amount] MONEY NOT NULL,

	CONSTRAINT CHK_Transaction_Amount CHECK(Amount > 0)
)
