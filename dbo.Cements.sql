CREATE TABLE [dbo].[Cements] (
    [Cement_ID]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Material_ID] TINYINT      NULL,
    PRIMARY KEY CLUSTERED ([Cement_ID] ASC),
    CONSTRAINT [FK_Materials_Cements] FOREIGN KEY ([Material_ID]) REFERENCES [dbo].[Materials] ([Material_ID])
);

