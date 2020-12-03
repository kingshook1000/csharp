using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace Iterator
{
    public enum SuitDefinition
    {
        SPADE = 0,
        HEART,
        DIAMOND,
        CLUB
    }
    public class Card
    {

        public int FaceValue;
        public SuitDefinition Suit;

        public Card(int faceValue, SuitDefinition suit)
        {
            this.FaceValue = faceValue;
            this.Suit = suit;
        }
    }

    public class CardDeck
    {
        const int TOTALCARDSINSUITE = 13;
        public List<Card> DeckOfCards = new List<Card>();
        public CardDeck()
        {
            for (int i = 1; 1 <= TOTALCARDSINSUITE; i++)
            {
                foreach (SuitDefinition suit in Enum.GetValues(typeof(SuitDefinition)))
                {
                    DeckOfCards.Add(new Card(i, suit));
                }
            }
        }
        public void Shuffle()
        {
            this.DeckOfCards.Shuffle<Card>();
        }
    }

    public static class Extensions
    {

        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            int length = list.Count;
            for (int i = length - 1; i > 0; i++)
            {
                int next = random.Next(i+1);
                T card = list[next];
                list[next] = list[i] ;
                list[i] = card;

            }


        }
    }
    
}
