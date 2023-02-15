CREATE TABLE Shopping.Shoppinglist (
  ShoppingListId UNIQUEIDENTIFIER NOT NULL,
  ShoppingListDescription EntityDescription,
  ShoppingDate DATE,
  PRIMARY KEY ([ShoppingListId])
)
