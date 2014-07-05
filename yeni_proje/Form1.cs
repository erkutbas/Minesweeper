using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;  // add to reach file operations 

namespace yeni_proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Formload            _
        /*
         * LOAD 
         */

        int form_flag = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (form_flag == 0)
            {
                this.Text = "Gathering Information";
                this.BackColor = Color.DarkGray;

                resizeForm();           // resize form with respect to change of size <level>
                createUserEnterance();
                createLight();
            }
            else
            {
                this.Text = "Minesweeper";
                this.Controls.Clear();
                this.BackColor = Color.LightGray;

                createButtons();        // creating buttons 
                createCheckBox();       // creating checkBoxes
                createRandomLocation(); // creating random location for bombing process
                createLabelScores();    // creating labels for score and time 
                createFlowLabel();      // creating label for flowing of process
                createRestartButton();  // creating restart button

                //loadBombImages();     // to control bomb images 

                flow.Text = "Game is commencing ......";
                /*
                 * creating score labels
                 */
                clearScoreLabel_2();
                clearScoreLabel_1();

                //loadBombImages();

            }
            
        }
        #endregion

        #region CheckBoxes          _
        /*
         * TO CREATE CHECKBOX 
         * to include alternatives to user in order to change 
         * the size of minesweeper :D
         *
         */
        CheckBox[] checkBox = new CheckBox[3];

        void createCheckBox()
        {
            for (int i = 0; i < 3; i++)
            {
                checkBox[i] = new CheckBox();

                checkBox[i].Size = new Size((10 * size), 20);
                checkBox[i].Location = new Point(i * (10 * size) + 2, 80);
                checkBox[i].FlatStyle = FlatStyle.Popup;
                checkBox[i].Text = "Level" + (i + 1);
                checkBox[i].Name = "chk_" + (i + 1);
                checkBox[i].Tag = (i + 1);

                checkBox[i].Click += new EventHandler(uploadSizes_OfMineSweeper);
                checkBox[i].Click += new EventHandler(uploadFlowLabel);

                this.Controls.Add(checkBox[i]);
            }
        }

        /*
         * method for making the checkboxes unable after starting the game :D
         */
        void unableCheckBoxes()
        {
            for (int i = 0; i < checkBox.Length; i++)
            {
                checkBox[i].Enabled = false;
            }
            flow.Text = "Started - level CAN NOT be choosen!";
        }

        /*
         * To start and provide chance to users choose another game
         * after bomb is found ! 
         */
        void enableCheckBOxes()
        {
            for (int i = 0; i < checkBox.Length; i++)
            {
                checkBox[i].Enabled = true;
            }
        }
        #endregion

        #region Labels              _
        /*
         * TO CREATE LABEL FOR SCORE & TIME 
         */
        Label[] scoreLabel;

        // scoreLabel[0] = contains score
        // scoreLabel[1] = contains time 
        void createLabelScores()
        {
            scoreLabel = new Label[2];

            for (int i = 0; i < scoreLabel.Length; i++)
            {
                scoreLabel[i] = new Label();

                scoreLabel[i].Size = new Size(10 * size, 50);
                scoreLabel[i].Location = new Point(2 + (i * size * 15), 2);
                scoreLabel[i].TextAlign = ContentAlignment.MiddleCenter;
                scoreLabel[i].Font = new System.Drawing.Font("Arial", size + 2);
                scoreLabel[i].BorderStyle = BorderStyle.Fixed3D;
                scoreLabel[i].BackColor = Color.Black;
                scoreLabel[i].ForeColor = Color.Lime;
               
                this.Controls.Add(scoreLabel[i]);
            }
        }

        /*
         * Creating label for game flow 
         */
        Label flow;

        void createFlowLabel()
        {
            flow = new Label();

            flow.Size = new Size((size * 25), 20);
            flow.Location = new Point(2, 55);
            flow.TextAlign = ContentAlignment.MiddleLeft;
            flow.BackColor = Color.Black;
            flow.ForeColor = Color.Lime;
            flow.BorderStyle = BorderStyle.Fixed3D;

            this.Controls.Add(flow);
        }

        /*
         * method for the checkBoxes in order to change info of flow label 
         */

        void uploadFlowLabel(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((CheckBox)sender).Tag) == 1)
            {
                flow.Text = "EASY LEVEL IS CHOSEN !";                                
            }
            else if (Convert.ToInt32(((CheckBox)sender).Tag) == 2)
            {
                flow.Text = "MEDIUM LEVEL IS CHOSEN !!";
            }
            else
            {
                flow.Text = "HARD LEVEL IS CHOSEN !!";
            }
        }

        /*
         * Clear and rearrange scoreLabel
         */
        void clearScoreLabel_2()
        {
            scoreLabel[1].Text = "00.00.0";
        }

        void clearScoreLabel_1()
        {
            scoreLabel[0].Text = "0";
            score = 0;
        }

        #endregion

        #region CheckBox Level      _

        /* 
         * size attributes with respect to the changes of the size of the minesweeper
         */
        private int level_1 = 10;
        private int level_2 = 15;
        private int level_3 = 20;
        /**/

        private int size = 10;  // size variable changing according to the attributes above

        void uploadSizes_OfMineSweeper(object sender_check_box, EventArgs e)
        {
            if (Convert.ToInt32(((CheckBox)sender_check_box).Tag) == 1)
            {
                MessageBox.Show("Level 1 has been chosen!!!");
                
                this.Controls.Clear();      // clear all controls onto form

                size = level_1;             // arranging size 

                createRandomLocation();     // creating random location

                // upper controls
                resizeForm();               // resizing form
                createCheckBox();           // creating checkBoxes
                createLabelScores();        // creating score and time labels
                createFlowLabel();          // create flow labe
                createRestartButton();      // creating restart button
                //***********************************************************

                // lower controls
                special_flag = 1;           // assing special flag to 1 in order to reset counter <index>
                createButtons();            // creating buttons

                // clearing score and time labels
                clearScoreLabel_2();        
                clearScoreLabel_1();
                //***********************************************************

                resetTimerAttributes();     // reseting time attributes <second, minute>

                score_flag = 0;             // score flag  

                // timer controls 
                FalseClockEnable();         // to false timer.enable
                createTimer();              // calling creatiTimer to stop timer
                button_flag = 0;            // button_flag is created above and explained there

               
                ((CheckBox)sender_check_box).Checked = false;
            }
            else if (Convert.ToInt32(((CheckBox)sender_check_box).Tag) == 2)
            {
                MessageBox.Show("Level 2 has been chosen!!!");
                this.Controls.Clear();      // clear all controls onto form

                size = level_2;             // arranging size 

                createRandomLocation();     // creating random location

                // upper controls
                resizeForm();               // resizing form
                createCheckBox();           // creating checkBoxes
                createLabelScores();        // creating score and time labels
                createFlowLabel();          // create flow labe
                createRestartButton();      // creating restart button
                //***********************************************************

                // lower controls
                special_flag = 1;           // assing special flag to 1 in order to reset counter <index>
                createButtons();            // creating buttons

                // clearing score and time labels
                clearScoreLabel_2();
                clearScoreLabel_1();
                //***********************************************************

                resetTimerAttributes();     // reseting time attributes <second, minute>

                score_flag = 0;             // score flag  

                // timer controls 
                FalseClockEnable();         // to false timer.enable
                createTimer();              // calling creatiTimer to stop timer
                button_flag = 0;            // button_flag is created above and explained there
                
                ((CheckBox)sender_check_box).Checked = false;
            }
            else
            {
                MessageBox.Show("Level 3 has been chosen!!!");
                this.Controls.Clear();      // clear all controls onto form

                size = level_3;             // arranging size 

                createRandomLocation();     // creating random location

                // upper controls
                resizeForm();               // resizing form
                createCheckBox();           // creating checkBoxes
                createLabelScores();        // creating score and time labels
                createFlowLabel();          // create flow labe
                createRestartButton();      // creating restart button
                //***********************************************************

                // lower controls
                special_flag = 1;           // assing special flag to 1 in order to reset counter <index>
                createButtons();            // creating buttons

                // clearing score and time labels
                clearScoreLabel_2();
                clearScoreLabel_1();
                //***********************************************************

                resetTimerAttributes();     // reseting time attributes <second, minute>

                score_flag = 0;             // score flag  

                // timer controls 
                FalseClockEnable();         // to false timer.enable
                createTimer();              // calling creatiTimer to stop timer
                button_flag = 0;            // button_flag is created above and explained there
               
                ((CheckBox)sender_check_box).Checked = false;
            }
        }
        
        #endregion

        #region Buttons             _
        /*
         * TO CREATE BUTTONS DYNAMICALLY 
         */

        int counter;            // index for btn array
        
        Button[] Btn;           // a global array for buttons
        
        int special_flag = 0;   // a special flag variable used to reset counter if scope below
                                // it has be used because counter is an global variable and we do not
                                // want to increase that everytime calling the function createButtons

        void createButtons()
        {
            Btn = new Button[size * size];
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (special_flag == 1)
                    {
                        counter = 0;        // reset counter by assigning 0 (zero)
                        special_flag = 0;   // reset special flag to avoid enterance this scope 
                                            // when calling this function again
                    }

                    Btn[counter] = new Button();

                    Btn[counter].Size = new Size(25, 25);
                    Btn[counter].Location = new Point(i * 25, j * 25 + 100);
                    Btn[counter].Click += new EventHandler(test);
                    
                    this.Controls.Add(Btn[counter]);
                    
                    counter++;      // increase counter to pass the next index of array Btn
                }
            }
            //Console.WriteLine(this.Controls.Count);
        }

        /*
         * RESTART BUTTON
         */

        Button restartButton; 

        void createRestartButton()
        {
            restartButton = new Button();

            restartButton.Size = new Size(5 * size, 50);
            restartButton.Location = new Point(2 + size * 10, 2);
            restartButton.Font = new Font("Arial", 8, FontStyle.Bold);
            restartButton.Text = "RESTART";
            restartButton.Name = "btn_restart";
            restartButton.FlatStyle = FlatStyle.Popup;
            restartButton.BackColor = Color.Gold;
            restartButton.Click += new EventHandler(functionRestart);

            this.Controls.Add(restartButton);

            smileFace();
        }

        // to unable all buttons but restart button after game is finished
        // bomb is found 
        void unableButton()
        {
            foreach (Control item in this.Controls)
            {
                if (item is Button)
                {
                    item.Enabled = false;
                }
            }
            restartButton.Enabled = true;
        }
        /**/

        #endregion

        #region Method Restart      _

        /* the function getting the form restarted 
         */
        void functionRestart(object sender, EventArgs e)
        {
            this.Controls.Clear();

            size = level_1;             // size is assigned to first level

            createRandomLocation();     // creating random location

            // upper controls
            resizeForm();               // resizing form
            createCheckBox();           // creating checkBoxes
            createLabelScores();        // creating score and time labels
            createFlowLabel();          // create flow labe
            createRestartButton();      // creating restart button
            //***********************************************************

            // lower controls
            special_flag = 1;           // assing special flag to 1 in order to reset counter <index>
            createButtons();            // creating buttons

            flow.Text = "GAME IS RESTARTED !";
            // clearing score and time labels
            clearScoreLabel_2();
            clearScoreLabel_1();
            //***********************************************************

            resetTimerAttributes();     // reseting time attributes <second, minute>

            score_flag = 0;             // score flag  

            // timer controls 
            FalseClockEnable();         // to false timer.enable
            createTimer();              // calling creatiTimer to stop timer
            button_flag = 0;            // button_flag is created above and explained there

            smile = 0;
            smileFace();
        }

        #endregion

        #region Searching The Bomb  _
        
        /*
         * CONTROL FOR createButtons and createRandomLocation
         */
        int button_flag = 0;

        void test(object sender, EventArgs e)
        {
            unableCheckBoxes();

            /*
             * button_flag is used once after one button is pushed! After game is started, 
             * button_flag is assigned to 1 in order NOT to enter this scope again and run timer
             * once
             */
            if (button_flag == 0)
            {
                TrueClockEnable();
                createTimer();
                button_flag = 1;
            }     
            /*
             */
            
            // loop for searching if the button location contains bomb or not.
            for (int i = 0; i < size; i++)
            {
                // if location includes bomb
                if (randomPoint[i].Equals(((Button)sender).Location))
                {
                    score_flag = 1;             // to stop increasing score
                    loadBombImages();           // loading bomb image that location  

                    resetTimerAttributes();     // resetting timer attributes <second, minute>  

                    // timer controls 
                    FalseClockEnable();         // to false timer.enable
                    createTimer();              // calling creatiTimer to stop timer
                    button_flag = 0;            // button_flag is created above and explained there

                    smile = 1;
                    smileFace();

                    flow.Text = ("You Found the bomb :D score = " + score);
                    MessageBox.Show("BOOOOMMMM!\nSorry you lost the game :D\nTry again!");

                    addScore();

                    unableButton();             // unable all buttons but restart not to maintain the 
                    enableCheckBOxes();         // after game is over get checkboxes enable again

                    // clear scorelabel <score, time>
                    clearScoreLabel_2();        
                    clearScoreLabel_1();
                }
                // the condition that location does not contain bomb and loading okey image :D
                else
	            {
                    ((Button)sender).Enabled = false;
                    ((Button)sender).Image = img_list1.Images[4];
	            }
            }
            // condition after loop ends and calls score function
            if (score_flag == 0)
            {
                createScore();
            }
        }

        #endregion 

        #region Random Locations    _
        /*
         * To create random location in order to launch the bombs :D 
         */
        Random randomLocation;
        Point temporary;
        Point[] randomPoint;

        void createRandomLocation()
        {
            temporary = new Point();
            randomPoint = new Point[0];
            randomLocation = new Random();

            for (int i = 0; i < size; i++)
            {

                Array.Resize<Point>(ref randomPoint, randomPoint.Length + 1);
                
                temporary.X = randomLocation.Next(0, size) * 25;
                temporary.Y = randomLocation.Next(4, size + 2) * 25;

                randomPoint[i] = temporary;
            }

            for (int i = 0; i < (size - 1); i++)
            {
                if (randomPoint[i] == randomPoint[i + 1])
                {
                    Console.WriteLine("AOOOO aynı random locatiom yakaladık :D");
                    randomPoint[i + 1].X = randomLocation.Next(0, size) * 25;
                    randomPoint[i + 1].Y = randomLocation.Next(4, size + 2) * 25;
                }
            }
            
        }

        #endregion

        #region Resizing Form       _
       
        /*
         * METHOD FOR RESIZING THE FORM FOR CHOOSING THE LEVEL 
         */

        void resizeForm()
        {
            if (size == 10)
            {
               this.Size = new Size(27 * size, 39 * size);
            }
            else if (size == 15)
            {
                this.Size = new Size(27 * size, 35 * size);
            }
            else
            {
                this.Size = new Size(26 * size, 32 * size);
            }

        }

        #endregion

        #region Loading Bomb Images _
        /*
         * function to load bomb images
         */
        int index = 0;

        void loadBombImages()
        {

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size * size; j++)
                {
                    if (randomPoint[i] == Btn[j].Location)
                    {
                        Btn[j].Image = img_list1.Images[2];
                    }
                }
            }
          
        }
        #endregion

        #region Score               _

        int score = 0;
        int score_flag = 0;
        
        void createScore()
        {
            score += 10;
            scoreLabel[0].Text = score.ToString();
        }
        #endregion

        #region Timer On Minesweeper_

        Timer clock = new Timer();

        int flag_clock = 0; // for controlling timer stopping and starting
        int tick_flag = 0;  // for controlling clock.Tick eventhandler 

        void createTimer()
        {
            clock.Interval = 1000;

            /*
             * to create clock.Tick just once. If we do not use that in a if condition 
             * when every creatTimer function call, we build another clockTimer after and after 
             * THIS IS CRITICAL
             */
            if (tick_flag == 0)
            {
                clock.Tick += new EventHandler(clockTick);
                tick_flag = 1;
            }
            /*
             */

            if (flag_clock == 0)
            {
                clock.Start();
            }
            else
            {
                clock.Stop();
            }
        }

        int second;
        int minute;
        /*
         * Clock Tick function
         */
        void clockTick(object sender, EventArgs e)
        {

            if (second < 60)
            {
                second++;
            }
            else if (second > 59)
            {
                second = 0;
                minute++;
            }
            scoreLabel[1].Text = "00." + minute + "." + second;
        }

        /*
         * to reset timer attributes after restarting game or changing the level of the game
         */
        void resetTimerAttributes()
        {
            second = 0;
            minute = 0;
        }
        /*
         */

        /*
         * TrueClockEnable and FalseClockEnable functions
         * are created against complexity
         *
         */
        void TrueClockEnable()
        {
            flag_clock = 0;
        }

        void FalseClockEnable()
        {
            flag_clock = 1;
        }
        /*
         */

        #endregion

        #region Interface           _

        /*
         * these controls for creating file, name, password for users
         * 
         */
        Label nameLabel, surnameLabel, passwordLabel;
        TextBox txt_name, txt_surName, txt_password;
        Button btn_enter, btn_password;

        Button btn_info;
        Label lbl_info;

        Label lbl_file;
        TextBox txt_file;

        Button btn_create_file;

        ComboBox cmbFileExtension;

        // creation of controls <user interface>
        void createUserEnterance()
        {

            // file 
            lbl_file = new Label();
            txt_file = new TextBox();

            lbl_file.Size = new Size(250, 25);
            lbl_file.Location = new Point(2, 2);
            lbl_file.BackColor = Color.Black;
            lbl_file.ForeColor = Color.Lime;
            lbl_file.TextAlign = ContentAlignment.MiddleCenter;
            lbl_file.Font = new Font("SegeoPrint", 12, FontStyle.Bold);
            lbl_file.BorderStyle = BorderStyle.Fixed3D;
            lbl_file.Text = "File Name";
            lbl_file.Name = "lbl_file";


            txt_file.Size = new Size(120, 20);
            txt_file.Location = new Point(3, 30);
            txt_file.TextAlign = HorizontalAlignment.Left;
            txt_file.Font = new Font("Arial", 9, FontStyle.Bold);
            txt_file.Name = "txt_file";
            txt_file.CharacterCasing = CharacterCasing.Lower;

            this.Controls.Add(lbl_file);
            this.Controls.Add(txt_file);

            btn_create_file = new Button();

            btn_create_file.Size = new Size(250, 25);
            btn_create_file.Location = new Point(2, 55);
            btn_create_file.Font = new Font("SegeoPrint", 8, FontStyle.Bold);
            btn_create_file.FlatStyle = FlatStyle.System;
            btn_create_file.Text = "Create-Reopen";

            btn_create_file.Click += new EventHandler(methodFor_btn_create_file);

            this.Controls.Add(btn_create_file);

            // combobox :D 
            cmbFileExtension = new ComboBox();

            cmbFileExtension.Size = new Size(120, 20);
            cmbFileExtension.Location = new Point(130, 30);

            cmbFileExtension.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbFileExtension.Items.Add(".txt");
            cmbFileExtension.Items.Add(".cpp");
            cmbFileExtension.Items.Add(".doc");
            cmbFileExtension.Items.Add(".excl");
            cmbFileExtension.Items.Add(".ppt");

            this.Controls.Add(cmbFileExtension);

            //////////////////////////////////////////////////////////////////////////////
            // name and surname
            nameLabel = new Label();
            txt_name = new TextBox();

            nameLabel.Size = new Size(120, 20);
            nameLabel.Location = new Point(2, 84);
            nameLabel.BackColor = Color.Black;
            nameLabel.ForeColor = Color.Lime;
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.Font = new Font("SegeoPrint", 12, FontStyle.Bold);
            nameLabel.BorderStyle = BorderStyle.Fixed3D;
            nameLabel.Text = "Name";
            nameLabel.Name = "lbl_name";


            txt_name.Size = new Size(120, 20);
            txt_name.Location = new Point(130, 83);
            txt_name.TextAlign = HorizontalAlignment.Left;
            txt_name.Font = new Font("Arial", 9, FontStyle.Bold);
            txt_name.Name = "txt_name";
            txt_name.CharacterCasing = CharacterCasing.Lower;

            this.Controls.Add(nameLabel);
            this.Controls.Add(txt_name);

            surnameLabel = new Label();
            txt_surName = new TextBox();

            surnameLabel.Size = new Size(120, 20);
            surnameLabel.Location = new Point(2, 110);
            surnameLabel.BackColor = Color.Black;
            surnameLabel.ForeColor = Color.Lime;
            surnameLabel.TextAlign = ContentAlignment.MiddleLeft;
            surnameLabel.Font = new Font("SegeoPrint", 12, FontStyle.Bold);
            surnameLabel.BorderStyle = BorderStyle.Fixed3D;
            surnameLabel.Text = "Surname";
            surnameLabel.Name = "lbl_surname";


            txt_surName.Size = new Size(120, 20);
            txt_surName.Location = new Point(130, 109);
            txt_surName.TextAlign = HorizontalAlignment.Left;
            txt_surName.Font = new Font("Arial", 9, FontStyle.Bold);
            txt_surName.Name = "txt_surname";
            txt_surName.CharacterCasing = CharacterCasing.Lower;

            unableName();

            this.Controls.Add(surnameLabel);
            this.Controls.Add(txt_surName);

            // password
            passwordLabel = new Label();
            txt_password = new TextBox();

            passwordLabel.Size = new Size(120, 20);
            passwordLabel.Location = new Point(2, 136);
            passwordLabel.BackColor = Color.Black;
            passwordLabel.ForeColor = Color.Lime;
            passwordLabel.TextAlign = ContentAlignment.MiddleLeft;
            passwordLabel.Font = new Font("SegeoPrint", 12, FontStyle.Bold);
            passwordLabel.BorderStyle = BorderStyle.Fixed3D;
            passwordLabel.Text = "UserID";
            passwordLabel.Name = "lbl_surname";


            txt_password.Size = new Size(120, 20);
            txt_password.Location = new Point(130, 135);
            txt_password.TextAlign = HorizontalAlignment.Left;
            txt_password.Font = new Font("Arial", 10, FontStyle.Bold);
            txt_password.UseSystemPasswordChar = true;
            txt_password.Name = "txt_surname";

            unablePassword();

            this.Controls.Add(passwordLabel);
            this.Controls.Add(txt_password);

            ///////////////////////////////////////////////////////////////////////
            // btn_enter
            btn_enter = new Button();

            btn_enter.Size = new Size(170, 30);
            btn_enter.Location = new Point(40, 200);
            btn_enter.Font = new Font("SegeoPrint", 8, FontStyle.Bold);
            btn_enter.BackColor = Color.LightGray;
            btn_enter.ForeColor = Color.Black;
            btn_enter.FlatStyle = FlatStyle.Popup;
            btn_enter.Text = "Enter Account";
            btn_enter.Name = "btn_enter";

            unableButtonEnter();

            btn_enter.Click += new EventHandler(methodFor_btn_enter);

            this.Controls.Add(btn_enter);

            ///////////////////////////////////////////////////////////////////////
            // btn_password
            btn_password = new Button();

            btn_password.Size = new Size(170, 30);
            btn_password.Location = new Point(40, 170);
            btn_password.Font = new Font("SegeoPrint", 8, FontStyle.Bold);
            btn_password.FlatStyle = FlatStyle.Popup;
            btn_password.Text = "Create Password";
            btn_password.Name = "btn_password";

            btn_password.Click += new EventHandler(methodFor_btn_password);

            this.Controls.Add(btn_password);

            // btn_info
            btn_info = new Button();

            btn_info.Size = new Size(170, 30);
            btn_info.Location = new Point(40, 230);
            btn_info.Font = new Font("SegeoPrint", 8, FontStyle.Bold);
            btn_info.BackColor = Color.LightGray;
            btn_info.ForeColor = Color.Black;
            btn_info.FlatStyle = FlatStyle.Popup;
            btn_info.Text = "Information";
            btn_info.Name = "btn_info";

            btn_info.Click += new EventHandler(methodFor_btn_info);

            this.Controls.Add(btn_info);

            // information
            lbl_info = new Label();

            lbl_info.Size = new Size(250, 85);
            lbl_info.Location = new Point(2, 265);
            lbl_info.Font = new Font("SegeoPrint", 8, FontStyle.Bold);
            lbl_info.Name = "lbl_info";

            this.Controls.Add(lbl_info);

        }

        #endregion

        #region Interface Controls  _

        void enableName()
        {
            txt_name.Enabled = true;
            txt_surName.Enabled = true;
        }
        void unableName()
        {
            txt_name.Enabled = false;
            txt_surName.Enabled = false;
        }

        void enablePassword()
        {
            txt_password.Enabled = true;
        }
        void unablePassword()
        {
            txt_password.Enabled = false;
        }

        void enableButtonInfo()
        {
            btn_password.Enabled = true;
        }
        void unableButtonPassword()
        {
            btn_password.Enabled = false;
        }

        void enableButtonEnter()
        {
            btn_enter.Enabled = true;
        }
        void unableButtonEnter()
        {
            btn_enter.Enabled = false;
        }

        void focus_txt_password()
        {
            txt_password.Clear();
            txt_password.Focus();
        }

        void focus_txt_name()
        {
            txt_name.Clear();
            txt_surName.Clear();

            txt_surName.Focus();
            txt_name.Focus();
        }

        void unableFileModules()
        {
            txt_file.Enabled = false;
            btn_create_file.Enabled = false;
            cmbFileExtension.SelectedIndex = -1;
            cmbFileExtension.Enabled = false;
        }
        
        #endregion

        #region Interface Methods   _

        // creation of password 
        string password = "";
        int password_flag = 0;

        void createPassword(string name, string surname, out string password_carrier)
        {
            double num;
            string str1 = name.Trim();
            string str2 = surname.Trim();

            bool isNum1 = double.TryParse(str1, out num);
            bool isNum2 = double.TryParse(str2, out num);

            password_carrier = password;

            if (name == "" || surname == "")
            {
                MessageBox.Show("Insufficient information"); 
            }
            else
            {
                if (isNum1 == true || isNum2 == true)
                {
                    MessageBox.Show("You entered wrong personal information");
                    focus_txt_name();
                }
                else
                {
                    password_carrier = name[0] + surname;

                    if (password_carrier.Length > 7)
                    {
                        // CRITICAL :D
                        password_carrier = password_carrier.Substring(0, 7); // *******
                        password_flag = 1;
                    }
                    password_flag = 1;
                }
            }
        }
        
        // for creation password button
        string s;       // passwordkeeper
        
        void methodFor_btn_password(object sender, object e)
        {
            createPassword(txt_name.Text, txt_surName.Text, out s);

            if (password_flag == 1)
            {
                /*to stop lightning*/
                light_flag = 1;
                createLight();

                MessageBox.Show("Your password is : " + s);
                
                fileProcess();
                
                if (comparePassword(s, txt_name.Text, txt_surName.Text) == true)
                {
                    txt_password.Text = s.ToString();
                    
                    saksuka = 1;

                    /* enable password enterance */
                    enablePassword();
                    enableButtonEnter();


                    /* unable name enterance*/
                    unableName();
                    unableButtonPassword();

                    txt_password.Focus();
                }
                else
                {
                    //fileProcess();

                    /* enable password enterance */
                    enablePassword();
                    enableButtonEnter();


                    /* unable name enterance*/
                    unableName();
                    unableButtonPassword();

                    txt_password.Focus();

                    saksuka = 1;
                }
            }
            else
            {
                MessageBox.Show("Please try again :D");
            }
        }

        int saksuka = 0;

        // method for enter button
        void methodFor_btn_enter(object sender, EventArgs e)
        {
            if (saksuka == 1)
            {
                form_flag = 1;
                Form1_Load(sender, e);
                //fileProcess();
            }
            else
            {
                MessageBox.Show("Entered wrong password!!!");
                focus_txt_password();
            }
        }

        // information button
        void methodFor_btn_info(object sender, EventArgs e)
        {
            lbl_info.BorderStyle = BorderStyle.Fixed3D;

            lbl_info.BackColor = Color.Black;
            lbl_info.ForeColor = Color.Lime;

            lbl_info.Text = "Hello Welcome ! " + txt_name.Text + Environment.NewLine + "You can create a new file or reopen the file created before!" + Environment.NewLine + "Your score will be updated after game is over!" + Environment.NewLine + "You can find the file in LocalDisk \"C\"";
        }
 
        #endregion

        #region Timer For Blinking  _

        Timer light = new Timer();

        int light_flag = 0;
        int light_tick = 0;

        int color = 0;

        void createLight()
        {
            light.Interval = 100;

            if (light_tick == 0)
            {
                light.Tick += new EventHandler(light_Tick);
            }

            if (light_flag == 0)
            {
                light.Start();
            }
            else
            {
                light.Stop();    
            }
        }

        void light_Tick(object sender, EventArgs e)
        {
            if (color == 0)
            {
                btn_password.BackColor = Color.Yellow;
                btn_password.ForeColor = Color.Red;
                color = 1;
            }
            else
            {
                btn_password.BackColor = Color.Blue;
                btn_password.ForeColor = Color.Yellow;
                color = 0;
            }
        }

        #endregion

        #region File Process        _

        string[] userInfo = new string[0];
        int i = 0;

        string[] search = new string[0];
        int k = 0;

        string[] search_name = new string[0];
        string[] search_surname = new string[0];

        void fileProcess()
        {
            StreamReader read = File.OpenText(fileNameKeeper);
            
            string temp = read.ReadLine();

            if ( temp == null)
            {
                MessageBox.Show("Empty File");
            }
            else
            {
                while (temp != null)
                {
                    Array.Resize<string>(ref userInfo, userInfo.Length + 1);
                    Array.Resize<string>(ref search, search.Length + 1);
                    Array.Resize<string>(ref search_name, search_name.Length + 1);
                    Array.Resize<string>(ref search_surname, search_surname.Length + 1);
                    
                    userInfo[i] = temp;
                    search[k] = temp.Split(';')[2];
                    search_name[k] = temp.Split(';')[0];
                    search_surname[k] = temp.Split(';')[1];

                    temp = read.ReadLine();
                    //Console.WriteLine(userInfo[i]);
                    
                    i++;
                    k++;
                }
            }
            read.Close();
            // dosya okuması bitti
            /////////////////////////////////////////////////////////////////////////////////
            // dosya yazma basliyor
            if (comparePassword(s, txt_name.Text, txt_surName.Text) == false)
            {
                Array.Resize<string>(ref userInfo, userInfo.Length + 1);
                userInfo[i] = txt_name.Text + ';' + txt_surName.Text + ';' + s;

                StreamWriter var = new StreamWriter(fileNameKeeper);

                for (int k = 0; k < userInfo.Length; k++)
                {
                    var.WriteLine(userInfo[k].ToString());
                }

                var.Close();
            }
            else
            {
                MessageBox.Show("Dear " + txt_name.Text + "\nWelcome Back :D");
            }
        }

        #endregion

        #region Comparing Password  _
       
        bool comparePassword(string p_in, string name_in, string surname_in)
        {
            for (int i = 0; i < search.Length; i++)
            {
                if (p_in.Trim() == search[i].Trim() && name_in == search_name[i] && surname_in == search_surname[i])
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }
        #endregion

        #region Add Score Into File _
     
        void addScore()
        {

            StreamReader read = File.OpenText(fileNameKeeper);

            string temp = read.ReadLine();

            int index_counter = 0;

            while (temp != null)
            {
                if (userInfo[index_counter].Split(';')[2] == s && temp.Split(';')[2] == s && temp.Split(';')[0] == txt_name.Text && temp.Split(';')[1] == txt_surName.Text)
                {
                    userInfo[index_counter] += ";time = " + DateTime.Now.ToString() + ";puan = " + score;                    
                }

                index_counter++;
                temp = read.ReadLine();

                if (temp == null)
                {
                    break;
                }
            }

            read.Close();

            StreamWriter var = new StreamWriter(fileNameKeeper);

            for (int k = 0; k < userInfo.Length; k++)
            {
                var.WriteLine(userInfo[k].ToString());
            }

            var.Close();
        }

        #endregion

        #region Create/Reopen File  _

        string fileName = "C:\\Users\\erkut\\Desktop\\";
        string fileNameKeeper;

        void setFileName(string in_fileName, string file_extension, out string fileNameKeeper)
        {

            fileName += in_fileName + file_extension;

            fileNameKeeper = fileName;
        }

        void methodFor_btn_create_file(object sender, EventArgs e)
        {
            if (txt_file.Text.Trim() == "" || cmbFileExtension.SelectedIndex == -1)
            {
                MessageBox.Show("Insufficient information");
            }
            else
            {
                setFileName(txt_file.Text.Trim(), cmbFileExtension.SelectedItem.ToString(), out fileNameKeeper);

                FileStream f = new FileStream(fileNameKeeper, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                unableFileModules();

                f.Close();

                enableName();
                focus_txt_name();
            }
            
        }

        bool koko(string name_in, string surname_in)
        {

            StreamReader f = new StreamReader(fileNameKeeper);

            string temp;

            temp = f.ReadLine();

            while (temp != null)
	        {
                if (temp.Split(';')[0].Trim() == name_in && temp.Split(';')[1].Trim() == surname_in)
	            {
                    f.Close();
                    return true;
                    
	            }
                else
	            {
                    temp = f.ReadLine();
                    continue;        
	            }
	        }

            f.Close();

            return false;
        }

        #endregion

        #region Restart Button Text _
       
        int smile = 0;

        void smileFace()
        {
            if (smile == 0)
            {
                restartButton.Text = "RESTART!!!";
            }
            else
            {
                restartButton.Text = "DEATH!!!";
            }
        }
        #endregion
        
    }
}
