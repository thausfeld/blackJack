using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bj
{
    public class blackJackGambler
    {
        public float amtFunds = 0;
        public float bet = 0;
        public List<int> cards = new List<int>();
        public List<string> cardNames = new List<string>();
        public List<int> handTotal = new List<int>();
        public bool bust = false;
        public bool blackJack = false;
        public List<int> deck = new List<int>();


        public void deckCreation()
        {
            deck = Enumerable.Range(0, 52).ToList();
        }

        public void fundChange(int outcome)
        {
            if (outcome == 1) {
                amtFunds += bet;
            }
            else {
                amtFunds -= bet;
            }
        }

        public void drawCard()
        {
            Random random = new Random();
            if (deck.Count > 0)
            {
                //Get random card
                //Append random card to 'cards' list
                int randCard = random.Next(0, deck.Count() - 1);
                cards.Add(deck[randCard]);
                //cards.Append(deck[randCard]);
                deck.Remove(deck[randCard]);
            }
            else
            {
                deckCreation();
                int randCard = random.Next(0, deck.Count() - 1);
                cards.Add(deck[randCard]);
                //cards.Append(deck[randCard]);
                deck.Remove(deck[randCard]);
            }

            cardSum();
        }

        public void cardSum()
        {
            List<int> sumCards = new List<int>() { 0, 0 };
            cardNames.Clear();
            
            for (int c=0; c < cards.Count(); c++)
            {
                string cardName;
                int cardIdx = cards[c];
                string head = "";
                string suitName = "";
                int suitNum = ((cardIdx + 1) / 13);
                int val = (cardIdx + 1) - (suitNum * 13);

                if (suitNum <= 1)
                {
                    suitName = "Hearts";
                }
                else if(suitNum <= 2)
                {
                    suitName = "Spades";
                }
                else if(suitNum <= 3)
                {
                    suitName = "Clubs";
                }
                else
                {
                    suitName = "Diamonds";
                }

                if (val == 1)
                {
                    head = "ACE";
                }
                else if (val == 11)
                {
                    head = "KING";
                    val = 10;
                }
                else if (val == 12)
                {
                    head = "QUEEN";
                    val = 10;
                }
                else if(val == 0)
                {
                    head = "JACK";
                    val = 10;
                }

                if (head != "")
                {
                    cardName = $"{head} of {suitName}";
                }
                else
                { 
                    cardName = $"{val} of {suitName}";
                }

                cardNames.Add(cardName);
                //cardNames.Append(cardName);

                if (head == "ACE")
                {
                    sumCards[0] += 1;
                    sumCards[1] += 11;
                }
                else
                {
                    sumCards[0] += val;
                    sumCards[1] += val;
                }

            }
            handTotal = sumCards;
            busted();

        }

        public void busted()
        {
            if ((handTotal[0] == 21) || (handTotal[1] == 21))
            {
                blackJack = true;
            }
            else if ((handTotal[0] > 21) && (handTotal[1] > 21))
            {
                bust = true;
            }
        }

        public void newRound()
        {
            cards.Clear();
            cardNames.Clear();
            handTotal.Clear();
            bust = false;
            blackJack = false;
        }

        public int bestScenario()
        {
            int best = 0;
            for (int c=0; c < handTotal.Count(); c++)
            {
                if ((handTotal[c] > best) && (handTotal[c] <= 21))
                {
                    best = handTotal[c];
                }
            }
            return best;
        }
    }
}

