import random as rand
import math

# This class has the following methods: fundChange, drawCard, cardSum, busted, and newRound
class blackJackGambler:
    def __init__(self, amtFunds):
        self.amtFunds = amtFunds
        self.bet = 0
        self.cards = []
        self.cardNames = []
        self.handTotal = []
        self.bust = False
        self.blackJack = False
        self.deck = list(range(0,51))

    def fundChange(self, outcome):  # When a round is over, the gamblers pot will either increase of decrease.
        if outcome==1:
            self.amtFunds+=self.bet
        else:
            self.amtFunds-=self.bet

    def drawCard(self): # A number from 0 to 51 is chosen at random, each number representing a card. This number (card) is then taken out of circulation until a new deck is needed
        if len(self.deck) > 0:
            randCard = rand.randint(0,len(self.deck)-1)
            self.cards.append(self.deck.pop(randCard))
        else:
            self.deck = list(range(0, 51))
            print("New Deck!")
            randCard = rand.randint(0,len(self.deck)-1)
            self.cards.append(self.deck.pop(randCard))

        self.cardSum()
    
    def cardSum(self):  # Takes the random integers in a gamblers cards and finds their associated real life card and value! It is a list in the event of a drawn ACE which can have two different value scenarios.
        sumCards = [0, 0]
        self.cardNames.clear()
        for c in self.cards:
            head = ""
            suit = math.floor(((c+1)/13))
            val = (c+1) - (suit*13)

            if suit <= 1:
                suit = "Hearts"
            elif suit <= 2:
                suit = "Spades"
            elif suit <= 3:
                suit = "Clubs"
            else:
                suit = "Diamonds"
            
            if val == 1:
                head = "ACE"
            elif val == 11:
                head = "KING"
                val = 10
            elif val == 12:
                head = "QUEEN"
                val = 10
            elif val == 0:
                head = "JACK"
                val = 10
            
            if head != "":
                cardName = f'{head} of {suit}'
            else:
                cardName = f'{val} of {suit}'
            self.cardNames.append(cardName)
            
            if head == "ACE":
                sumCards[0]+=1
                sumCards[1]+=11
            else:
                sumCards[0]+=val
                sumCards[1]+=val

        self.handTotal = sumCards
        self.busted()

    def busted(self):   # Determines if the gambler has a hand that is over 21 (in both scenarios in the event of an ACE) or not.
        if (self.handTotal[0] == 21) or (self.handTotal[1] == 21):
            self.blackJack = True
        else:
            if (self.handTotal[0] > 21) and (self.handTotal[1] > 21):
                self.bust = True

    def newRound(self):     # Initiates a new hand by taking away gamblers cards and values to set them up fresh for a new round.
        self.cards = []
        self.cardNames = []
        self.handTotal = []
        self.bust = False
        self.blackJack = False