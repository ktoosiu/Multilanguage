namespace MultiChain.Handlers
{
    class PolishHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if ((request as string) == "pl-PL")
            {
                return request;

            }
            else
            {
                return base.Handle(request, lang);
            }
        }
    }

    class EnglishHandler : AbstractHandler
    {
        public override object Handle(object request, string lang)
        {
            if ((request as string) == "en-GB")
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
