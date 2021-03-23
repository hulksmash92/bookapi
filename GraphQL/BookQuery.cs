using BooksApi.Services;
using GraphQL;
using GraphQL.Types;

namespace BooksApi.GraphQL
{
    public class BookQuery : ObjectGraphType<object>
    {
        public BookQuery(BooksService booksService)
        {
            Name = "BookQuery";

            Field<BookType>(
                name: "book", 
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" }
                ),
                resolve: context => booksService.Get(context.GetArgument<string>("id"))
            );

            Field<ListGraphType<BookType>>(
                name: "books",
                resolve: context => booksService.Get()
            );
        }
    }
}