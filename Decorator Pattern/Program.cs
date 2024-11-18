public interface INotifier
{
    void Send(string message);
}

public class Notifier : INotifier
{
    private readonly string _email;

    public Notifier(string email)
    {
        _email = email;
    }

    public void Send(string message)
    {
        // Логика отправки email
        Console.WriteLine($"Написать сообщение по почте {_email}: {message}");
    }
}

public class NotifierDecorator : INotifier
{
    protected INotifier _wrappedNotifier;

    public NotifierDecorator(INotifier wrappedNotifier)
    {
        _wrappedNotifier = wrappedNotifier;
    }

    public virtual void Send(string message)
    {
        // Передаем вызов обернутому объекту
        _wrappedNotifier.Send(message);
    }
}

public class SmsNotifier : NotifierDecorator
{
    public SmsNotifier(INotifier wrappedNotifier) : base(wrappedNotifier)
    {
    }

    public override void Send(string message)
    {
        // Логика отправки SMS
        Console.WriteLine($"Написать по SMS: {message}");
        // Затем передаем сообщение дальше
        base.Send(message);
    }
}

public class FacebookNotifier : NotifierDecorator
{
    public FacebookNotifier(INotifier wrappedNotifier) : base(wrappedNotifier)
    {
    }

    public override void Send(string message)
    {
        // Логика отправки через Facebook
        Console.WriteLine($"Написать по Facebook: {message}");
        // Затем передаем сообщение дальше
        base.Send(message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Базовый Notifier (отправка email)
        INotifier emailNotifier = new Notifier("manager@gmail.com");

        // Оборачиваем его в SMS-отправку
        INotifier smsNotifier = new SmsNotifier(emailNotifier);

        // Оборачиваем его в Facebook-отправку
        INotifier facebookNotifier = new FacebookNotifier(smsNotifier);

        // Теперь мы можем отправлять сообщения через все каналы
        facebookNotifier.Send("Здравствуйте, всё готово");
    }
}
