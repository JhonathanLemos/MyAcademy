using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyApi.Enums;

namespace MyApi.Informacoes.Dto
{
    [AutoMapFrom(typeof(Informacao))]
    public class InformacaoDto : EntityDto<long>
    {
        public EnumAnosInformacao AnosInformacao { get; set; }
        public EnumCondicionamento CondicionamentoInformacao { get; set; }
        public EnumDiasInformacao DiasInformacao { get; set; }
        public EnumMelhoresHorarios HorariosInformacao { get; set; }
        public EnumObjetivoAcademia ObjetivoInformacao { get; set; }

        public string RestricaoAlimentar { get; set; }
        public string OutraInformacao { get; set; }
        public long UserId { get; set; }
        public bool Finalizado { get; set; }
    }
}
