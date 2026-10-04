using Assignment18AsynchronousProgramming.Utilities;

namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Provides methods for executing HTTP requests using an instance of HttpClient.
    /// </summary>
    internal class Task1HttpClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task1HttpClient"/> class.
        /// </summary>
        /// <param name="httpClient">The instance of the httpClient.</param>
        public Task1HttpClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        /// <summary>
        /// Asynchronously retrieves the content of the Google homepage and writes it to the console.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ExecuteRequestAsync()
        {
            ConsoleHelpers.DisplayStatus("Executing HTTP request to Google");
            string googleContent = await this._httpClient.GetStringAsync("https://www.google.com");

            ConsoleHelpers.DisplaySuccess("Response received successfully");
            Console.WriteLine(googleContent);
        }
    }
}
