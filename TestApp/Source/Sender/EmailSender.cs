namespace TestApp
{
    public class EmailSender : ISender
    {
        public string Send(string from, string to, string message)
        {
            if (to == "bomb@myspacemail.net")
            {
                return "Error: The email exploded in transit";
            }
            else if (from == "haxor")
            {
                return "Error: Our email is being hacked!";
            }

            return "Success: Sent via email!";
        }
    }
}
