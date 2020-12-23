using App.Properties;
using System;
using System.Text;

namespace App
{
    public static class Analyzer
    {
        private static readonly int LENGTH_OF_IDENT = 8;
        private static readonly int MIN_INT = -32768;
        private static readonly int MAX_INT = 32767;

        private enum States
        {
            Start, Error, W, I1, T,
            H, Space1, Identifier1, Comma, Space2,
            Dot1, Identifier2, Identifier3, Colon, ClosedSquareBracket1,
            D1, O1, Space3, OpenSquareBracket1, Identifier4,
            FromOneToNine1, Minus1, Zero1, Space4, EqualsSign,
            Identifier5, SingleQuotationMark1, Space6, OpenSquareBracket2, SingleQuotationMark2,
            AnySymbol, Dot2, FromOneToNine2, Minus2, Zero2,
            E, V, D2, M, Identifier6,
            FromOneToNine3, Minus3, Zero3, Numbers, I2,
            O2, ClosedSquareBracket2, Space5, Zero4, FromOneToNine4,
            PlusOrMinus, Finish
        };

        public static bool Analyze(
            string str,
            out int currPos,
            out StringBuilder sbForIdents,
            out StringBuilder sbForIntConsts,
            out StringBuilder sbForAnyConsts
            )
        {
            sbForIdents = new StringBuilder();
            sbForIntConsts = new StringBuilder();
            sbForAnyConsts = new StringBuilder();

            int len = str.Length;
            int counter;

            States state = States.Start;
            char symb;
            currPos = 0;

            int startOfIdent = 0;
            int endOfIdent = 0;
            int startOfInt = -1;
            int endOfInt = -1;

            string newLine = Environment.NewLine;

            while (state != States.Finish && state != States.Error)
            {
                symb = str[currPos];

                switch (state)
                {
                    case States.Start:
                        ByLettOfResWord(symb, out state, States.W, 'W');
                        break;

                    case States.W:
                        ByLettOfResWord(symb, out state, States.I1, 'I');
                        break;

                    case States.I1:
                        ByLettOfResWord(symb, out state, States.T, 'T');
                        break;

                    case States.T:
                        ByLettOfResWord(symb, out state, States.H, 'H');
                        break;

                    case States.H:
                        ByWhSp(symb, out state, States.Space1);
                        break;

                    case States.Space1:
                        {
                            if (ByWhSpAndIdent(symb, out state, States.Space1, States.Identifier1))
                                sbForIdents.Append(symb);
                        }
                        break;

                    case States.Identifier1:
                        {
                            ++endOfIdent;
                            if (char.IsLetterOrDigit(symb) || symb == '_')
                            {
                                state = States.Identifier1;
                                sbForIdents.Append(symb);
                            }
                            else
                            {
                                ++endOfIdent; // посколку два символа: "\r\n"
                                sbForIdents.Append(newLine);
                                CheckIdent(startOfIdent, endOfIdent, ref currPos, sbForIdents, ref state);

                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space2;
                                        break;

                                    case '.':
                                        state = States.Dot1;
                                        break;

                                    case ',':
                                        state = States.Comma;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateIdentifier1Exception(Resources.StateIdentifier1ExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.Comma:
                        {
                            if (ByWhSpAndIdent(symb, out state, States.Comma, States.Identifier1))
                            {
                                sbForIdents.Append(symb);
                                startOfIdent = ++endOfIdent;
                            }
                        }
                        break;

                    case States.Space2:
                        {
                            switch (symb)
                            {
                                case ' ':
                                    state = States.Space2;
                                    break;
                                case ',':
                                    state = States.Comma;
                                    break;
                                case 'D':
                                    state = States.D1;
                                    break;
                                default:
                                    state = States.Error;
                                    throw new StateSpace2Exception(Resources.StateSpace2ExceptionMessage);
                            }
                        }
                        break;

                    case States.Dot1:
                        {
                            if (char.IsLetter(symb) || symb == '_')
                            {
                                state = States.Identifier2;
                                sbForIdents.Append(symb);
                                startOfIdent = ++endOfIdent;
                            }
                            else
                            {
                                state = States.Error;
                                throw new WrongIdentifierFirstSymbolException(Resources.WrongIdentifierFirstSymbolExceptionMessage);
                            }
                        }
                        break;

                    case States.Identifier2:
                        {
                            endOfIdent++;
                            if (char.IsLetterOrDigit(symb) || symb == '_')
                            {
                                state = States.Identifier2;
                                sbForIdents.Append(symb);
                            }
                            else
                            {
                                ++endOfIdent;
                                sbForIdents.Append(newLine);
                                CheckIdent(startOfIdent, endOfIdent, ref currPos, sbForIdents, ref state);

                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space2;
                                        break;
                                    case ',':
                                        state = States.Comma;
                                        break;
                                    default:
                                        state = States.Error;
                                        throw new StateIdentifier2Exception(Resources.StateIdentifier2ExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.D1:
                        ByLettOfResWord(symb, out state, States.O1, 'O');
                        break;

                    case States.O1:
                        ByWhSp(symb, out state, States.Space3);
                        break;

                    case States.Space3:
                        {
                            if (ByWhSpAndIdent(symb, out state, States.Space3, States.Identifier3))
                            {
                                sbForIdents.Append(symb);
                                startOfIdent = ++endOfIdent;
                            }
                        }
                        break;

                    case States.Identifier3:
                        {
                            ++endOfIdent;
                            if (char.IsLetterOrDigit(symb) || symb == '_')
                            {
                                state = States.Identifier3;
                                sbForIdents.Append(symb);
                            }
                            else
                            {
                                ++endOfIdent;
                                sbForIdents.Append(newLine);
                                CheckIdent(startOfIdent, endOfIdent, ref currPos, sbForIdents, ref state);

                                switch (symb)
                                {
                                    case ' ':
                                        state = States.ClosedSquareBracket1;
                                        break;

                                    case ':':
                                        state = States.Colon;
                                        break;

                                    case '[':
                                        state = States.OpenSquareBracket1;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateIdentifier3Exception(Resources.StateIdentifier3ExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.Colon:
                        {
                            if (symb == '=')
                            {
                                state = States.EqualsSign;
                            }
                            else
                            {
                                state = States.Error;
                                throw new StateColonException(Resources.StateColonExceptionMessage);
                            }
                        }
                        break;

                    case States.ClosedSquareBracket1:
                        {
                            switch (symb)
                            {
                                case ' ':
                                    state = States.ClosedSquareBracket1;

                                    break;
                                case ':':
                                    state = States.Colon;
                                    break;

                                default:
                                    state = States.Error;
                                    throw new StateClosedSquareBracket1Exception(Resources.StateClosedSquareBracket1ExceptionMessage);
                            }
                        }
                        break;

                    case States.OpenSquareBracket1:
                        {
                            counter = ByWhSpIdentAndIntConst(symb, out state, States.OpenSquareBracket1, States.Identifier4, States.FromOneToNine1, States.Minus1, States.Zero1);
                            if (counter > 0)
                            {
                                sbForIdents.Append(symb);
                                startOfIdent = ++endOfIdent;
                            }
                            else if (counter < 0)
                            {
                                sbForIntConsts.Append(symb);
                                startOfInt = ++endOfInt;
                            }
                            else { }
                        }
                        break;

                    case States.Identifier4:
                        {
                            ++endOfIdent;
                            if (ByWhSpIdentAndClSqBr(symb, out state, States.Space4, States.Identifier4, States.ClosedSquareBracket1))
                            {
                                sbForIdents.Append(symb);
                            }
                            else
                            {
                                ++endOfIdent;
                                sbForIdents.Append(newLine);
                                CheckIdent(startOfIdent, endOfIdent, ref currPos, sbForIdents, ref state);
                            }
                        }
                        break;

                    case States.FromOneToNine1:
                        {
                            ++endOfInt;
                            if (ByWhSpNumbAndClSqBr(symb, out state, States.Space4, States.FromOneToNine1, States.ClosedSquareBracket1))
                            {
                                sbForIntConsts.Append(symb);
                            }
                            else
                            {
                                ++endOfInt;
                                sbForIntConsts.Append(newLine);
                                CheckInt(startOfInt, endOfInt, ref currPos, sbForIntConsts, ref state);
                            }
                        }
                        break;

                    case States.Minus1:
                        {
                            ByFromOneToNine(symb, out state, States.FromOneToNine1);
                            sbForIntConsts.Append(symb);
                            ++endOfInt;
                        }
                        break;

                    case States.Zero1:
                        {
                            sbForIntConsts.Append(newLine);
                            endOfInt = endOfInt + 2;
                            ByWhSpAndClSqBr(symb, out state, States.Space4, States.ClosedSquareBracket1);
                        }
                        break;

                    case States.Space4:
                        ByWhSpAndClSqBr(symb, out state, States.Space4, States.ClosedSquareBracket1);
                        break;

                    case States.EqualsSign:
                        {
                            if (char.IsLetter(symb) || symb == '_')
                            {
                                state = States.Identifier5;
                                sbForIdents.Append(symb);
                                startOfIdent = ++endOfIdent;
                            }
                            else if (symb == '0')
                            {
                                state = States.Zero2;
                                sbForAnyConsts.Append(symb);
                            }
                            else if (char.IsDigit(symb))
                            {
                                state = States.FromOneToNine2;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case ' ':
                                        state = States.EqualsSign;
                                        break;

                                    case '\'':
                                        state = States.SingleQuotationMark1;
                                        sbForAnyConsts.Append(symb);
                                        break;

                                    case '-':
                                        state = States.Minus2;
                                        sbForAnyConsts.Append(symb);
                                        break;

                                    case '.':
                                        state = States.Dot2;
                                        sbForAnyConsts.Append(symb);
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateEqualsSignException(Resources.StateEqualsSignExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.Identifier5:
                        {
                            ++endOfIdent;
                            if (char.IsLetterOrDigit(symb) || symb == '_')
                            {
                                state = States.Identifier5;
                                sbForIdents.Append(symb);
                            }
                            else
                            {
                                ++endOfIdent;
                                sbForIdents.Append(newLine);
                                CheckIdent(startOfIdent, endOfIdent, ref currPos, sbForIdents, ref state);

                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space6;
                                        break;

                                    case '[':
                                        state = States.OpenSquareBracket2;
                                        break;

                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        state = States.EqualsSign;
                                        break;

                                    case ';':
                                        state = States.Finish;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateIdentifier5Exception(Resources.StateIdentifier5ExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.SingleQuotationMark1:
                        {
                            sbForAnyConsts.Append(symb);
                            if (symb == '\'')
                            {
                                state = States.SingleQuotationMark2;
                                sbForAnyConsts.Append(newLine);
                            }
                            else
                            {
                                state = States.AnySymbol;
                            }
                        }
                        break;

                    case States.FromOneToNine2:
                        {
                            if (char.IsDigit(symb))
                            {
                                state = States.FromOneToNine2;
                                sbForAnyConsts.Append(symb);
                            }
                            else if (symb == '.')
                            {
                                state = States.Dot2;
                                sbForAnyConsts.Append(symb);
                            }
                            else if (symb == 'e' || symb == 'E')
                            {
                                state = States.E;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space6;
                                        break;

                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        state = States.EqualsSign;
                                        break;

                                    case ';':
                                        state = States.Finish;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateFromOneToNine2Exception(Resources.StateFromOneToNine2ExceptionMessage);
                                }

                                sbForAnyConsts.Append(newLine);
                            }
                        }
                        break;

                    case States.Minus2:
                        {
                            if (char.IsDigit(symb) && symb != '0')
                            {
                                state = States.FromOneToNine2;
                                sbForAnyConsts.Append(symb);
                            }
                            switch (symb)
                            {
                                case '.':
                                    state = States.Dot2;
                                    sbForAnyConsts.Append(symb);
                                    break;

                                default:
                                    state = States.Error;
                                    throw new IsNotOneToNineException(Resources.IsNotOneToNineExceptionMessage);
                            }
                        }
                        break;

                    case States.Zero2:
                        {
                            if (symb == '.')
                            {
                                state = States.Dot2;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space6;
                                        break;

                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        state = States.EqualsSign;
                                        break;

                                    case ';':
                                        state = States.Finish;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateZero2Exception(Resources.StateZero2ExceptionMessage);
                                }

                                sbForAnyConsts.Append(newLine);
                            }
                        }
                        break;

                    case States.Space6:
                        {
                            switch (symb)
                            {
                                case ' ':
                                    state = States.Space6;
                                    break;

                                case '+':
                                case '-':
                                case '*':
                                case '/':
                                    state = States.EqualsSign;
                                    break;

                                case 'd':
                                    state = States.D2;
                                    break;

                                case 'm':
                                    state = States.M;
                                    break;

                                case ';':
                                    state = States.Finish;
                                    break;

                                default:
                                    state = States.Error;
                                    throw new StateSpace6Exception(Resources.StateSpace6Message);
                            }
                        }
                        break;

                    case States.OpenSquareBracket2:
                        {
                            counter = ByWhSpIdentAndIntConst(symb, out state, States.OpenSquareBracket2, States.Identifier6, States.FromOneToNine3, States.Minus3, States.Zero3);
                            if (counter > 0)
                            {
                                sbForIdents.Append(symb);
                                startOfIdent = ++endOfIdent;
                            }
                            else if (counter < 0)
                            {
                                sbForIntConsts.Append(symb);
                                startOfInt = ++endOfInt;
                            }
                            else { }
                        }
                        break;

                    case States.SingleQuotationMark2:
                        {
                            switch (symb)
                            {
                                case ' ':
                                    state = States.SingleQuotationMark2;
                                    break;

                                case ';':
                                    state = States.Finish;
                                    break;

                                default:
                                    state = States.Error;
                                    throw new StateSingleQuotationMark2Exception(Resources.StateSingleQuotationMark2ExceptionMessage);
                            }
                        }
                        break;

                    case States.AnySymbol:
                        {
                            sbForAnyConsts.Append(symb);
                            if (symb == '\'')
                            {
                                state = States.SingleQuotationMark2;
                                sbForAnyConsts.Append(newLine);
                            }
                            else
                            {
                                state = States.AnySymbol;
                            }
                        }
                        break;

                    case States.Dot2:
                        {
                            if (char.IsDigit(symb))
                            {
                                state = States.Numbers;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case 'e':
                                    case 'E':
                                        state = States.E;
                                        sbForAnyConsts.Append(symb);
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateDot2Exception(Resources.StateDot2ExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.D2:
                        ByLettOfResWord(symb, out state, States.I2, 'i');
                        break;

                    case States.M:
                        ByLettOfResWord(symb, out state, States.O2, 'o');
                        break;

                    case States.Identifier6:
                        {
                            ++endOfIdent;
                            if (ByWhSpIdentAndClSqBr(symb, out state, States.Space5, States.Identifier6, States.ClosedSquareBracket2))
                            {
                                sbForIdents.Append(symb);
                            }
                            else
                            {
                                ++endOfIdent;
                                sbForIdents.Append(newLine);
                                CheckIdent(startOfIdent, endOfIdent, ref currPos, sbForIdents, ref state);
                            }
                        }
                        break;

                    case States.FromOneToNine3:
                        {
                            ++endOfInt;
                            if (ByWhSpNumbAndClSqBr(symb, out state, States.Space5, States.FromOneToNine3, States.ClosedSquareBracket2))
                            {
                                sbForIntConsts.Append(symb);
                            }
                            else
                            {
                                ++endOfInt;
                                sbForIntConsts.Append(newLine);
                                CheckInt(startOfInt, endOfInt, ref currPos, sbForIntConsts, ref state);
                            }
                        }
                        break;

                    case States.Minus3:
                        {
                            ByFromOneToNine(symb, out state, States.FromOneToNine3);
                            sbForIntConsts.Append(symb);
                            ++endOfInt;
                        }
                        break;

                    case States.Zero3:
                        {
                            sbForIntConsts.Append(newLine);
                            endOfInt = endOfInt + 2;
                            ByWhSpAndClSqBr(symb, out state, States.Space5, States.ClosedSquareBracket2);
                        }
                        break;

                    case States.Numbers:
                        {
                            if (char.IsDigit(symb))
                            {
                                state = States.Numbers;
                                sbForAnyConsts.Append(symb);
                            }
                            else if (symb == 'e' || symb == 'E')
                            {
                                state = States.E;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space6;
                                        break;

                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        state = States.EqualsSign;
                                        break;

                                    case ';':
                                        state = States.Finish;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateNumbersException(Resources.StateNumbersExceptionMessage);
                                }

                                sbForAnyConsts.Append(newLine);
                            }
                        }
                        break;

                    case States.I2:
                        ByLettOfResWord(symb, out state, States.V, 'v');
                        break;

                    case States.O2:
                        ByLettOfResWord(symb, out state, States.V, 'd');
                        break;

                    case States.E:
                        {
                            if (symb == '0')
                            {
                                state = States.Zero4;
                                sbForAnyConsts.Append(symb);
                                sbForAnyConsts.Append(newLine);
                            }
                            else if (char.IsDigit(symb))
                            {
                                state = States.FromOneToNine4;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case ' ':
                                        state = States.E;
                                        break;

                                    case '+':
                                    case '-':
                                        state = States.PlusOrMinus;
                                        sbForAnyConsts.Append(symb);
                                        break;

                                    default:
                                        state = States.E;
                                        throw new StateEException(Resources.StateEExceptionMessage);
                                }
                            }
                        }
                        break;

                    case States.V:
                        ByWhSp(symb, out state, States.EqualsSign);
                        break;

                    case States.ClosedSquareBracket2:
                        ByWhSpOpsAndSemicolon(symb, out state, States.Space6, States.EqualsSign, States.Finish);
                        break;

                    case States.Space5:
                        ByWhSpAndClSqBr(symb, out state, States.Space5, States.ClosedSquareBracket2);
                        break;

                    case States.Zero4:
                        ByWhSpOpsAndSemicolon(symb, out state, States.Space6, States.EqualsSign, States.Finish);
                        break;

                    case States.FromOneToNine4:
                        {
                            if (char.IsDigit(symb))
                            {
                                state = States.FromOneToNine4;
                                sbForAnyConsts.Append(symb);
                            }
                            else
                            {
                                switch (symb)
                                {
                                    case ' ':
                                        state = States.Space6;
                                        break;

                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        state = States.EqualsSign;
                                        break;

                                    case ';':
                                        state = States.Finish;
                                        break;

                                    default:
                                        state = States.Error;
                                        throw new StateFromOneToNine4Exception(Resources.StateFromOneToNine4ExceptionMessage);
                                }

                                sbForAnyConsts.Append(newLine);
                            }
                        }
                        break;

                    case States.PlusOrMinus:
                        {
                            if (char.IsDigit(symb) && symb != '0')
                            {
                                state = States.FromOneToNine4;
                                sbForAnyConsts.Append(symb);
                            }
                            switch (symb)
                            {
                                case ' ':
                                    state = States.PlusOrMinus;
                                    break;

                                default:
                                    state = States.Error;
                                    throw new IsNotOneToNineException(Resources.IsNotOneToNineExceptionMessage);
                            }
                        }
                        break;
                }

                if (++currPos >= len)
                    break;
            }

            if (str.Length > currPos)
            {
                state = States.Error;
                throw new IndexOutOfRangeException(Resources.IndexOutOfRangeExceptionMessage);
            }

            return state == States.Finish;
        }

        private static void ByLettOfResWord(char symb, out States currSt, States newSt, char lett)
        {
            if (symb == lett)
            {
                currSt = newSt;
            }
            else
            {
                currSt = States.Error;
                throw new WrongSymbOfResWordException(Resources.WrongSymbOfResWordExceptionMessage);
            }
        }

        private static void ByWhSp(char symb, out States currSt, States newSt)
        {
            if (char.IsWhiteSpace(symb))
            {
                currSt = newSt;
            }
            else
            {
                currSt = States.Error;
                throw new IsNotWhSpException(Resources.IsNotWhSpExceptionMessage);
            }
        }

        private static bool ByWhSpAndIdent(char symb, out States currSt, States newSt1, States newSt2)
        {
            if (char.IsLetter(symb) || symb == '_')
            {
                currSt = newSt2;
                return true;
            }

            if (char.IsWhiteSpace(symb))
            {
                currSt = newSt1;
            }
            else
            {
                currSt = States.Error;
                throw new WrongIdentifierFirstSymbolException(Resources.WrongIdentifierFirstSymbolExceptionMessage);
            }

            return false;
        }

        private static int ByWhSpIdentAndIntConst(char symb, out States currSt, States newSt1, States newSt2, States newSt3, States newSt4, States newSt5)
        {
            if (char.IsLetter(symb) || symb == '_')
            {
                currSt = newSt2;
                return 1;
            }

            if (symb == '0')
            {
                currSt = newSt5;
                return -1;
            }

            if (char.IsDigit(symb))
            {
                currSt = newSt3;
                return -1;
            }

            switch (symb)
            {
                case ' ':
                    currSt = newSt1;
                    break;

                case '-':
                    currSt = newSt4;
                    return -1;

                default:
                    currSt = States.Error;
                    string newLine = Environment.NewLine;
                    throw new IsNotWhSpIdentAndIntConstException(Resources.IsNotWhSpIdentAndIntConstExceptionMessage);
            }

            return 0;
        }

        private static bool ByWhSpIdentAndClSqBr(char symb, out States currSt, States newSt1, States newSt2, States newSt3)
        {
            if (char.IsLetterOrDigit(symb) || symb == '_')
            {
                currSt = newSt2;
                return true;
            }

            switch (symb)
            {
                case ' ':
                    currSt = newSt1;
                    break;

                case ']':
                    currSt = newSt3;
                    break;

                default:
                    currSt = States.Error;
                    throw new IsNotWhSpIdentAndClSqBrException(Resources.IsNotWhSpIdentAndClSqBrExceptionMessage);
            }

            return false;
        }

        private static bool ByWhSpNumbAndClSqBr(char symb, out States currSt, States newSt1, States newSt2, States newSt3)
        {
            if (char.IsDigit(symb))
            {
                currSt = newSt2;
                return true;
            }

            switch (symb)
            {
                case ' ':
                    currSt = newSt1;
                    break;

                case ']':
                    currSt = newSt3;
                    break;

                default:
                    currSt = States.Error;
                    string newLine = Environment.NewLine;
                    throw new IsNotWhSpNumbAndClSqBrException(Resources.IsNotWhSpNumbAndClSqBrExceptionMessage);
            }

            return false;
        }

        private static void ByFromOneToNine(char symb, out States currSt, States newSt)
        {
            if (char.IsDigit(symb) && symb != '0')
            {
                currSt = newSt;
            }
            else
            {
                currSt = States.Error;
                throw new IsNotOneToNineException(Resources.IsNotOneToNineExceptionMessage);
            }
        }

        private static void ByWhSpAndClSqBr(char symb, out States currSt, States newSt1, States newSt2)
        {
            switch (symb)
            {
                case ' ':
                    currSt = newSt1;
                    break;

                case ']':
                    currSt = newSt2;
                    break;

                default:
                    currSt = States.Error;
                    throw new IsNotWhSpAndClSqBrException(Resources.IsNotWhSpAndClSqBrExceptionMessage);
            }
        }

        private static void ByWhSpOpsAndSemicolon(char symb, out States currSt, States newSt1, States newSt2, States newSt3)
        {
            switch (symb)
            {
                case ' ':
                    currSt = newSt1;
                    break;

                case '+':
                case '-':
                case '*':
                case '/':
                    currSt = newSt2;
                    break;

                case ';':
                    currSt = newSt3;
                    break;

                default:
                    currSt = States.Error;

                    throw new IsNotWhSpOrOpsOrSemicolonException(Resources.IsNotWhSpOrOpsOrSemicolonExceptionMessage);
            }
        }

        private static void CheckIdent(int startOfIdent, int endOfIdent, ref int pos, StringBuilder sbForIdents, ref States currSt)
        {
            int diff = endOfIdent - startOfIdent - 1; // поскольку два символа
            string str = sbForIdents.ToString(startOfIdent, diff);
            pos = pos - diff;

            if (diff > LENGTH_OF_IDENT)
            {
                currSt = States.Error;
                throw new IndexOutOfRangeException("Ошибка. Длина константы больше " + LENGTH_OF_IDENT + ".");
            }
            else if (str == "DO")
            {
                currSt = States.Error;
                throw new MathchedIdentWithResWordException("Ошибка. Идентификатор совпадает с ключевым словом DO.");
            }
            else if (str == "div")
            {
                currSt = States.Error;
                throw new MathchedIdentWithResWordException("Ошибка. Идентификатор совпадает с ключевым словом div.");
            }
            else if (str == "mod")
            {
                currSt = States.Error;
                throw new MathchedIdentWithResWordException("Ошибка. Идентификатор совпадает с ключевым словом mod.");
            }
            else if (str == "WITH")
            {
                currSt = States.Error;
                throw new MathchedIdentWithResWordException("Ошибка. Идентификатор совпадает с ключевым словом WITH.");
            }
            else { }

            pos = pos + diff;
        }

        private static void CheckInt(int startOfInt, int endOfInt, ref int pos, StringBuilder sbForIntConsts, ref States currSt)
        {
            int diff = endOfInt - startOfInt - 1;
            pos = pos - diff;

            if (int.TryParse(sbForIntConsts.ToString(startOfInt, diff), out int verNumb))
            {
                if (verNumb > MAX_INT || verNumb < MIN_INT)
                {
                    currSt = States.Error;
                    throw new IndexOutOfRangeException("Ошибка. Целочисленная константа вышла за границы допустимого диапазона от " + MIN_INT + " до " + MAX_INT + ".");
                }
            }
            else
            {
                throw new Exception("Ошибка в Int32.TryParse().");
            }

            pos = pos + diff;
        }
    }
}
