using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace bj 
{
    class Program
    {
        static void Main()
        {
            blackJackGambler player = new blackJackGambler();
            blackJackGambler dealer = new blackJackGambler();

            Console.WriteLine("How much are you bringing to the table today?\n");
            float funds = float.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            player.amtFunds= funds;
            dealer.amtFunds = 5000;

            string anotherOne = "Y";
            while ((player.amtFunds> 0) && (dealer.amtFunds> 0) && (anotherOne == "Y"))
            {
                Nullable<int> winner = null;
                Console.WriteLine("How much would you like to bet this round?\n");
                float bet = float.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                player.bet = bet;
                dealer.bet = bet;

                while (player.bet > player.amtFunds)
                {
                    Console.WriteLine($"You only have {player.amtFunds}. We are not loaning you any money, choose a bet you can backup!\n");
                    bet = float.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                    player.bet = bet;
                    dealer.bet = bet;
                }

                player.drawCard();
                dealer.drawCard();
                player.drawCard();
                dealer.drawCard();

                Console.WriteLine($"\n\nYour cards: {string.Join(", ", player.cardNames)}");
                Console.WriteLine($"Dealers First Card: {dealer.cardNames[0]}");

                string act = "";
                string hit = "H";
                if (player.blackJack == false)
                {
                    Console.WriteLine("Would you like to hit or stay (H/S)?\n");
                    act = Console.ReadLine().ToUpper();
                }

                int same = string.Compare(act, hit);
                while ((same == 0) && (player.bust == false) && (player.blackJack == false))
                {
                    player.drawCard();
                    Console.WriteLine($"\n\nYour cards: {string.Join(", ", player.cardNames)}");
                    Console.WriteLine($"Dealers First Card: {dealer.cardNames[0]}");

                    if ((player.blackJack == false) && (player.bust == false) ) 
                    {
                        Console.WriteLine("Would you like to hit or stay (H/S)?\n");
                        act = Console.ReadLine().ToUpper();
                        same = string.Compare(act, hit);
                    }

                }

                if (player.blackJack == true)
                {
                    Console.WriteLine("You got a BLACKJACK!");
                }
                if (player.bust == true)
                {
                    winner = 0;
                    Console.WriteLine("Busted!");
                }
                else
                {
                    int playerBest = player.bestScenario();
                    winner = -1;

                    while ((dealer.bust == false) && (winner == -1))
                    {
                        int dealerBest = dealer.bestScenario();
                        if (dealerBest > playerBest)
                        {
                            winner = 0;
                        }
                        else if ((dealerBest == playerBest) && (dealerBest > 16))
                        {
                            winner = 2;
                        }
                        else
                        {
                            dealer.drawCard();
                            if (dealer.bust == true)
                            {
                                winner = 1;
                            }
                        }
                    }
                }

                if (winner == 0)
                {
                    Console.WriteLine("\n\nThe Dealer won.");
                    player.fundChange(0);
                    dealer.fundChange(1);
                }
                else if (winner == 1)
                {
                    Console.WriteLine("\n\nYou won this round!");
                    player.fundChange(1);
                    dealer.fundChange(0);
                }
                else if (winner == 2)
                {
                    Console.WriteLine("\n\nIt's a push!");
                }

                Console.WriteLine($"Your Cards: {string.Join(", ", player.cardNames)}");
                Console.WriteLine($"Dealers Cards: {string.Join(", ", dealer.cardNames)}");

                if ((player.amtFunds > 0) && dealer.amtFunds > 0)
                {
                    Console.WriteLine($"\nYour Balance is now: {player.amtFunds}\nWould you like to play another round (Y/N)?\n");
                    anotherOne = Console.ReadLine().ToUpper();

                    if (anotherOne == "Y")
                    {
                        dealer.newRound();
                        player.newRound();
                    }
                    else
                    {
                        Console.WriteLine($"You are walking away with {player.amtFunds} monies. See you next time!\n\n\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nYou lost all your money! Game over.\n\n\n");
                }


            }


        }
    }
}
