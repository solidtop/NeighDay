using Microsoft.AspNetCore.Identity;
using NeighDay.Server.Features.Chats.Channels;
using NeighDay.Server.Features.Users;
using System.Text.RegularExpressions;

namespace NeighDay.Server.Features.Chats
{
    public class ChatService(IChatRepository chatRepository, UserManager<ApplicationUser> userManager, ILogger<ChatService> logger) : IChatService
    {
        private readonly IChatRepository _chatRepository = chatRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ChatMessageValidator _messageValidator = new();
        private readonly ILogger<ChatService> _logger = logger;

        public async Task<IEnumerable<ChatMessageResponse>> GetChatHistory(int channelId)
        {
            var messages = await _chatRepository.FindLatest(channelId, 30);
            return messages.Select(ChatMessageResponse.Create).ToList();
        }

        public async Task<ChatMessageResponse> CreateMessage(string userId, ChatMessageRequest request)
        {
            var result = _messageValidator.Validate(request);

            if (!result.IsValid)
            {
                _logger.LogDebug("Message request resulted with errors: {errors}", result.Errors);
                throw new InvalidOperationException("Invalid message");
            }

            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
            var channel = await _chatRepository.FindChannel(request.ChannelId) ?? throw new ChannelNotFoundException(request.ChannelId);

            var message = new ChatMessage()
            {
                Text = SanitizeText(request.Text),
                Timestamp = DateTime.UtcNow,
                ChannelId = channel.Id,
                Channel = channel,
                UserId = user.Id,
                User = user,
            };

            await _chatRepository.Add(message);

            _logger.LogInformation("{message}", message.ToString());

            return ChatMessageResponse.Create(message);
        }

        public async Task<IEnumerable<ChatChannel>> GetChannels()
        {
            return await _chatRepository.FindChannels();
        }

        public async Task<ChatChannel> GetChannel(int channelId)
        {
            return await _chatRepository.FindChannel(channelId) ?? throw new ChannelNotFoundException(channelId);
        }

        private static string SanitizeText(string text)
        {
            string pattern = @"[^a-zA-Z0-9\s~`!@#$%^&*()-_=+{}|;:'<>,.?/\\[\]\""]";
            string replacement = "*";

            if (Regex.IsMatch(text, pattern))
            {
                text = Regex.Replace(text, pattern, replacement);
            }

            return text;
        }
    }
}
