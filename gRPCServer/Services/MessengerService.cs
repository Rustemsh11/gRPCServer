using Grpc.Core;
using Stream;

namespace gRPCServer.Services
{
    public class MessengerService: Messenger.MessengerBase
    {
        string[] messages = { "Привет", "Норм", "...", "Нет", "пока" };
        public override async Task DataStream(IAsyncStreamReader<Stream.Request> requestStream, IServerStreamWriter<Responce> responseStream, ServerCallContext context)
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    Console.WriteLine($"Client: {message.Content}");
                }
            });

            foreach (var message in messages)
            {
                if (!readTask.IsCompleted)
                {
                    await responseStream.WriteAsync(new Responce { Content = message });
                    Console.WriteLine(message);
                    await Task.Delay(2000);
                }
            }
        }
    }
}
