using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Callbacks;
using TelegramBot.Callbacks.RemoveDescription;

namespace TelegramBot.BotEvents.OnCallbackQuery
{
    public class OnCallbackQueryBotEventHandler : INotificationHandler<OnCallbackQueryBotEvent>
    {
        private readonly IMediator mediator;
        private readonly ITelegramBotClient botClient;

        public OnCallbackQueryBotEventHandler(IMediator mediator, ITelegramBotClient botClient)
        {
            this.mediator = mediator;
            this.botClient = botClient;
        }

        public async Task Handle(OnCallbackQueryBotEvent notification, CancellationToken ct)
        {
            try
            {
                await InnerHandle(notification.CallbackQuery, ct);
            }
            catch (Exception e)
            {
                await mediator.Publish(new OnErrorEvent(notification.CallbackQuery.Message.Chat, e), ct);
            }
            finally
            {
                await botClient.AnswerCallbackQueryAsync(notification.CallbackQuery.Id, cancellationToken: ct);
            }
        }

        private async Task InnerHandle(CallbackQuery callback, CancellationToken ct)
        {
            dynamic dynamicCallbackData = JsonConvert.DeserializeObject(callback.Data);
            CallbackDataType callbackType = dynamicCallbackData.Type;

            if (callbackType == CallbackDataType.DescriptionRemoval)
            {
                var callbackData = JsonConvert.DeserializeObject<RemoveDescriptionCallbackData>(callback.Data);
                await mediator.Publish(new RemoveDescriptionCallback(callback, callbackData), ct);
            }
            else
            {
                throw new NotImplementedException($"Unsupported callback type {callbackType}");
            }
        }
    }
}