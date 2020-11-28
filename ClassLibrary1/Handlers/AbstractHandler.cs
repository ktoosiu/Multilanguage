using MultiChain.Interface;

namespace MultiChain.Handlers
{
    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual object Handle(object request, string lang)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request, lang);
            }
            else
            {
                return null;
            }
        }
    }
}
