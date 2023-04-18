using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceStrategy.Factories.Interfaces
{
    public interface IGameFactory
    {
        DiceGame Create();
    }
}
