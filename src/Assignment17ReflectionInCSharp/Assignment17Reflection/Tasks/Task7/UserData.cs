namespace Assignment17Reflection.Tasks.Task7
{
    /// <summary>
    /// Represents the user data.
    /// </summary>
    internal class UserData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserData"/> class.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="age">The age of the user.</param>
        public UserData(string name, string email, int age)
        {
            this.Name = name;
            this.Email = email;
            this.Age = age;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>A string holding the name of the user.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        /// <value>A string holding the email of the user.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        /// <value>An integer holding the age of the user.</value>
        public int Age { get; set; }
    }
}
