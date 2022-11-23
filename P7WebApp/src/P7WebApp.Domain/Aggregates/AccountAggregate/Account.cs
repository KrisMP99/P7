using Microsoft.IdentityModel.Tokens;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using P7WebApp.Domain.Events.AccountsEvents;
using P7WebApp.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace P7WebApp.Domain.Aggregates.AccountAggregate
{
    public class Account : EntityBase, IAggregateRoot
    {
        public Account(string userId, string username, AccountProfile profile)
        {
            UserId = userId;
            Username = username;
            Profile = profile;
        }

        public string UserId { get; }
        public string Username { get; }
        public AccountProfile Profile { get; private set; }
        public string Token { get; private set; }

        public void EditProfileInformation(AccountProfile profile)
        {
            try
            {
                if (profile is null || Profile.Equals(profile))
                {
                    throw new AccountException("Unable to edit profile given invalid informations");
                }
                else
                {
                    Profile = profile;
                    
                    var profileUpdatedEvent = new ProfileUpdatedEvent(UserId, Profile);
                    
                    this.RegisterDomainEvent(profileUpdatedEvent);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditPassword(string password)
        {
            throw new NotImplementedException();
        }

        public void SetToken(string issuer, string audience, int expires, byte[] secret)
        {
            try
            {
                if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || expires == 0 || secret is null)
                {
                    throw new AccountException("Was given invalid date for creation of token");
                }
                else
                {
                    var handler = new JwtSecurityTokenHandler();
                    var descriptor = new SecurityTokenDescriptor
                    {
                        Issuer = issuer,
                        Audience = audience,
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserId", this.UserId),
                            new Claim("FirstName", $"{this.Profile.FirstName}"),
                            new Claim("LastName", $"{this.Profile.LastName}"),
                            new Claim("Username", this.Username),
                            new Claim(ClaimTypes.Email, this.Profile.Email),
                            new Claim(ClaimTypes.NameIdentifier, this.UserId)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(expires),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var jwtToken = handler.CreateToken(descriptor);
                    var writtenToken = handler.WriteToken(jwtToken);

                    this.Token = writtenToken;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}