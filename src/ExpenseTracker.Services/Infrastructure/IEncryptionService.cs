using System;

namespace ExpenseTracker.Services.Infrastructure
{
    public interface IEncryptionService
    {
        string CreateSalt();
        string EncryptPassword(string password, string salt);
    }
}
