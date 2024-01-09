using Azure.AI.OpenAI;

//  https://platform.openai.com/api-keys
const string nonAzureOpenAIApiKey = "<open-ai-key>";

var openAI = new OpenAIClient(nonAzureOpenAIApiKey, new OpenAIClientOptions());

var completionOptions = new ChatCompletionsOptions
{
    //  When using non-Azure OpenAI, this corresponds to "model" in the request options
    //  and should use the appropriate name of the model (example: gpt-4).
    DeploymentName = "gpt-3.5-turbo",

    //  The collection of context messages associated with this chat completions request.
    //  Typical usage begins with a chat message for the System role that provides instructions
    //  for the behavior of the assistant, followed by alternating messages between the
    //  User and Assistant roles. Please note Azure.AI.OpenAI.ChatRequestMessage is the
    //  base class. According to the scenario, a derived class of the base class might
    //  need to be assigned here, or this property needs to be casted to one of the possible
    //  derived classes. The available derived classes include Azure.AI.OpenAI.ChatRequestSystemMessage,
    //  Azure.AI.OpenAI.ChatRequestUserMessage, Azure.AI.OpenAI.ChatRequestAssistantMessage,
    //  Azure.AI.OpenAI.ChatRequestToolMessage and Azure.AI.OpenAI.ChatRequestFunctionMessage.
    Messages =
    {
        // The system message represents instructions or other guidance about how the assistant should behave
        new ChatRequestSystemMessage("You'll act like you're a real estate agent."),
        new ChatRequestUserMessage(
            @"Write a description of a property with the following characteristics:
            The property is an apartment
            The sale value is $2,160,000
            It has 80m² of living area
            It has 100m² of total area
            It has 2 parking spaces
            It has 4 bedrooms"),
    },

    //  https://help.openai.com/en/articles/4936856-what-are-tokens-and-how-to-count-them
    MaxTokens = 256,

    //  Higher values will make output more random while lower values will make results
    //  more focused and deterministic. It is not recommended to modify Azure.AI.OpenAI.ChatCompletionsOptions.Temperature
    //  and Azure.AI.OpenAI.ChatCompletionsOptions.NucleusSamplingFactor for the same
    //  chat completions request as the interaction of these two settings is difficult
    //  to predict.    
    Temperature = 0.4f,

    //  Positive values will make tokens less likely to appear as their frequency increases
    //  and decrease the model's likelihood of repeating the same statements verbatim.
    FrequencyPenalty = 0,

    //  Positive values will make tokens less likely to appear when they already exist
    //  and increase the model's likelihood to output new topics.
    PresencePenalty = 0,

    //  As an example, a value of 0.1 will cause only the tokens comprising the top 10%
    //  of probability mass to be considered. It is not recommended to modify Azure.AI.OpenAI.ChatCompletionsOptions.Temperature
    //  and Azure.AI.OpenAI.ChatCompletionsOptions.NucleusSamplingFactor for the same
    //  chat completions request as the interaction of these two settings is difficult
    //  to predict. Azure.AI.OpenAI.ChatCompletionsOptions.NucleusSamplingFactor is equivalent
    //  to 'top_p' in the REST request schema.
    NucleusSamplingFactor = 0
};

var response = await openAI.GetChatCompletionsAsync(completionOptions, CancellationToken.None);
var responseMessageContent = response.Value.Choices[0].Message.Content;

Console.WriteLine(responseMessageContent);