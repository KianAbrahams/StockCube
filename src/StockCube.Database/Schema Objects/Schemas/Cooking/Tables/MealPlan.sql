CREATE TABLE Cooking.MealPlan (
  MealPlanId UNIQUEIDENTIFIER NOT NULL, 
  MealPlanName EntityName,
  StartDate DATE,
  EndDate DATE,
  PRIMARY KEY (MealPlanId)
)
