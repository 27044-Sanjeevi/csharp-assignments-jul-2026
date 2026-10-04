using System.Net.Http.Json;
using System.Text.Json;
using Assignment18AsynchronousProgramming.Utilities;

namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Handles cascading asynchronous operations involving HTTP requests and data extraction.
    /// </summary>
    internal class Task4CascadingAsyncOperations
    {
        private const string WebServiceBaseUrl = "https://dummyjson.com/";
        private const int ParameterCount = 9;
        private readonly HttpClient _httpClient;
        private readonly string[] _parameters = new string[ParameterCount]
            {
                "products",
                "users",
                "comments",
                "todos",
                "image",
                "carts",
                "posts",
                "quotes",
                "recipes",
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="Task4CascadingAsyncOperations"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient used to send HTTP requests.</param>
        public Task4CascadingAsyncOperations(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        /// <summary>
        /// Executes a sequence of asynchronous operations, handling exceptions that may occur during execution.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task RunCascadingOperationsAsync()
        {
            ConsoleHelpers.DisplaySubtitle("Running Cascading Async Operations");
            ConsoleHelpers.DisplayStatus("Offloading MethodA to a worker thread");

            try
            {
                int extractedKeyCount = await this.MethodC();

                ConsoleHelpers.DisplaySuccess($"Total keys extracted from HTTPBin payload: {extractedKeyCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Main Thread] failure detected during pipeline cascade: {ex.Message}");
            }
        }

        private int MethodA()
        {
            ConsoleHelpers.DisplayStatus("[MethodA] Started Method A execution on a worker thread");
            long sum = 0;
            int totalIterations = 500;

            Random random = new Random();
            for (int i = 0; i < totalIterations; i++)
            {
                sum += random.Next(1, 10);
                Thread.Sleep(1);
            }

            int parameterId = (int)(sum % ParameterCount);
            ConsoleHelpers.DisplaySuccess($"Calculated Parameter Id : {parameterId}");
            return parameterId;
        }

        private async Task<Dictionary<string, object>?> MethodB()
        {
            ConsoleHelpers.DisplayStatus("[MethodB] Started Method B execution");
            int parameterId = await Task.Run(() => this.MethodA());

            string constructedUrl = $"{WebServiceBaseUrl}{this._parameters[parameterId]}";
            ConsoleHelpers.DisplaySuccess($"[MethodB] URL constructed successfully: {constructedUrl}");
            ConsoleHelpers.DisplayStatus($"[MethodB] Dispatching non-blocking web service request to {WebServiceBaseUrl} API");

            var responsePayload = await this._httpClient.GetFromJsonAsync<Dictionary<string, object>>(constructedUrl);
            return responsePayload;
        }

        private async Task<int> MethodC()
        {
            ConsoleHelpers.DisplayStatus("[MethodC] Started Method C execution");
            Dictionary<string, object>? webServiceData = await this.MethodB();

            if (webServiceData == null)
            {
                ConsoleHelpers.DisplayFailure("[MethodC] Empty JSON network container returned.");
                return 0;
            }

            ConsoleHelpers.DisplayStatus("[MethodC] Starting to extract the keys");

            foreach (KeyValuePair<string, object> jsonElement in webServiceData)
            {
                string displayValue = jsonElement.Value is JsonElement element ? element.ToString() : jsonElement.Value?.ToString() ?? "null";
                if (displayValue.Length > 60)
                {
                    displayValue = displayValue.Substring(0, 57) + "...";
                }

                Console.WriteLine($"-> Key Found: [{jsonElement.Key}] | Extracted Element Value: {displayValue}");
            }

            return webServiceData.Count;
        }
    }
}
