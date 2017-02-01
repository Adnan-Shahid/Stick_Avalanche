using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AdnanShahid_FinalCulminating
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }
        
        //Variable Declaration
        const int intRunSpeed = 20;
        Boolean blnRunRight = false;
        Boolean blnRunLeft = false;
        Boolean blnStand = false;
        int intSeconds = 0;
        int intExplosion1 = 0;
        int intExplosion2 = 0;
        int intExplosion3 = 0;
        int intExplosion4 = 0;
        int intExplosion5 = 0;
        int intBallExplosion1 = 0;
        int intBallExplosion2 = 0;
        Random RandomClass = new Random();
        int intRandom1;
        int intRandom2;
        int intRandom3;
        int intRandom4;
        int intRandom5;
        int intRandomAngle;
        int intRandomAngle2;
        int intRandomXBall;
        int intRandomXBall2;
        int intSpeed = 5;
        int intxMove = 0;
        int intxMove2 = 0;
        int intyMove = 0;
        int intyMove2 = 0;
        int intPoints = 1;
        int intScore = 0;
        int intBossText;
        PictureBox pcbBoss;
        Boolean blnBoss = false;
        Boolean blnWeaponAnimation = false;
        int intBossDirectionX = 1;
        int intAlienAnimation;
        PictureBox[] pcbBossSpikes = new PictureBox[1000];
        int intCounter = 0;
        Boolean blnDead = false;
        public static int intFinalScore = 0;
        Boolean blnCollision = true;
        int intBossHealth = 30;
        int intRandomChance;
        int intRandomPowerUp;
        int intBossDeathAnimation = 0;
        int intRandomLocation;
        Boolean blnPowerUp = false;
        int intInvinciblity = 0;
        Boolean blnSpeedUp = false;
        int intSpeedUp = 0;
        Boolean blnSpecialMove = false;
        int intRandomSpecial;

        
        
        
        
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Exit game
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //CLose all forms when clicking x button
        }

        private void pcbRunRight_Click(object sender, EventArgs e)
        {

        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)    //Moving left
            {
                if (this.pcbStanding.Left > 173 && blnDead == false && blnSpeedUp == false) //Prevents it from going through the score/time/shots panel
                {
                    if (blnRunLeft == false)    //Makes sure only the moving left animation shows
                    {
                        blnRunLeft = true;
                        blnRunRight = false;
                        blnStand = false;
                        pcbStanding.Image = Pics.StickRunLeft;
                    }
                    this.pcbStanding.Left -= intRunSpeed;    //Moves all images and sets them so that you can only see the correct one
                    if (tmrPlayerSpike.Enabled == false)
                    {
                        this.pcbPlayerSpike.Left -= intRunSpeed; //Moves the player spike with the player
                    }

                }
                else if (this.pcbStanding.Left > 173 && blnDead == false && blnSpeedUp == true) //if the player has the speed up on
                {
                    if (blnRunLeft == false)    //Makes sure only the moving left animation shows
                    {
                        blnRunLeft = true;
                        blnRunRight = false;
                        blnStand = false;
                        pcbStanding.Image = Pics.StickRunLeft;
                    }
                    this.pcbStanding.Left -= intRunSpeed+20;    //Moves all images and sets them so that you can only see the correct one
                    if (tmrPlayerSpike.Enabled == false)
                    {
                        this.pcbPlayerSpike.Left -= intRunSpeed+20; //Moves the player spike with the player
                    }

                }
            }
            else if (e.KeyCode == Keys.D)       //Moving right
            {
                if (this.pcbStanding.Left < 830 && blnDead == false && blnSpeedUp == false)
                {
                    this.pcbStanding.Left += intRunSpeed;    //Making sure only moving right animation shows
                    if (blnRunRight == false)
                    {
                        blnRunLeft = false;
                        blnRunRight = true;
                        blnStand = false;
                        pcbStanding.Image = Pics.StickRunRight;
                    }
                    if (tmrPlayerSpike.Enabled == false)    //Moves the player spike with the player
                    {
                        this.pcbPlayerSpike.Left += intRunSpeed;
                    }
                }
                else if (this.pcbStanding.Left < 830 && blnDead == false && blnSpeedUp == true) //If player has the speed up on
                {
                    this.pcbStanding.Left += intRunSpeed+20;    //Making sure only moving right animation shows
                    if (blnRunRight == false)
                    {
                        blnRunLeft = false;
                        blnRunRight = true;
                        blnStand = false;
                        pcbStanding.Image = Pics.StickRunRight;
                    }
                    if (tmrPlayerSpike.Enabled == false)    //Moves the player spike with the player
                    {
                        this.pcbPlayerSpike.Left += intRunSpeed+20;
                    }
                }
            }
            
            else if (e.KeyCode == Keys.L && intAlienAnimation >= 14 && blnDead == false)    //Allows player to shoot after alien teaches the way
            {

                if (pcbPlayerSpike.Top > 0) //Shooting plays the spike shot animation
                {
                    pcbPlayerSpike.BringToFront();
                    pcbStanding.BringToFront();
                    pnlGround.BringToFront();
                    pnlMain.BringToFront();
                    tmrPlayerSpike.Enabled = true;
                }
            }

        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (blnStand == false)  //Leaves the player standing animation on when doing nothing
            {
                //Prevents other pictures from showing up
                blnStand = true;
                blnRunRight = false;
                blnRunLeft = false;
                pcbStanding.Image = Pics.StickStand;    //Change to stick standing animation
            }
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            if (intRandomSpecial == 3 && blnSpecialMove == false)   //Player activates his special move if the ranomizer hits 3
            {
                blnSpecialMove = true;
                pcbSpecial.Location = new Point(pcbBoss.Left + 20, pcbBoss.Top + 40);
                tmrAttack.Enabled = true;
            }
            intRandomChance = RandomClass.Next(1, 5);    //1 in 4 chance to get a powerup
            if (intRandomChance == 3 && blnPowerUp == false)
            {
                blnPowerUp = true;
                Powerups();
            }

            intSeconds++;   //Total time
            this.lblTime.Text = intSeconds.ToString();
            intScore = intSeconds * intPoints * 2;  //Calculating total score
            this.lblScore.Text = intScore.ToString();   //Showing score as output to player
            if (pcbPlayerSpike.Top <= 0)
            {
                pcbPlayerSpike.Location = new Point(pcbStanding.Left, pcbStanding.Top);
                tmrPlayerSpike.Enabled = false;
            }
            if (intSeconds >= 1)    //Enabling the spikes after a certain time limit
            {
                tmrSpike1.Enabled = true;
                pcbSpike1.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intSeconds >= 2)    //Enabling the spikes after a certain time limit
            {
                tmrSpike2.Enabled = true;
                pcbSpike2.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intSeconds >= 7)    //Enabling the spikes after a certain time limit
            {
                tmrSpike3.Enabled = true;
                pcbSpike3.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intSeconds >= 15)    //Enabling the spikes after a certain time limit
            {
                tmrSpike4.Enabled = true;
                pcbSpike4.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intSeconds >= 28)   //Enabling the spikes after a certain time limit
            {
                tmrSpike5.Enabled = true;
                pcbSpike5.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intScore >= 500)   //Enabling the ball after a certain score limit
            {
                tmrBall.Enabled = true;
                pcbBall1.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intScore >= 700)   //Enabling the ball after a certain score limit
            {
                tmrBall2.Enabled = true;
                pcbBall2.BringToFront();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
            }
            if (intScore >= 1300)
            {
                //****************************
                //Preparing for the boss level
                //Enabling boss timers
                tmrSpike1.Enabled = false;
                tmrSpike2.Enabled = false;
                tmrSpike3.Enabled = false;
                tmrSpike4.Enabled = false;
                tmrSpike5.Enabled = false;
                tmrBall.Enabled = false;
                tmrBall2.Enabled = false;
                tmrSpikeImpact1.Enabled = false;
                tmrSpikeImpact2.Enabled = false;
                tmrSpikeImpact3.Enabled = false;
                tmrSpikeImpact4.Enabled = false;
                tmrSpikeImpact5.Enabled = false;
                tmrBallImpact.Enabled = false;
                tmrBallImpact2.Enabled = false;
                pcbSpike1.SendToBack();
                pcbSpike2.SendToBack();
                pcbSpike3.SendToBack();
                pcbSpike4.SendToBack();
                pcbSpike5.SendToBack();
                pcbExplode1.SendToBack();
                pcbExplode2.SendToBack();
                pcbExplode3.SendToBack();
                pcbExplode4.SendToBack();
                pcbExplode5.SendToBack();
                pcbBall1.SendToBack();
                pcbBall2.SendToBack();
                pcbExplodeBall1.SendToBack();
                pcbExplodeBall2.SendToBack();

                tmrWeaponAnimation.Enabled = true;

                //Creating boss image and having it appear
                if (blnBoss == false && intAlienAnimation >= 14)   //Preventing boss image from reappearing
                {
                    pcbBoss = new PictureBox();
                    pcbBoss.Location = new Point(430, -183);
                    pcbBoss.Width = 183;
                    pcbBoss.Height = 99;
                    Controls.Add(pcbBoss);
                    pcbBoss.Image = Pics.Boss;
                    pcbBoss.BringToFront();
                    pnlGround.BringToFront();
                    pnlMain.BringToFront();
                    blnBoss = true;

                    tmrBossAnimation.Enabled = true;


                }

                if (intBossText >= 2)
                {
                    //Boss shooting
                    //Creates a spike where the boss is
                    pcbBossSpikes[intCounter] = new PictureBox();
                    pcbBossSpikes[intCounter].Size = new Size(41, 40);
                    pcbBossSpikes[intCounter].Location = new Point(pcbBoss.Left + 20, pcbBoss.Top + 40);
                    pcbBossSpikes[intCounter].Image = Pics.BossSpike;
                    pcbBossSpikes[intCounter].Name = "pcbBossSpike" + intCounter;
                    Controls.Add(pcbBossSpikes[intCounter]);
                    pcbBossSpikes[intCounter].BringToFront();
                    tmrBossSpikes.Enabled = true;
                    tmrBossMove.Enabled = true;
                    intCounter++;
                    tmrSpecialMove.Enabled = true;
                    
                   
                }
                

            }
        }

        private void tmrSpike1_Tick(object sender, EventArgs e)
        {
            //First spike
            intRandom1 = RandomClass.Next(145, 860);
            if (pcbSpike1.Top < 515)
            {
                if (tmrSpikeImpact1.Enabled == false)
                {
                    intExplosion1 = 0;   //Resetting so that explode animation may play again
                    pcbSpike1.Top += 5;     //Movement
                    pcbExplode1.Top += 5;
                }
            }
            else if (pcbSpike1.Top >= 515)
            {
                tmrSpikeImpact1.Enabled = true;
                if (intExplosion1 == 3)
                {
                    tmrSpikeImpact1.Enabled = false;    //Allowing explode animation to occur
                    pcbSpike1.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbSpike1.Location = new Point(intRandom1, 0);      //Sets spike to a new random location
                    pcbExplode1.Location = new Point(intRandom1, 0);    //Makes explosion follow the spike
                }

            }

            //***************************
            //COLLISION
            if (pcbSpike1.Location.X + pcbSpike1.Width > pcbStanding.Location.X && pcbSpike1.Location.X + pcbSpike1.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpike1.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbSpike1.Location.X >= pcbStanding.Location.X && pcbSpike1.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpike1.Location.Y + 33 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike1.Location.X + 15 > pcbStanding.Location.X && pcbSpike1.Location.X + 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike1.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike1.Location.X - 15 > pcbStanding.Location.X && pcbSpike1.Location.X - 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike1.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            

            
                
            
        }

        private void tmrSpikeImpact1_Tick(object sender, EventArgs e)   //The spike impacts are for the explosion animation to occur
        {
            intExplosion1++; //Explode animation
            pcbExplode1.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();
        }

        private void tmrSpike2_Tick(object sender, EventArgs e)
        {
            intRandom2 = RandomClass.Next(145, 860);
            if (pcbSpike2.Top < 515)
            {
                if (tmrSpikeImpact2.Enabled == false)
                {
                    intExplosion2 = 0;   //Resetting so that explode animation may play again
                    pcbSpike2.Top += 5; //Movement
                    pcbExplode2.Top += 5;
                }
            }
            else if (pcbSpike2.Top >= 515)
            {
                tmrSpikeImpact2.Enabled = true;
                if (intExplosion2 == 3) 
                {
                    tmrSpikeImpact2.Enabled = false;    //Allowing explode animation to occur
                    pcbSpike2.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbSpike2.Location = new Point(intRandom2, 0);  //Sets spike to a new random location
                    pcbExplode2.Location = new Point(intRandom2, 0);
                }

            }
            
            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision -- no idea why 
            if (pcbSpike2.Location.X + pcbSpike2.Width > pcbStanding.Location.X && pcbSpike2.Location.X + pcbSpike2.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpike2.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbSpike2.Location.X >= pcbStanding.Location.X && pcbSpike2.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpike2.Location.Y + 33 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike2.Location.X + 15 > pcbStanding.Location.X && pcbSpike2.Location.X + 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike2.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike2.Location.X - 15 > pcbStanding.Location.X && pcbSpike2.Location.X - 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike2.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

        }

        private void tmrSpikeImpact2_Tick(object sender, EventArgs e)
        {
            intExplosion2++; //Explode animation
            pcbExplode2.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();
        }

        private void tmrSpike3_Tick(object sender, EventArgs e)
        {
            intRandom3 = RandomClass.Next(145, 860);
            if (pcbSpike3.Top < 515)
            {
                if (tmrSpikeImpact3.Enabled == false)
                {
                    intExplosion3 = 0;   //Resetting so that explode animation may play again
                    pcbSpike3.Top += 5;     //Movement
                    pcbExplode3.Top += 5;
                }
            }
            else if (pcbSpike3.Top >= 515)
            {
                tmrSpikeImpact3.Enabled = true;
                if (intExplosion3 == 3)
                {
                    tmrSpikeImpact3.Enabled = false;    //Allowing explode animation to occur
                    pcbSpike3.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbSpike3.Location = new Point(intRandom3, 0);      //Sets spike to a new random location
                    pcbExplode3.Location = new Point(intRandom3, 0);
                }

            }
            
            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision -- no idea why 
            if (pcbSpike3.Location.X + pcbSpike3.Width > pcbStanding.Location.X && pcbSpike3.Location.X + pcbSpike3.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpike3.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbSpike3.Location.X >= pcbStanding.Location.X && pcbSpike3.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpike3.Location.Y + 33 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike3.Location.X + 15 > pcbStanding.Location.X && pcbSpike3.Location.X + 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike3.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike3.Location.X - 15 > pcbStanding.Location.X && pcbSpike3.Location.X - 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike3.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
        }
        private void tmrSpikeImpact3_Tick(object sender, EventArgs e)
        {
            intExplosion3++; //Explode animation
            pcbExplode3.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();
        }


        private void tmrSpike4_Tick(object sender, EventArgs e)
        {
            intRandom4 = RandomClass.Next(145, 860);
            if (pcbSpike4.Top < 515)
            {
                if (tmrSpikeImpact4.Enabled == false)
                {
                    intExplosion4 = 0;   //Resetting so that explode animation may play again
                    pcbSpike4.Top += 5;     //Movement
                    pcbExplode4.Top += 5;
                }
            }
            else if (pcbSpike4.Top >= 515)
            {
                tmrSpikeImpact4.Enabled = true;
                if (intExplosion4 == 3)
                {
                    tmrSpikeImpact4.Enabled = false;    //Allowing explode animation to occur
                    pcbSpike4.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbSpike4.Location = new Point(intRandom4, 0);      //Sets spike to a new random location
                    pcbExplode4.Location = new Point(intRandom4, 0);
                }

            }
            
            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision 
            if (pcbSpike4.Location.X + pcbSpike4.Width > pcbStanding.Location.X && pcbSpike4.Location.X + pcbSpike4.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpike4.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbSpike4.Location.X >= pcbStanding.Location.X && pcbSpike4.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpike4.Location.Y + 33 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike4.Location.X + 15 > pcbStanding.Location.X && pcbSpike4.Location.X + 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike4.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike4.Location.X - 15 > pcbStanding.Location.X && pcbSpike4.Location.X - 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike4.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
        }

        private void tmrSpikeImpact4_Tick(object sender, EventArgs e)
        {
            intExplosion4++; //Explode animation
            pcbExplode4.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();
        }


        private void tmrSpike5_Tick(object sender, EventArgs e)
        {
            intRandom5 = RandomClass.Next(145, 860);
            if (pcbSpike5.Top < 515)
            {
                if (tmrSpikeImpact5.Enabled == false)
                {
                    intExplosion5 = 0;   //Resetting so that explode animation may play again
                    pcbSpike5.Top += 5;     //Movement
                    pcbExplode5.Top += 5;
                }
            }
            else if (pcbSpike5.Top >= 515)
            {
                tmrSpikeImpact5.Enabled = true;
                if (intExplosion5 == 3)
                {
                    tmrSpikeImpact5.Enabled = false;    //Allowing explode animation to occur
                    pcbSpike5.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbSpike5.Location = new Point(intRandom5, 0);      //Sets spike to a new random location
                    pcbExplode5.Location = new Point(intRandom5, 0);
                }

            }
            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision 
            if (pcbSpike5.Location.X + pcbSpike5.Width > pcbStanding.Location.X && pcbSpike5.Location.X + pcbSpike5.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpike5.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbSpike5.Location.X >= pcbStanding.Location.X && pcbSpike5.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpike5.Location.Y + 33 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike5.Location.X + 15 > pcbStanding.Location.X && pcbSpike5.Location.X + 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike5.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpike5.Location.X - 15 > pcbStanding.Location.X && pcbSpike5.Location.X - 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpike5.Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
        }
        
        private void tmrSpikeImpact5_Tick(object sender, EventArgs e)
        {
            intExplosion5++; //Explode animation
            pcbExplode5.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();
        }

        //*****************************************
        //This code is for the spiked balls falling

        private void tmrBall_Tick(object sender, EventArgs e)
        {       //REMINDER IF BALL HITS AREA, PUT A GIF OF THE EXPLOSION AT THE PLACE OF THE BALL
            intRandomXBall = RandomClass.Next(340, 671);
            if (pcbBall1.Top == 0)
            {
                intRandomAngle = Angle(intRandomAngle);
            }
            if (pcbBall1.Top < 522)
            {
                if (tmrBallImpact.Enabled == false)
                {
                    intBallExplosion1 = 0;   //Resetting so that explode animation may play again
                    //Math for ball movement
                    intxMove = horizontalVal(intSpeed, intRandomAngle);
                    intyMove = verticalVal(intSpeed, intRandomAngle);

                    //Ball Movement
                    this.pcbBall1.Left += intxMove;
                    this.pcbExplodeBall1.Left += intxMove;
                    this.pcbBall1.Top -= intyMove;
                    this.pcbExplodeBall1.Top -= intyMove;
                }
            }
            else if (pcbBall1.Top >= 522 || pcbBall1.Left < 180 || pcbBall1.Left > 860)
            {
                tmrBallImpact.Enabled = true;
                if (intBallExplosion1 == 3)
                {
                    tmrBallImpact.Enabled = false;    //Allowing explode animation to occur
                    pcbBall1.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbBall1.Location = new Point(intRandomXBall, 0);      //Sets spike to a new random location
                    pcbExplodeBall1.Location = new Point(intRandomXBall, 0);   //340 670
                }
            }
            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision
            if (pcbBall1.Location.X + pcbBall1.Width-7 > pcbStanding.Location.X && pcbBall1.Location.X + pcbBall1.Width < pcbStanding.Width + pcbStanding.Location.X && pcbBall1.Location.Y + 5 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbBall1.Location.X >= pcbStanding.Location.X && pcbBall1.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbBall1.Location.Y + 10 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbBall1.Location.X + 3 > pcbStanding.Location.X && pcbBall1.Location.X + 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbBall1.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbBall1.Location.X - 3 > pcbStanding.Location.X && pcbBall1.Location.X - 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbBall1.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            
            
        }
        public int Angle(int intRandomAngle)    //Determining a random angle for the ball to fall in
        {
            intRandomAngle = RandomClass.Next(240,300);
            if (intRandomAngle >= 265 && intRandomAngle <= 275)
            {
                intRandomAngle = RandomClass.Next(225, 315);
            }
            return intRandomAngle;
        }
        public int horizontalVal(int intHyp, int intDegree) //Caculating ball movement on x axis
        {
            int intxMove;
            intxMove = (int)(Math.Cos(intDegree * Math.PI / 180) * intHyp);
            return intxMove;
        }
        public int verticalVal(int intHyp, int intDegree)       //Calculating ball movement on y axis
        {
            int intyMove;
            intyMove = (int)(Math.Sin(intDegree * Math.PI / 180) * intHyp);
            return intyMove;
        }

        private void tmrBallImpact_Tick(object sender, EventArgs e) //Ball hitting floor explosion animation
        {
            intBallExplosion1++;
            pcbExplodeBall1.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();

        }
        private void tmrBall2_Tick(object sender, EventArgs e)
        {
            intRandomXBall2 = RandomClass.Next(340, 671);
            if (pcbBall2.Top == 0)
            {
                intRandomAngle2 = Angle2(intRandomAngle2);
            }
            if (pcbBall2.Top < 522)
            {
                if (tmrBallImpact2.Enabled == false)
                {
                    intBallExplosion2 = 0;   //Resetting so that explode animation may play again
                    //Math for ball movement
                    intxMove2 = horizontalVal2(intSpeed, intRandomAngle2);
                    intyMove2 = verticalVal2(intSpeed, intRandomAngle2);
                    
                    //Ball Movement
                    this.pcbBall2.Left += intxMove2;
                    this.pcbExplodeBall2.Left += intxMove2;
                    this.pcbBall2.Top -= intyMove2;
                    this.pcbExplodeBall2.Top -= intyMove2;
                }
            }
            else if (pcbBall2.Top >= 522 || pcbBall2.Left < 180 || pcbBall2.Left > 860)
            {
                tmrBallImpact2.Enabled = true;
                if (intBallExplosion2 == 3)
                {
                    tmrBallImpact2.Enabled = false;    //Allowing explode animation to occur
                    pcbBall2.BringToFront();
                    pnlMain.BringToFront();
                    pnlGround.BringToFront();
                    pcbBall2.Location = new Point(intRandomXBall2, 0);      //Sets spike to a new random location
                    pcbExplodeBall2.Location = new Point(intRandomXBall2, 0);   //340 670
                }
            }

            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision -- no idea why 
            if (pcbBall2.Location.X + pcbBall2.Width - 7 > pcbStanding.Location.X && pcbBall2.Location.X + pcbBall2.Width < pcbStanding.Width + pcbStanding.Location.X && pcbBall2.Location.Y + 5 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbBall2.Location.X >= pcbStanding.Location.X && pcbBall2.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbBall2.Location.Y + 10 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbBall2.Location.X + 3 > pcbStanding.Location.X && pcbBall2.Location.X + 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbBall2.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbBall2.Location.X - 3 > pcbStanding.Location.X && pcbBall2.Location.X - 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbBall2.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            
        }
        public int Angle2(int intRandomAngle2)      //Chooses a random angle upon reappearing at the top of the screen
        {
            intRandomAngle2 = RandomClass.Next(240, 300);
            if (intRandomAngle2 >= 265 && intRandomAngle2 <= 275)
            {
                intRandomAngle2 = RandomClass.Next(225, 315);
            }
            return intRandomAngle2;
        }
        public int horizontalVal2(int intHyp, int intDegree) //Caculating ball movement on x axis
        {
            int intxMove2;
            intxMove2 = (int)(Math.Cos(intDegree * Math.PI / 180) * intHyp);
            return intxMove2;
        }
        public int verticalVal2(int intHyp, int intDegree)       //Calculating ball movement on y axis
        {
            int intyMove2;
            intyMove2 = (int)(Math.Sin(intDegree * Math.PI / 180) * intHyp);
            return intyMove2;
        }

        private void tmrBallImpact2_Tick(object sender, EventArgs e)
        {
            intBallExplosion2++;    //Explosion animation
            pcbExplodeBall2.BringToFront();
            pnlGround.BringToFront();
            pnlMain.BringToFront();
        }

        private void btnLevel2_Click(object sender, EventArgs e)    //Programmer controls
        {
            intSeconds++;
        }

        private void btnLevel3_Click(object sender, EventArgs e)    //Programmer controls
        {
            intSeconds += 500; 
        }

        private void tmrBossAnimation_Tick(object sender, EventArgs e)
        {
            if (pcbBoss.Top <= 0)       //Boss Entrance
            {
                pcbBoss.Top++;
            }
            else
            {
                lblBoss.Location = new Point(380, 120); //Having the boss say something as he enters
                lblBoss.Text = "Curses! I will have to deal with you myself!";
                lblBoss.BringToFront();
                tmrBossText.Enabled = true;
                tmrBossAnimation.Enabled = false;
            }
        }

        private void tmrBossText_Tick(object sender, EventArgs e)   //So that the boss text disappears
        {
            intBossText++;
            if (intBossText == 2)
            {
                lblBoss.SendToBack();
                tmrBossText.Enabled = false;
            }
        }

        private void tmrBossMove_Tick(object sender, EventArgs e)
        {
            pcbBoss.Left += 5 * intBossDirectionX;  //Boss movement
            if (pcbBoss.Left <= 80)     //Go other way when hitting the left wall - intended to slightly go through wall
            {
                intBossDirectionX = 1;
            }
            if (pcbBoss.Left >= 780)    //Go other way when hitting the right wall
            {
                intBossDirectionX = -1;
            }
        }

        private void tmrWeaponAnimation_Tick(object sender, EventArgs e)
        {
            if (pcbAlienWalkRight.Left < 250)   //Alien entrance and speech
            {
                pcbAlienWalkRight.BringToFront();   //Showing walking right animation
                pnlGround.BringToFront();
                pnlMain.BringToFront();
                this.pcbAlienWalkRight.Left += 2;   //Moving all pictureboxes to the right
                pcbAlienWalkLeft.Left += 2;
                pcbAlienStand.Left += 2;
            }
            else if (pcbAlienWalkRight.Left >= 250 && blnWeaponAnimation == false)
            {
                intAlienAnimation++;    //Beginning talking animation
                pcbAlienStand.BringToFront();   //Showing alien standing
                pcbAlienWalkRight.SendToBack();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
                blnWeaponAnimation = true;  //Beginning talking animation
                tmrAlienTalk.Enabled = true;    //Beginnning talking animation
                
            }
            else if (intAlienAnimation >= 14)
            {
                //Alien Exit
                pcbAlienWalkLeft.BringToFront();    //Showing alien moving to the left
                pcbAlienStand.SendToBack();
                pnlGround.BringToFront();
                pnlMain.BringToFront();
                pcbAlienWalkLeft.Left -= 2; //pcb's moving to the left
            }
            else if (intAlienAnimation >= 14 && pcbAlienWalkLeft.Left <= 0)
            {
                tmrWeaponAnimation.Enabled = false; //Disabling the timer
            }

        }

        private void tmrAlienTalk_Tick(object sender, EventArgs e)
        {
            intAlienAnimation = intAlienAnimation + 1;  //Alien speech
            if (intAlienAnimation <= 3)
            {
                lblAlienTalk.Text = "Those dumb aliens kicked me out because I ate all their chips";
                lblAlienTalk.BringToFront();
            }
            else if (intAlienAnimation <= 6)
            {
                lblAlienTalk.Text = "So, in spite, I will now tell you the secret of their spikes";
            }
            else if (intAlienAnimation <= 9)
            {
                lblAlienTalk.Text = "To create the death spikes with your mind, simply\n@#$*!#$%#$ %$#$ #$@##$%@%$#^%&";
            }
            else if (intAlienAnimation <= 12)
            {
                lblAlienTalk.Text = "Got that? After that, to fire the spikes, just press the L button\nThe spikes don't always appear where you want them to though, so be wary";
            }
            else if (intAlienAnimation == 14)
            {
                lblAlienTalk.SendToBack();
                tmrAlienTalk.Enabled = false;
            }
        }
        
        
        //****************************
        //PLAYER SHOOTING
        private void tmrPlayerSpike_Tick(object sender, EventArgs e)
        {
            if (pcbPlayerSpike.Top > 0)     //Player shots
            {
                pcbPlayerSpike.Top -= 5;
            }
            else if (pcbPlayerSpike.Top < 0)    //So the spike doesnt go infront of the stickman
            {
                pcbPlayerSpike.SendToBack();
            }

            //**************************
            //Collision for player shots
            //Lowering boss health when hit
            if (intBossText >= 2)
            {
                if (pcbPlayerSpike.Location.X + pcbPlayerSpike.Width - 7 > pcbBoss.Location.X && pcbPlayerSpike.Location.X + pcbPlayerSpike.Width < pcbBoss.Width + pcbBoss.Location.X && pcbPlayerSpike.Location.Y < pcbBoss.Location.Y + 100 && blnCollision == true && blnDead == false)
                {
                    pcbPlayerSpike.Location = new Point(pcbStanding.Location.X, pcbStanding.Location.Y+40);
                    intBossHealth--;
                    WinCheck();
                    pcbPlayerSpike.SendToBack();
                }

                if (pcbPlayerSpike.Location.X >= pcbBoss.Location.X && pcbPlayerSpike.Location.X <= pcbBoss.Location.X + (pcbBoss.Width - 10) && pcbPlayerSpike.Location.Y <= pcbBoss.Location.Y + 100 && blnCollision == true && blnDead == false)
                {
                    pcbPlayerSpike.Location = new Point(pcbStanding.Location.X, pcbStanding.Location.Y+40);
                    intBossHealth--;
                    WinCheck();
                    pcbPlayerSpike.SendToBack();
                }
                else if (pcbPlayerSpike.Location.X + 3 > pcbBoss.Location.X && pcbPlayerSpike.Location.X + 5 < pcbBoss.Location.X + (pcbBoss.Width) && pcbPlayerSpike.Location.Y < pcbBoss.Location.Y + 100 && blnCollision == true && blnDead == false)
                {
                    pcbPlayerSpike.Location = new Point(pcbStanding.Location.X, pcbStanding.Location.Y+40);
                    intBossHealth--;
                    WinCheck();
                    pcbPlayerSpike.SendToBack();
                }
                else if (pcbPlayerSpike.Location.X - 3 > pcbBoss.Location.X && pcbPlayerSpike.Location.X - 5 < pcbBoss.Location.X + (pcbBoss.Width) && pcbPlayerSpike.Location.Y < pcbBoss.Location.Y + 100 && blnCollision == true && blnDead == false)
                {
                    pcbPlayerSpike.Location = new Point(pcbStanding.Location.X, pcbStanding.Location.Y+40);
                    intBossHealth--;
                    WinCheck();
                    pcbPlayerSpike.SendToBack();
                }
            }
        }

        //*************************
        //BOSS SHOOTING 
        private void tmrBossSpikes_Tick(object sender, EventArgs e)
        {
            for (int intCounter2 = 0; intCounter2 < intCounter; intCounter2++)
            {
                pcbBossSpikes[intCounter2].Top += 30;
                //****************************
                //COLLISION
                if (pcbBossSpikes[intCounter2].Top < 600)
                {
                    if (pcbBossSpikes[intCounter2].Location.X >= pcbStanding.Location.X && pcbBossSpikes[intCounter2].Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbBossSpikes[intCounter2].Location.Y + 33 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
                    {
                        Death();
                    }
                    else if (pcbBossSpikes[intCounter2].Location.X + 15 > pcbStanding.Location.X && pcbBossSpikes[intCounter2].Location.X + 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbBossSpikes[intCounter2].Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
                    {
                        Death();
                    }
                    else if (pcbBossSpikes[intCounter2].Location.X - 15 > pcbStanding.Location.X && pcbBossSpikes[intCounter2].Location.X - 15 < pcbStanding.Location.X + (pcbStanding.Width) && pcbBossSpikes[intCounter2].Location.Y + 33 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
                    {
                        Death();
                    }
                }
                
            }

        }

        private void button1_Click(object sender, EventArgs e)//Programmer controls
        {
            intAlienAnimation = 14;
        }

        public void Death() //Death functio enables the next enter name form
        {
            tmrDeath.Enabled = true;
            blnDead = true;     //So player can't move when dead
            pcbStanding.Image = Pics.StickmanExplode;


                intFinalScore = intScore;
                EnterName EnterName = new EnterName();     //Open an enter name window
                this.Hide();
                EnterName.Show();

        }



        private void tmrDeath_Tick(object sender, EventArgs e)
        {
            /*intDeathAnimation++;
            if (intDeathAnimation == 1)
            {
                intDeathAnimation = 1;
                tmrDeath.Enabled = false;
            }
             */
        }

        //****************************************************
        //Checks damage done to the boss and if the player won
        public void WinCheck()
        {
            if (intBossHealth == 0) //Ending game -- bug fix
            {
                tmrBossMove.Enabled = false;
                tmrBossSpikes.Enabled = false;
                tmrTime.Enabled = false;
            }
            if (intBossHealth == 20)    //Changing the boss image so it looks like it is damaged
            {
                pcbBoss.Image = Pics.BossDamaged;
                tmrBossSpikes.Interval = 700;
            }
            else if (intBossHealth == 10)  //Changing the boss image so it looks like it is very damaged 
            {
                pcbBoss.Image = Pics.BossCritDamaged;
            }
            else if (intBossHealth == 0)    //End game - enable win animation and text
            {
                lblWin.BringToFront();  //Showing you win statement
                tmrBossMove.Enabled = false;    //Stopping boss from moving
                tmrTime.Enabled = false;    //Stopping most active timers
                pcbBoss.Image = Pics.StickmanExplode;   //Changing boss image to blowing up
                tmrBossDeath.Enabled = true;
                
            }
            
           
            
        }
        public void Powerups()
        {
            intRandomPowerUp = RandomClass.Next(1,7);  //Random Powerup
            intRandomLocation = RandomClass.Next(173, 800); //Random location for powerup
            tmrPowerUp.Enabled = true;
            
        }

        private void tmrBossDeath_Tick(object sender, EventArgs e)
        {
            intBossDeathAnimation = intBossDeathAnimation + 1;
            if (intBossDeathAnimation == 3)
            {
                intScore = intScore + 10000;
                intFinalScore = intScore;
                EnterName EnterName = new EnterName();     //Open an enter name window
                this.Hide();
                EnterName.Show();
            }
        }

        private void tmrPowerUp_Tick(object sender, EventArgs e)
        {

            if (intRandomPowerUp == 1 || intRandomPowerUp == 2 || intRandomPowerUp == 3)    //High chance for point up
            {
                //pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbPointUp.Top += 3;
            }
            else if (intRandomPowerUp == 4 || intRandomPowerUp == 5)
            {
                //pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Top += 3;
            }
            else if (intRandomPowerUp == 6)
            {
                //1/6 chance for invincibility
                pcbInvincibility.Top += 3;
            }
            if (pcbPointUp.Top >= 540 || pcbSpeedUp.Top >= 540 || pcbInvincibility.Top >= 540)  //Resetting the powerups location when it hits the ground level 
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false; //Letting powerups be dropped again
                tmrPowerUp.Enabled = false;
            }
            
            
            //**************************
            //COLLISION FOR INVINCIBILITY
            if (pcbInvincibility.Location.X + pcbInvincibility.Width - 7 > pcbStanding.Location.X && pcbInvincibility.Location.X + pcbInvincibility.Width < pcbStanding.Width + pcbStanding.Location.X && pcbInvincibility.Location.Y + 5 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnCollision = false;
                tmrInvincibility.Enabled = true;
            }

            if (pcbInvincibility.Location.X >= pcbStanding.Location.X && pcbInvincibility.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbInvincibility.Location.Y + 10 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnCollision = false;
                tmrInvincibility.Enabled = true;
            }
            else if (pcbInvincibility.Location.X + 3 > pcbStanding.Location.X && pcbInvincibility.Location.X + 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbInvincibility.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnCollision = false;
                tmrInvincibility.Enabled = true;
            }
            else if (pcbInvincibility.Location.X - 3 > pcbStanding.Location.X && pcbInvincibility.Location.X - 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbInvincibility.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnCollision = false;
                tmrInvincibility.Enabled = true;
            }
            


            //***********************
            //COLLISION FOR POINTUP
            if (pcbPointUp.Location.X + pcbPointUp.Width - 7 > pcbStanding.Location.X && pcbPointUp.Location.X + pcbPointUp.Width < pcbStanding.Width + pcbStanding.Location.X && pcbPointUp.Location.Y + 5 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                intPoints+=2;
            }
            if (pcbPointUp.Location.X >= pcbStanding.Location.X && pcbPointUp.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbPointUp.Location.Y + 10 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                intPoints+=2;
            }
            else if (pcbPointUp.Location.X + 3 > pcbStanding.Location.X && pcbPointUp.Location.X + 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbPointUp.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                intPoints+=2;
            }
            else if (pcbPointUp.Location.X - 3 > pcbStanding.Location.X && pcbPointUp.Location.X - 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbPointUp.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                intPoints+=2;
            }



            //***********************
            //COLLISION FOR SPEEDUP
            if (pcbSpeedUp.Location.X + pcbSpeedUp.Width - 7 > pcbStanding.Location.X && pcbSpeedUp.Location.X + pcbSpeedUp.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpeedUp.Location.Y + 5 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnSpeedUp = true;
                tmrSpeedUp.Enabled = true;
            }
            if (pcbSpeedUp.Location.X >= pcbStanding.Location.X && pcbSpeedUp.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpeedUp.Location.Y + 10 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnSpeedUp = true;
                tmrSpeedUp.Enabled = true;
            }
            else if (pcbSpeedUp.Location.X + 3 > pcbStanding.Location.X && pcbSpeedUp.Location.X + 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpeedUp.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnSpeedUp = true;
                tmrSpeedUp.Enabled = true;
            }
            else if (pcbSpeedUp.Location.X - 3 > pcbStanding.Location.X && pcbSpeedUp.Location.X - 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpeedUp.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                pcbPointUp.Location = new Point(intRandomLocation, 0);
                pcbSpeedUp.Location = new Point(intRandomLocation, 0);
                pcbInvincibility.Location = new Point(intRandomLocation, 0);
                blnPowerUp = false;
                tmrPowerUp.Enabled = false;
                blnSpeedUp = true;
                tmrSpeedUp.Enabled = true;
            }

            

        }

        private void pcbPointUp_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//Programmer controls
        {
            blnCollision = false;
        }

        private void tmrInvincibility_Tick(object sender, EventArgs e)  //Turning off invincibility after 10 seconds have occured
        {
            intInvinciblity++;
            if (intInvinciblity == 10)
            {
                intInvinciblity = 0;    //Reset invinciblity timer
                blnCollision = true;
                tmrInvincibility.Enabled = false;
            }
        }

        private void tmrSpeedUp_Tick(object sender, EventArgs e)    //Turning off powerup after 10 seconds pass
        {
            intSpeedUp++;
            if (intSpeedUp == 10)
            {
                intSpeedUp = 0;
                blnSpeedUp = false;
                tmrSpeedUp.Enabled = false;
            }
        }

        private void tmrSpecialMove_Tick(object sender, EventArgs e)
        {
            //Chance for the special move to occur
            intRandomSpecial = RandomClass.Next(1, 13); //Low chance

        }

        private void tmrAttack_Tick(object sender, EventArgs e)
        {
            pcbSpecial.Top += 10;
            pcbSpecial.Left += (int)(3 * Math.Sin(pcbSpecial.Top) * 30);  //Going in a wave motion downwards
            if (pcbSpecial.Top > 530)
            {
                tmrAttack.Enabled = false;
                blnSpecialMove = false;
            }

            //***************************
            //COLLISION
            //The if statement below fixes a bug for collision -- no idea why 
            if (pcbSpecial.Location.X + pcbSpecial.Width - 7 > pcbStanding.Location.X && pcbSpecial.Location.X + pcbSpecial.Width < pcbStanding.Width + pcbStanding.Location.X && pcbSpecial.Location.Y + 5 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

            if (pcbSpecial.Location.X >= pcbStanding.Location.X && pcbSpecial.Location.X <= pcbStanding.Location.X + (pcbStanding.Width - 5) && pcbSpecial.Location.Y + 10 >= pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpecial.Location.X + 3 > pcbStanding.Location.X && pcbSpecial.Location.X + 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpecial.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }
            else if (pcbSpecial.Location.X - 3 > pcbStanding.Location.X && pcbSpecial.Location.X - 5 < pcbStanding.Location.X + (pcbStanding.Width) && pcbSpecial.Location.Y + 10 > pcbStanding.Location.Y && blnCollision == true && blnDead == false)
            {
                Death();
            }

        }
    }
}
