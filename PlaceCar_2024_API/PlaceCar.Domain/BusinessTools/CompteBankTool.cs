using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessTools
{
    public static class CompteBankTool
    {
        public static string GenereUniqueNumCompte()
        {
            Random random = new Random();
            StringBuilder numCompte = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                int randomNumber = random.Next(0, 10);
                numCompte.Append(randomNumber);
            }
            return numCompte.ToString();
        }
    }
}
