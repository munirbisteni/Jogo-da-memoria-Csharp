using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace WFJogoMemoria
{
    class metodosJogo
    {
        // construtor
        public metodosJogo()
        {

        }
        //métodos GETTERS

        //métodos SETTERS

        //Métodos Funcionais
        public bool ChecagemPares(int[] t)
        {
            if (t[0] == t[1]) { return true; } else { return false; }
        }
    }
}
