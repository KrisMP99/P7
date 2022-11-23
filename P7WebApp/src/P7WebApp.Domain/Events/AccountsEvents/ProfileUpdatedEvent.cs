using P7WebApp.Domain.Aggregates.AccountAggregate;
using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Events.AccountsEvents
{
    public class ProfileUpdatedEvent : DomainEventBase
    {
        public ProfileUpdatedEvent(string userID, AccountProfile profile)
        {
            UserId = userID;
            Profile = profile;
        }

        public string UserId { get; }
        public AccountProfile Profile { get; }
    }
}