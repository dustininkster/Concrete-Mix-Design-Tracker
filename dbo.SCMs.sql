CREATE TABLE [dbo].[SCMs] (
    [SCM_ID]      TINYINT      IDENTITY (1, 1) NOT NULL,
    [Material_ID] TINYINT      NULL,
    PRIMARY KEY CLUSTERED ([SCM_ID] ASC),
    CONSTRAINT [FK_Materials_SCMs] FOREIGN KEY ([Material_ID]) REFERENCES [dbo].[Materials] ([Material_ID])
);

