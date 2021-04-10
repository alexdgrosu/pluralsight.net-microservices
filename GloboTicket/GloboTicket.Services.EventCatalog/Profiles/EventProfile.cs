using AutoMapper;

namespace GloboTicket.Services.EventCatalog.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Entities.Event, Models.Event>()
                .ReverseMap();
        }
    }
}
