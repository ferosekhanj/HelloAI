namespace HelloAI
{
    class Message
    {
        public string role { get; set; }
        public string content { get; set; }
        public Message(string _role, string _message)
        {
            role = _role;
            content = _message;
        }
    }
}
