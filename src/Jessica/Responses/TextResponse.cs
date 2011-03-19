namespace Jessica.Responses
{
    public class TextResponse : Response
    {
        public TextResponse(string value)
        {
            Contents = GetStringContents(value);
            ContentType = "text/plain";
        }

        public TextResponse(string format, params object[] args)
        {
            Contents = GetStringContents(string.Format(format, args));
            ContentType = "text/plain";
        }
    }
}
