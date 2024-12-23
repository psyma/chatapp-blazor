using Microsoft.AspNetCore.Identity;

namespace chatapp_blazor.Components.Account;

public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public EmailConfirmationTokenProviderOptions()
    {
        Name = "EmailConfirmationTokenProvider";
        TokenLifespan = TimeSpan.FromHours(1);
    }
}