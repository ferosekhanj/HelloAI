namespace HelloAI
{
    class ChatRequest
    {
        public string model { get; set; } = "gpt-3.5-turbo";
        public double temperature { get; set; } = 0.7;
        public List<Message> messages { get; set; } = new List<Message>();

        public void AddMessage(string role, string message) => messages.Add(new Message(role, message));
        public void ClearMessage()=> messages.Clear();
    }
}
