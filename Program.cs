using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace HelloAI
{

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, AI! Start chatting at the prompt.");
            if(args.Length == 0)
            {
                Console.WriteLine("Usage: HelloAI <openapi key>");
                return;
            }
            var client = new ClientForOpenAI(args[0]);
            StringBuilder text = new();
            while (true)
            {
                Console.Write(">");
                string question = Console.ReadLine();
                if (question != "quit")
                {
                    switch (question)
                    {
                        case "help":
                            Console.WriteLine("list-models,set-model,get-model,set-temperature,get-temperature,get-stats,start-newchat,complete-text");
                            break;
                        case "list-models":
                            int i = 0;
                            foreach (string m in client.Models)
                            {
                                Console.WriteLine($"{i++}.{m},");
                            }
                            break;
                        case "set-model":
                            string model = Console.ReadLine();
                            client.Model = model;
                            break;
                        case "get-model":
                            Console.WriteLine($"Current model => {client.Model}");
                            break;
                        case "set-temperature":
                            double temp = Convert.ToDouble(Console.ReadLine());
                            client.Temperature = temp;
                            break;
                        case "get-temperature":
                            Console.WriteLine($"Temperature {client.Temperature}");
                            break;
                        case "get-stats":
                            Console.WriteLine($"{client.Stats}");
                            break;
                        case "start-newchat":
                            client.NewChatContext();
                            Console.WriteLine("New chat started now.");
                            break;
                        case "start-newcompletion":
                            text.Clear();
                            break;
                        case "complete-text":
                            string oneline = Console.ReadLine();
                            while (oneline != ".") 
                            {
                                text.Append(oneline);
                                oneline = Console.ReadLine();
                            }
                            string completion = client.Complete(text.ToString());
                            Console.WriteLine(completion);
                            break;
                        default:
                            string answer = client.CompleteChat(question);
                            Console.WriteLine(answer);
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}