
using System.ComponentModel;

namespace TaimeApi.Enums
{
    public enum TaimeApiErrors
    {
        /// <summary>
        /// O id informado é inválido.
        /// </summary>
        [Description("O id informado é inválido.")]
        TaimeApi_Post_400_Invalid_Id,

        /// <summary>
        /// Informe email e senha para logar.
        /// </summary>
        [Description("Informe email e senha para logar.")]
        TaimeApi_Post_400_Invalid_Login,

        /// <summary>
        /// Usuário não encontrado.
        /// </summary>
        [Description("Usuário não encontrado.")]
        TaimeApi_Post_400_User_Not_Finded
    }
}