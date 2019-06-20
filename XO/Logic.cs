using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XO
{
    class Logic
    {
        public static GFX.Player[] board = new GFX.Player[9];
        public enum WinState { Win = 1, Draw = 2, Nothing = 0 }
        static int[,] winsArray = new int[8, 3]
        {
            {0,4,8 },
            {2,4,6 },
            {0,3,6 },
            {1,4,7},
            {2,5,8 },
            {0,1,2 },
            {3,4,5 },
            {6,7,8 },
        };
        static int[] GetWinCoords = new int[3];
        private static bool win = false;
        public static GFX.Player wineer = GFX.Player.Empty;
        public static bool Win { get => win; set => win = value; }

        public GFX.Player Winner
        {
            get => wineer;
        }

        public GFX.Player this[int i]
        {
            get => board[i];
        }
        public Logic()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = GFX.Player.Empty;
            }
        }
        public static void reset()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = GFX.Player.Empty;
            }
            win = false;
            for (int i = 0; i < 3; i++)
            {
                GetWinCoords[i] = 0;
            }
        }
        public static void Move(int pos, GFX.Player p)
        {
            if (board[pos] == GFX.Player.Empty)
            {
                board[pos] = p;
            }
        }
        public static WinState checkWin()
        {
            // int[] temp = new int[3];
            bool full = true;
            for (int i = 0; i < 8; i++)
            {
                int[] z = GetFromWinArray(i);
                if (board[z[0]] == board[z[1]] && board[z[0]] == board[z[2]] && board[z[0]] != GFX.Player.Empty)
                {
                    GetWinCoords = z;
                    Win = true;
                    wineer = board[z[0]];
                    if (wineer == GFX.Player.X) Form1.xScore++;
                    else Form1.oScore++;
                    return WinState.Win;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == GFX.Player.Empty)
                {
                    full = false;
                    break;
                }
            }
            if (full == true){ return WinState.Draw; }
            return WinState.Nothing;
        }
        public static int[] getWinCoords() { if (Win == true) return GetWinCoords; else return new int[3]; }
        public static int[] GetFromWinArray(int x)
        {
            int[] t = new int[3];
            for (int i = 0; i < 3; i++)
            {
                t[i] = winsArray[x, i];
            }
            return t;
        }
    }

}
