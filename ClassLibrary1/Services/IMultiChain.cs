
using MultiChain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiChain.Interface
{
    public interface IMultiChain
    {

        Task<Book> Get (string  language);
        Task<IEnumerable<Book>> GetAll(string language);

    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request, string lang);
    }

}
