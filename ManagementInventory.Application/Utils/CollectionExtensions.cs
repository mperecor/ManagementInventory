namespace ManagementInventory.Application.Utils
{
    /// <summary>
    /// Extended class of the IEnumerable object
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Method that indicates whether an object of type IEnumerable has elements or not
        /// </summary>
        /// <typeparam name="T">Type entity</typeparam>
        /// <param name="list">Object type IEnumerable</param>
        /// <returns>Returns true if it has elements otherwise returns false</returns>
        public static bool HasElements<T>(this IEnumerable<T> list)
        {
            return list?.Any() == true;
        }
    }
}