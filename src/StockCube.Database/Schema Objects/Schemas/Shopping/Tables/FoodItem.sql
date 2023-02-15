CREATE TABLE Shopping.FoodItem(
  FoodItemId UNIQUEIDENTIFIER NOT NULL,
  UnitId UNIQUEIDENTIFIER NOT NULL,
  Size FLOAT,
  FoodItemDescription EntityDescription,
  Quantity FLOAT,
  PRIMARY KEY (FoodItemId)
)
