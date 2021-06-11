namespace EmailQueue.Services
{
    public interface IApplicationConfiguration
    {
        string SmtpPassword { get; }
        string SmtpServerOne { get; }
        string SmtpServerThree { get; }
        string SmtpServerTwo { get; }
        string SmtpUserName { get; }
        string DatabaseIp { get;  }
        string Database { get; }
        int Port { get;  }
    }
}