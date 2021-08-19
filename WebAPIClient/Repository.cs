using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WebAPIClient
{
    /*The code: (Deserialize the JSON Result, Configure deserialization, Deserialize more properties, Add a date property) 
        The preceding code defines a class to represent the JSON object
        returned from the GitHub API. Using this class to display a list of repository names.*/

    /*The Uri and int types have built-in functionality to convert to and from string representation.
        No extra code is needed to deserialize from JSON string format to those target types.
        If the JSON packet contains data that doesn't convert to a target type, the serialization
        action throws an exception.*/

    /*The LastPush property is defined using an expression-bodied member for the get accessor.
        There's no set accessor. Omitting the set accessor is one way to define a read-only property in C#.
        (Yes, you can create write-only properties in C#, but their value is limited.)*/

    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [JsonPropertyName("homepage")]
        public Uri Homepage { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime LastPushUtc { get; set; }
        public DateTime LastPush => LastPushUtc.ToLocalTime();
    }
}
