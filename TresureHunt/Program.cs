// <copyright file="Program.cs" company="MLK AH CompSci Solutions">
// Copyright (c) MLK AH CompSci Solutions. All rights reserved.
// </copyright>

namespace TresureHunt
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class initliased by the framework.
    /// </summary>
    internal class Program
    {
        private const int TRESUREHUNTCARDHEIGHT = 7;
        private const int TRESUREHUNTCARDWIDTH = 8;
        private const int MAXSQUARESPERPLAYER = 2;

        private static void Main()
        {
            TresureHuntRound tresureHuntRound = new TresureHuntRound(TRESUREHUNTCARDHEIGHT, TRESUREHUNTCARDWIDTH, MAXSQUARESPERPLAYER);
            tresureHuntRound.AddRandomAmountOfRandomPeopleToCard();
            InteractivePlayer.PlayerInput(tresureHuntRound);
            Console.WriteLine($"the winner of this round is: {tresureHuntRound.Winner}");
        }
    }
}
