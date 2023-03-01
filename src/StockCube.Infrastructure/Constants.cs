namespace StockCube.Infrastructure;

internal class Constants
{
    internal class Db
    {
        internal class Kitchen
        {
            public const string schemaName = "kitchen";
            public const string USP_GetSectionList = "kitchen.USP_GetSectionList";
            public const string USP_GetSectionById = "kitchen.USP_GetSectionById";
            public const string USP_DeleteSectionById = "kitchen.USP_DeleteSectionById";
            public const string USP_CreateSection = "kitchen.USP_CreateSection";
        }
        internal class Cooking
        {
            public const string schemaName = "cooking";
        }
        internal class dbo
        {
            public const string schemaName = "dbo";
        }
        internal class Shopping
        {
            public const string schemaName = "shopping";
        }
    }
}
