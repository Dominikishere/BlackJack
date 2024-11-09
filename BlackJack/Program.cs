using System;

class Program
{
    static void Main(string[] args)
    {
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
        }
        foreach (string card in dealerHand)
        {
            int cardIndex = cards.IndexOf(card);
            dealerHandValue += cardValues[cardIndex];
        }

        if (dealerHandValue == 21)
        {
            Console.WriteLine("BlackJack for the dealer! You lost!\n");
            Console.WriteLine($"{yourHandCards}\n");
            Console.WriteLine($"{dealerHandCards}\n");
            Console.WriteLine($"Your cards value: {handValue}");
            Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
            Thread.Sleep(5000);
            run = false;
        }
        else if (handValue == 21)
        {
            Console.WriteLine("BlackJack for the player! You won!\n");
            Console.WriteLine($"{yourHandCards}\n");
            Console.WriteLine($"{dealerHandCards}\n");
            Console.WriteLine($"Your cards value: {handValue}");
            Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
            Thread.Sleep(5000);
            run = false;
        }

        while (run)
        {

            Console.WriteLine($"{yourHandCards}\n");
            Console.WriteLine($"{dealerHandCards}\n");
            Console.WriteLine($"Your cards value: {handValue}");
            Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
            Console.WriteLine("1. Hit");
            Console.WriteLine("2. Stand\n");
            Console.Write("Please choose an option (you can also write the word): ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                option = "hit";
            } else if (option == "2")
            {
                option = "stand";
            }

            switch (option)
            {
                case "hit":
                    Console.Clear();
                    string drawPlayer = cards[randomCard.Next(cards.Count)];
                    int aceCount = playerHand.FindAll(card => card == "A").Count;
                    while (handValue > 21 && aceCount > 0)
                    {
                        handValue -= 10;
                        aceCount--;
                    }
                    yourHandCards += $", {drawPlayer}";
                    int newCardIndex = cards.IndexOf(drawPlayer);
                    handValue += cardValues[newCardIndex];
                    if (handValue > 21)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"Bust with total value of {handValue}! You lost!");
                        Thread.Sleep(3000);
                        run = false;
                    } else if (handValue == 21)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine("You won!");
                        return;
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
                            int aceCount1 = dealerHand.FindAll(card => card == "A").Count;
                            while (dealerHandValue > 21 && aceCount1 > 0)
                            {
                                dealerHandValue -= 10;
                                aceCount1--;
                            }
                            dealerHandCards += $", {drawDealer}";
                            int newCardIndex1 = cards.IndexOf(drawDealer);
                            dealerHandValue += cardValues[newCardIndex1];
                            if (dealerHandValue > 21)
                            {
                                Console.Clear();
                                Console.WriteLine($"{dealerHandCards}\n");
                                Console.WriteLine($"Bust for the dealer with total value of {dealerHandValue}! You won!");
                                Thread.Sleep(3000);
                                run = false;
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
                        return;
                    } else if (dealerHandValue > handValue)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine("You lost!");
                        return;
                    } else if (dealerHandValue == handValue)
                    {
                        Console.Clear();
                        Console.WriteLine($"{yourHandCards}\n");
                        Console.WriteLine($"{dealerHandCards}\n");
                        Console.WriteLine($"Your cards value: {handValue}");
                        Console.WriteLine($"Dealer's cards value: {dealerHandValue}\n");
                        Console.WriteLine("It's a draw!");
                        return;
                    }
                    break;
            }
        }
    }
}
