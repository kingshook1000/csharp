using System;
using System.Threading.Tasks;

namespace TestException
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.aggregateexception.handle?view=netframework-4.7.2
    //https://stackoverflow.com/questions/6755541/aggregateexception-c-sharp-example
    class Program
    {
        public static void Main()
        {
            var task1 = Task.Run(() => { throw new CustomException("This exception is expected!"); });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                // Call the Handle method to handle the custom exception,
                // otherwise rethrow the exception.
                ae.Handle(ex => {
                    if (ex is CustomException)
                        Console.WriteLine(ex.Message);
                    return ex is CustomException;
                });
            }
        }
    }

    public class CustomException : Exception
    {
        public CustomException(String message) : base(message)
        { }

    }
