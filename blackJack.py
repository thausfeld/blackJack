import blackJackGambler as bjg
import time

if __name__ == '__main__':
    # Initializing the dealer and player objects
    player = bjg.blackJackGambler(float(input("How much are you bringing to the table today?\n")))
    dealer = bjg.blackJackGambler(5000)

    anotherOne = 'Y'
    while player.amtFunds > 0 and dealer.amtFunds > 0 and anotherOne == 'Y':  # Keeps the game going while gamblers have more than 0 monies.
        bet = float(input("How much would you like to bet this round?\n"))
        player.bet = bet
        dealer.bet = bet
        while player.bet > player.amtFunds:     # When user submits to high of a bet.
            bet = float(input(f'You only have {player.amtFunds}. We are not loaning you any money, choose a bet you can backup!\n'))
            player.bet = bet
            dealer.bet = bet
    
        # Gamblers always start off with two cards to start.
        player.drawCard()
        dealer.drawCard()
        player.drawCard()
        dealer.drawCard()


        print(f'\n\nYour cards: {player.cardNames}')
        print(f'Dealers First Card: {dealer.cardNames[0]}')

        if player.blackJack == False:   # Users don't hit when they have a blackjack, they wait to see how the dealer does.
            action = input("Would you like to hit or stay (H/S)?\n").upper().strip()
        while action == "H" and player.bust == False and player.blackJack == False:
            player.drawCard()
            print(f'\n\nYour cards: {player.cardNames}')
            print(f'Dealers First Card: {dealer.cardNames[0]}')
            if player.blackJack == False and player.bust==False:
                action = input("Would you like to hit or stay (H/S)?\n").upper().strip()
        
        if player.blackJack == True:
            print(f'You got a BLACKJACK!')

        if player.bust == True:
            winner = 0
            player.fundChange(0)
            print(f'Busted!')
        else:   # Given user didn't bust, now we need to see how the dealer does.
            playerBest = max([x for x in player.handTotal if x <= 21])  # Finds the best scenario the player currently has
            winner = -1
            while dealer.bust == False and winner == -1:
                dealerBest = max(filter(lambda x: x <= 21, dealer.handTotal)) # Finds the best scenario the dealer currently has
                if dealerBest > playerBest:
                    winner = 0
                elif dealerBest == playerBest and dealerBest > 16:
                    winner = 2
                else:   # If the dealer hasn't won, tied, or busted already, they need to draw another card.
                    dealer.drawCard()
                    if dealer.bust == True:
                        winner = 1
          
        # Determine affects on gamblers funds after the round, report out, and determine if there is enough monies to continue play!
        if winner == 0:
            print("\n\nThe Dealer won.")
            player.fundChange(0)
            dealer.fundChange(1)
        elif winner == 1:
            print("\n\nYou won this round!")
            dealer.fundChange(0)
            player.fundChange(1)
        elif winner == 2:
            print("\n\nIts a push!")

        print(f'Your Cards: {player.cardNames}')
        print(f'Dealers Cards: {dealer.cardNames}')
        if player.amtFunds > 0 and dealer.amtFunds > 0:
            print(f'\nYour Balance is now: {player.amtFunds}')
            anotherOne = input("Would you like to play another round (Y/N)?\n").upper().strip()
            if anotherOne == 'Y':
                dealer.newRound()
                player.newRound()
            else:
                print(f'You are walking away with {player.amtFunds} monies. See you next time!\n\n\n')
        else:
            print(f'\nYou lost all your money! Game over.\n\n\n')

time.sleep(20)
