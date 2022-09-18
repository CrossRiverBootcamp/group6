﻿namespace CustomerAccount.Services.Interfaces;

    public interface IEmailVerificationService
    {
        Task<bool> AddEmailVerification(string emailVerificationAddress);
        Task SendEmail(string email, string verificationCode);
        Task<bool> CheckVerification(string email, string verifiCode);
        Task<int> DeleteExpiredCodes();
    }
