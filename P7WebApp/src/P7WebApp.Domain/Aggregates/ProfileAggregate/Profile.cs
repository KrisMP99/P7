using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;

namespace P7WebApp.Domain.Aggregates.ProfileAggregate
{
    public class Profile : EntityBase, IAggregateRoot
    {
        public Profile(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; set; }
    }
}
