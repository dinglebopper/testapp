namespace TestApp
{
    public enum SenderType
    {
        Email,
        Irc
    };

    public static class SenderFactory
    {
        public static ISender Get(SenderType type)
        {
            ISender sender = null;
            switch(type)
            {
                case SenderType.Irc:
                    sender = new IrcSender();
                    break;
                case SenderType.Email:
                default:
                    sender = new EmailSender();
                    break;
            }

            return sender;
        }
    }
}
