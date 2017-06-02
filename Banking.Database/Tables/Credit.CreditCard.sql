CREATE TABLE [Credit].[CreditCard]
(
	[CreditCardId] INT NOT NULL PRIMARY KEY,
	[Number] VARCHAR(16) NOT NULL,
	[StartMonth] INT NOT NULL,
	[StartYear] INT NOT NULL,
	[EndMonth] INT NOT NULL,
	[EndYear] INT NOT NULL,

	CONSTRAINT CreditCard_Number_UQ UNIQUE(Number)
)
