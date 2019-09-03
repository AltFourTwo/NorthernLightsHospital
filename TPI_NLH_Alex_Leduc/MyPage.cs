using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TPI_NLH_Alex_Leduc
{
    public interface MyPage
    {
        void actualiser();
        void receiveTransfer(Object transfer, string objectType);
    }
}
