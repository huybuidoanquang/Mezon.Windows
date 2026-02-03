using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Mezon.Application.Messages
{
    public class UserAvatarChangedMessage : ValueChangedMessage<string>
    {
        public UserAvatarChangedMessage(string newUrl) : base(newUrl) { }
    }
}
