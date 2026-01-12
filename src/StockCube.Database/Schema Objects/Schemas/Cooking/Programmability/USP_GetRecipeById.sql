CREATE PROCEDURE [cooking].[USP_GetRecipeById]
    @ID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RecipeId AS Id,
        RecipeName AS Name,
        CreatedAt AS CreatedAt
    FROM [Cooking].[Recipe]
    WHERE RecipeId = @ID;
END;
