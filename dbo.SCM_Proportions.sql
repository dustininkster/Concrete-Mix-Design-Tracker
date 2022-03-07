CREATE TABLE [dbo].[SCM_Proportions] (
    [SCM_Proportions_ID] INT     IDENTITY (1, 1) NOT NULL,
    [Prototype_ID]       TINYINT NOT NULL,
    [SCM_ID]             TINYINT NOT NULL,
    [Weight_of_SCMs] INT NULL, 
    PRIMARY KEY CLUSTERED ([SCM_Proportions_ID] ASC),
    CONSTRAINT [FK_Prototypes_SCM_Proportions] FOREIGN KEY ([Prototype_ID]) REFERENCES [dbo].[Prototypes] ([Prototype_ID]),
    CONSTRAINT [FK_SCMS_SCM_Proportions] FOREIGN KEY ([SCM_ID]) REFERENCES [dbo].[SCMs] ([SCM_ID])
);

