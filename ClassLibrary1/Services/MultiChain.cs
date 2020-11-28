using MongoDB.Bson;
using MongoDB.Driver;
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

        public async Task populateDB()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Books");


            database.CreateCollection("books");//tworzy kolekcje w db
            //database.CreateCollection("authors");

            var booksColection = database.GetCollection<Book>("books");//odczytuje kolekcje
            //var authorsCollection = database.GetCollection<Autor>("authors");

            var tempList = new[]
            {
                new Autor
                {
                    Id=1,
                    FirstName="Kamil",
                    LastName="Pajak",
                    Books=new[]
                    {

                          new Book
                        {
                            Title="test1",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        },
                           new Book
                        {
                            Title="test2",
                            ShortDescription="dunski opis",
                            LanguageCode="da-DK"
                        },

                         new Book
                        {
                            Title="test4",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        }
                    }

                },
                  new Autor
                {
                    Id=2,
                    FirstName="Bartosz",
                    LastName="Jarosz",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test5",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test6",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                          new Book
                        {
                            Title="test7",
                            ShortDescription="angielski opis",
                            LanguageCode="en-GB"
                        }
                    }
                },
                   new Autor
                {
                    Id=3,
                    FirstName="tt",
                    LastName="tt",
                    Books=new[]
                    {
                        new Book
                        {
                            Title="test8",
                            ShortDescription="portugalski opisz",
                            LanguageCode="pt-BR"
                        },
                         new Book
                        {
                            Title="test9",
                            ShortDescription="Norweski ospi",
                            LanguageCode="nb-NO"
                        },
                         new Book
                        {
                            Title="test3",
                            ShortDescription="polski opisz",
                            LanguageCode="pl-PL"
                        }
                                             }
                                    },
                               };
            var resultList = new List<Book>();
            //authorsCol.InsertMany(tempList);
            foreach (var item in tempList)
            {
                foreach (var bookTemp in item.Books)
                {
                    booksColection.InsertOne(bookTemp);//zapisuje do kolekcji

                }
            }

        }

        public async Task<IEnumerable<Book>> GetAll(string language)
        {
            _ = populateDB();

            var userLang = new UserLanguageHandler();
            var pol = new PolishHandler();
            var eng = new EnglishHandler();

            userLang.SetNext(eng).SetNext(pol);

            var temp = Client.ClientCodeAll(userLang, language);
            

            return temp;
        }
    }
}
