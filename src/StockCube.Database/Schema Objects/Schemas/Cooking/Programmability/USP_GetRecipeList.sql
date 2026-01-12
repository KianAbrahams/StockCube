CREATE PROCEDURE [cooking].[USP_GetRecipeList]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [RecipeId]   AS Id,
        [RecipeName] AS Name,
        [CreatedAt]  AS CreatedAt
    FROM [Cooking].[Recipe];
END;
