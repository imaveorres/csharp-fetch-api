using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
            await ProcesRepositories();
        }

        /*This code:
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
        private static async Task ProcesRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
            );
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.Write(msg);
        }
    }
}
