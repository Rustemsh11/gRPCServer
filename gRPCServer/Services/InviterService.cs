using Google.Protobuf.WellKnownTypes; // пространство имен для Timestamp и Duration
using Grpc.Core;

namespace gRPCServer.Services
{
    public class InviterService: Inviter.InviterBase
    {
        public override Task<Response> Invite(Request request, ServerCallContext context)
        {
            var eventDateTime = DateTime.UtcNow.AddDays(1);

            var duration = TimeSpan.FromHours(2);

            return Task.FromResult(new Response
            {
                Invitation = $"{request.Name}, приглашаем вас посетить мероприятие",
                Start = Timestamp.FromDateTime(eventDateTime),
                Duration = Duration.FromTimeSpan(duration)
            });
        }
    }
    
}
