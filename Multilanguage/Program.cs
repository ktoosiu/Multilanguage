using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Multilanguage
{
    //https://refactoring.guru/pl/design-patterns/chain-of-responsibility/csharp/example
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

        static void MainMain(string[] args)
        {
            // The other part of the client code constructs the actual chain.
            var english = new EnglishHandler();
            var polish = new PolishHandler();
            var any = new AnyHandler();
            //firs.setNex english
            english.SetNext(polish).SetNext(any);

            // The client should be able to send a request to any handler, not
            // just the first one in the chain.
            Console.WriteLine("Chain: English > Polish > Any\n");
            Client.ClientCode(english);//jakiś język który chcemy
            Console.WriteLine();
        }
    }

    class Client
    {
        // The client code is usually suited to work with a single handler. In
        // most cases, it is not even aware that the handler is part of a chain.
        public static void ClientCode(AbstractHandler handler)
        {
            foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee" })
            {
                Console.WriteLine($"Client: Who wants a {food}?");

                var result = handler.Handle(food);

                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   {food} was left untouched.");
                }
            }
        }
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }
    //language podany
    //eng language
    //pl language
    //any languge
    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }

    class EnglishHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "english")
            {
                return "Język angielski";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class PolishHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Polish")
            {
                return "Język polski";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class AnyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Any")
            {
                return "Język any/szukam czegokolwiek w bazie :F";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}