using AutoMapper;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Models.dto;

namespace TalkToApiStudyTest.Helpers
{
    public class DTOMapperProfile: Profile
    {

        public DTOMapperProfile()
        {

            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Name, orig => orig.MapFrom(src =>
               src.FullName));


            CreateMap<ApplicationUser, UserDTOSemHyperlink>()
       .ForMember(dest => dest.Name, orig => orig.MapFrom(src => src.FullName));




            CreateMap<Message, MessageDTO>();

            CreateMap<MessageConnectionId, Message>();

        }
    }
}
