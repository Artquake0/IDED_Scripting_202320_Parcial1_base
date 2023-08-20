using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    internal class TestMethods
    {
        internal enum EValueType
        {
            Two,
            Three,
            Five,
            Seven,
            Prime
        }

        internal static Stack<int> GetNextGreaterValue(Stack<int> sourceStack)
        {
            Stack<int> result = new Stack<int>();
            List<int> usedNum = new List<int>();
            List<int> reverseNum = new List<int>();
            int a = sourceStack.Count;

            for (int i = 0; i < a; i++)
            {
                int popedNum = sourceStack.Pop();
                usedNum.Add(popedNum);
            }

            for (int i = usedNum.Count - 1; i >= 0; i--)
            {
                reverseNum.Add(usedNum[i]);
            }

            int b = reverseNum.Count;

            for (int i = 1; i < b; i++)
            {
                if (reverseNum[i - 1] < reverseNum[i])
                {
                    int pushNum = reverseNum[i];

                    if (result.Count == 0)
                    {
                        pushNum = -1;
                        result.Push(pushNum);
                    }
                    else
                    {
                        result.Push(pushNum);
                    }
                }
                else if (reverseNum[i] < reverseNum[i - 1] || reverseNum[i - 1] == 0 || reverseNum[0 + b] >= -1)
                {
                    result.Push(-1);
                }
            }

            return result;
        }


        internal static Dictionary<int, EValueType> FillDictionaryFromSource(int[] sourceArr)
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();
            int NumMax = sourceArr.Max();
            int RaizMax = (int)Math.Sqrt(NumMax);
            List<int> prime = new List<int>();
            for (int i = 2; i <= RaizMax; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    prime.Add(i);
                }
            }

            foreach (int num in sourceArr)
            {
                if (num % 2 == 0)
                {
                    result[num] = EValueType.Two;
                }
                else if (num % 3 == 0)
                {
                    result[num] = EValueType.Three;
                }
                else if (num % 5 == 0)
                {
                    result[num] = EValueType.Five;
                }
                else if (num % 7 == 0)
                {
                    result[num] = EValueType.Seven;
                }
                else
                {
                    bool isPrime = true;
                    foreach (int primo in prime)
                    {
                        if (num % primo == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        result[num] = EValueType.Prime;
                    }
                }
            }

            return result;
        }

        internal static int CountDictionaryRegistriesWithValueType(Dictionary<int, EValueType> sourceDict, EValueType type)
        {
            int count = 0;

            foreach (EValueType value in sourceDict.Values)
            {
                if (value == type)
                {
                    count++;
                }
            }

            return count;
        }

        internal static Dictionary<int, EValueType> SortDictionaryRegistries(Dictionary<int, EValueType> sourceDict)
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();
            List<int> Keys = new List<int>(sourceDict.Keys);
            Keys.Sort();
            Keys.Reverse();

            foreach (int key in Keys)
            {
                result[key] = sourceDict[key];
            }

            return result;
        }

        private static bool IsPrime(int num)
        {
            if (num <= 1) return false;
            if (num <= 3) return true;

            if (num % 2 == 0 || num % 3 == 0) return false;

            for (int i = 5; i * i <= num; i += 6)
            {
                if (num % i == 0 || num % (i + 2) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        internal static Queue<Ticket>[] ClassifyTickets(List<Ticket> sourceList)
        {
            Queue<Ticket> payment = new Queue<Ticket>();
            Queue<Ticket> subscription = new Queue<Ticket>();
            Queue<Ticket> cancellation = new Queue<Ticket>();

            sourceList.Sort((t1, t2) => t1.Turn.CompareTo(t2.Turn));

            foreach (var ticket in sourceList)
            {
                switch (ticket.RequestType)
                {
                    case Ticket.ERequestType.Payment:
                        payment.Enqueue(ticket);
                        break;
                    case Ticket.ERequestType.Subscription:
                        subscription.Enqueue(ticket);
                        break;
                    case Ticket.ERequestType.Cancellation:
                        cancellation.Enqueue(ticket);
                        break;
                    default:
                        break;
                }
            }
            Queue<Ticket>[] result = { payment, subscription, cancellation };
            return result;
        }

        internal static bool AddNewTicket(Queue<Ticket> targetQueue, Ticket ticket)
        {
            if (ticket.Turn < 1 || ticket.Turn > 99)
            {
                return false;
            }

            if (targetQueue.Peek().RequestType != ticket.RequestType)
            {
                return false;
            }

            targetQueue.Enqueue(ticket);

            return true;
        }        
    }
}