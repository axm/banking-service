CREATE TABLE [Credit].[CreditData]
(
	[CreditId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Limit] MONEY NOT NULL,
	[Interest] DECIMAL(10, 2) NOT NULL,
	[Usage] MONEY NOT NULL,

	CONSTRAINT CHK_Limit_Positive CHECK(Limit > 0),
	CONSTRAINT CHK_Usage_Positive CHECK(Usage >= 0),
	CONSTRAINT CHK_CreditData_Interest_Positive CHECK(Interest >= 0)
)
