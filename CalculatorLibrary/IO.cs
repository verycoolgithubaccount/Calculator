namespace CalculatorLibrary
{
    public static class IO
    {
        /// <summary>
        /// Generates a console-based menu using the strings in options as the menu items.
        /// Automatically numbers each option starting at 1 and incrementing by 1.
        /// Reserves the number 0 for the "quit" option when withQuit is true.
        /// </summary>
        /// <param name="options">strings representing the menu options</param>
        /// <param name="withQuit">adds option 0 for "quit" when true</param>
        /// <returns>the int of the selection made by the user</returns>
        /// <exception cref="ArgumentException">
        ///     options is null
        ///     options is empty and withQuit is false
        /// </exception>
        public static int PromptForMenuSelection(IEnumerable<string> options, bool withQuit, string prompt = "Pick an option: ")
        {
            if (prompt == null) prompt = "Pick an option: ";
            int input = 0;
            bool isInvalid = true;

            do
            {
                Console.WriteLine(prompt);
                Console.WriteLine();
                int maxOptions = 0;
                foreach (string option in options)
                {
                    maxOptions++;
                    Console.WriteLine(maxOptions + ". " + option);
                }
                if (withQuit) Console.WriteLine("0. Quit");

                Console.WriteLine();
                bool isParsed = int.TryParse(PromptForInput("Option: ", false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid menu input!");
                    continue;
                }
                else if (withQuit ? input < 0 : input < 1 || input > maxOptions)
                {
                    Console.WriteLine("Input must be between " + (withQuit ? 0 : 1) + " and " + maxOptions + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects the user to enter one of two responses that will equate
        /// to a boolean value. The trueString represents the case-insensitive response that will equate to true. 
        /// The falseString acts similarly, but for a false boolean value.
        ///     <para>
        ///         Example: Assume this method is called with a trueString argument of "yes" and a falseString
        ///         argument of "no". If the user enters "YES", the method returns true. If the user enters "no",
        ///         the method returns false. All other inputs are considered invalid, the user will be informed, 
        ///         and the prompt will repeat.
        ///     </para>
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="trueString">the case-insensitive value that will evaluate to true</param>
        /// <param name="falseString">the case-insensitive value that will evaluate to false</param>
        /// <returns>the boolean result based on the user's input</returns>
        /// <exception cref="ArgumentException">
        ///     prompt, trueString, or falseString is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     trueString and falseString are case-insensitively equal
        /// </exception>
        public static bool PromptForBool(string prompt, string trueString, string falseString)
        {
            string input;

            do
            {
                input = PromptForInput(prompt, false);

                if (input.Equals(trueString, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (input.Equals(falseString, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                Console.WriteLine("Please input either " + trueString + " or " + falseString + "!");
            } while (true);
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a byte value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid byte value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static byte PromptForByte(string prompt, byte min, byte max)
        {
            byte input = 0;
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            do
            {
                bool isParsed = byte.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid byte!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a short value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid short value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static short PromptForShort(string prompt, short min, short max)
        {
            short input;
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            do
            {
                bool isParsed = short.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid short!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing an int value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid int value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static int PromptForInt(string prompt, int min, int max)
        {
            int input;
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            do
            {
                bool isParsed = int.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid int!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a long value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid long value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static long PromptForLong(string prompt, long min, long max)
        {
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            long input;
            do
            {
                bool isParsed = long.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid long!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a float value.
        /// This method loops until valid input is given.
		///
		/// <para>NOTE: For the purposes of this method, two floats are considered equal if the absolute value of their difference
		/// is less than or equal to 0.00001.</para>
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid float value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static float PromptForFloat(string prompt, float min, float max)
        {
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            float input;
            do
            {
                bool isParsed = float.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid float!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a double value.
        /// This method loops until valid input is given.
        /// 
		/// <para>NOTE: For the purposes of this method, two doubles are considered equal if the absolute value of their difference
		/// is less than or equal to 0.0000000000001.</para>
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid double value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static double PromptForDouble(string prompt, double min, double max)
        {
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            double input;
            do
            {
                bool isParsed = double.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid double!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a decimal value.
        /// This method loops until valid input is given.
        /// 
		/// <para>NOTE: For the purposes of this method, two decimals are considered equal if the absolute value of their difference
		/// is less than or equal to 0.00000000000000000000000000001.</para>
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid decimal value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static decimal PromptForDecimal(string prompt, decimal min, decimal max)
        {
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            decimal input;
            do
            {
                bool isParsed = decimal.TryParse(PromptForInput(prompt, false), out input);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid decimal!");
                    continue;
                }
                else if (input < min || input > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that allows the user to enter any response and returns the string.
        /// When allowEmpty is true, empty responses are valid. When false, responses must contain
        /// at least one character (including whitespace). Null is never a valid user input for this method.
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user.</param>
        /// <param name="allowEmpty">when true, makes empty responses valid</param>
        /// <returns>the input from the user as a string</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        /// </exception>
        public static string PromptForInput(string prompt, bool allowEmpty)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                throw new ArgumentException("Prompt cannot not be null, empty, or whitespace!");
            }

            string input;
            bool isInvalid = true;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) && !allowEmpty)
                {
                    Console.WriteLine("You must provide input!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input;
        }

        /// <summary>
        /// Generates a prompt that expects a single character input representing a char value.
        /// This method loops until valid input is given.
		///
		/// <para>NOTE: When validating user input and min/max values, this method IS case sensitive.</para>
        /// </summary>
        /// <param name="prompt">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the user's valid char value</returns>
        /// <exception cref="ArgumentException">
        ///     prompt is null
        ///     prompt is empty
        ///     prompt is just whitespace
        ///     min is greater than max
        /// </exception>
        public static char PromptForChar(string prompt, char min, char max)
        {
            char[] input;
            bool isInvalid = true;
            if (min > max)
            {
                throw new ArgumentException("Min (" + min + ") is greater than max (" + max + ") for prompt \"" + prompt + "\"");
            }

            do
            {
                input = PromptForInput(prompt, false).ToCharArray();

                if (input.GetLength(0) != 1)
                {
                    Console.WriteLine("Please input only one character!");
                    continue;
                }
                else if (input[0] < min || input[0] > max)
                {
                    Console.WriteLine("Input must be between " + min + " and " + max + "!");
                    continue;
                }

                isInvalid = false;

            } while (isInvalid);

            return input[0];
        }

        public static List<List<decimal>> PromptForMatrix(string prompt, int rowCount = -1, int columnCount = -1)
        {
            if (rowCount < 1) rowCount = CalculatorLibrary.IO.PromptForInt("How many rows? ", 1, int.MaxValue);
            if (columnCount < 1) columnCount = CalculatorLibrary.IO.PromptForInt("How many columns? ", 1, int.MaxValue);

            Console.Clear();

            List<List<decimal>> matrix = new();
            int count = 0;

            List<string> matrixText = new();

            for (int i = 0; i < rowCount; i++)
            {
                List<decimal> columns = new();
                for (int j = 0; j < columnCount; j++)
                {
                    Console.WriteLine(prompt);
                    Console.WriteLine();
                    foreach (string rowText in matrixText)
                    {
                        Console.WriteLine(rowText);
                    }

                    string promptForNextItem = "Row " + (i + 1) + ":   ";
                    if (columns.Count > 0)
                    {
                        foreach (int item in columns)
                        {
                            promptForNextItem += item + ",  ";
                        }
                    }
                    columns.Add(CalculatorLibrary.IO.PromptForDecimal(promptForNextItem, decimal.MinValue, decimal.MaxValue));
                    Console.Clear();
                }
                string row = "Row " + (matrix.Count + 1) + ":   ";
                foreach (decimal number in columns)
                {
                    row += number + ",  ";
                }
                matrixText.Add(row);

                matrix.Add(columns);
            }
            return matrix;
        }

        public static List<List<string>> PromptForStringMatrix(string prompt, int rowCount = -1, int columnCount = -1)
        {
            if (rowCount < 1) rowCount = CalculatorLibrary.IO.PromptForInt("How many rows? ", 1, int.MaxValue);
            if (columnCount < 1) columnCount = CalculatorLibrary.IO.PromptForInt("How many columns? ", 1, int.MaxValue);

            Console.Clear();

            List<List<string>> matrix = new();
            int count = 0;

            List<string> matrixText = new();

            for (int i = 0; i < rowCount; i++)
            {
                List<string> columns = new();
                for (int j = 0; j < columnCount; j++)
                {
                    Console.WriteLine(prompt);
                    Console.WriteLine();
                    foreach (string rowText in matrixText)
                    {
                        Console.WriteLine(rowText);
                    }

                    string promptForNextItem = "Row " + (i + 1) + ":   ";
                    if (columns.Count > 0)
                    {
                        foreach (string item in columns)
                        {
                            promptForNextItem += item + ",  ";
                        }
                    }
                    columns.Add(CalculatorLibrary.IO.PromptForInput(promptForNextItem, false));
                    Console.Clear();
                }
                string row = "Row " + (matrix.Count + 1) + ":   ";
                foreach (string text in columns)
                {
                    row += text + ",  ";
                }
                matrixText.Add(row);

                matrix.Add(columns);
            }
            return matrix;
        }
    }
}
