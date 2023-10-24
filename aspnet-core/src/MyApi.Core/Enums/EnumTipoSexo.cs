using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Enums
{
    public enum EnumTipoSexo
    {
        [Description("Masculino")]
        Masculino = 1,

        [Description("Feminino")]
        Feminino = 2,
    }
}
