CREATE TABLE Cooking.Ingredient (
  IngredientId UNIQUEIDENTIFIER  NOT NULL,
  FoodItemId UNIQUEIDENTIFIER  NOT NULL,
  UnitId UNIQUEIDENTIFIER  NOT NULL,
  Size FLOAT,
  IngredientDescription EntityDescription,
  Quantity FLOAT,
  PRIMARY KEY (IngredientId)
)
