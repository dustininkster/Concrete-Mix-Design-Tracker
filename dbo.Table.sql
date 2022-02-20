CREATE TABLE [dbo].[Aggregates]
(
	[Aggregate_ID] TINYINT NOT NULL PRIMARY KEY IDENTITY, 
    [Minimum_Dosage] DECIMAL(3, 1) NOT NULL,
	[Maximum_Dosage] DECIMAL(3, 1) NOT NULL,
	[Is_ByCWT] BIT NOT NULL,
	[Material_ID] TINYINT NOT NULL,
	CONSTRAINT FK_Materials_Aggregates FOREIGN KEY (Material_ID)
	REFERENCES Materials(Material_ID)
)
