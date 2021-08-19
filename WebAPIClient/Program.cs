using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        /*This code:
            Changes the signature of Main by adding the async modifier
            and changing the return type to task*/

        static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories)
            {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"GitHubHomeUrl: {repo.GitHubHomeUrl}");
                Console.WriteLine($"Homepage: {repo.Homepage}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"LastPush: {repo.LastPush}");
                Console.WriteLine();
            }
        }

        /*This code: (Make HTTP requests, Refactor the code)
         Sets up HTTP headers for all request:
            - An Accept header to accept JSON responses
            - A User-Agent header. These headers are check by the
            GitHub server code and are necessary to retrieve information
            from GitHub
         Calls HttpClient.GetStringAsync(String)
            - To make a web request and retrieve the response.
            - This method start a task that makes the web request. When
            the request returns, the task reads the response stream and extracts
            the content from the stream. The body of the response is returned as a
            String, which is available when the task is completes.
         Awaits the task for the response string and prints the response
            to the console.*/

        /*Use the serializer to convert JSON into C# objects.*/

        /*The updated code replaces GetStringAsync(String) with GetStreamAsync(String).
            This serializer method uses a stream instead of a string as its source.*/

        /*The first argument to JsonSerializer.DeserializeAsync<TValue>(Stream, JsonSerializerOptions,
            CancellationToken) is an await expression. await expressions can appear almost anywhere in your code,
            even though up to now, you've only seen them as part of an assignment statement.*/

        /*The DeserializeAsync method is generic, which means you supply type arguments for what kind
            of objects should be created from the JSON text. In this example, you're deserializing to a
            List<Repository>, which is another generic object, a System.Collections.Generic.List<T>.
            The List<T> class stores a collection of objects. The type argument declares the type of
            objects stored in the List<T>. The type argument is your Repository class, because the JSON text
            represents a collection of repository objects.*/

        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
            );
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            return repositories;
        }
    }
}
