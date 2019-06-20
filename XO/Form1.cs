using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XO
{
    public partial class Form1 : Form
    {
        public static GFX g;
        GFX.Player t = GFX.Player.X;
        bool AIGame = false;
        bool game = false;
        bool isGame = false;
        AI.difficulty allDif;
        bool aiTurn = false;
        public static int xScore = 0, oScore = 0, dScore = 0;
        public static anim[] animData = new anim[9];
        public static Timer ani = new Timer();
        Point start; Size end;
        public static int animeind = 0;
        public static int curind = 0;
        bool IsWin = false;
        int sWin, eWin;
        double opt = 1;
        bool isFocused = true;
        public Form1()
        {
            InitializeComponent();

            //   this.ResizeRedraw = true;
            ani.Interval = 1;
            ani.Tick += new EventHandler(Animations_Tick);
            ani.Enabled = true;
        }
        public static void getAnime(GFX.Player p, Point p1 = new Point(), Point p2 = new Point(), Point p3 = new Point(), Point o = new Point(), Point o2 = new Point())
        {
            animData[animeind] = new anim();
            animData[animeind].p1 = p1;
            animData[animeind].p2 = p2;
            animData[animeind].p3 = p3;
            animData[animeind].o = o;
            animData[animeind].o2 = o2;
            animData[animeind].p = p;
            animeind++;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowInfo("Select New Game Mode :", "NewGame", "2 Player's", "1 Player");
            //  this.Focus();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!game) return;
            int z = g.getBox(e.X, e.Y);
            if (z == -1) return;
            if (isFocused == false) return;
            // if (AIGame == true) t = GFX.Player.X;
            // this.Text = "Box = " + z;
            if (Logic.Win == false && Logic.board[z] == GFX.Player.Empty)
            {
                g.DrawPlayer(t, z);
                Logic.Move(z, t);
                check();
                if (AIGame == false)
                {
                    if (t == GFX.Player.X) t = GFX.Player.O;
                    else t = GFX.Player.X;
                }
                else
                {
                    if (Logic.checkWin() != Logic.WinState.Nothing) return;
                    //t = GFX.Player.X;
                    int d;
                    d = AI.GetNextMove(allDif);
                    //if (d != null) Logic.board = d;
                    if (d != -1) g.DrawPlayer(GFX.Player.O, d);
                }
                //check(

            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            //g.DrawBoard();

        }

        void ShowInfo(string InfoL, string Title, string b1Text, string b2Text)
        {
            InfoW.Text = Title;
            lInfo.Text = InfoL;
            b1.Text = b1Text;
            b2.Text = b2Text;
            int w = this.Width - InfoW.Width;
            w = w / 2;
            int h = this.Height - InfoW.Height;
            h = h / 2;
            //h += 10;
            InfoW.Location = new Point(w, h);
            InfoW.Visible = true;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            g.DrawBoard(false);
            if (AIGame == false) this.Text = "{Turn : Player" + t.ToString() + "} - Wins : [PlayerX : " + xScore + " time's, PlayerO : " + oScore + " time's, Draws : " + dScore + " time's]";
            else this.Text = "{Turn : Player" + t.ToString() + "} - Wins : [Human : " + xScore + " time's, Computer : " + oScore + " time's, Draws : "+ dScore  +" time's]";
            check();

            //     if (resettime == 6) { Logic.reset(); g.DrawBoard(); resettime = 0; }
        }
        void check()
        {
            if (Logic.checkWin() == Logic.WinState.Win)
            {
                int[] z = new int[3];
                z = Logic.getWinCoords();
                IsWin = true;
                sWin = z[0]; eWin = z[2];
                ShowInfo("Select New Game Mode :", "The Winner Is : " + Logic.wineer.ToString(), "2 Player's", "1 Player");
                timer1.Enabled = false;
                game = false;
                isGame = false;
                AIGame = false;
                //resettime++;

            }
            else if (Logic.checkWin() == Logic.WinState.Draw)
            {
                ShowInfo("Select New Game Mode :", "Draw!", "2 Player's", "1 Player");
                timer1.Enabled = false;
                game = false;
                isGame = false;
                dScore++;
            }
        }
        private void B1_Click(object sender, EventArgs e)
        {
            if (isGame == false) AIGame = false;
            game = true;
            Logic.reset();
            g.DrawBoard();
            timer1.Enabled = true;
            InfoW.Visible = false;
            if (isGame == true) allDif = AI.difficulty.Easy;
            animeind = 0;
            curind = 0;
            IsWin = false;
        }

        private void B2_Click(object sender, EventArgs e)
        {
            AIGame = true;
            System.Diagnostics.Debug.WriteLine(AIGame.ToString());
            if (AIGame == true && isGame == false)
            {
                isGame = true;
                ShowInfo("Set Difficulty :", "1 Player", "Easy", "Hard");
                return;
            }
            else
            {
                Logic.reset();
                g.DrawBoard();
                timer1.Enabled = true;
                InfoW.Visible = false;
                game = true;
                allDif = AI.difficulty.Hard;
                animeind = 0;
                curind = 0;
                IsWin = false;
                if (isGame == true && AIGame == true)
                {
                    if (aiTurn == true)
                    {
                        //aiTurn = !aiTurn;
                        AI.GetNextMove(allDif);
                    }
                    aiTurn = !aiTurn;
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Width = this.Height - 20;
            g = new GFX(this);
            int w = this.Width - InfoW.Width;
            w = w / 2;
            int h = this.Height - InfoW.Height;
            h = h / 2;
            //h += 10;
            InfoW.Location = new Point(w, h);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        public void Animations_Tick(object sender, EventArgs e)
        {
            Timer ti = (Timer)sender;
            if (curind == animeind) return;
            GFX.Player cur_ani = animData[curind].p;
            if (cur_ani == GFX.Player.X)
            {
                int plusfactor = 7;
                int h = animData[curind].p2.Y - animData[curind].p1.Y;
                int w = animData[curind].p2.X - animData[curind].p1.X;
                if (animData[curind].p3.X < w)
                {
                    animData[curind].p3 = new Point(animData[curind].p3.X + plusfactor, animData[curind].p3.Y);
                }
                if (animData[curind].p3.Y < h) animData[curind].p3 = new Point(animData[curind].p3.X, animData[curind].p3.Y + plusfactor);
                if (animData[curind].p3.X > w) animData[curind].p3.X = w;
                if (animData[curind].p3.Y > h) animData[curind].p3.Y = h;

                Pen t = new Pen(Color.Red);
                t.Width = 10;
                g.g.DrawLine(t, animData[curind].p1, new Point(animData[curind].p1.X + animData[curind].p3.X, animData[curind].p1.Y + animData[curind].p3.Y));
                g.g.DrawLine(t, animData[curind].o, new Point(animData[curind].o.X - animData[curind].p3.X, animData[curind].o.Y + animData[curind].p3.Y));
                Point temp = new Point(animData[curind].p1.X + animData[curind].p3.X, animData[curind].p1.Y + animData[curind].p3.Y);
                if (temp.X == animData[curind].p2.X || temp.Y == animData[curind].p2.Y)
                {
                    //cur_ani = GFX.Player.Empty;
                    //animData[curind].p3 = new Point();
                    curind++;
                }
            }

            if (cur_ani == GFX.Player.O)
            {
                int plusfactor = 7;
                //int h = animData[curind].p2.Y - animData[curind].p1.Y;
                //int w = animData[curind].p2.X - animData[curind].p1.X;
                g.g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(animData[curind].p1.X, animData[curind].p1.Y, animData[curind].p2.X, animData[curind].p2.Y));
                //   g.g.DrawEllipse(new Pen(Color.Pink), new Rectangle(start, new Size(50, 50)));
                if (animData[curind].p3.X < animData[curind].p2.X) animData[curind].p3 = new Point(animData[curind].p3.X + plusfactor, animData[curind].p3.Y);
                if (animData[curind].p3.Y < animData[curind].p2.Y) animData[curind].p3 = new Point(animData[curind].p3.X, animData[curind].p3.Y + plusfactor);
                if (animData[curind].p3.X > animData[curind].p2.X) animData[curind].p3.X = animData[curind].p2.X;
                if (animData[curind].p3.Y > animData[curind].p2.Y) animData[curind].p3.Y = animData[curind].p2.Y;
                Pen t = new Pen(Color.Blue);
                t.Width = 10;
                start = new Point(animData[curind].p1.X + (animData[curind].p2.X - animData[curind].p3.X), animData[curind].p1.Y + (animData[curind].p2.Y - animData[curind].p3.Y));
                end = new Size((animData[curind].p3.X - animData[curind].o.X) * 2, (animData[curind].p3.Y - animData[curind].o.Y) * 2);
                g.g.DrawEllipse(t, new Rectangle(start, end));
                //  System.Diagnostics.Debug.WriteLine(start.X + "," + start.Y + "--->" + end.Width + "," + end.Height);
                //Point temp = new Point(animData[curind].p1.X + animData[curind].p3.X, animData[curind].p1.Y + animData[curind].p3.Y);
                if (animData[curind].p3.X == animData[curind].p2.X || animData[curind].p3.Y == animData[curind].p2.Y)
                {
                    curind++;
                    //animData[curind].p3 = new Point();
                }
            }
            if (IsWin == true) g.DrawWin(sWin, eWin);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.KeyChar.ToString());
            if (!game) return;
            if (e.KeyChar > '9' || e.KeyChar < '1')
            {
                return;
            }
            int z = -1;
            switch (e.KeyChar)
            {
                case '1':
                    z = 6;
                    break;
                case '2':
                    z = 7;
                    break;
                case '3':
                    z = 8;
                    break;
                case '4':
                    z = 3;
                    break;
                case '5':
                    z = 4;
                    break;
                case '6':
                    z = 5;
                    break;
                case '9':
                    z = 2;
                    break;
                case '8':
                    z = 1;
                    break;
                case '7':
                    z = 0;
                    break;
            }
            if (z == -1) return;
            if (AIGame == true) t = GFX.Player.X;
            // this.Text = "Box = " + z;
            if (Logic.Win == false && Logic.board[z] == GFX.Player.Empty)
            {
                g.DrawPlayer(t, z);
                Logic.Move(z, t);
                check();
                if (AIGame == false)
                {
                    if (t == GFX.Player.X) t = GFX.Player.O;
                    else t = GFX.Player.X;
                }
                else
                {
                    if (Logic.checkWin() != Logic.WinState.Nothing) return;
                    //t = GFX.Player.X;
                    int d;
                    d = AI.GetNextMove(allDif);
                    if (d != -1) g.DrawPlayer(GFX.Player.O, d);
                }
                //check(

            }
        }

        private void IWRC_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int r = rand.Next(255);
            int g = rand.Next(255);
            int b = rand.Next(255);
            InfoW.BackColor = Color.FromArgb(r, g, b);
        }

        private void Obedit_Tick(object sender, EventArgs e)
        {
            if (Focused == false && InfoW.Focused == false && b1.Focused == false && b2.Focused == false)
            {
                if (opt > 0.6) opt -= 0.01;
                isFocused = false;
            }
            if (Focused == true || InfoW.Focused == true || b1.Focused == true || b2.Focused == true)
            {
                if (opt < 1) opt += 0.01;
                isFocused = true;
            }
            this.Opacity = opt;
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g.DrawAll();
            if (IsWin == true) g.DrawWin(sWin, eWin);
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string help = "TicTacToe Game :" +
                "A game with two player's and two piece's\n" +
                "[X] or [O]. Your objective is to put the\n" +
                "piece's in diagonals or horizontally or\n" +
                "vertically so you win. You can play it\n" +
                "with your friend or with the computer\n" +
                "but be awere that the computer is very\n" +
                "hard! Enjoy :)";
            MessageBox.Show(help, "Help");
        }
    }
    public class anim
    {
        public Point p1 = new Point(),
        p2 = new Point(),
        p3 = new Point(),
        o = new Point(),
        o2 = new Point();
        public GFX.Player p = GFX.Player.Empty;
    }
}
