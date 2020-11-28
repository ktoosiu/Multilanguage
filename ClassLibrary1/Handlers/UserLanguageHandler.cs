namespace MultiChain.Handlers
{
    class UserLanguageHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if ((request as string) == lang)
            {
                return request;
            }
            else
            {
                return base.Handle(request, lang);
            }
        }
    }
}
