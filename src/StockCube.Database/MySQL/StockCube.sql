CREATE SCHEMA `Kitchen`;
ALTER SCHEMA `Kitchen` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE SCHEMA `Cooking`;
ALTER SCHEMA `Cooking` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE SCHEMA `Shopping`;
ALTER SCHEMA `Shopping` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE SCHEMA `dbo`;
ALTER SCHEMA `Shopping` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Kitchen`.`Section` (
  `SectionId` Char(36),
  `Name` nvarchar(30),
  PRIMARY KEY (`SectionId`)
);
ALTER TABLE `Kitchen`.`Section` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Kitchen`.`SectionFoodItem` (
  `SectionFoodItemId` CHAR(36) ,
  `SectionId` CHAR(36) ,
  `FoodItemId` CHAR(36) ,
  `ExpiryDate` DATE,
  PRIMARY KEY (`SectionFoodItemId`)
);
ALTER TABLE `Kitchen`.`SectionFoodItem` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `dbo`.`Unit` (
  `UnitId` CHAR(36) ,
  `Name` NVARCHAR(30),
  PRIMARY KEY (`UnitId`)
);
ALTER TABLE `dbo`.`Unit` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Cooking`.`Recipe` (
  `RecipeId` CHAR(36) ,
  `Name` NVARCHAR(30),
  PRIMARY KEY (`RecipeId`)
);
ALTER TABLE `Cooking`.`Recipe` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Cooking`.`MealPlan` (
  `MealPlanId` CHAR(36) ,
  `Name` NVARCHAR(30),
  `StartDate` DATE,
  `EndDate` DATE,
  PRIMARY KEY (`MealPlanId`)
);
ALTER TABLE `Cooking`.`MealPlan` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Cooking`.`MealPlanRecipe` (
  `MealPlanRecipeId` CHAR(36) ,
  `RecipeId` CHAR(36) ,
  `MealPlanId` CHAR(36) ,
  PRIMARY KEY (`MealPlanRecipeId`)
);
ALTER TABLE `Cooking`.`MealPlanRecipe` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Cooking`.`RecipeIngredient` (
  `RecipeIngredientId` CHAR(36) ,
  `RecipeId` CHAR(36) ,
  `IngredientId` CHAR(36) ,
  PRIMARY KEY (`RecipeIngredientId`)
);
ALTER TABLE `Cooking`.`RecipeIngredient` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Cooking`.`Ingredient` (
  `IngredientId` CHAR(36) ,
  `FoodItemId` CHAR(36) ,
  `UnitId` CHAR(36) ,
  `Size` FLOAT,
  `Description` NVARCHAR(255),
  `Quantity` FLOAT,
  PRIMARY KEY (`IngredientId`)
);
ALTER TABLE `Cooking`.`Ingredient` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Shopping`.`Shoppinglist` (
  `ShoppingListId` CHAR(36) ,
  `Description` NVARCHAR(255),
  `ShoppingDate` DATE,
  PRIMARY KEY (`ShoppingListId`)
);
ALTER TABLE `Shopping`.`Shoppinglist` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Shopping`.`ShoppingListFoodItem` (
  `ShoppingListFoodItemId` CHAR(36) ,
  `ShoppingListId` CHAR(36) ,
  `FoodItemId` CHAR(36) ,
  PRIMARY KEY (`ShoppingListFoodItemId`)
);
ALTER TABLE `Shopping`.`ShoppingListFoodItem` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Shopping`.`FoodItem` (
  `FoodItemId` CHAR(36) ,
  `UnitId` CHAR(36) ,
  `Size` FLOAT,
  `Description` NVARCHAR(255),
  `Quantity` FLOAT,
  PRIMARY KEY (`FoodItemId`)
);
ALTER TABLE `Shopping`.`FoodItem` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

ALTER TABLE `Kitchen`.`SectionFoodItem` ADD FOREIGN KEY (`SectionId`) REFERENCES `Kitchen`.`Section` (`SectionId`);

ALTER TABLE `Kitchen`.`SectionFoodItem` ADD FOREIGN KEY (`FoodItemId`) REFERENCES `Shopping`.`FoodItem` (`FoodItemId`);

ALTER TABLE `Cooking`.`Ingredient` ADD FOREIGN KEY (`UnitId`) REFERENCES `dbo`.`Unit` (`UnitId`);

ALTER TABLE `Cooking`.`MealPlanRecipe` ADD FOREIGN KEY (`RecipeId`) REFERENCES `Cooking`.`Recipe` (`RecipeId`);

ALTER TABLE `Cooking`.`RecipeIngredient` ADD FOREIGN KEY (`RecipeId`) REFERENCES `Cooking`.`Recipe` (`RecipeId`);

ALTER TABLE `Cooking`.`MealPlanRecipe` ADD FOREIGN KEY (`MealPlanRecipeId`) REFERENCES `Cooking`.`MealPlan` (`MealPlanId`);

ALTER TABLE `Cooking`.`RecipeIngredient` ADD FOREIGN KEY (`IngredientId`) REFERENCES `Cooking`.`Ingredient` (`IngredientId`);

ALTER TABLE `Shopping`.`FoodItem` ADD FOREIGN KEY (`UnitId`) REFERENCES `dbo`.`Unit` (`UnitId`);

ALTER TABLE `Shopping`.`ShoppingListFoodItem` ADD FOREIGN KEY (`ShoppingListId`) REFERENCES `Shopping`.`Shoppinglist` (`ShoppingListId`);

ALTER TABLE `Shopping`.`ShoppingListFoodItem` ADD FOREIGN KEY (`FoodItemId`) REFERENCES `Shopping`.`FoodItem` (`FoodItemId`);

DELIMITER $$
CREATE PROCEDURE kitchen.USP_CreateSection 
(
	IN Id CHAR(36), 
	IN Name NVARCHAR(30)
)
BEGIN
	INSERT INTO kitchen.Section(SectionId, Name) 
    VALUES (Id,Name);
END$$

CREATE PROCEDURE kitchen.USP_GetSectionList()
BEGIN
	SELECT SectionId AS Id, Name 
    FROM kitchen.Section;
END$$

CREATE PROCEDURE kitchen.USP_GetSectionById
(
	IN Id CHAR(36)
)
BEGIN
	SELECT SectionId AS Id, Name 
    FROM Kitchen.Section WHERE SectionId = Id;
END$$

CREATE PROCEDURE kitchen.USP_DeleteSectionById
(
	IN Id CHAR(36)
)
BEGIN
	DECLARE result BOOLEAN DEFAULT 0;
    
    IF(NOT EXISTS(SELECT SectionId FROM kitchen.Section WHERE SectionId = Id)) THEN 
		SELECT result AS deleted;
    END IF;
    
	DELETE FROM kitchen.Section
    WHERE SectionId = Id;
    SET result = 1;
    
    SELECT result AS deleted;
END$$