using System;
using System.Data.SqlTypes;

class Program
{
    static void Main(string[] args)
    {
        int money = 1000;
        bool run = true;
        int handValue = 0;
        int dealerHandValue = 0;
        Random randomCard = new Random();
        List<string> cards = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        List<int> cardValues = new List<int> {11,2,3,4,5,6,7,8,9,10,10,10,10};
        List<string> playerHand = new List<string> {cards[randomCard.Next(cards.Count)], cards[randomCard.Next(cards.Count)]};
        List<string> dealerHand = new List<string> {cards[randomCard.Next(cards.Count)], cards[randomCard.Next(cards.Count)]};
        string yourHandCards =  $"Your cards: {playerHand[0]}, {playerHand[1]}";
        string dealerHandCards = $"Dealer's cards: {dealerHand[0]}, {dealerHand[1]}";
        foreach (string card in playerHand)
        {
            int cardIndex = cards.IndexOf(card);
            handValue += cardValues[cardIndex];
            int aceCount = playerHand.FindAll(card => card == "A").Count;
            while (handValue > 21 && aceCount > 0)
            {
                handValue -= 10;
                aceCount--;
            }
        }
        foreach (string card in dealerHand)
        {
            int cardIndex = cards.IndexOf(card);
            dealerHandValue += cardValues[cardIndex];
            int aceCount1 = playerHand.FindAll(card => card == "A").Count;
            while (handValue > 21 && aceCount1 > 0)
            {
                handValue -= 10;
                aceCount1--;
            }
        }

        if (dealerHandValue == 21)
        {
            Console.WriteLine("BlackJack for the dealer! You lost!\n");
            Console.WriteLine($"{yourHandCards}\n");
            Console.WriteLine($"{dealerHandCards}\n");
            Console.WriteLine($"Your cards value: {handValue}");
            Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
            blackjackMoney(false, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
            Thread.Sleep(3000);
            Console.Clear();
        }
        else if (handValue == 21)
        {
            Console.WriteLine("BlackJack for the player! You won!\n");
            Console.WriteLine($"{yourHandCards}\n");
            Console.WriteLine($"{dealerHandCards}\n");
            Console.WriteLine($"Your cards value: {handValue}");
            Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
            blackjackMoney(true, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
            Thread.Sleep(3000);
            Console.Clear();
        }

        while (run)
        {

            Console.WriteLine($"{yourHandCards}");
            Console.WriteLine($"Your cards value: {handValue}\n");
            Console.WriteLine($"{dealerHandCards}");
            Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
            Console.WriteLine($"Your money: {money}\n");
            Console.WriteLine("1. Hit");
            Console.WriteLine("2. Stand");
            Console.WriteLine("3. Exit\n");
            Console.Write("Please choose an option (you can also write the word): ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                option = "hit";
            } else if (option == "2")
            {
                option = "stand";
            } else if (option == "3")
            {
                option = "exit";
            }

            switch (option)
            {
                case "hit":
                    Console.Clear();
                    string drawPlayer = cards[randomCard.Next(cards.Count)];
                    yourHandCards += $", {drawPlayer}";
                    int newCardIndex = cards.IndexOf(drawPlayer);
                    handValue += cardValues[newCardIndex];
                    int aceCount = playerHand.FindAll(card => card == "A").Count;
                    while (handValue > 21 && aceCount > 0)
                    {
                        handValue -= 10;
                        aceCount--;
                    }
                    if (handValue > 21)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"Bust with total value of {handValue}! You lost!");
                        blackjackMoney(false, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
                        Thread.Sleep(3000);
                        Console.Clear();
                    } else if (handValue == 21)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine($"Your money: {money}");
                        Console.WriteLine("You won!");
                        blackjackMoney(true, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
                        Thread.Sleep(3000);
                        Console.Clear();
                    }
                    break;
                case "stand":
                    Console.Clear();
                    while (true)
                    {
                        if (dealerHandValue >= 17)
                        {
                            break;
                        } else
                        {
                            string drawDealer = cards[randomCard.Next(cards.Count)];
                            dealerHandCards += $", {drawDealer}";
                            int newCardIndex1 = cards.IndexOf(drawDealer);
                            dealerHandValue += cardValues[newCardIndex1];
                            int aceCount1 = dealerHand.FindAll(card => card == "A").Count;
                            while (dealerHandValue > 21 && aceCount1 > 0)
                            {
                                dealerHandValue -= 10;
                                aceCount1--;
                            }
                            if (dealerHandValue > 21)
                            {           
                                Console.Clear();
                                Console.WriteLine($"{dealerHandCards}\n");
                                Console.WriteLine($"Bust for the dealer with total value of {dealerHandValue}! You won!");
                                Thread.Sleep(3000);
                                blackjackMoney(true, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
                                Console.Clear();
                            }
                            break;
                        }
                    }

                    if (dealerHandValue < handValue)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine("You won!");
                        Thread.Sleep(3000);
                        blackjackMoney(true, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
                        Console.Clear();
                    } else if (dealerHandValue > handValue)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine("You lost!");
                        blackjackMoney(false, ref money, ref playerHand, ref dealerHand, ref handValue, ref dealerHandValue, ref yourHandCards, ref dealerHandCards, ref randomCard, ref cards, ref cardValues);
                        Thread.Sleep(3000);
                        Console.Clear();
                    } else if (dealerHandValue == handValue)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine("It's a draw!");
                        Thread.Sleep(3000);
                        Console.Clear();
                    }
                    break;
                case "exit":
                    Console.Clear();
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(1500);
                    run = false;
                    break;
            }
        }

    }
    static void blackjackMoney(bool whoWin, ref int money, ref List<string> playerHand, ref List<string> dealerHand, ref int handValue, ref int dealerHandValue, ref string yourHandCards, ref string dealerHandCards, ref Random randomCard, ref List<string> cards, ref List<int> cardValues)
    {
        if (whoWin == true)
        {
            money += 500;
            playerHand = new List<string> { cards[randomCard.Next(cards.Count)], cards[randomCard.Next(cards.Count)] };
            dealerHand = new List<string> { cards[randomCard.Next(cards.Count)], cards[randomCard.Next(cards.Count)] };
            yourHandCards = $"Your cards: {playerHand[0]}, {playerHand[1]}";
            dealerHandCards = $"Dealer's cards: {dealerHand[0]}, {dealerHand[1]}";
            dealerHandValue = 0;
            handValue = 0;
            foreach (string card in playerHand)
            {
                int cardIndex = cards.IndexOf(card);
                handValue += cardValues[cardIndex];
                int aceCount = playerHand.FindAll(card => card == "A").Count;
                while (handValue > 21 && aceCount > 0)
                {
                    handValue -= 10;
                    aceCount--;
                }
            }
            foreach (string card in dealerHand)
            {
                int cardIndex = cards.IndexOf(card);
                dealerHandValue += cardValues[cardIndex];
                int aceCount1 = playerHand.FindAll(card => card == "A").Count;
                while (handValue > 21 && aceCount1 > 0)
                {
                    handValue -= 10;
                    aceCount1--;
                }
            }
        }
        else if (whoWin == false) 
        {
            money -= 500;
            playerHand = new List<string> { cards[randomCard.Next(cards.Count)], cards[randomCard.Next(cards.Count)] };
            dealerHand = new List<string> { cards[randomCard.Next(cards.Count)], cards[randomCard.Next(cards.Count)] };
            yourHandCards = $"Your cards: {playerHand[0]}, {playerHand[1]}";
            dealerHandCards = $"Dealer's cards: {dealerHand[0]}, {dealerHand[1]}";
            dealerHandValue = 0;
            handValue = 0;
            foreach (string card in playerHand)
            {
                int cardIndex = cards.IndexOf(card);
                handValue += cardValues[cardIndex];
                int aceCount = playerHand.FindAll(card => card == "A").Count;
                while (handValue > 21 && aceCount > 0)
                {
                    handValue -= 10;
                    aceCount--;
                }
            }
            foreach (string card in dealerHand)
            {
                int cardIndex = cards.IndexOf(card);
                dealerHandValue += cardValues[cardIndex];
                int aceCount1 = playerHand.FindAll(card => card == "A").Count;
                while (handValue > 21 && aceCount1 > 0)
                {
                    handValue -= 10;
                    aceCount1--;
                }
            }
        }
    }
}