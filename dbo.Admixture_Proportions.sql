CREATE TABLE [dbo].[Admixture_Proportions] (
    [Admixture_Proportions_ID] INT     IDENTITY (1, 1) NOT NULL,
    [Prototype_ID]             TINYINT NOT NULL,
    [Admixture_ID]             TINYINT NOT NULL,
    [Admixture_Qty]            DECIMAL(5, 2)     NULL,
    PRIMARY KEY CLUSTERED ([Admixture_Proportions_ID] ASC),
    CONSTRAINT [FK_Prototypes_Admixture_Proportions] FOREIGN KEY ([Prototype_ID]) REFERENCES [dbo].[Prototypes] ([Prototype_ID]),
    CONSTRAINT [FK_Admixtures_Admixture_Proportions] FOREIGN KEY ([Admixture_ID]) REFERENCES [dbo].[Admixtures] ([Admixture_ID])
);

