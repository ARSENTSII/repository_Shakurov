using System;

delegate void NotificationMessage(string newNotification);
delegate void NotificationCall(int newNotification);
delegate void NotificationEmail(string newNotification);

class Notification
{
    public event NotificationMessage MessageReceived;
    private string message;

    public event NotificationCall CallReceived;
    private int call;

    public event NotificationEmail EmailReceived;
    private string email;

    public void SetMessage(string newMessage)
    {
        message = newMessage;
        if (MessageReceived != null)
        {
            MessageReceived(message);
        }
    }

    public void SetCall(int newCall)
    {
        call = newCall;
        if (CallReceived != null)
        {
            CallReceived(call);
        }
    }

    public void SetEmail(string newEmail)
    {
        email = newEmail;
        if (EmailReceived != null)
        {
            EmailReceived(email);
        }
    }
}

class Program
{
    static void OnMessageReceived(string message)
    {
        Console.WriteLine($"[Обработчик] Новое сообщение: {message}");
    }

    static void OnCallReceived(int call)
    {
        Console.WriteLine($"[Обработчик] Звонок от номера: {call}");
    }

    static void OnEmailReceived(string email)
    {
        Console.WriteLine($"[Обработчик] Письмо: {email}");
    }

    static void Main()
    {
        Notification notification = new Notification();

        notification.MessageReceived += OnMessageReceived;
        notification.CallReceived += OnCallReceived;
        notification.EmailReceived += OnEmailReceived;

        notification.SetMessage("Привет!");
        notification.SetCall(123456789);
        notification.SetEmail("Важная встреча");
    }
}