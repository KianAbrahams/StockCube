CREATE TABLE Shopping.ShoppingListFoodItem(
  ShoppingListFoodItemId UNIQUEIDENTIFIER NOT NULL,
  ShoppingListId UNIQUEIDENTIFIER NOT NULL,
  FoodItemId UNIQUEIDENTIFIER NOT NULL,
  PRIMARY KEY (ShoppingListFoodItemId)
)
