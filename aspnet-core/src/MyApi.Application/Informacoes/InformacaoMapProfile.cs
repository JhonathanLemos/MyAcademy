using AutoMapper;
using MyApi.Informacoes.Dto;

namespace MyApi.Informacoes
{
    public class InformacaoMapProfile : Profile
    {
        public InformacaoMapProfile()
        {
            CreateMap<InformacaoDto, Informacao>();
            CreateMap<Informacao, InformacaoDto>();
        }
    }
}
