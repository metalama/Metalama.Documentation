using System;
using Metalama.Framework.RunTime;
namespace Doc.BindingByName;
[RedirectMail]
public class Newsletter
{
  public void SendWelcome() => MailOutbox.Send(recipient: "alice@example.com", subject: "Welcome");
  public void SendReport() => MailOutbox.Send(recipient: GetRecipient(), subject: CallSiteHelper.DropAfter("Report", NextPriority()));
  private static string GetRecipient()
  {
    Console.WriteLine("Evaluating the recipient.");
    return "bob@example.com";
  }
  private static int NextPriority()
  {
    Console.WriteLine("Evaluating the priority.");
    return 2;
  }
}
public static class Mailer
{
  public static void Send(string recipient, string subject, int priority = 1) => Console.WriteLine($"Sending '{subject}' to {recipient} with priority {priority}.");
}
internal class Program
{
  private static void Main()
  {
    var newsletter = new Newsletter();
    newsletter.SendWelcome();
    newsletter.SendReport();
  }
}