﻿using OctoAwesome.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace OctoAwesome.Server
{
    public partial class MainForm : Form
    {
        

        public MainForm()
        {
            InitializeComponent();

            ServerConsole.Console = console_textBox;
            ServerConsole.Log("Initializing...");
            Runtime.Server.Instance.OnRegister += Instance_OnRegister;
            Runtime.Server.Instance.OnDeregister += Instance_OnDeregister;

            Runtime.Server.Instance.Open();
            ServerConsole.Log("---Server started---");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Runtime.Server.Instance.Close();

            base.OnFormClosed(e);
        }

        private void Instance_OnDeregister(Client client)
        {
            //ListViewItem bestehend aus Playername & Planet
            //TODO - fix item.remove
            listViewPlayers.Items.Remove(new ListViewItem(new String[] { client.Playername, "Default" }));
            ServerConsole.Log("Player " + client.Playername + " left the game");
        }

        private void Instance_OnRegister(Client client)
        {
            //ListViewItem bestehend aus Playername & Planet
            listViewPlayers.Items.Add(new ListViewItem(new String[] { client.Playername, "Default" }));
            ServerConsole.Log("Player " + client.Playername + " joined the game");
        }

        private void button_stopServer_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Stop")
            {
                Runtime.Server.Instance.Close();
                ServerConsole.Log("---Server started---");
                ((Button)sender).Text = "Start";
            }
            else
            {
                Runtime.Server.Instance.Open();
                ServerConsole.Log("---Server stopped---");
                ((Button)sender).Text = "Stop";
            }
        }
    }
}
