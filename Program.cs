using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Kurwa
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите выражение.\n");
            char LastOperation;
            string InitialInput = Console.ReadLine();
            string OutputResult = string.Empty;
            InitialInput = InitialInput.Replace(" ", "");
            TrivialStack<char> OperationStack = new();
            for (int i = 0; i < InitialInput.Length; i++)
            {

                if (Char.IsDigit(InitialInput[i]))
                {
                    OutputResult += InitialInput[i];
                    continue;
                }
                else if (IsOperation(InitialInput[i]))
                {
                    if (!(OperationStack.IsEmpty))
                        LastOperation = OperationStack.Peek();
                    else
                    {
                        OperationStack.Push(InitialInput[i]);
                        continue;
                    }
                    if (OperationPriority(LastOperation) < OperationPriority(InitialInput[i]))
                    {
                        OperationStack.Push(InitialInput[i]);
                        continue;
                    }
                    else
                    {
                        OutputResult += OperationStack.Pop();
                        OperationStack.Push(InitialInput[i]);
                        continue;
                    }
                }
                else if (InitialInput[i].Equals('('))
                {
                    OperationStack.Push(InitialInput[i]);
                    continue;
                }
                else if (InitialInput[i].Equals(')'))
                {
                    while (OperationStack.Peek() != '(')
                    {
                        OutputResult += OperationStack.Pop();
                    }
                    OperationStack.Pop();
                    continue;
                }
            }
            while (!(OperationStack.IsEmpty))
            {
                OutputResult += OperationStack.Pop();
            }
            Console.WriteLine("\nВыражение в ОПЗ:\n\n" + OutputResult);
            
            var numStack = new TrivialStack<double>();
            for (int i = 0; i < OutputResult.Length; i++)
            {
                char currentChar = OutputResult[i];
                if (Char.IsDigit(currentChar))
                {
                    numStack.Push(Convert.ToDouble(currentChar.ToString()));
                }
                else if (IsOperation(currentChar))
                {
                    double b = numStack.Pop();
                    double a = numStack.Pop();
                    switch (currentChar)
                    {
                        case '+':
                            numStack.Push(a + b);
                            break;
                        case '-':
                            numStack.Push(a - b);
                            break;
                        case '*':
                            numStack.Push(a * b);
                            break;
                        case '/':
                            numStack.Push(a / b);
                            break;
                    }
                }
            }
            Console.WriteLine("\nРезультат: " + numStack.Peek());
        }

        private static bool IsOperation(char c)
        {
            if (c == '+' ||
                c == '-' ||
                c == '*' ||
                c == '/')
                return true;
            else
                return false;
        }
        private static int OperationPriority(char c)
        {
            return c switch
            {
                '+' => 1,
                '-' => 1,
                '*' => 2,
                '/' => 2,
                _ => 0,
            };
        }
    }
}