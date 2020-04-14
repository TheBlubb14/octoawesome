﻿using engenious;
using engenious.Input;
using engenious.UI;
using engenious.UI.Controls;
using OctoAwesome.Client.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OctoAwesome.Client.Screens
{
    internal sealed class LoadingScreen : BaseScreen
    {
        private readonly GameScreen gameScreen;

        public LoadingScreen(ScreenComponent manager) : base(manager)
        {
            Padding = new Border(0, 0, 0, 0);

            Title = "Loading";

            SetDefaultBackground();

            //Main Panel
            var mainStack = new Grid(manager);
            mainStack.Columns.Add(new ColumnDefinition() { ResizeMode = ResizeMode.Parts, Width = 4 });
            mainStack.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Parts, Height = 1 });
            mainStack.Margin = Border.All(50);
            mainStack.HorizontalAlignment = HorizontalAlignment.Stretch;
            mainStack.VerticalAlignment = VerticalAlignment.Stretch;

            Controls.Add(mainStack);

            var backgroundStack = new Panel(manager)
            {
                Background = new BorderBrush(Color.White * 0.5f),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = Border.All(10)
            };
            mainStack.AddControl(backgroundStack, 0, 0, 1, 1);

            var mainGrid = new Grid(manager)
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            mainGrid.Columns.Add(new ColumnDefinition() { ResizeMode = ResizeMode.Parts, Width = 1 });
            mainGrid.Columns.Add(new ColumnDefinition() { ResizeMode = ResizeMode.Parts, Width = 3 });
            mainGrid.Columns.Add(new ColumnDefinition() { ResizeMode = ResizeMode.Parts, Width = 1 });
            mainGrid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Parts, Height = 4 });
            mainGrid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Parts, Height = 1 });
            mainGrid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Parts, Height = 1 });
            mainGrid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Parts, Height = 4 });

            backgroundStack.Controls.Add(mainGrid);

            var text = new Label(manager)
            {
                Text = "Konfuzius sagt: Das hier lädt!",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = Border.All(10),
            };

            mainGrid.AddControl(text, 1, 1);


            //Buttons
            var buttonStack = new StackPanel(manager)
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Orientation = Orientation.Horizontal
            };
            mainGrid.AddControl(buttonStack, 1, 2);

            Button cancelButton = GetButton(Languages.OctoClient.Cancel);
            buttonStack.Controls.Add(cancelButton);
            cancelButton.LeftMouseClick += (s, e) =>
            {
                manager.NavigateBack();
            };

            Debug.WriteLine("Create GameScreen");

            gameScreen = new GameScreen(manager);
            gameScreen.Update(new GameTime());
            gameScreen.OnCenterChanged += SwitchToGame;
        }

        private void SwitchToGame(object sender, System.EventArgs args)
        {
            Manager.Invoke(() =>
            {
                Manager.NavigateToScreen(gameScreen);
                gameScreen.OnCenterChanged -= SwitchToGame;
            });
        }
    }
}
