// <copyright file="TresureHuntRound.cs" company="MLK AH CompSci Solutions">
// Copyright (c) MLK AH CompSci Solutions. All rights reserved.
// </copyright>

namespace TresureHunt
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the data for each card.
    /// </summary>
    internal class TresureHuntRound
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TresureHuntRound"/> class.
        /// </summary>
        /// <param name="height">Cells on the y axis of the table.</param>
        /// <param name="width">Cells on the x axis of the table.</param>
        /// <param name="maxSquaresPerPlayer">The maximum amount of cells one player can be marked down for.</param>
        internal TresureHuntRound(int height, int width, int maxSquaresPerPlayer)
        {
            this.MaxSquaresPerPlayer = maxSquaresPerPlayer;
            this.Card = new Player[width][];
            for (int i = 0; i < this.Card.Length; i++)
            {
                this.Card[i] = new Player[height];
            }
        }

        /// <summary>
        /// Gets the table storing where each player has marked them self down for.
        /// </summary>
        internal Player[][] Card { get; }

        /// <summary>
        /// Gets the length of the arrays of cells inside of the array of arrays of cells.
        /// </summary>
        internal int Height
        {
            get
            {
                if (this.Card.Length == 0)
                {
                    throw new InvalidOperationException();
                }

                return this.Card[0].Length;
            }
        }

        /// <summary>
        /// Gets or sets the number of cells one player can be in.
        /// </summary>
        internal int MaxSquaresPerPlayer { get; set; }

        /// <summary>
        /// Gets the Length of the array of arrays of cells.
        /// </summary>
        internal int Width { get => this.Card.Length; }

        /// <summary>
        /// Gets the winner of this round.
        /// </summary>
        internal Player Winner
        {
            get
            {
                Player winner = null;
                int width = this.Width;
                int height = this.Height;
                while (winner is null)
                {
                    Random random = new Random();
                    winner = this.Card[random.Next(width)][random.Next(height)];
                }

                return winner;
            }
        }

        /// <summary>
        /// Gets the max string width of all the players on the card.
        /// </summary>
        private int MaxPersonWidth
        {
            get
            {
                int maxWidth = 0;
                int tempPlayerWidth;
                foreach (Player player in this.PlayerList)
                {
                    tempPlayerWidth = player.ToString().Length;
                    maxWidth = maxWidth < tempPlayerWidth ? tempPlayerWidth : maxWidth;
                }

                return maxWidth;
            }
        }

        /// <summary>
        /// Gets the distinct players in this current round.
        /// </summary>
        private IEnumerable<Player> PlayerList
        {
            get
            {
                Stack<Player> players = new Stack<Player>();
                foreach (Player[] playersInCard in this.Card)
                {
                    foreach (Player player in playersInCard)
                    {
                        if (!players.Contains(player) & player is not null)
                        {
                            players.Push(player);
                        }
                    }
                }

                return players;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            int tableWidth = this.Width;
            int tableHeight = this.Height;
            int maxCellWidth = this.MaxPersonWidth;
            string output = "|   ";
            for (int i = 0; i < this.Width; i++)
            {
                output += "| " + (char)(97 + i);
                for (int j = 0; j < maxCellWidth + 1; j++)
                {
                    output += " ";
                }
            }

            output += $"|{Environment.NewLine}+---";
            for (int i = 0; i < tableWidth; i++)
            {
                output += "+--";
                for (int j = 0; j < maxCellWidth + 1; j++)
                {
                    output += "-";
                }
            }

            output += "+";
            for (int i = 0; i < tableHeight; i++)
            {
                output += $"{Environment.NewLine}| {i + 1} ";
                for (int j = 0; j < tableWidth; j++)
                {
                    Player currentCellPlayer = this.Card[j][i];
                    output += $"| {currentCellPlayer}";
                    for (int k = 0; k < maxCellWidth - (currentCellPlayer is null ? 0 : currentCellPlayer.ToString().Length) + 2; k++)
                    {
                        output += " ";
                    }
                }

                output += "|";
            }

            return output;
        }

        /// <summary>
        /// Slightly pre-fills the card with people, leaving a chance that the user will not be able to enter a value.
        /// </summary>
        internal void AddRandomAmountOfRandomPeopleToCard()
        {
            List<Player> players = new List<Player>(Player.GetRandomPlayers());
            Random random = new Random();
            for (int i = 0; i < this.Height * this.Width; i++)
            {
                for (int j = 0; j < random.Next(this.MaxSquaresPerPlayer) + 1; j++)
                {
                    this.TryAdd(players[random.Next(players.Count)]);
                }
            }
        }

        /// <summary>
        /// Trys to add the specified player to the specified position.
        /// </summary>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <param name="player">The player being added.</param>
        /// <returns>Returns true if player is added.</returns>
        internal bool TryAdd(int x, int y, Player player)
        {
            bool isAdded = false;
            if (this.Card[x][y] is null)
            {
                this.Card[x][y] = player;
                isAdded = true;
            }

            return isAdded;
        }

        private bool TryAdd(Player player)
        {
            Random random = new Random();
            int x = random.Next(this.Width);
            int y = random.Next(this.Height);
            bool isAdded = this.TryAdd(x, y, player);

            return isAdded;
        }
    }
}