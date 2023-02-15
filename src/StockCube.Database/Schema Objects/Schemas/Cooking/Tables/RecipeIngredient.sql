CREATE TABLE Cooking.RecipeIngredient (
  RecipeIngredientId UNIQUEIDENTIFIER NOT NULL,
  RecipeId UNIQUEIDENTIFIER NOT NULL,
  IngredientId UNIQUEIDENTIFIER NOT NULL,
  PRIMARY KEY (RecipeIngredientId)
)
