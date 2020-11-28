using MultiChain.Handlers;
using MultiChain.Interface;
using MultiChain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiChain.Services
{
    public class MultiChain : IMultiChain
    {
        //TODO dla clienta wszystko gra a dla wielu dodaje poza tym o co pytam to reszte w zbiorze tzn wszystkie PL i GB , mozna by jeszcze to jakos rozdzelic ale jak dla mnie jest w pyte 
        //TODO w klasie client sa chwilowe dane, trzeba dorobic mongo i w tym miejscu powinien pobierac dane 
        //TODO osobne pliki
        public async Task<Book> Get(string language)
        {
            var userLang = new UserLanguageHandler();
            var pol = new PolishHandler();
            var eng = new EnglishHandler();

            userLang.SetNext(eng).SetNext(pol);

            var client = Client.ClientCode(userLang, language);

            return client;
        }

        public async Task<IEnumerable<Book>> GetAll(string language)
        {
            var userLang = new UserLanguageHandler();
            var pol = new PolishHandler();
            var eng = new EnglishHandler();

            userLang.SetNext(eng).SetNext(pol);

            var temp = Client.ClientCodeAll(userLang, language);

            return temp;
        }
    }
}
