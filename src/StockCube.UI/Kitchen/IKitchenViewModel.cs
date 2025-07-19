using System.Threading.Tasks;

namespace StockCube.UI.Kitchen
{
    public interface IKitchenViewModel
    {
        /// <summary>
        /// Gets or sets the list of kitchen sections
        /// </summary>
        List<KitchenSection> Sections { get; set; }

        /// <summary>
        /// Gets the list of items expiring in the current month
        /// </summary>
        List<KitchenItem> ExpiringItems { get; }

        /// <summary>
        /// Gets or sets whether all sections should be shown expanded
        /// </summary>
        bool ShowAllSections { get; set; }

        /// <summary>
        /// Gets the list of section names
        /// </summary>
        List<string> ListOfSections { get; set; }

        /// <summary>
        /// Gets or sets the list of food items
        /// </summary>
        List<FoodItemModel> FoodItems { get; set; }

        /// <summary>
        /// Gets all sections from the API
        /// </summary>
        Task<HttpResponseMessage> GetSections();

        /// <summary>
        /// Gets all food items for a specific section
        /// </summary>
        /// <param name="sectionId">The ID of the section</param>
        Task<HttpResponseMessage> GetFoodItems(Guid sectionId);

        /// <summary>
        /// Create a new section in the database
        /// </summary>
        /// <param name="section">The name of the section</param>
        Task<HttpResponseMessage> AddSection(string section);

        /// <summary>
        /// Refreshes the data from the API
        /// </summary>
        Task Refresh();
    }
}
