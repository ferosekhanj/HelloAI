using RestSharp.Authenticators.OAuth2;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HelloAI
{
    internal class ClientForOpenAI
    {
        public List<string> Models { get; private set; } = new List<string>();
        public ChatRequest Messages { get; private set; } = new ChatRequest();

        public double Temperature { get => Messages.temperature; set => Messages.temperature = value; }
        public string Model { get => Messages.model; set => Messages.model = value; }
        RestClient RestClient { get; set; }
        public (int count, string prompt, string completion, string total) Stats { get; set; }

        public ClientForOpenAI(string token)
        {
            RestClient = new RestClient(new RestClientOptions("https://api.openai.com/v1")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
            });
            GetModels();
        }

        public string CompleteChat(string chatText)
        {
            Messages.AddMessage("user", chatText);
            var parameter = JsonSerializer.Serialize<ChatRequest>(Messages);
            var request = new RestRequest("chat/completions").AddJsonBody(parameter);
            var response = RestClient.Post(request);
            string answer = "";
            var content = JsonDocument.Parse(response.Content);
            var choices = content.RootElement.GetProperty("choices");
            foreach (var choice in choices.EnumerateArray())
            {
                answer = choice.GetProperty("message").GetProperty("content").ToString();
            }

            //looks like not adding the response still works.
            Messages.AddMessage("assistant", answer);

            var usage = content.RootElement.GetProperty("usage");
            Stats = (Messages.messages.Count, usage.GetProperty("prompt_tokens").ToString(), usage.GetProperty("completion_tokens").ToString(), usage.GetProperty("total_tokens").ToString());
            return answer.ToString();
        }
        StringBuilder CompletionRequest = new();
        public string Complete(string chatText)
        {
            CompletionRequest.Append(chatText);
            var completionRequest = new CompletionRequest { prompt = CompletionRequest.ToString() };
            var parameter = JsonSerializer.Serialize<CompletionRequest>(completionRequest);
            var request = new RestRequest("completions").AddJsonBody(parameter);
            var response = RestClient.Post(request);
            string answer = response.Content;
            var content = JsonDocument.Parse(response.Content);
            var choices = content.RootElement.GetProperty("choices");
            foreach (var choice in choices.EnumerateArray())
            {
                answer = choice.GetProperty("text").ToString();
            }

            var usage = content.RootElement.GetProperty("usage");
            Stats = (Messages.messages.Count, usage.GetProperty("prompt_tokens").ToString(), usage.GetProperty("completion_tokens").ToString(), usage.GetProperty("total_tokens").ToString());
            return answer.ToString();
        }

        public void NewChatContext()
        {
            Messages.ClearMessage();
            CompletionRequest.Clear();
        }

        private void GetModels()
        {
            var request = new RestRequest("models");
            var response = RestClient.Get(request);
            var content = JsonDocument.Parse(response.Content);
            var data = content.RootElement.GetProperty("data");
            foreach (var entry in data.EnumerateArray())
            {
                Models.Add(entry.GetProperty("id").ToString());
            }
        }

        
    }
}
