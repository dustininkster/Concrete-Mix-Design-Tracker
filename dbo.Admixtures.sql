CREATE TABLE [dbo].[Admixtures] (
    [Admixture_ID]   TINYINT        IDENTITY (1, 1) NOT NULL,
    [Minimum_Dosage] DECIMAL (3, 1) NOT NULL,
    [Maximum_Dosage] DECIMAL (3, 1) NOT NULL,
    [Is_ByCWT]       BIT            NOT NULL,
    [Material_ID] TINYINT NOT NULL, 
    CONSTRAINT [PK_Admixtures] PRIMARY KEY CLUSTERED ([Admixture_ID] ASC),
	CONSTRAINT [FK_Admixtures_Materials] FOREIGN KEY ([Material_ID]) REFERENCES [dbo].[Materials]([Material_ID])
);

