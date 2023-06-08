README.md

# HelloAI Project

This project is a chatbot application utilizing OpenAI's GPT-3 Language model to provide answers to user input.

## Getting Started
1.	Clone this repository to your local machine
2.	In Visual Studio, open the HelloAI.sln file
3.	In the Main function of Program.cs, pass in your OpenAI API key as a command line argument:
```
static void Main(string[] args)
{
    // ...
    var client = new ClientForOpenAI(args[0]);
    // ...
}
```
4.	Run the application and begin chatting!

## Features
- `help` - displays list of available commands
- `list-models` - displays list of available AI language models
- `set-model` - sets the active AI language model
- `get-model` - displays the active AI language model
- `set-temperature` - sets the temperature for generating responses (0.0 - 1.0)
- `get-temperature` - displays the temperature for generating responses
- `get-stats` - displays statistics about the usage of the OpenAI API
- `start-newchat` - start a new chat session
- `complete-text` - receive a completion of provided text

## License
This project is licensed under the MIT License.

