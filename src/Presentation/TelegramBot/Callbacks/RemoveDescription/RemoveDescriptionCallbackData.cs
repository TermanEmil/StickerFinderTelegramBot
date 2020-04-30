namespace TelegramBot.Callbacks.RemoveDescription
{
    public class RemoveDescriptionCallbackData : ICallbackData
    {
        public RemoveDescriptionCallbackData(int descriptionId)
        {
            DescriptionId = descriptionId;
        }

        public CallbackDataType Type => CallbackDataType.DescriptionRemoval;

        public int DescriptionId { get; }
    }
}
