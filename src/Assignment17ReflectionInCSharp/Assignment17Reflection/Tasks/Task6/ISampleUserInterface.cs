namespace Assignment17Reflection.Tasks.Task6
{
    /// <summary>
    /// Represents the interface for sample user.
    /// </summary>
    public interface ISampleUserInterface
    {
        /// <summary>
        /// Gets the user count.
        /// </summary>
        /// <returns>An integer holding the user count.</returns>
        int GetUserCount();

        /// <summary>
        /// Gets the user email Id.
        /// </summary>
        /// <param name="userId">The email Id of the user.</param>
        /// <returns>A string holding the email.</returns>
        string GetUserEmail(int userId);

        /// <summary>
        /// Checks if the user is admin.
        /// </summary>
        /// <param name="userId">Id of the user to check.</param>
        /// <returns>True if the user is admin; otherwise false.</returns>
        bool IsAdmin(int userId);

        /// <summary>
        /// Deletes the given user.
        /// </summary>
        /// <param name="userId">Id of the user to be deleted.</param>
        void DeleteUser(int userId);
    }
}
