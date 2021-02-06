using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.Helpers
{ 
    /// <summary>
    /// Helper Class for managing Dice roll
    /// </summary>
    class DiceHelper
    {
        // static variable for random dice value
        private static Random rnd = new Random();

        // static variable for force Roll to not be random
        public static bool ForceRollsToNotRandom = false;

        // Variable with roll value of 1
        public static int ForcedRandomValue = 1;

        /// <summary>
        /// Method use to compute dice roll
        /// </summary>
        /// <param name="rolls">integer</param>
        /// <param name="dice">integer</param>
        /// <returns>integer</returns>
        public static int RollDice(int rolls, int dice)
        {
            if (ForceRollsToNotRandom)
            {
                return rolls * ForcedRandomValue;
            }

            if (rolls < 1)
            {
                return 0;
            }

            if (dice < 1)
            {
                return 0;
            }

            var myReturn = 0;

            for (var i = 0; i < rolls; i++)
            {
                myReturn += rnd.Next(1, dice + 1);
            }

            return myReturn;
        }

    }
}
