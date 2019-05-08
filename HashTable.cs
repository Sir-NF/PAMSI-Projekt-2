using System;
using System.Collections;
using System.Collections.Generic;

namespace Hasz {
    public class HashTable {
        private List < KeyValuePair < int, int[] >> list;
        private Random random = new Random();
        private CollisionSolveWay collisionSolveWay;

        public HashTable(int collisionSolveWay) {
            list = new List < KeyValuePair < int, int[] >> ();
            this.collisionSolveWay = (CollisionSolveWay) collisionSolveWay;
        }

        public void AddElement() {
            //Generowanie losowej liczby i sprawdzenie czy taka wartość już jest na liście
            int randomValue;
            do {
                randomValue = random.Next(1, 10000);
            }
            while (ValueExistInList(list, randomValue));


            int hash = randomValue % 11; //Tab.length = liczba pierwsza (11)
            Console.WriteLine($"Hasz: {hash}");

            if (HashExist(list, hash)) {
                Console.WriteLine($"Hasz istnieje... rozwiązywanie sposobem {collisionSolveWay}");
                switch (collisionSolveWay) {
                    case CollisionSolveWay.Linking:
                        ResolveCollisionLinking(list, hash, randomValue);
                        break;
                    case CollisionSolveWay.LinearProbing:
                        ResolveCollisionLinearProbing(list, hash, randomValue);
                        break;
                    case CollisionSolveWay.SecondaryHashing:
                        ResolveCollisionSecondaryHashing(list, hash, randomValue);
                        break;
                    default:
                        break;
                }
            } else {
                list.Add(new KeyValuePair < int, int[] > (hash, new int[1] {
                    randomValue
                }));
            }

            Console.WriteLine($"Ilość próbek {list.Count}");
        }

        public void RemoveElement(int key) {
            bool removed = false;

            for (int i = 0; i < list.Count; i++) {
                Console.WriteLine($"Próbka element nr: {i}");
                if (list[i].Key == key) {
                    list.Remove(list[i]);
                    removed = true;
                }
            }

            if (removed) {
                Console.WriteLine("Usunięto element");
            } else {
                Console.WriteLine("Nie znaleziono elementu do usunięcia po podanym kluczu.");
            }
        }

        public void PrintKey(int value) {
            KeyValuePair < bool, int > keyValuePair = FindKey(value);
            if (keyValuePair.Key == true) {
                Console.WriteLine($"Key: {keyValuePair.Value}");
            } else {
                Console.WriteLine("Nie znaleziono klucza odpowiadającego danej wartości");
            }
        }

        private KeyValuePair < bool, int > FindKey(int value) {
            foreach(var values in list) {
                Console.WriteLine($"Próbka nr: {list.IndexOf(values) + 1}");
                foreach(int item in values.Value) {
                    if (item == value) {
                        return new KeyValuePair < bool, int > (true, values.Key);
                    }
                }
            }

            return new KeyValuePair < bool, int > (false, 0);
        }

        public void PrintValue(int key) {
            int[] values = ReturnValue(key);
            if (values != null) {
                foreach(int item in values) {
                    Console.WriteLine(item);
                }
            } else {
                Console.WriteLine("Nie znaleziono wartości odpowiadającej danemu kluczowi");
            }
        }

        private int[] ReturnValue(int key) {
            foreach(var item in list) {
                if (item.Key == key) {
                    return item.Value;
                }
            }

            return null;
        }

        private void ResolveCollisionLinking(List < KeyValuePair < int, int[] >> list, int hashValue, int randomValue) {
            Console.WriteLine("Linkowanie rozpoczęte");

            KeyValuePair < int, int[] > pair = new KeyValuePair < int, int[] > ();
            foreach(var item in list) {
                if (item.Key == hashValue) {
                    pair = item;
                    break;
                }
            }


            list.Remove(pair);

            int[] values = new int[pair.Value.Length + 1];
            for (int i = 0; i < pair.Value.Length; i++) {
                values[i] = pair.Value[i];
            }
            values[values.Length - 1] = randomValue;

            list.Add(new KeyValuePair < int, int[] > (hashValue, values));
        }

        private void ResolveCollisionLinearProbing(List < KeyValuePair < int, int[] >> list, int hashValue, int randomValue) {
            Console.WriteLine("Próbkowanie liniowe rozpoczęte");
            do {
                hashValue++;
                Console.WriteLine($"Próbka: {hashValue}");
            } while (HashExist(list, hashValue));

            list.Add(new KeyValuePair < int, int[] > (hashValue, new int[1] {
                randomValue
            }));
        }

        private void ResolveCollisionSecondaryHashing(List < KeyValuePair < int, int[] >> list, int hashValue, int randomValue) {
            int secondaryHashing = (int) MathF.Abs((GenerateRandomFirstNumber(1, 100) - (randomValue % GenerateRandomFirstNumber(1, 100))));
            Console.WriteLine($"Wartość podwójnego haszowania:{secondaryHashing}");
            if (HashExist(list, secondaryHashing)) {
                ResolveCollisionSecondaryHashing(list, secondaryHashing, randomValue);
            } else {
                list.Add(new KeyValuePair < int, int[] > (secondaryHashing, new int[1] {
                    randomValue
                }));
            }
        }

        public void PrintAllElementsAndIndexes() {
            Console.WriteLine($"List length {list.Count}");
            foreach(var item in list) {
                Console.WriteLine($"Hash: {item.Key} Wartość(i): {PrintAllElementsFromIntTab(item.Value)}");
            }
        }

        private string PrintAllElementsFromIntTab(int[] tab) {
            string elements = string.Empty;
            foreach(int item in tab) {
                elements += $" {item}";
            }
            return elements;
        }

        private int GenerateRandomFirstNumber(int minRange, int tabLength) {
            int number;
            do {
                number = random.Next(minRange, tabLength);
            } while (!IsFirstNumber(number));

            return number;
        }

        private bool ValueExistInList(List < KeyValuePair < int, int[] >> list, int value) {
            foreach(var tab in list) {
                foreach(int item in tab.Value) {
                    if (item == value) {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HashExist(List < KeyValuePair < int, int[] >> list, int key) {
            foreach(var item in list) {
                if (item.Key == key) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsFirstNumber(int number) {
            if (number < 2) {
                return false;
            }

            for (int i = 2; i * i <= number; i++) {
                if (number % i == 0) {
                    return false;
                }
            }

            return true;
        }

        public static bool CollisionSolveWayValid(int number) {
            if (number < 1 || number > 3) {
                return false;
            } else {
                return true;
            }
        }
    }
}