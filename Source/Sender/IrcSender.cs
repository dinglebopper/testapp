namespace TestApp
{
    public class IrcSender : ISender
    {
        public string Send(string from, string to, string message)
        {
            if (to == "irc://freenode.net:6665/linux")
            {
                return "Success: Penguins detected!";
            }
            else if(from == "haxor")
            {
                return "Error: We're being hacked!";
            }

            return "Success: Everything went off without a hitch!";
        }
    }
}
